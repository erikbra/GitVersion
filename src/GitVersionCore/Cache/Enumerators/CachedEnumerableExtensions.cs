namespace GitVersion.Cache.Enumerators
{
    using System.Collections.Generic;

    public static class CachedEnumerableExtensions
    {
        public static CachedEnumerable<T> Cache<T>(this IEnumerable<T> source) => new CachedEnumerable<T>(source);
    }
}
