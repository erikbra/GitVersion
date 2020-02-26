using System;
using System.Collections;
using System.Collections.Generic;
using GitVersion.Models.Abstractions;

namespace GitVersion.Models
{
    public class CachedGitCommitLog : CachedGitCollection<IGitCommit, IQueryableGitCommitLog>, IQueryableGitCommitLog
    {
        public CachedGitCommitLog(IGitCommitLog wrapped)
        {
            if (!(wrapped is IQueryableGitCommitLog queryableCommitLog))
            {
                throw new ArgumentException(nameof(queryableCommitLog));
            }

            Wrapped = queryableCommitLog;
        }

        public CachedGitCommitLog(IQueryableGitCommitLog wrapped)
        {
            Wrapped = wrapped;
        }

        public GitCommitSortStrategies SortedBy => Wrapped.SortedBy;
        public IGitCommitLog QueryBy(GitCommitFilter filter) => new CachedGitCommitLog(Wrapped.QueryBy(filter));
        //public IGitCommitLog QueryBy(GitCommitFilter filter) => GetCacheEntry(filter);

        public IEnumerable<IGitLogEntry> QueryBy(string path) => Wrapped.QueryBy(path).Cached();
        public IEnumerable<IGitLogEntry> QueryBy(string path, GitCommitFilter filter) => Wrapped.QueryBy(path, filter).Cached();

        private IGitCommitLog GetCacheEntry(GitCommitFilter filter)
        {
            var key = GetCacheKey(filter);

            lock (_commitLogCache)
            {
                IGitCommitLog newEntry = null;

                var foundInCache = _commitLogCache.ContainsKey(key);

                if (foundInCache)
                {
                    newEntry =  _commitLogCache[key];
                }

                // Dummy call if found in cache - just for statistics
                newEntry = GetCachedOrUnderlying("QueryBy(GitCommitFilter filter)", newEntry, () => Wrapped.QueryBy(filter));

                if (!foundInCache)
                {
                    _commitLogCache.Add(key, newEntry);
                }

                return newEntry;
            }
        }

        private static string GetCacheKey(GitCommitFilter filter)
        {
            return $"{filter.IncludeReachableFrom.Sha}-{filter.ExcludeReachableFrom.Sha}-{filter.SortBy}-{filter.FirstParentOnly}";
        }

        private static IDictionary<string, IGitCommitLog> _commitLogCache = new Dictionary<string, IGitCommitLog>();

        private T GetCachedOrUnderlying<T>(string methodName, T cached, Func<T> underlying)
        {
            return Utils.GetCachedOrUnderlying(GetType().Name + "." + methodName, cached, underlying);
        }
    }
}
