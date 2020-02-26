using System;
using System.Collections;
using System.Collections.Generic;

namespace GitVersion.Models
{
    public abstract class CachedGitCollection<TItem, TCollection>: IEnumerable<TItem> where TCollection: IEnumerable<TItem>
    {
        private IEnumerator<TItem> _enumerator;
        private string _uniqueId = Guid.NewGuid().ToString();

        protected TCollection Wrapped { get; set; }

        public IEnumerator<TItem> GetEnumerator() => _enumerator ??= Wrapped.GetEnumerator().Cached(_uniqueId);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
