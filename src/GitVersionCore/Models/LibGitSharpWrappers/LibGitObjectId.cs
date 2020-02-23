using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitObjectId: IGitObjectId
    {
        private ObjectId _wrapped;

        public LibGitObjectId(string sha)
        {
            _wrapped = new ObjectId(sha);
        }

        public LibGitObjectId(ObjectId id)
        {
            _wrapped = id;
        }

        public string ToString(int prefixLength) => _wrapped.ToString(prefixLength);
        public string Sha => _wrapped.Sha;
    }
}
