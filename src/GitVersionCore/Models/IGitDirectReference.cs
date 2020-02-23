namespace GitVersion.Models
{
    public interface IGitDirectReference: IGitReference
    {
        IGitReference Target { get; }
    }
}
