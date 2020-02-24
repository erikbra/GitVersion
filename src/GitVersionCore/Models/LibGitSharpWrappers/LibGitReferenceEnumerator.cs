using System.Collections.Generic;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitReferenceEnumerator : WrappingEnumerator<LibGitDirectReference, Reference>
    {
        public LibGitReferenceEnumerator(IEnumerator<Reference> wrapped) : base(wrapped)
        {
        }

    }
}
