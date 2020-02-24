using System.Collections.Generic;

namespace GitVersion.Models.Abstractions
{
    public interface IGitRepository
    {
        IEnumerable<IGitTag> Tags { get; }
        IQueryableGitCommitLog Commits { get; }
        IGitBranch Head { get; }
        IGitBranchCollection Branches { get; }
        IGitRepositoryInformation Info { get; }
        IGitNetwork Network { get; }
        IGitObjectDatabase ObjectDatabase { get; }
        IGitReferenceCollection Refs { get; }
    }
}
