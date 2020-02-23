using GitVersion;
using GitVersion.Models;
using GitVersion.VersionCalculation;
using LibGit2Sharp;

namespace GitVersionCore.Tests.VersionCalculation
{
    public class TestMetaDataCalculator : IMetaDataCalculator
    {
        private readonly SemanticVersionBuildMetaData metaData;

        public TestMetaDataCalculator(SemanticVersionBuildMetaData metaData)
        {
            this.metaData = metaData;
        }

        public SemanticVersionBuildMetaData Create(IGitCommit baseVersionSource, GitVersionContext context)
        {
            return metaData;
        }
    }
}
