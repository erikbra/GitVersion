namespace GitVersion.Models.Abstractions
{
    public interface IGitObjectId: IGitObject
    {
        string ToString(int prefixLength);
    }
}
