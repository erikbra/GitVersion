using System;
using System.Collections.Generic;
using LibGit2Sharp;

namespace GitVersion.Models
{
    public interface IGitRemoteCollection: IEnumerable<IGitRemote>
    {
        void Update(string remoteName, params Action<IGitRemoteUpdater>[] actions);
    }
}
