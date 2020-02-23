using System.Collections;
using System.Collections.Generic;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitBranchEnumerator : IEnumerator<IGitBranch>
    {
        private IEnumerator<Branch> _wrapped;

        // TODO: Add cache here? Or in the reop?
        public LibGitBranchEnumerator(BranchCollection branchCollection)
        {
            _wrapped = branchCollection.GetEnumerator();
        }

        public bool MoveNext() => _wrapped.MoveNext();
        public void Reset() => _wrapped.Reset();
        public IGitBranch Current => new LibGitBranch(_wrapped.Current);
        object IEnumerator.Current => Current;
        public void Dispose() => _wrapped.Dispose();

    }
}
