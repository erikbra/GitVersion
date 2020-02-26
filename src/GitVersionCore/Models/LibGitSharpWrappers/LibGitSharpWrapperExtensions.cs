using GitVersion.Models.Abstractions;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public static class LibGitSharpWrapperExtensions
    {
        public static IGitRepository Wrap(this IRepository repository) => new LibGitRepository(repository).Cached();
    }
}
