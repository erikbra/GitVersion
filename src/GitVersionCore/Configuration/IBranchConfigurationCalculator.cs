using System.Collections.Generic;
using GitVersion.Models;

namespace GitVersion.Configuration
{
    public interface IBranchConfigurationCalculator
    {
        /// <summary>
        /// Gets the <see cref="BranchConfig"/> for the current commit.
        /// </summary>
        BranchConfig GetBranchConfiguration(IGitBranch targetBranch, IList<IGitBranch> excludedInheritBranches = null);
    }
}
