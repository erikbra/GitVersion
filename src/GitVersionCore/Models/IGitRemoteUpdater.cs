using System.Collections.Generic;
using LibGit2Sharp;

namespace GitVersion.Models
{
    public interface IGitRemoteUpdater
    {
        ICollection<string> FetchRefSpecs { get; }

    }

}
