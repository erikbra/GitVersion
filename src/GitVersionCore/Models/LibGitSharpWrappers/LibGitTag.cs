using System;
using GitVersion.Models.Abstractions;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitTag: IGitTag
    {
        public LibGitTag(Tag wrapped)
        {
            Wrapped = wrapped;
        }

        private Tag Wrapped { get; }

        public string FriendlyName => Wrapped.FriendlyName;

        public IGitObject Target => Wrapped.Target switch
        {
            Commit c => new LibGitCommit(c),
            TagAnnotation annotation => new LibGitTagAnnotation(annotation),
            _ => throw new ArgumentException("Unexpected target type: " + Wrapped?.Target?.GetType())
        };

        public IGitTagAnnotation Annotation => new LibGitTagAnnotation(Wrapped.Annotation);
    }
}
