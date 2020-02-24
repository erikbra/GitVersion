using GitVersion.Models.Abstractions;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitRepositoryInformation : IGitRepositoryInformation
    {
        public LibGit2Sharp.RepositoryInformation Wrapped { get; }

        public LibGitRepositoryInformation(LibGit2Sharp.RepositoryInformation wrapped)
        {
            Wrapped = wrapped;
        }

        public string Path => Wrapped.Path;
        public bool IsHeadDetached => Wrapped.IsHeadDetached;
    }
}
