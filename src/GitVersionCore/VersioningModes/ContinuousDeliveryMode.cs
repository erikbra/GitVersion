using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;
using GitVersion.Extensions;
using GitVersion.Models;

namespace GitVersion.VersioningModes
{
    public class ContinuousDeliveryMode : VersioningModeBase
    {
        public override SemanticVersionPreReleaseTag GetPreReleaseTag(GitVersionContext context, List<IGitTag> possibleCommits, int numberOfCommits)
        {
            return RetrieveMostRecentOptionalTagVersion(context, possibleCommits) ?? context.Configuration.IGitTag + ".1";
        }

        private static SemanticVersionPreReleaseTag RetrieveMostRecentOptionalTagVersion(GitVersionContext context, List<IGitTag> applicableTagsInDescendingOrder)
        {
            if (applicableTagsInDescendingOrder.Any())
            {
                var taggedCommit = applicableTagsInDescendingOrder.First().PeeledTarget();
                var preReleaseVersion = applicableTagsInDescendingOrder.Select(IGitTag => SemanticVersion.Parse(IGitTag.FriendlyName, context.Configuration.GitTagPrefix)).FirstOrDefault();
                if (preReleaseVersion != null)
                {
                    if (taggedCommit != context.CurrentCommit)
                    {
                        preReleaseVersion.PreReleaseTag.Number++;
                    }
                    return preReleaseVersion.PreReleaseTag;
                }
            }
            return null;
        }
    }
}
