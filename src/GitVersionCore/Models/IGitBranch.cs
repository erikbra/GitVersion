using System.Collections.Generic;

namespace GitVersion.Models
{
    public interface IGitBranch: IGitObject //, ICollection<IGitCommit>
    {
        string CanonicalName { get; }
        string FriendlyName { get; }
        IGitCommit Tip { get; }
        bool IsTracking { get; }
        bool IsRemote { get; }
        IGitCommitLog Commits { get; }
        string NameWithoutRemote();
        bool IsDetachedHead();
    }
}
