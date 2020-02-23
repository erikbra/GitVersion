using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GitVersion.Models;

namespace GitVersionCore.Tests.Mocks
{
    public class MockBranchCollection : IGitBranchCollection, ICollection<IGitBranch>
    {
        public List<IGitBranch> Branches = new List<IGitBranch>();

        public IEnumerator<IGitBranch> GetEnumerator()
        {
            return Branches.GetEnumerator();
        }

        public IGitBranch this[string friendlyName]
        {
            get { return Branches.FirstOrDefault(x => x.FriendlyName == friendlyName); }
        }

        public IGitBranch Update(IGitBranch branch, params Action<IGitBranchUpdater>[] actions)
        {
            throw new NotImplementedException();
        }

        public void Add(IGitBranch item)
        {
            Branches.Add(item);
        }

        public void Clear()
        {
            Branches.Clear();
        }

        public bool Contains(IGitBranch item)
        {
            return Branches.Contains(item);
        }

        public void CopyTo(IGitBranch[] array, int arrayIndex)
        {
            Branches.CopyTo(array, arrayIndex);
        }

        bool ICollection<IGitBranch>.Remove(IGitBranch item)
        {
            throw new NotImplementedException();
        }

        public void Remove(IGitBranch item)
        {
            Branches.Remove(item);
        }


        public int Count => Branches.Count;
        public bool IsReadOnly => false;
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
