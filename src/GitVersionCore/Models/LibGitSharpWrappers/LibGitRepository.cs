using System;
using System.Collections.Generic;
using System.Linq;
using GitVersion.Models.Abstractions;
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

        public IEnumerable<IGitTag> Tags => Log(nameof(Tags), Wrapped.Tags.Select(t => new LibGitTag(t)));
        public IQueryableGitCommitLog Commits => Log(nameof(Commits), new LibGitCommitLog(Wrapped.Commits));
        public IGitBranch Head => Log(nameof(Head), new LibGitBranch(Wrapped.Head));
        public IGitBranchCollection Branches => Log(nameof(Branches), new LibGitBranchCollection(Wrapped.Branches));
        public IGitRepositoryInformation Info => Log(nameof(Info), new LibGitRepositoryInformation(Wrapped.Info));
        public IGitNetwork Network => Log(nameof(Network), new LibGitNetwork(Wrapped.Network));
        public IGitObjectDatabase ObjectDatabase => Log(nameof(ObjectDatabase), new LibGitObjectDatabase(Wrapped.ObjectDatabase));
        public IGitReferenceCollection Refs => Log(nameof(Refs), new LibGitReferenceCollection(Wrapped.Refs));
        public void Dispose() => Wrapped.Dispose();

        private T Log<T>(string name, T value)
        {
            Stats.Called(GetType().Name, name);
            Stats.Called(GetType().Name, name, value);

            return value;
        }
    }
}
