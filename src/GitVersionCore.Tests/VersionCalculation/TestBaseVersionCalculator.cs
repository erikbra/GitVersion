using GitVersion;
using GitVersion.Models.Abstractions;
using GitVersion.VersionCalculation;

namespace GitVersionCore.Tests.VersionCalculation
{
    public class TestBaseVersionCalculator : IBaseVersionCalculator
    {
        private readonly SemanticVersion semanticVersion;
        private readonly bool shouldIncrement;
        private readonly IGitCommit source;

        public TestBaseVersionCalculator(bool shouldIncrement, SemanticVersion semanticVersion, IGitCommit source)
        {
            this.semanticVersion = semanticVersion;
            this.source = source;
            this.shouldIncrement = shouldIncrement;
        }

        public BaseVersion GetBaseVersion(GitVersionContext context)
        {
            return new BaseVersion(context, "Test source", shouldIncrement, semanticVersion, source, null);
        }
    }
}
