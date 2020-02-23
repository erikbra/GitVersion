using System;
using System.Collections;
using System.Collections.Generic;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitReferenceCollection : IGitReferenceCollection
    {
        private IEnumerator<IGitReference> _enumerator;
        public LibGit2Sharp.ReferenceCollection Wrapped { get; }

        public LibGitReferenceCollection(LibGit2Sharp.ReferenceCollection wrapped)
        {
            Wrapped = wrapped;
            _enumerator = new LibGitReferenceEnumerator(wrapped.GetEnumerator());
        }

        public IEnumerator<IGitReference> GetEnumerator() => _enumerator;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IGitDirectReference Add(string localCanonicalName, IGitObjectId objectId, bool b)
        {
            throw new NotImplementedException();
        }

        public IGitDirectReference Add(string localCanonicalName, string repoTipId)
        {
            throw new NotImplementedException();
        }

        public IGitDirectReference Add(string name, IGitObjectId targetId)
        {
            throw new NotImplementedException();
        }

        public IGitReference Head { get; }
        public IGitReferenceCollection FromGlob(string s)
        {
            throw new NotImplementedException();
        }

        public IGitReference this[string name] => throw new NotImplementedException();

        public void UpdateTarget(IGitReference repoRef, string repoTipId)
        {
            throw new NotImplementedException();
        }

        public void UpdateTarget(IGitReference repoRef, IGitObjectId repoTipId)
        {
            throw new NotImplementedException();
        }
    }
}
