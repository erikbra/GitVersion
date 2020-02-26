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

        public string CanonicalName => Log(nameof(CanonicalName), Wrapped.CanonicalName);
        public string TargetIdentifier => Log(nameof(TargetIdentifier), Wrapped.TargetIdentifier));
        public string Id => Log(nameof(Id), Target.Id);
        public string Sha => Log(nameof(Sha), Target.Sha);

        public IGitDirectReference ResolveToDirectReference() => Log(nameof(ResolveToDirectReference), new LibGitDirectReference(Wrapped.ResolveToDirectReference()));

        public IGitReference Target { get; }

        private T Log<T>(string name, T value)
        {
            Stats.Called(GetType().Name, name);
            Stats.Called(GetType().Name, name, value);

            return value;
        }

    }
}
