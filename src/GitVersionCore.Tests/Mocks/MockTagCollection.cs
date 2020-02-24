using System.Collections;
using System.Collections.Generic;
using GitVersion.Models.Abstractions;

namespace GitVersionCore.Tests.Mocks
{
    public class MockTagCollection : IGitTagCollection, ICollection<IGitTag>
    {
        public List<IGitTag> Tags = new List<IGitTag>();
        public IEnumerator<IGitTag> GetEnumerator()
        {
            return Tags.GetEnumerator();
        }

        IEnumerator<IGitTag> IEnumerable<IGitTag>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IGitTag item)
        {
            Tags.Add(item);
        }

        public void Clear()
        {
            Tags.Clear();
        }

        public bool Contains(IGitTag item)
        {
            return Tags.Contains(item);
        }

        public void CopyTo(IGitTag[] array, int arrayIndex)
        {
            Tags.CopyTo(array, arrayIndex);
        }

        public void Remove(IGitTag tag)
        {
            Tags.Remove(tag);
        }

        bool ICollection<IGitTag>.Remove(IGitTag item)
        {
            return Tags.Remove(item);
        }

        public int Count => Tags.Count;
        public bool IsReadOnly => false;
    }
}
