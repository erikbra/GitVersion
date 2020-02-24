using System;
using System.Collections;
using System.Collections.Generic;
using GitVersion.Models.Abstractions;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitRemoteCollection : IGitRemoteCollection
    {
        public RemoteCollection Wrapped { get; }

        public LibGitRemoteCollection(RemoteCollection wrapped)
        {
            Wrapped = wrapped;
        }

        public IEnumerator<IGitRemote> GetEnumerator() => new LibGitRemoteEnumerator(Wrapped.GetEnumerator());
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        public void Update(string remoteName, params Action<IGitRemoteUpdater>[] actions)
        {
            throw new NotImplementedException();
        }

    }
}
