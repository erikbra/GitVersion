namespace GitVersion.Models
{
    public interface IGitRefSpec
    {
        string Specification { get; }
        string Source { get; }
    }
}
