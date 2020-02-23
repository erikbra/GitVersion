using System.Collections.Generic;
using GitVersion.Models;
using LibGit2Sharp;

namespace GitVersion.VersioningModes
{
    public class ContinuousDeploymentMode : VersioningModeBase
    {
        public override SemanticVersionPreReleaseTag GetPreReleaseTag(GitVersionContext context, List<IGitTag> possibleTags, int numberOfCommits)
        {
            return context.Configuration.Tag + "." + numberOfCommits;
        }
    }
}
