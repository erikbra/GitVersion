namespace GitVersion.Models.Abstractions
{
    public class GitCommitFilter : IGitCommitFilter
    {
        public IGitObject IncludeReachableFrom { get; set; }
        public GitCommitSortStrategies? SortBy { get; set; }
        public IGitObject ExcludeReachableFrom { get; set; }
        public bool? FirstParentOnly { get; set; }
    }
}
