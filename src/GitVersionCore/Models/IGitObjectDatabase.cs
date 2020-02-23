namespace GitVersion.Models
{
    public interface IGitObjectDatabase
    {
        IGitCommit FindMergeBase(IGitCommit commit, IGitCommit commitToFindCommonBase);
        // implementation should do: Repository.ObjectDatabase.FindMergeBase(Commit, commitToFindCommonBase);
        string ShortenObjectId(IGitCommit contextCurrentCommit);
        // implementation should do: var shortSha = context.Repository.ObjectDatabase.ShortenObjectId(context.CurrentCommit);
    }
}
