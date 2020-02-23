using System;
using System.Collections.Generic;
using lgs = LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitRepository: IGitRepository, IDisposable
    {
        private lgs.IRepository _libGitSharpRepo;

        public LibGitRepository(string dotGitDirectory)
        {
            _libGitSharpRepo = new lgs.Repository(dotGitDirectory);
        }

        public LibGitRepository(lgs.IRepository repo)
        {
            _libGitSharpRepo = repo;
        }

        public IEnumerable<IGitTag> Tags { get; set; }
        public IQueryableGitCommitLog Commits { get; set; }
        public IGitBranch Head { get; set; }
        public IGitBranchCollection Branches { get; set; }
        public IGitRepositoryInformation Info { get; }
        public IGitNetwork Network { get; }
        public IGitObjectDatabase ObjectDatabase { get; }
        public IGitReferenceCollection Refs { get; }

        public void Dispose()
        {
            _libGitSharpRepo.Dispose();
        }
    }
}
