using System.Collections.Generic;

namespace GitVersion.Models
{
    public interface IQueryableGitCommitLog : IGitCommitLog
    {
        IGitCommitLog QueryBy(IGitCommitFilter filter);
        IEnumerable<IGitLogEntry> QueryBy(string path);
        IEnumerable<IGitLogEntry> QueryBy(string path, IGitCommitFilter filter);
    }
}
