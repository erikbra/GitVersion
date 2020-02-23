using System.Linq;
using GitVersion.Extensions;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitBranch: IGitBranch
    {
        private Branch _wrapped;

        public LibGitBranch(Branch branch)
        {
            _wrapped = branch;
        }

        public string Sha { get; }
        public string CanonicalName => _wrapped.CanonicalName;
        public string FriendlyName => _wrapped.FriendlyName;
        public IGitCommit Tip => new LibGitCommit(_wrapped.Tip);
        public bool IsTracking => _wrapped.IsTracking;
        public bool IsRemote => _wrapped.IsRemote;
        public IGitCommitLog Commits => new LibGitCommitLog(_wrapped.Commits);
        public string NameWithoutRemote() => _wrapped.NameWithoutRemote();
        public bool IsDetachedHead() => _wrapped.IsDetachedHead();

    }
}
