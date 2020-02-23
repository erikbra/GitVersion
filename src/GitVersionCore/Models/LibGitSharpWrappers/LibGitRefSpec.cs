namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitRefSpec : IGitRefSpec
    {
        public LibGit2Sharp.RefSpec Wrapped { get; }

        public LibGitRefSpec(LibGit2Sharp.RefSpec wrapped)
        {
            Wrapped = wrapped;
        }

        public string Specification => Wrapped.Specification;
        public string Source => Wrapped.Source;
    }
}
