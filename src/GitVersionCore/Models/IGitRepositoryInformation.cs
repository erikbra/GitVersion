namespace GitVersion.Models
{
    public interface IGitRepositoryInformation
    {
        string Path { get; }
        bool IsHeadDetached { get; }
    }
}
