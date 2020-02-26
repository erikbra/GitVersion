using System.Collections.Generic;

namespace GitVersion.Models.Abstractions
{
    public interface IGitNetwork
    {
        IGitRemoteCollection Remotes { get; }
        IEnumerable<IGitDirectReference> ListReferences(IGitRemote remote);
        IEnumerable<IGitDirectReference> ListReferences(IGitRemote remote, GitCredentialsHandler handler);
    }
}