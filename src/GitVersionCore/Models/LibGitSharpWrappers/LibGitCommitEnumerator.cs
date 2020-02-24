using System.Collections.Generic;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitCommitEnumerator : WrappingEnumerator<LibGitCommit, Commit>
    {
        // TODO: Add cache here? Or in the repo?
        public LibGitCommitEnumerator(IEnumerator<Commit> commitLog) : base(commitLog)
        {
        }
    }
}
