namespace GitVersion.Models
{
    public interface IGitTag
    {
        string FriendlyName { get; }
        IGitObject Target { get; }
        IGitTagAnnotation Annotation { get; }
    }
}
