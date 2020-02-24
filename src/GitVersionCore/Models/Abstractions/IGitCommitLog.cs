using System.Collections.Generic;

namespace GitVersion.Models.Abstractions
{
    public interface IGitCommitLog: IEnumerable<IGitCommit>
    {
        /// <summary>
        /// Gets the current sorting strategy applied when enumerating the log.
        /// </summary>
        GitCommitSortStrategies SortedBy { get; }
    }
}
