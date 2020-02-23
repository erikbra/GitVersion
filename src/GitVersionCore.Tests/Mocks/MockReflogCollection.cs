using System.Collections;
using System.Collections.Generic;
using GitVersion.Models;
using LibGit2Sharp;

namespace GitVersionCore.Tests.Mocks
{
    public class MockReflogCollection : ReflogCollection, ICollection<IGitCommit>
    {
        public List<IGitCommit> Commits = new List<IGitCommit>();

        public new IEnumerator<IGitCommit> GetEnumerator()
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
    }
}
