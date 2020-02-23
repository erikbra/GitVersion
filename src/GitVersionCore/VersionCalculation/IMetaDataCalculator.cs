using GitVersion.Models;
using LibGit2Sharp;

namespace GitVersion.VersionCalculation
{
    public interface IMetaDataCalculator
    {
        SemanticVersionBuildMetaData Create(IGitCommit baseVersionSource, GitVersionContext context);
    }
}
