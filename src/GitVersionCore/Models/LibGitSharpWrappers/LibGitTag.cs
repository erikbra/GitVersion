using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitTag: IGitTag
    {
        public LibGitTag(Tag wrapped)
        {
            Wrapped = wrapped;
        }

        private Tag Wrapped { get; }

        public string FriendlyName { get; }
        public IGitObject Target { get; }
        public IGitTagAnnotation Annotation { get; }
    }
}
