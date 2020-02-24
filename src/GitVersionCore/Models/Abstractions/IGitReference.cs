namespace GitVersion.Models.Abstractions
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
