using System.Collections.Generic;

namespace GitVersion.Models.Abstractions
{
    public interface IGitRemoteUpdater
    {
        ICollection<string> FetchRefSpecs { get; }

    }

}
