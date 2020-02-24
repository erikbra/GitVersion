using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GitVersion.Logging;
using GitVersion.Configuration;
using GitVersion.Extensions;
using GitVersion.Models.Abstractions;

namespace GitVersion.VersionCalculation
{
    internal class MainlineVersionCalculator : IMainlineVersionCalculator
    {
        private readonly IMetaDataCalculator metaDataCalculator;
        private readonly ILog log;

        public MainlineVersionCalculator(ILog log, IMetaDataCalculator metaDataCalculator)
        {
            this.metaDataCalculator = metaDataCalculator ?? throw new ArgumentNullException(nameof(metaDataCalculator));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public SemanticVersion FindMainlineModeVersion(BaseVersion baseVersion, GitVersionContext context)
        {
            if (baseVersion.SemanticVersion.PreReleaseTag.HasTag())
            {
                throw new NotSupportedException("Mainline development mode doesn't yet support pre-release tags on master");
            }

            using (log.IndentLog("Using mainline development mode to calculate current version"))
            {
                var mainlineVersion = baseVersion.SemanticVersion;

                // Forward merge / PR
                //          * feature/foo
                //         / |
                // master *  *
                //

                var mergeBase = baseVersion.BaseVersionSource;
                var mainline = GetMainline(context, baseVersion.BaseVersionSource);
                var mainlineTip = mainline.Tip;

                // when the current branch is not mainline, find the effective mainline tip for versioning the branch
                if (!context.CurrentBranch.IsSameBranch(mainline))
                {
                    mergeBase = FindMergeBaseBeforeForwardMerge(context, baseVersion.BaseVersionSource, mainline, out mainlineTip);
                    log.Info($"Current branch ({context.CurrentBranch.FriendlyName}) was branch from {mergeBase}");
                }

                var mainlineCommitLog = context.Repository.Commits.QueryBy(new GitCommitFilter
                {
                    IncludeReachableFrom = mainlineTip,
                    ExcludeReachableFrom = baseVersion.BaseVersionSource,
                    SortBy = GitCommitSortStrategies.Reverse,
                    FirstParentOnly = true
                })
                .ToList();
                var directCommits = new List<IGitCommit>(mainlineCommitLog.Count);

                // Scans Commit log in reverse, aggregating merge commits
                foreach (var IGitCommit in mainlineCommitLog)
                {
                    directCommits.Add(IGitCommit);
                    if (IGitCommit.Parents.Count() > 1)
                    {
                        mainlineVersion = AggregateMergeCommitIncrement(context, IGitCommit, directCommits, mainlineVersion, mainline);
                    }
                }

                // This will increment for any direct commits on mainline
                mainlineVersion = IncrementForEachCommit(context, directCommits, mainlineVersion, mainline);
                mainlineVersion.BuildMetaData = metaDataCalculator.Create(mergeBase, context);

                // branches other than master always get a bump for the act of branching
                if (context.CurrentBranch.FriendlyName != "master")
                {
                    var branchIncrement = FindMessageIncrement(context, null, context.CurrentCommit, mergeBase, mainlineCommitLog);
                    log.Info($"Performing {branchIncrement} increment for current branch ");

                    mainlineVersion = mainlineVersion.IncrementVersion(branchIncrement);
                }

                return mainlineVersion;
            }
        }

        private SemanticVersion AggregateMergeCommitIncrement(GitVersionContext context, IGitCommit IGitCommit, List<IGitCommit> directCommits, SemanticVersion mainlineVersion, IGitBranch mainline)
        {
            // Merge Commit, process all merged commits as a batch
            var mergeCommit = IGitCommit;
            var mergedHead = GetMergedHead(mergeCommit);
            var findMergeBase = context.Repository.ObjectDatabase.FindMergeBase(mergeCommit.Parents.First(), mergedHead);
            var findMessageIncrement = FindMessageIncrement(context, mergeCommit, mergedHead, findMergeBase, directCommits);

            // If this collection is not empty there has been some direct commits against master
            // Treat each Commit as it's own 'release', we need to do this before we increment the branch
            mainlineVersion = IncrementForEachCommit(context, directCommits, mainlineVersion, mainline);
            directCommits.Clear();

            // Finally increment for the branch
            mainlineVersion = mainlineVersion.IncrementVersion(findMessageIncrement);
            log.Info($"Merge Commit {mergeCommit.Sha} incremented base versions {findMessageIncrement}, now {mainlineVersion}");
            return mainlineVersion;
        }

        private IGitBranch GetMainline(GitVersionContext context, IGitCommit baseVersionSource)
        {
            var mainlineBranchConfigs = context.FullConfiguration.Branches.Where(b => b.Value.IsMainline == true).ToList();
            var mainlineBranches = context.Repository.Branches
                .Where(b =>
                {
                    return mainlineBranchConfigs.Any(c => Regex.IsMatch(b.FriendlyName, c.Value.Regex));
                })
                .Select(b => new
                {
                    MergeBase = context.Repository.ObjectDatabase.FindMergeBase(b.Tip, context.CurrentCommit),
                    Branch = b
                })
                .Where(a => a.MergeBase != null)
                .GroupBy(b => b.MergeBase.Sha, b => b.Branch)
                .ToDictionary(b => b.Key, b => b.ToList());

            var allMainlines = mainlineBranches.Values.SelectMany(branches => branches.Select(b => b.FriendlyName));
            log.Info("Found possible mainline branches: " + string.Join(", ", allMainlines));

            // Find closest mainline branch
            var firstMatchingCommit = context.CurrentBranch.Commits.First(c => mainlineBranches.ContainsKey(c.Sha));
            var possibleMainlineBranches = mainlineBranches[firstMatchingCommit.Sha];

            if (possibleMainlineBranches.Count == 1)
            {
                var mainlineBranch = possibleMainlineBranches[0];
                log.Info("Mainline for current branch is " + mainlineBranch.FriendlyName);
                return mainlineBranch;
            }

            // prefer current branch, if it is a mainline branch
            if (possibleMainlineBranches.Any(context.CurrentBranch.IsSameBranch))
            {
                log.Info($"Choosing {context.CurrentBranch.FriendlyName} as mainline because it is the current branch");
                return context.CurrentBranch;
            }

            // prefer a branch on which the merge base was a direct Commit, if there is such a branch
            var firstMatchingCommitBranch = possibleMainlineBranches
                .FirstOrDefault(b =>
                {
                    var filter = new GitCommitFilter
                    {
                        IncludeReachableFrom = b,
                        ExcludeReachableFrom = baseVersionSource,
                        FirstParentOnly = true,
                    };
                    var query = context.Repository.Commits.QueryBy(filter);

                    return query.Contains(firstMatchingCommit);
                });
            if (firstMatchingCommitBranch != null)
            {
                var message = string.Format(
                    "Choosing {0} as mainline because {1}'s merge base was a direct Commit to {0}",
                    firstMatchingCommitBranch.FriendlyName,
                    context.CurrentBranch.FriendlyName);
                log.Info(message);

                return firstMatchingCommitBranch;
            }

            var chosenMainline = possibleMainlineBranches[0];
            log.Info($"Multiple mainlines ({string.Join(", ", possibleMainlineBranches.Select(b => b.FriendlyName))}) have the same merge base for the current branch, choosing {chosenMainline.FriendlyName} because we found that branch first...");
            return chosenMainline;
        }

        /// <summary>
        /// Gets the Commit on mainline at which <paramref name="mergeBase"/> was fully integrated.
        /// </summary>
        /// <param name="mainlineCommitLog">The collection of commits made directly to mainline, in reverse order.</param>
        /// <param name="mergeBase">The best possible merge base between <paramref name="mainlineTip"/> and the current Commit.</param>
        /// <param name="mainlineTip">The tip of the mainline branch.</param>
        /// <returns>The Commit on mainline at which <paramref name="mergeBase"/> was merged, if such a Commit exists; otherwise, <paramref name="mainlineTip"/>.</returns>
        /// <remarks>
        /// This method gets the most recent Commit on mainline that should be considered for versioning the current branch.
        /// </remarks>
        private IGitCommit GetEffectiveMainlineTip(IEnumerable<IGitCommit> mainlineCommitLog, IGitCommit mergeBase, IGitCommit mainlineTip)
        {
            // find the Commit that merged mergeBase into mainline
            foreach (var commit in mainlineCommitLog)
            {
                if (commit.Equals(mergeBase) || commit.Parents.Contains(mergeBase))
                {
                    log.Info($"Found branch merge point; choosing {commit} as effective mainline tip");
                    return commit;
                }
            }

            return mainlineTip;
        }

        /// <summary>
        /// Gets the best possible merge base between the current Commit and <paramref name="mainline"/> that is not the child of a forward merge.
        /// </summary>
        /// <param name="context">The current versioning context.</param>
        /// <param name="baseVersionSource">The Commit that establishes the contextual base version.</param>
        /// <param name="mainline">The mainline branch.</param>
        /// <param name="mainlineTip">The Commit on mainline at which the returned merge base was fully integrated.</param>
        /// <returns>The best possible merge base between the current Commit and <paramref name="mainline"/> that is not the child of a forward merge.</returns>
        private IGitCommit FindMergeBaseBeforeForwardMerge(GitVersionContext context, IGitCommit baseVersionSource, IGitBranch mainline, out IGitCommit mainlineTip)
        {
            var mergeBase = context.Repository.ObjectDatabase.FindMergeBase(context.CurrentCommit, mainline.Tip);
            var mainlineCommitLog = context.Repository.Commits
                .QueryBy(new GitCommitFilter
                {
                    IncludeReachableFrom = mainline.Tip,
                    ExcludeReachableFrom = baseVersionSource,
                    SortBy = GitCommitSortStrategies.Reverse,
                    FirstParentOnly = true
                })
                .ToList();

            // find the mainline Commit effective for versioning the current branch
            mainlineTip = GetEffectiveMainlineTip(mainlineCommitLog, mergeBase, mainline.Tip);

            // detect forward merge and rewind mainlineTip to before it
            if (Equals(mergeBase, context.CurrentCommit) && !mainlineCommitLog.Contains(mergeBase))
            {
                var mainlineTipPrevious = mainlineTip.Parents.First();
                var message = $"Detected forward merge at {mainlineTip}; rewinding mainline to previous Commit {mainlineTipPrevious}";

                log.Info(message);

                // re-do mergeBase detection before the forward merge
                mergeBase = context.Repository.ObjectDatabase.FindMergeBase(context.CurrentCommit, mainlineTipPrevious);
                mainlineTip = GetEffectiveMainlineTip(mainlineCommitLog, mergeBase, mainlineTipPrevious);
            }

            return mergeBase;
        }

        private SemanticVersion IncrementForEachCommit(GitVersionContext context, List<IGitCommit> directCommits, SemanticVersion mainlineVersion, IGitBranch mainline)
        {
            foreach (var directCommit in directCommits)
            {
                var directCommitIncrement = IncrementStrategyFinder.GetIncrementForCommits(context, new[]
                                            {
                                                directCommit
                                            }) ?? IncrementStrategyFinder.FindDefaultIncrementForBranch(context, mainline.FriendlyName);
                mainlineVersion = mainlineVersion.IncrementVersion(directCommitIncrement);
                log.Info($"Direct Commit on master {directCommit.Sha} incremented base versions {directCommitIncrement}, now {mainlineVersion}");
            }
            return mainlineVersion;
        }

        private static VersionField FindMessageIncrement(
            GitVersionContext context, IGitCommit mergeCommit, IGitCommit mergedHead, IGitCommit findMergeBase, List<IGitCommit> commitLog)
        {
            var filter = new GitCommitFilter
            {
                IncludeReachableFrom = mergedHead,
                ExcludeReachableFrom = findMergeBase
            };
            var commits = mergeCommit == null ?
                context.Repository.Commits.QueryBy(filter).ToList() :
                new[] { mergeCommit }.Union(context.Repository.Commits.QueryBy(filter)).ToList();
            commitLog.RemoveAll(c => commits.Any(c1 => c1.Sha == c.Sha));
            return IncrementStrategyFinder.GetIncrementForCommits(context, commits)
                ?? TryFindIncrementFromMergeMessage(mergeCommit, context);
        }

        private static VersionField TryFindIncrementFromMergeMessage(IGitCommit mergeCommit, GitVersionContext context)
        {
            if (mergeCommit != null)
            {
                var mergeMessage = new MergeMessage(mergeCommit.Message, context.FullConfiguration);
                if (mergeMessage.MergedBranch != null)
                {
                    var config = context.FullConfiguration.GetConfigForBranch(mergeMessage.MergedBranch);
                    if (config?.Increment != null && config.Increment != IncrementStrategy.Inherit)
                    {
                        return config.Increment.Value.ToVersionField();
                    }
                }
            }

            // Fallback to config increment value
            return IncrementStrategyFinder.FindDefaultIncrementForBranch(context);
        }

        private static IGitCommit GetMergedHead(IGitCommit mergeCommit)
        {
            var parents = mergeCommit.Parents.Skip(1).ToList();
            if (parents.Count > 1)
                throw new NotSupportedException("Mainline development does not support more than one merge source in a single Commit yet");
            return parents.Single();
        }
    }
}
