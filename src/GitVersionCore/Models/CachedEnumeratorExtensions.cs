using System.Collections.Generic;

namespace GitVersion.Models
{
    public static class CachedEnumeratorExtensions
    {
        private static IDictionary<string, object> _enumerators = new Dictionary<string, object>();

        public static IEnumerator<T> Cached<T>(this IEnumerator<T> enumerator, string cacheKey)
        {
            IEnumerator<T> e;

            if (_enumerators.ContainsKey(cacheKey))
            {
                e = (IEnumerator<T>) _enumerators[cacheKey];
                // TOOD: This is not thread-safe, as someone else might be using the enumerator already
                e.Reset();
            }
            else
            {
                e = new  CachedEnumerator<T>(enumerator);
                _enumerators.Add(cacheKey, e);
            }

            return e;
        }

    }
}
