using System;
using System.Collections.Generic;
using GitVersion.Models.Abstractions;

namespace GitVersion.Models
{
    public class CachedGitCommitLog : CachedGitCollection<IGitCommit, IQueryableGitCommitLog>, IQueryableGitCommitLog
    {
        public CachedGitCommitLog(IGitCommitLog wrapped)
        {
            if (!(wrapped is IQueryableGitCommitLog queryableCommitLog))
            {
                throw new ArgumentException(nameof(queryableCommitLog));
            }
            Wrapped = queryableCommitLog;
        }

        public CachedGitCommitLog(IQueryableGitCommitLog wrapped)
        {
            Wrapped = wrapped;
        }

        public GitCommitSortStrategies SortedBy => Wrapped.SortedBy;
        public IGitCommitLog QueryBy(GitCommitFilter filter) => new CachedGitCommitLog(Wrapped.QueryBy(filter));

        public IEnumerable<IGitLogEntry> QueryBy(string path) => Wrapped.QueryBy(path).Cached();
        public IEnumerable<IGitLogEntry> QueryBy(string path, GitCommitFilter filter) => Wrapped.QueryBy(path, filter).Cached();
    }
}
