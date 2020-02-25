using System.Collections.Generic;
using GitVersion.Models.Abstractions;

namespace GitVersion.Models
{
    public class CachedGitReferenceCollection: CachedGitCollection<IGitReference, IGitReferenceCollection>, IGitReferenceCollection
    {
        public CachedGitReferenceCollection(IGitReferenceCollection wrapped)
        {
            Wrapped = wrapped;
        }

        public IGitReference Add(string localCanonicalName, IGitObjectId objectId, bool allowOverwrite)
            => Wrapped.Add(localCanonicalName, objectId, allowOverwrite);

        public IGitReference Add(string localCanonicalName, string repoTipId)
            => Wrapped.Add(localCanonicalName, repoTipId);

        public IGitReference Add(string name, IGitObjectId targetId)
            => Wrapped.Add(name, targetId);

        public IGitReference Head => Wrapped.Head;
        public IEnumerable<IGitReference> FromGlob(string pattern) => Wrapped.FromGlob(pattern).Cached();

        public IGitReference this[string name] => Wrapped[name];

        public void UpdateTarget(IGitReference repoRef, string objectish) => Wrapped.UpdateTarget(repoRef, objectish);
        public void UpdateTarget(IGitReference repoRef, IGitObjectId targetId) => Wrapped.UpdateTarget(repoRef, targetId);
    }
}
