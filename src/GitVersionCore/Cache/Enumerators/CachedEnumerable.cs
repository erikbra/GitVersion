namespace GitVersion.Cache.Enumerators
{
    using System.Collections;
    using System.Collections.Generic;

    public class CachedEnumerable<T>: IEnumerable<T>
    {
        private IEnumerable<T> _source;
        private IEnumerator<T> _enumerator;

        public CachedEnumerable(IEnumerable<T> source)
        {
            _source = source;
        }

        public IEnumerator<T> GetEnumerator() =>  _enumerator ?? (_enumerator = new CachedEnumerator<T>(_source.GetEnumerator()));

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
