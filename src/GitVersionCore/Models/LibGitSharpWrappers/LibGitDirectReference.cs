using GitVersion.Models.Abstractions;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitDirectReference : LibGitReference, IGitDirectReference
    {
        public new DirectReference Wrapped => (DirectReference) base.Wrapped;

        public LibGitDirectReference(Reference wrapped): base(wrapped)
        {
        }

    }
}
