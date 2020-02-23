using LibGit2Sharp;

namespace GitVersion.Models
{
    public interface IGitObject
    {
        string Sha { get; }
        object Wrapped { get; }
    }
}
