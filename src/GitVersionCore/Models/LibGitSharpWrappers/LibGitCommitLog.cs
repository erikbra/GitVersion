using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GitVersion.Models.Abstractions;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitCommitLog: IQueryableGitCommitLog
    {
        private IQueryableCommitLog _wrapped;

        public LibGitCommitLog(IQueryableCommitLog wrapped)
        {
            _wrapped = wrapped;
        }

        public LibGitCommitLog(ICommitLog wrapped)
        {
            if (!(wrapped is IQueryableCommitLog queryableCommitLog))
            {
                throw new ArgumentException(nameof(queryableCommitLog));
            }
            _wrapped = queryableCommitLog;
        }


        public IEnumerator<IGitCommit> GetEnumerator() => new LibGitCommitEnumerator(_wrapped.GetEnumerator());
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public GitCommitSortStrategies SortedBy { get; }

        public IGitCommitLog QueryBy(GitCommitFilter filter)
            => new LibGitCommitLog(_wrapped.QueryBy(GetCommitFilter(filter)));

        public IEnumerable<IGitLogEntry> QueryBy(string path) => _wrapped.QueryBy(path).Select(c => new LibGitLogEntry(c));

        public IEnumerable<IGitLogEntry> QueryBy(string path, GitCommitFilter filter)
            => _wrapped.QueryBy(path, GetCommitFilter(filter)).Select(l => new LibGitLogEntry(l));


        private static CommitFilter GetCommitFilter(GitCommitFilter filter)
        {
            var commitFilter = new CommitFilter();

            if (filter.ExcludeReachableFrom != null)
            {
                commitFilter.ExcludeReachableFrom = filter.ExcludeReachableFrom.Wrapped;
            }

            if (filter.IncludeReachableFrom != null)
            {
                commitFilter.IncludeReachableFrom = filter.IncludeReachableFrom.Wrapped;
            }

            if (filter.FirstParentOnly.HasValue)
            {
                commitFilter.FirstParentOnly = filter.FirstParentOnly.Value;
            }

            if (filter.SortBy.HasValue)
            {
                commitFilter.SortBy = (CommitSortStrategies) filter.SortBy.Value;
            }

            return commitFilter;
        }
    }
}
