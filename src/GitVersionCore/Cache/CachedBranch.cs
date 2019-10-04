namespace GitVersion.Cache
{
    using LibGit2Sharp;

    public class CachedBranch: Branch
    {
        private Branch _source;

        public CachedBranch(Branch source)
        : base(source.repo, source.)

        {
            _source = source;
            Commits = source.Commits.Cache();
        }

        public override Branch TrackedBranch => _source.TrackedBranch;
        public override BranchTrackingDetails TrackingDetails => _source.TrackingDetails;
        public override bool IsCurrentRepositoryHead => _source.IsCurrentRepositoryHead;
        public override Commit Tip => _source.Tip;
        public override string UpstreamBranchCanonicalName => _source.UpstreamBranchCanonicalName;
        public override string RemoteName => _source.RemoteName;
        public override string CanonicalName => _source.CanonicalName;
        public override ICommitLog Commits { get; }
        public override TreeEntry this[string relativePath] => _source[relativePath];
        public override bool IsRemote => _source.IsRemote;
    }
}
