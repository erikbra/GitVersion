namespace GitVersion.Models
{
    public interface IGitRefSpec
    {
        string Specification { get; set; }
        string Source { get; set; }
    }
}
