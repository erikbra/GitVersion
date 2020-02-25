using System.Collections;
using System.Collections.Generic;

namespace GitVersion.Models
{
    public abstract class CachedGitCollection<TItem, TCollection>: IEnumerable<TItem> where TCollection: IEnumerable<TItem>
    {
        protected TCollection Wrapped { get; set; }

        public IEnumerator<TItem> GetEnumerator() => Wrapped.GetEnumerator().Cached();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
