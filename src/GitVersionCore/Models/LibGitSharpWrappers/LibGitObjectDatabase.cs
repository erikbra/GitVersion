using System;
using GitVersion.Models.Abstractions;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitObjectDatabase : IGitObjectDatabase
    {
        public LibGit2Sharp.ObjectDatabase Wrapped { get; }

        public LibGitObjectDatabase(LibGit2Sharp.ObjectDatabase wrapped)
        {
            Wrapped = wrapped;
        }

        public IGitCommit FindMergeBase(IGitCommit commit, IGitCommit commitToFindCommonBase)
        {
            if (!(commit is LibGitCommit firstCommit))
            {
                throw new ArgumentException(nameof(commit));
            }

            if (!(commitToFindCommonBase is LibGitCommit secondCommit))
            {
                throw new ArgumentException(nameof(commitToFindCommonBase));
            }

            return new LibGitCommit(Wrapped.FindMergeBase(firstCommit.Wrapped, secondCommit.Wrapped));
        }

        public string ShortenObjectId(IGitCommit commit)
        {
            if (!(commit is LibGitCommit lgc))
            {
                throw new ArgumentException(nameof(commit));
            }

            return Wrapped.ShortenObjectId(lgc.Wrapped);
        }
    }
}
