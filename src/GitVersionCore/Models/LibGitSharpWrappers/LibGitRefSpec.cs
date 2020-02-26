using GitVersion.Models.Abstractions;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitRefSpec : IGitRefSpec
    {
        public LibGit2Sharp.RefSpec Wrapped { get; }

        public LibGitRefSpec(LibGit2Sharp.RefSpec wrapped)
        {
            Wrapped = wrapped;
        }

        public string Specification => Log(nameof(Specification), Wrapped.Specification);
        public string Source => Log(nameof(Source), Wrapped.Source);

        private T Log<T>(string name, T value)
        {
            Stats.Called(GetType().Name, name);
            Stats.Called(GetType().Name, name, value);

            return value;
        }
    }
}
