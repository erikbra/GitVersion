using GitVersion.Models.Abstractions;

namespace GitVersion.VersionCalculation
{
    public class BaseVersion
    {
        private readonly GitVersionContext context;

        public BaseVersion(GitVersionContext context, string source, bool shouldIncrement, SemanticVersion semanticVersion, IGitCommit baseVersionSource, string branchNameOverride)
        {
            Source = source;
            ShouldIncrement = shouldIncrement;
            SemanticVersion = semanticVersion;
            BaseVersionSource = baseVersionSource;
            BranchNameOverride = branchNameOverride;
            this.context = context;
        }

        public string Source { get; private set; }

        public bool ShouldIncrement { get; private set; }

        public SemanticVersion SemanticVersion { get; private set; }

        public IGitCommit BaseVersionSource { get; private set; }

        public string BranchNameOverride { get; private set; }

        public override string ToString()
        {
            return $"{Source}: {SemanticVersion?.ToString("f")} with Commit count source {(BaseVersionSource == null ? "External Source" : BaseVersionSource.Sha)} (Incremented: {(ShouldIncrement ? BaseVersionCalculator.MaybeIncrement(context, this)?.ToString("t") : "None")})";
            //return $"{Source}: {SemanticVersion.ToString("f")} with Commit count source {(BaseVersionSource == null ? "External Source" : BaseVersionSource.Sha)}";
        }
    }
}
