namespace GitVersion.Models.Abstractions
{
    public interface IGitDirectReference: IGitReference
    {
        IGitReference Target { get; }
    }
}
