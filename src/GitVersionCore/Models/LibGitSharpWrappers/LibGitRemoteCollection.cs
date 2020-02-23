using System;
using System.Collections;
using System.Collections.Generic;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitRemoteCollection : IGitRemoteCollection
    {
        private IEnumerator<IGitRemote> _enumerator;
        public RemoteCollection Wrapped { get; }

        public LibGitRemoteCollection(RemoteCollection wrapped)
        {
            Wrapped = wrapped;
            _enumerator = new LibGitRemoteEnumerator(wrapped);
        }

        public IEnumerator<IGitRemote> GetEnumerator() => _enumerator;


        public void Update(string remoteName, params Action<IGitRemoteUpdater>[] actions)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
