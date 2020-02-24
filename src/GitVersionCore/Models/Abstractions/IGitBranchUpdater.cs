namespace GitVersion.Models.Abstractions
{
    public interface IGitBranchUpdater
    {
        string TrackedBranch { set; }
    }
}
