namespace GitVersion.Models
{
    public interface IGitBranch: IGitObject //, ICollection<Commit>
    {
        string CanonicalName { get; }
        string FriendlyName { get; }
        IGitCommit Tip { get; }
        bool IsTracking { get; }
        bool IsRemote { get; }
        IGitCommitLog Commits { get; }
        string NameWithoutRemote();
        bool IsDetachedHead();
    }
}
