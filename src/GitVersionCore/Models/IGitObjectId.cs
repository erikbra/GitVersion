namespace GitVersion.Models
{
    public interface IGitObjectId: IGitObject
    {
        string ToString(int prefixLength);
    }
}
