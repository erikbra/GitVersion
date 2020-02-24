namespace GitVersion.Models.Abstractions
{
    public interface IGitTagAnnotation: IGitObject
    {
        IGitObject Target { get; }
        //object Wrapped { get; }
        //object Target { get; }
    }
}
