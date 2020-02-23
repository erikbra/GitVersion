using System.Collections;
using System.Collections.Generic;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitRemoteEnumerator : IEnumerator<IGitRemote>
    {
        private IEnumerator<Remote> _wrappedEnumerator;
        public RemoteCollection Wrapped { get; }

        public LibGitRemoteEnumerator(RemoteCollection wrapped)
        {
            Wrapped = wrapped;
            _wrappedEnumerator = wrapped.GetEnumerator();
        }

        public bool MoveNext() => _wrappedEnumerator.MoveNext();
        public void Reset() => _wrappedEnumerator.Reset();
        public IGitRemote Current => new LibGitRemote(_wrappedEnumerator.Current);
        object IEnumerator.Current => Current;
        public void Dispose() => _wrappedEnumerator.Dispose();
        
    }
}
