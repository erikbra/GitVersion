using System.Collections.Generic;

namespace GitVersion.Models
{
    public static class CachedEnumerableExtensions
    {
        public static IEnumerable<T> Cached<T>(this IEnumerable<T> items)
        {
            var enumerator = items.GetEnumerator();
            return Cached(enumerator, new List<T>());
        }

        private static IEnumerable<T> Cached<T>(IEnumerator<T> enumerator, IList<T> cache)
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
