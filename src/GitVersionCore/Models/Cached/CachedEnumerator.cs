using System.Collections;
using System.Collections.Generic;

namespace GitVersion.Models
{
    public class CachedEnumerator<T>: IEnumerator<T>
    {
        private IList<T> _cache;
        private int _current = -1;

        public CachedEnumerator(IEnumerator<T> wrapped)
        {
            Wrapped = wrapped;
            _cache = new List<T>();
        }

        private IEnumerator<T> Wrapped { get; }

        public bool MoveNext() => ++_current < _cache.Count || Wrapped.MoveNext();

        public void Reset() => _current = -1;

        public T Current => InCache() ? GetFromCache() : CacheCurrentWrapped();

        private bool InCache() => _current < _cache.Count;
        private T GetFromCache() => _cache[_current];

        private T CacheCurrentWrapped()
        {
            var t = Wrapped.Current;
            _cache.Add(t);
            return t;
        }

        object IEnumerator.Current => Current;

        public void Dispose() => Wrapped?.Dispose();
    }
}
