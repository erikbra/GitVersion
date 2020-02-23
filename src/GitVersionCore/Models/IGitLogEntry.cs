namespace GitVersion.Models
{
    public interface IGitLogEntry
    {
        /// <summary>
        /// The file's path relative to the repository's root.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// The commit in which the file was created or changed.
        /// </summary>
        IGitCommit Commit { get; }
    }
}
