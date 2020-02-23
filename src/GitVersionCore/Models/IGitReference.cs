namespace GitVersion.Models
{
    public interface IGitReference
    {
        string CanonicalName { get; }
        string TargetIdentifier { get; }
        string Id { get; }
        string Sha { get; }
        IGitDirectReference ResolveToDirectReference();
    }
}
