using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GitVersion.Configuration;
using GitVersion.Extensions;
using GitVersion.Logging;
using GitVersion.Models.Abstractions;

namespace GitVersion.VersionCalculation
{
    /// <summary>
    /// Version is extracted from older commits's merge messages.
    /// BaseVersionSource is the Commit where the message was found.
    /// Increments if PreventIncrementForMergedBranchVersion (from the branch config) is false.
    /// </summary>
    public class MergeMessageVersionStrategy : IVersionStrategy
    {
        private readonly ILog log;

        public MergeMessageVersionStrategy(ILog log)
        {
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        private class MergeCommit
        {
            public IGitCommit Commit { get; set; }
            public MergeMessage MergeMessage { get; set; }
            public bool HasVersion => HasVersion(MergeMessage);
        }

        private class DistinctBaseVersionComparer : IEqualityComparer<BaseVersion> {

            public bool Equals(BaseVersion x, BaseVersion y)
            {
                return x.BaseVersionSource == y.BaseVersionSource;
            }

            public int GetHashCode(BaseVersion obj)
            {
                return obj.BaseVersionSource.GetHashCode();
            }
        }

        public virtual IEnumerable<BaseVersion> GetVersions(GitVersionContext context)
        {
            log.Info($"Finding commits prior to {context.CurrentCommit} ({context.CurrentCommit.When()}" );

            var commitsPriorToThan = context.CurrentBranch
                .CommitsPriorToThan(context.CurrentCommit.When());

            log.Info($"Found {commitsPriorToThan.Count()} commits." );

            log.Info($"Finding merge commits" );

            var mergeCommits = commitsPriorToThan.Where(IsMergeCommit)
                .Select(c =>  new MergeCommit()
                {
                    Commit = c,
                    MergeMessage = GetMergeMessage(c, context)
                }).Where(m => HasVersion(m.MergeMessage));

            log.Info($"Found {mergeCommits.Count()} commits." );


            log.Info($"Finding base versions" );

            var baseVersions = mergeCommits //commitsPriorToThan
                .SelectMany(c =>
                //.Select(c =>
                {
                    var mergeMessage = c.MergeMessage;
                    if (IsMergeToReleaseBranch(context, mergeMessage))
                    {
                        log.Info($"Found Commit [{context.CurrentCommit.Sha}] matching merge message format: {mergeMessage.FormatName}");
                        log.Info($"Found Commit [{c.Commit.Sha}] matching merge message format: {mergeMessage.FormatName}");
                        var shouldIncrement = !context.Configuration.PreventIncrementForMergedBranchVersion;
                        return new[]
                        {
                            //new BaseVersion(context, $"{MergeMessageStrategyPrefix} '{c.MergeMessage.Trim()}'", shouldIncrement, mergeMessage.Version, c, null)
                            new BaseVersion(context, $"{MergeMessageStrategyPrefix} '{c.Commit.Message.Trim()}'", shouldIncrement, mergeMessage.Version, c.Commit, null)
                        };
                    }
                    else
                    {
                        return Enumerable.Empty<BaseVersion>();
                        //return null;
                    }
                }).ToList()
            .Take(5);

            log.Info($"Found {baseVersions.Count()} baseVersions." );

            return baseVersions;
        }

        private static bool IsMergeToReleaseBranch(GitVersionContext context, MergeMessage mergeMessage)
        {
            return //HasVersion(mergeMessage) &&
                   context.FullConfiguration.IsReleaseBranch(TrimRemote(mergeMessage.MergedBranch));
        }

        private static bool HasVersion(MergeMessage mergeMessage)
        {
            return mergeMessage?.Version != null;
        }

        public static readonly string MergeMessageStrategyPrefix = "Merge message";

        private static MergeMessage GetMergeMessage(IGitCommit mergeCommit, GitVersionContext context)
        {
             return new MergeMessage(mergeCommit.Message, context.FullConfiguration);
        }

        private static bool IsMergeCommit(IGitCommit mergeCommit)
        {
            return mergeCommit.Parents.Count() >= 2;
        }

        private static string TrimRemote(string branchName) => branchName
            .RegexReplace("^refs/remotes/", string.Empty, RegexOptions.IgnoreCase)
            .RegexReplace("^origin/", string.Empty, RegexOptions.IgnoreCase);
    }
}
