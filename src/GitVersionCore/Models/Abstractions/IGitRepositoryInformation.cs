namespace GitVersion.Models.Abstractions
{
    public interface IGitRepositoryInformation
    {
        string Path { get; }
        bool IsHeadDetached { get; }
    }
}
