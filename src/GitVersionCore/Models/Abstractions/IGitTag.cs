namespace GitVersion.Models.Abstractions
{
    public interface IGitTag
    {
        string FriendlyName { get; }
        IGitObject Target { get; }
        IGitTagAnnotation Annotation { get; }
    }
}
