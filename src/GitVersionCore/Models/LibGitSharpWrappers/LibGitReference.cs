using GitVersion.Models.Abstractions;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitReference : IGitReference
    {
        public Reference Wrapped { get; }

        public LibGitReference(Reference wrapped)
        {
            Wrapped = wrapped;
        }

        public string CanonicalName => Wrapped.CanonicalName;
        public string TargetIdentifier => Wrapped.TargetIdentifier;
        public string Id => Target.Id;
        public string Sha => Target.Sha;

        public IGitDirectReference ResolveToDirectReference() => new LibGitDirectReference(Wrapped.ResolveToDirectReference());

        public IGitReference Target { get; }

    }
}
