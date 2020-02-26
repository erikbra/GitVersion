using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GitVersion.Models.Abstractions;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitReferenceCollection : IGitReferenceCollection
    {
        private ReferenceCollection Wrapped { get; }

        public LibGitReferenceCollection(ReferenceCollection wrapped)
        {
            Wrapped = wrapped;
        }

        public IEnumerator<IGitReference> GetEnumerator() => Log(nameof(GetEnumerator), new LibGitReferenceEnumerator(Wrapped.GetEnumerator()));
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IGitReference Add(string localCanonicalName, IGitObjectId objectId, bool allowOverwrite)
        {
            return Wrap(Wrapped.Add(localCanonicalName, LibGit(objectId).Wrapped, allowOverwrite));
        }

        public IGitReference Add(string localCanonicalName, string repoTipId)
        {
            return Wrap(Wrapped.Add(localCanonicalName, repoTipId));
        }

        public IGitReference Add(string name, IGitObjectId targetId)
        {
            return Wrap(Wrapped.Add(name, LibGit(targetId).Wrapped));
        }

        public IGitReference Head => new LibGitReference(Wrapped.Head);

        public IEnumerable<IGitReference> FromGlob(string pattern)
        {
            return Wrapped.FromGlob(pattern).Select(Wrap);
        }

        public IGitReference this[string name] => Wrap(Wrapped[name]);

        public void UpdateTarget(IGitReference repoRef, string objectish)
        {
            Wrapped.UpdateTarget(LibGit(repoRef).Wrapped, objectish);
        }

        public void UpdateTarget(IGitReference repoRef, IGitObjectId targetId)
        {
            Wrapped.UpdateTarget(LibGit(repoRef).Wrapped, LibGit(targetId).Wrapped);
        }

        private static LibGitObjectId LibGit(IGitObjectId objectId)
        {
            if (!(objectId is LibGitObjectId libGitObjectId))
            {
                throw new ArgumentException("Unknown reference type: " + objectId.GetType(), nameof(objectId));
            }
            return libGitObjectId;
        }

        private static LibGitReference LibGit(IGitReference reference)
        {
            if (!(reference is LibGitReference libGitReference))
            {
                throw new ArgumentException("Unknown reference type: " + reference.GetType(), nameof(reference));
            }
            return libGitReference;
        }

        private static LibGitReference Wrap(Reference reference) =>
            reference switch
            {
                Reference r => new LibGitReference(r),
                null => null
            };

        private T Log<T>(string name, T value)
        {
            Stats.Called(GetType().Name, name);
            Stats.Called(GetType().Name, name, value);

            return value;
        }

    }
}
