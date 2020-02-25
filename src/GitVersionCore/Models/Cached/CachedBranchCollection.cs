using System;
using GitVersion.Models.Abstractions;

namespace GitVersion.Models
{
    public class CachedBranchCollection: CachedGitCollection<IGitBranch, IGitBranchCollection>, IGitBranchCollection
    {
        public CachedBranchCollection(IGitBranchCollection wrapped)
        {
            Wrapped = wrapped;
        }

        public IGitBranch this[string name] => Wrapped[name];
        public IGitBranch Update(IGitBranch branch, params Action<IGitBranchUpdater>[] actions) => Wrapped.Update(branch, actions);
    }
}
