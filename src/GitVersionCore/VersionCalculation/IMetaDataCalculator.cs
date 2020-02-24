using GitVersion.Models.Abstractions;

namespace GitVersion.VersionCalculation
{
    public interface IMetaDataCalculator
    {
        SemanticVersionBuildMetaData Create(IGitCommit baseVersionSource, GitVersionContext context);
    }
}
