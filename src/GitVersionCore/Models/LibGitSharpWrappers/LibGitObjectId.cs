using GitVersion.Models.Abstractions;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitObjectId: IGitObjectId
    {
        public ObjectId Wrapped { get; }
        object IGitObject.Wrapped => Wrapped;

        public LibGitObjectId(string sha)
        {
            Wrapped = new ObjectId(sha);
        }

        public LibGitObjectId(ObjectId id)
        {
            Wrapped = id;
        }

        public string ToString(int prefixLength) => Wrapped.ToString(prefixLength);
        public string Sha => Wrapped.Sha;
    }
}
