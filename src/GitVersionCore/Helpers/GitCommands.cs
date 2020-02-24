using System;
using System.Collections.Generic;
using GitVersion.Models.Abstractions;
using GitVersion.Models.LibGitSharpWrappers;
using LibGit2Sharp;

namespace GitVersion.Helpers
{
    public static class GitCommands
    {
        public static void Checkout(IGitRepository repo, IGitBranch branch)
        {
            if (!(repo is LibGitRepository libGitRepository))
            {
                throw new ArgumentException("Unexpected repository type: " + repo.GetType(), nameof(repo));
            }
            if (!(branch is LibGitBranch libGitBranch))
            {
                throw new ArgumentException("Unexpected branch type: " + branch.GetType(), nameof(branch));
            }

            Commands.Checkout(libGitRepository.Wrapped, libGitBranch.Wrapped);
        }

        public static void Fetch(IGitRepository repo, string remote, IEnumerable<string> refSpecs, FetchOptions options, string logMessage)
        {
            if (!(repo is LibGitRepository libGitRepository))
            {
                throw new ArgumentException("Unexpected repository type: " + repo.GetType(), nameof(repo));
            }

            if (!(libGitRepository.Wrapped is Repository wrapped))
            {
                throw new ArgumentException("Unexpected repository type: " + libGitRepository.Wrapped.GetType(), nameof(libGitRepository.Wrapped));
            }

            Commands.Fetch(wrapped, remote, refSpecs, options, logMessage);
        }

        public static void Checkout(IGitRepository repo, string committishOrBranchSpec)
        {
            if (!(repo is LibGitRepository libGitRepository))
            {
                throw new ArgumentException("Unexpected repository type: " + repo.GetType(), nameof(repo));
            }

            Commands.Checkout(libGitRepository.Wrapped, committishOrBranchSpec);
        }
    }
}
