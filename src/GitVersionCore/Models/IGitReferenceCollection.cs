using System.Collections.Generic;

namespace GitVersion.Models
{
    public interface IGitReferenceCollection: IEnumerable<IGitReference>
    {
        IGitDirectReference Add(string localCanonicalName, IGitObjectId objectId, bool b);
        IGitDirectReference Add(string localCanonicalName, string repoTipId);
        IGitDirectReference Add(string name, IGitObjectId targetId);

        IGitReference Head { get; }
        IGitReferenceCollection FromGlob(string s);

        IGitReference this[string name] { get; }

        void UpdateTarget(IGitReference repoRef, string repoTipId);
        void UpdateTarget(IGitReference repoRef, IGitObjectId repoTipId);

        //ReflogCollection Log(string canonicalName)

    }
}
