using GitVersion.Models.Abstractions;

namespace GitVersion.Models
{
    public static class CachedBranchCollectionExtensions
    {
        public static IQueryableGitCommitLog Cached(this IQueryableGitCommitLog source)
            => new CachedGitCommitLog(source);

        public static IGitBranchCollection Cached(this IGitBranchCollection source)
            => new CachedBranchCollection(source);

        public static IGitReferenceCollection Cached(this IGitReferenceCollection source)
            => new CachedGitReferenceCollection(source);

        public static IGitRepository Cached(this IGitRepository source)
            => new CachedGitRepository(source);
    }
}
