using System.Collections.Generic;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitBranchEnumerator : WrappingEnumerator<LibGitBranch, Branch>
    {
        // TODO: Add cache here? Or in the repo?
        public LibGitBranchEnumerator(IEnumerator<Branch> branchCollection) : base(branchCollection)
        {
        }
    }
}
