using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GitVersion.Models.Abstractions;

namespace GitVersionCore.Tests.Mocks
{
    public class MockCommitLog : IGitCommitLog, ICollection<IGitCommit>
    {
        public List<IGitCommit> Commits = new List<IGitCommit>();

        public IEnumerator<IGitCommit> GetEnumerator()
        {
            if (SortedBy == GitCommitSortStrategies.Reverse)
                return Commits.GetEnumerator();

            return Enumerable.Reverse(Commits).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public GitCommitSortStrategies SortedBy { get; set; }
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
