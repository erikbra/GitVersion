using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitCommitFilter: IGitCommitFilter
    {
        public CommitFilter Wrapped { get; }

        public LibGitCommitFilter(CommitFilter wrapped)
        {
            Wrapped = wrapped;
        }

    }
}
