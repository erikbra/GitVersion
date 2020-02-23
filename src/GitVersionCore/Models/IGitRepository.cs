using System.Collections.Generic;
using LibGit2Sharp;

namespace GitVersion.Models
{
    public interface IGitRepository
    {
        IEnumerable<IGitTag> Tags { get; set; }
        IQueryableGitCommitLog Commits { get; set; }
        IGitBranch Head { get; set; }
        IGitBranchCollection Branches { get; set; }
        IGitRepositoryInformation Info { get; }
        IGitNetwork Network { get; }
        IGitObjectDatabase ObjectDatabase { get; }
        IGitReferenceCollection Refs { get; }
    }
}
