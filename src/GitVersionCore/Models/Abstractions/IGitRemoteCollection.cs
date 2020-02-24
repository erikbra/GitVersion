using System;
using System.Collections.Generic;

namespace GitVersion.Models.Abstractions
{
    public interface IGitRemoteCollection: IEnumerable<IGitRemote>
    {
        void Update(string remoteName, params Action<IGitRemoteUpdater>[] actions);
    }
}
