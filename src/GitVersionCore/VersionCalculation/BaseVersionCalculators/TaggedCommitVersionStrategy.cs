using System;
using System.Collections.Generic;
using System.Linq;
using GitVersion.Extensions;
using GitVersion.Logging;
using GitVersion.Models;

namespace GitVersion.VersionCalculation
{
    /// <summary>
    /// Version is extracted from all tags on the branch which are valid, and not newer than the current commit.
    /// BaseVersionSource is the Tag's commit.
    /// Increments if the Tag is not the current commit.
    /// </summary>
    public class TaggedCommitVersionStrategy : IVersionStrategy
    {
        private readonly ILog log;

        public TaggedCommitVersionStrategy(ILog log)
        {
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public virtual IEnumerable<BaseVersion> GetVersions(GitVersionContext context)
        {
            return GetTaggedVersions(context, context.CurrentBranch, context.CurrentCommit.When());
        }

        public IEnumerable<BaseVersion> GetTaggedVersions(GitVersionContext context, IGitBranch currentBranch, DateTimeOffset? olderThan)
        {
            var gitRepoMetadataProvider = new GitRepoMetadataProvider(context.Repository, log, context.FullConfiguration);
            var allTags = gitRepoMetadataProvider.GetValidVersionTags(context.Repository, context.Configuration.GitTagPrefix, olderThan);

            var tagsOnBranch = currentBranch
                .Commits
                .SelectMany(commit => { return allTags.Where(t => IsValidTag(t.Item1, commit)); })
                .Select(t =>
                {
                    var IGitCommit = t.Item1.PeeledTarget() as IGitCommit;
                    if (IGitCommit != null)
                        return new VersionTaggedCommit(IGitCommit, t.Item2, t.Item1.FriendlyName);

                    return null;
                })
                .Where(a => a != null)
                .ToList();

            return tagsOnBranch.Select(t => CreateBaseVersion(context, t));
        }

        private BaseVersion CreateBaseVersion(GitVersionContext context, VersionTaggedCommit version)
        {
            var shouldUpdateVersion = version.Commit.Sha != context.CurrentCommit.Sha;
            var baseVersion = new BaseVersion(context, FormatSource(version), shouldUpdateVersion, version.SemVer, version.Commit, null);
            return baseVersion;
        }

        protected virtual string FormatSource(VersionTaggedCommit version)
        {
            return $"Git Tag '{version.Tag}'";
        }

        protected virtual bool IsValidTag(IGitTag IGitTag, IGitCommit IGitCommit)
        {
            return IGitTag.PeeledTarget() == IGitCommit;
        }

        protected class VersionTaggedCommit
        {
            public string Tag;
            public IGitCommit Commit;
            public SemanticVersion SemVer;

            public VersionTaggedCommit(IGitCommit commit, SemanticVersion semVer, string tag)
            {
                Tag = tag;
                Commit = commit;
                SemVer = semVer;
            }

            public override string ToString()
            {
                return $"{Tag} | {Commit} | {SemVer}";
            }
        }
    }
}
