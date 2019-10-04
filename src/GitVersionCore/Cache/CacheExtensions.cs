namespace GitVersion.Cache
{
    using LibGit2Sharp;

    public static class CacheExtensions
    {
        public static ICommitLog Cache(this ICommitLog source) => new CachedCommitLog(source);
        public static Branch Cache(this Branch source) => new CachedBranch(source);
    }
}
