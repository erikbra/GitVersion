using System.Collections.Generic;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitRemoteEnumerator : WrappingEnumerator<LibGitRemote, Remote>
    {
        public LibGitRemoteEnumerator(IEnumerator<Remote> wrapped) : base(wrapped)
        {
        }
    }
}
