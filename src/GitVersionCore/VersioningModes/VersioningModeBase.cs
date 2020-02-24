using System.Collections.Generic;
using GitVersion.Models.Abstractions;

namespace GitVersion.VersioningModes
{
    public abstract class VersioningModeBase
    {
        public abstract SemanticVersionPreReleaseTag GetPreReleaseTag(GitVersionContext context, List<IGitTag> possibleTags, int numberOfCommits);
    }
}
