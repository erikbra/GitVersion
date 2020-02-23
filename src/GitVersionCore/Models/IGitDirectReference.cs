namespace GitVersion.Models
{
    public interface IGitDirectReference: IGitReference
    {
        IGitDirectReference ResolveToDirectReference();
        IGitReference Target { get; }
    }
}
