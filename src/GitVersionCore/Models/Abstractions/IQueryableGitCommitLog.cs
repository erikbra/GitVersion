using System.Collections.Generic;

namespace GitVersion.Models.Abstractions
{
    public interface IQueryableGitCommitLog : IGitCommitLog
    {
        IGitCommitLog QueryBy(GitCommitFilter filter);
        IEnumerable<IGitLogEntry> QueryBy(string path);
        IEnumerable<IGitLogEntry> QueryBy(string path, GitCommitFilter filter);
    }
}
