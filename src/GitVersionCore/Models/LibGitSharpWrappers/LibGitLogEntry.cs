using GitVersion.Models.Abstractions;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitLogEntry : IGitLogEntry
    {
        private readonly LogEntry _wrapped;

        public LibGitLogEntry(LogEntry wrapped)
        {
            _wrapped = wrapped;
        }

        public string Path => _wrapped.Path;
        public IGitCommit Commit => new LibGitCommit(_wrapped.Commit);
    }
}
