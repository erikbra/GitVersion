namespace GitVersion.Models.Abstractions
{
    public interface IGitObject
    {
        string Sha { get; }
        object Wrapped { get; }
    }
}
