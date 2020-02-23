using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitCommitLog: IQueryableGitCommitLog
    {
        private IQueryableCommitLog _wrapped;
        private IEnumerator<IGitCommit> _enumerator;

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


        public IEnumerator<IGitCommit> GetEnumerator() => _enumerator ??= new LibGitCommitEnumerator(_wrapped);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public GitCommitSortStrategies SortedBy { get; }

        public IGitCommitLog QueryBy(IGitCommitFilter filter)
            => new LibGitCommitLog(_wrapped.QueryBy(GetCommitFilter(filter)));

        public IEnumerable<IGitLogEntry> QueryBy(string path) => _wrapped.QueryBy(path).Select(c => new LibGitLogEntry(c));

        public IEnumerable<IGitLogEntry> QueryBy(string path, IGitCommitFilter filter)
            => _wrapped.QueryBy(path, GetCommitFilter(filter)).Select(l => new LibGitLogEntry(l));


        private CommitFilter GetCommitFilter(IGitCommitFilter filter)
        {
            if (!(filter is LibGitCommitFilter lgf))
            {
                throw new ArgumentException(nameof(filter));
            }

            return lgf.Wrapped;
        }
    }
}
