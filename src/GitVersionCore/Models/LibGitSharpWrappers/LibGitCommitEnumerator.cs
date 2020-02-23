using System.Collections;
using System.Collections.Generic;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitCommitEnumerator : IEnumerator<IGitCommit>
    {
        private IEnumerator<Commit> _wrapped;

        // TODO: Add cache here? Or in the reop?
        public LibGitCommitEnumerator(ICommitLog commitLog)
        {
            _wrapped = commitLog.GetEnumerator();
        }

        public bool MoveNext() => _wrapped.MoveNext();
        public void Reset() => _wrapped.Reset();
        public IGitCommit Current => new LibGitCommit(_wrapped.Current);
        object IEnumerator.Current => Current;
        public void Dispose() => _wrapped.Dispose();

    }
}
