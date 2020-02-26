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
            Stats.Called(GetType().Name, "<ctor>", branch.CanonicalName);
            Wrapped = branch;
        }

        public string Sha => Log(nameof(Sha), Tip?.Sha);
        public string CanonicalName => Log(nameof(CanonicalName), Wrapped.CanonicalName);
        public string FriendlyName => Log(nameof(FriendlyName),  Wrapped.FriendlyName);
        public IGitCommit Tip => Log(nameof(Tip), new LibGitCommit(Wrapped.Tip));
        public bool IsTracking => Log(nameof(IsTracking), Wrapped.IsTracking);
        public bool IsRemote => Log(nameof(IsRemote), Wrapped.IsRemote);
        public IGitCommitLog Commits => Log(nameof(Commits), new LibGitCommitLog(Wrapped.Commits));
        public string NameWithoutRemote() => Log(nameof(NameWithoutRemote), Wrapped.NameWithoutRemote());
        public bool IsDetachedHead() => Log(nameof(IsDetachedHead), Wrapped.IsDetachedHead());

        private T Log<T>(string name, T value)
        {
            Stats.Called(GetType().Name, name);
            Stats.Called(GetType().Name, name, value);

            return value;
        }
    }
}
