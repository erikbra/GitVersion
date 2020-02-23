using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitDirectReference : IGitDirectReference
    {
        public DirectReference Wrapped { get; }

        public LibGitDirectReference(Reference wrapped)
        {
            Wrapped = (DirectReference) wrapped;
        }

        public string CanonicalName => Wrapped.CanonicalName;
        public string TargetIdentifier => Wrapped.TargetIdentifier;
        public string Id => Target.Id;
        public string Sha => Target.Sha;

        IGitDirectReference IGitReference.ResolveToDirectReference() => this;

        public IGitReference Target { get; }

    }
}
