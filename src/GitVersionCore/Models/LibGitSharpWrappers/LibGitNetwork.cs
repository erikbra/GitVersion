using System;
using System.Collections.Generic;
using System.Linq;
using lgs =  LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitNetwork : IGitNetwork
    {
        public lgs.Network Wrapped { get; }

        public LibGitNetwork(lgs.Network wrapped)
        {
            Wrapped = wrapped;
        }

        public IGitRemoteCollection Remotes => new LibGitRemoteCollection(Wrapped.Remotes);
        public IEnumerable<IGitDirectReference> ListReferences(IGitRemote remote)
        {
            if (!(remote is LibGitRemote lgr))
            {
                throw new ArgumentException(nameof(remote));
            }

            return Wrapped.ListReferences(lgr.Wrapped).Select(r => new LibGitDirectReference(r));
        }

        public IEnumerable<IGitDirectReference> ListReferences(IGitRemote remote, GitCredentialsHandler handler)
        {
            // TODO: Implement
            throw new NotImplementedException();

            // if (!(remote is LibGitRemote lgr))
            // {
            //     throw new ArgumentException(nameof(remote));
            // }
            //
            // CredentialsHandler h = (url, fromUrl, types) => handler(url, fromUrl, new LibGitSupportedCredentialTypes(types).Wrapped);
            //
            // return Wrapped.ListReferences(lgr.Wrapped, handler).Select(r => new LibGitDirectReference(r));
        }
    }

    public class LibGitSupportedCredentialTypes : IGitSupportedCredentialTypes
    {
        public lgs.SupportedCredentialTypes Wrapped { get; }

        public LibGitSupportedCredentialTypes(lgs.SupportedCredentialTypes wrapped)
        {
            Wrapped = wrapped;
        }
    }
}
