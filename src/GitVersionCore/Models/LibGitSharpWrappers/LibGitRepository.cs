using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using lgs = LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitRepository: IGitRepository, IDisposable
    {
        public lgs.IRepository Wrapped { get; }

        public LibGitRepository(string dotGitDirectory)
        {
            Wrapped = new lgs.Repository(dotGitDirectory);
        }

        public LibGitRepository(lgs.IRepository repo)
        {
            Wrapped = repo;
        }

        public IEnumerable<IGitTag> Tags => Wrapped.Tags.Select(t => new LibGitTag(t));
        public IQueryableGitCommitLog Commits => new LibGitCommitLog(Wrapped.Commits);
        public IGitBranch Head => new LibGitBranch(Wrapped.Head);
        public IGitBranchCollection Branches => new LibGitBranchCollection(Wrapped.Branches);
        public IGitRepositoryInformation Info => new LibGitRepositoryInformation(Wrapped.Info);
        public IGitNetwork Network => new LibGitNetwork(Wrapped.Network);
        public IGitObjectDatabase ObjectDatabase => new LibGitObjectDatabase(Wrapped.ObjectDatabase);
        public IGitReferenceCollection Refs => new LibGitReferenceCollection(Wrapped.Refs);
        public void Dispose() => Wrapped.Dispose();
    }
}
