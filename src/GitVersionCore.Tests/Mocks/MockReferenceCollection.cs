using System.Collections;
using System.Collections.Generic;
using GitVersion.Models.Abstractions;
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
        public IGitReference Add(string localCanonicalName, IGitObjectId objectId, bool allowOverwrite)
        {
            throw new System.NotImplementedException();
        }

        public IGitReference Add(string localCanonicalName, string repoTipId)
        {
            throw new System.NotImplementedException();
        }

        public IGitReference Add(string name, IGitObjectId targetId)
        {
            throw new System.NotImplementedException();
        }

        public IGitReference Head { get; }
        public IEnumerable<IGitReference> FromGlob(string pattern)
        {
            throw new System.NotImplementedException();
        }

        public IGitReference this[string name] => throw new System.NotImplementedException();

        public void UpdateTarget(IGitReference repoRef, string objectish)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateTarget(IGitReference repoRef, IGitObjectId targetId)
        {
            throw new System.NotImplementedException();
        }
    }
}
