using System.Collections;
using System.Collections.Generic;
using GitVersion.Models;
using LibGit2Sharp;

namespace GitVersionCore.Tests.Mocks
{
    public class MockReferenceCollection : IGitReferenceCollection, ICollection<IGitCommit>
    {
        public ReflogCollection Log(string canonicalName)
        {
            return new MockReflogCollection
            {
                Commits = Commits
            };
        }

        public List<IGitCommit> Commits = new List<IGitCommit>();

        IEnumerator<IGitReference> IEnumerable<IGitReference>.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<IGitCommit> GetEnumerator()
        {
            return Commits.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IGitCommit item)
        {
            Commits.Add(item);
        }

        public void Clear()
        {
            Commits.Clear();
        }

        public bool Contains(IGitCommit item)
        {
            return Commits.Contains(item);
        }

        public void CopyTo(IGitCommit[] array, int arrayIndex)
        {
            Commits.CopyTo(array, arrayIndex);
        }

        public bool Remove(IGitCommit item)
        {
            return Commits.Remove(item);
        }

        public int Count => Commits.Count;

        public bool IsReadOnly => false;
        public IGitDirectReference Add(string localCanonicalName, IGitObjectId objectId, bool b)
        {
            throw new System.NotImplementedException();
        }

        public IGitDirectReference Add(string localCanonicalName, string repoTipId)
        {
            throw new System.NotImplementedException();
        }

        public IGitDirectReference Add(string name, IGitObjectId targetId)
        {
            throw new System.NotImplementedException();
        }

        public IGitReference Head { get; }
        public IGitReferenceCollection FromGlob(string s)
        {
            throw new System.NotImplementedException();
        }

        public IGitReference this[string name] => throw new System.NotImplementedException();

        public void UpdateTarget(IGitReference repoRef, string repoTipId)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateTarget(IGitReference repoRef, IGitObjectId repoTipId)
        {
            throw new System.NotImplementedException();
        }
    }
}
