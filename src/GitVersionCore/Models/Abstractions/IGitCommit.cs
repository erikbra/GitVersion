using System.Collections.Generic;

namespace GitVersion.Models.Abstractions
{
    public interface IGitCommit: IGitObject
    {
        IGitObjectId Id { get; }
        IEnumerable<IGitCommit> Parents { get; }
        IGitSignature Committer { get; }
        string Message { get; }

    }
}
