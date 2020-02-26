using System;
using System.Collections;
using System.Collections.Generic;

namespace GitVersion.Models
{
    public abstract class CachedGitCollection<TItem, TCollection>: IEnumerable<TItem> where TCollection: IEnumerable<TItem>
    {
        private readonly string _cacheKey;
        private IEnumerator<TItem> _enumerator;
        private string _uniqueId = Guid.NewGuid().ToString();

        protected TCollection Wrapped { get; set; }

        protected CachedGitCollection()
        {
            Stats.Called(GetType().Name, "<ctor>");
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            var status = Status.Existed;

            if (_enumerator == null)
            {
                status = Status.CalledUnderlying;
                _enumerator = Wrapped.GetEnumerator().Cached(_uniqueId);
                //_enumerator = Wrapped.GetEnumerator().Cached(_cacheKey);
                //_enumerator = Wrapped.GetEnumerator().Cached(GetType().Name);
            }

            Stats.Called(GetType().Name, nameof(GetEnumerator), status);

            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public enum Status
    {
        Existed,
        CalledUnderlying
    }
}
