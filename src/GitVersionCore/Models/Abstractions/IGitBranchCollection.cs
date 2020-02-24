using System;
using System.Collections.Generic;

namespace GitVersion.Models.Abstractions
{
    public interface IGitBranchCollection: IEnumerable<IGitBranch>
    {
        IGitBranch this[string name] { get; }
        IGitBranch Update(IGitBranch branch, params Action<IGitBranchUpdater>[] actions);

    }
}
