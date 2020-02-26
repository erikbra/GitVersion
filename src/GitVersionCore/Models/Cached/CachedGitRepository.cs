using System;
using System.Collections.Generic;
using GitVersion.Models.Abstractions;

namespace GitVersion.Models
{
    public class CachedGitRepository : IGitRepository
    {
        public CachedGitRepository(IGitRepository wrapped)
        {
            Wrapped = wrapped;
        }

        private IGitRepository Wrapped { get;  }

        public IEnumerable<IGitTag> Tags => Wrapped.Tags.Cached();
        public IQueryableGitCommitLog Commits => Wrapped.Commits.Cached();
        public IGitBranch Head => Wrapped.Head;
        public IGitBranchCollection Branches => Wrapped.Branches.Cached();
        public IGitRepositoryInformation Info => Wrapped.Info;
        public IGitNetwork Network => Wrapped.Network;
        public IGitObjectDatabase ObjectDatabase => Wrapped.ObjectDatabase;
        public IGitReferenceCollection Refs => Wrapped.Refs.Cached();

        //public IGitReferenceCollection Refs => GetCachedOrUnderlying(nameof(Refs), ref _cachedRefs, Wrapped.Refs.Cached);


        private T GetCachedOrUnderlying<T>(string methodName, T cached, Func<T> underlying, object lockObject)
        {
            lock (lockObject)
            {
                return Utils.GetCachedOrUnderlying(GetType().Name, methodName, cached, underlying);
            }
        }
    }
}
