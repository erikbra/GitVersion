using GitVersion.Models.Abstractions;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitBranchUpdater: IGitBranchUpdater
    {
        public LibGitBranchUpdater(BranchUpdater wrapped)
        {
            Wrapped = wrapped;
        }

        public string TrackedBranch
        {
            set => Wrapped.TrackedBranch = value;
        }

        public BranchUpdater Wrapped { get; }
    }
}
