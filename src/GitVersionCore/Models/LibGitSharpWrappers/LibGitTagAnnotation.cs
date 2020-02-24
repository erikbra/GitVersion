using System;
using GitVersion.Models.Abstractions;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitTagAnnotation : IGitTagAnnotation
    {
        public TagAnnotation Wrapped { get; }

        //public object Target => Wrapped.Target;
        public IGitObject Target => GetTarget();

        private IGitObject GetTarget()
        {
            return Wrapped.Target switch
            {
                Commit commit => new LibGitCommit(commit),
                TagAnnotation annotation => new LibGitTagAnnotation(annotation),
                _ => throw new ArgumentException("Unexpected target type: " + Wrapped?.Target?.GetType())
            };
        }

        public string Sha => Wrapped.Sha;

        object IGitObject.Wrapped => Wrapped;

        public LibGitTagAnnotation(TagAnnotation wrapped)
        {
            Wrapped = wrapped;
        }
    }
}
