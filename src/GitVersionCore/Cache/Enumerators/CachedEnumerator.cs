namespace GitVersion.Cache.Enumerators
{
    using System.Collections;
    using System.Collections.Generic;

    public class CachedEnumerator<T>: IEnumerator<T>
    {
        private IEnumerator<T> _source;

        public CachedEnumerator(IEnumerator<T> source)
        {
            _source = source;
        }

        public bool MoveNext() => _source.MoveNext();
        public void Reset() => _source.Reset();

        public T Current => _source.Current;

        object IEnumerator.Current => Current;

        public void Dispose() => _source.Dispose();
    }
}
