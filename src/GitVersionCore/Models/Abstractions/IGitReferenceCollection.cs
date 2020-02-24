using System.Collections.Generic;

namespace GitVersion.Models.Abstractions
{
    public interface IGitReferenceCollection: IEnumerable<IGitReference>
    {
        IGitReference Add(string localCanonicalName, IGitObjectId objectId, bool allowOverwrite);
        IGitReference Add(string localCanonicalName, string repoTipId);
        IGitReference Add(string name, IGitObjectId targetId);

        IGitReference Head { get; }
        IEnumerable<IGitReference> FromGlob(string pattern);

        IGitReference this[string name] { get; }

        void UpdateTarget(IGitReference repoRef, string objectish);
        void UpdateTarget(IGitReference repoRef, IGitObjectId targetId);

        //ReflogCollection Log(string canonicalName)

    }
}
