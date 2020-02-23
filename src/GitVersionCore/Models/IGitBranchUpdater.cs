namespace GitVersion.Models
{
    public interface IGitBranchUpdater
    {
        string TrackedBranch { get; set; }
    }
}
