namespace GitVersion.Cache
{
    using GitVersion.Cache.Enumerators;
    using LibGit2Sharp;

    public class CachedCommitLog: CachedEnumerable<Commit>, ICommitLog
    {
        public CommitSortStrategies SortedBy { get; set; }

        public CachedCommitLog(ICommitLog source) : base(source)
        {
        }
    }
}
