namespace GitVersion.Models.Abstractions
{
    public interface IGitRefSpec
    {
        string Specification { get; }
        string Source { get; }
    }
}
