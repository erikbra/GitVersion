using System.Collections.Generic;

namespace GitVersion.Models
{
    public static class CachedEnumeratorExtensions
    {
        public static IEnumerator<T> Cached<T>(this IEnumerator<T> enumerator)
        {
            return Cached(enumerator, new List<T>());
        }

        private static IEnumerator<T> Cached<T>(IEnumerator<T> enumerator, IList<T> cache)
        {
            for (var i = 0;; i++)
            {
                if (i < cache.Count) yield return cache[i];
                else if (enumerator.MoveNext())
                {
                    var t = enumerator.Current;
                    cache.Add(t);
                    yield return t;
                }
                else break;
            }
        }

    }
}
