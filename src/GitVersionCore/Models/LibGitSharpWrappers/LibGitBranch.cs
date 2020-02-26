using System;
using GitVersion.Extensions;
using GitVersion.Models.Abstractions;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitBranch: IGitBranch
    {
        public Branch Wrapped { get; }
        object IGitObject.Wrapped => Wrapped;

        public LibGitBranch(Branch branch)
        {
            Wrapped = branch;
        }

        public string Sha => Tip?.Sha;
        public string CanonicalName => Wrapped.CanonicalName;
        public string FriendlyName => Wrapped.FriendlyName;
        public IGitCommit Tip => new LibGitCommit(Wrapped.Tip);
        public bool IsTracking => Wrapped.IsTracking;
        public bool IsRemote => Wrapped.IsRemote;
        public IGitCommitLog Commits => new LibGitCommitLog(Wrapped.Commits);
        public string NameWithoutRemote() => Wrapped.NameWithoutRemote();
        public bool IsDetachedHead() => Wrapped.IsDetachedHead();

        private T Log<T>(string name, Func<T> func)
        {
            Stats.Called(GetType().Name + "." + name);
            return func();
        }

    }
}
