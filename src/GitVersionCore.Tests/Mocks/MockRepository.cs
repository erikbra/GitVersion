using System;
using System.Collections.Generic;
using GitVersion.Models;
using LibGit2Sharp;

using Index = LibGit2Sharp.Index;
namespace GitVersionCore.Tests.Mocks
{
    public class MockRepository : IGitRepository
    {
        private IQueryableGitCommitLog commits;

        public MockRepository()
        {
            Tags = new MockTagCollection();
            Refs = new MockReferenceCollection();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Branch Checkout(Branch branch, CheckoutOptions options, Signature signature = null)
        {
            throw new NotImplementedException();
        }

        public Branch Checkout(string committishOrBranchSpec, CheckoutOptions options, Signature signature = null)
        {
            throw new NotImplementedException();
        }

        public Branch Checkout(IGitCommit IGitCommit, CheckoutOptions options, Signature signature = null)
        {
            throw new NotImplementedException();
        }

        public void CheckoutPaths(string committishOrBranchSpec, IEnumerable<string> paths, CheckoutOptions checkoutOptions = null)
        {
            throw new NotImplementedException();
        }

        public MergeResult MergeFetchedRefs(Signature merger, MergeOptions options)
        {
            throw new NotImplementedException();
        }

        public CherryPickResult CherryPick(IGitCommit IGitCommit, Signature committer, CherryPickOptions options = null)
        {
            throw new NotImplementedException();
        }

        public GitObject Lookup(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public GitObject Lookup(string objectish)
        {
            throw new NotImplementedException();
        }

        public GitObject Lookup(ObjectId id, ObjectType type)
        {
            throw new NotImplementedException();
        }

        public IGitObject Lookup(string objectish, ObjectType type)
        {
            return new MockCommit();
        }

        public IGitCommit IGitCommit(string message, Signature author, Signature committer, CommitOptions options = null)
        {
            throw new NotImplementedException();
        }

        public void Reset(ResetMode resetMode, IGitCommit IGitCommit)
        {
            throw new NotImplementedException();
        }

        public void Reset(ResetMode resetMode, IGitCommit IGitCommit, CheckoutOptions options)
        {
            throw new NotImplementedException();
        }

        public IGitCommit IGitCommit(string message, Signature author, Signature committer, bool amendPreviousCommit = false)
        {
            throw new NotImplementedException();
        }

        public void Reset(ResetMode resetMode, IGitCommit IGitCommit, Signature signature = null, string logMessage = null)
        {
            throw new NotImplementedException();
        }

        public void Reset(IGitCommit IGitCommit, IEnumerable<string> paths = null, ExplicitPathsOptions explicitPathsOptions = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveUntrackedFiles()
        {
            throw new NotImplementedException();
        }

        public RevertResult Revert(IGitCommit IGitCommit, Signature reverter, RevertOptions options = null)
        {
            throw new NotImplementedException();
        }

        public MergeResult Merge(IGitCommit IGitCommit, Signature merger, MergeOptions options = null)
        {
            throw new NotImplementedException();
        }

        public MergeResult Merge(Branch branch, Signature merger, MergeOptions options = null)
        {
            throw new NotImplementedException();
        }

        public MergeResult Merge(string committish, Signature merger, MergeOptions options = null)
        {
            throw new NotImplementedException();
        }

        public BlameHunkCollection Blame(string path, BlameOptions options = null)
        {
            throw new NotImplementedException();
        }

        public void Stage(string path, StageOptions stageOptions)
        {
            throw new NotImplementedException();
        }

        public void Stage(IEnumerable<string> paths, StageOptions stageOptions)
        {
            throw new NotImplementedException();
        }

        public void Unstage(string path, ExplicitPathsOptions explicitPathsOptions)
        {
            throw new NotImplementedException();
        }

        public void Unstage(IEnumerable<string> paths, ExplicitPathsOptions explicitPathsOptions)
        {
            throw new NotImplementedException();
        }

        public void Move(string sourcePath, string destinationPath)
        {
            throw new NotImplementedException();
        }

        public void Move(IEnumerable<string> sourcePaths, IEnumerable<string> destinationPaths)
        {
            throw new NotImplementedException();
        }

        public void Remove(string path, bool removeFromWorkingDirectory, ExplicitPathsOptions explicitPathsOptions)
        {
            throw new NotImplementedException();
        }

        public void Remove(IEnumerable<string> paths, bool removeFromWorkingDirectory, ExplicitPathsOptions explicitPathsOptions)
        {
            throw new NotImplementedException();
        }

        public FileStatus RetrieveStatus(string filePath)
        {
            throw new NotImplementedException();
        }

        public RepositoryStatus RetrieveStatus(StatusOptions options)
        {
            throw new NotImplementedException();
        }

        public string Describe(IGitCommit IGitCommit, DescribeOptions options)
        {
            throw new NotImplementedException();
        }

        public void Checkout(Tree tree, IEnumerable<string> paths, CheckoutOptions opts)
        {
            throw new NotImplementedException();
        }

        public void RevParse(string revision, out Reference reference, out GitObject obj)
        {
            throw new NotImplementedException();
        }

        public IGitBranch Head { get; set; }
        public LibGit2Sharp.Configuration Config { get; set; }
        public Index Index { get; set; }
        public IGitReferenceCollection Refs { get; set; }

        public IQueryableGitCommitLog Commits
        {
            get => commits ?? new MockQueryableCommitLog(Head.Commits);
            set => commits = value;
        }

        public IGitBranchCollection Branches { get; set; }
        public IEnumerable<IGitTag> Tags { get; set; }
        public IGitRepositoryInformation Info { get; set; }
        public Diff Diff { get; set; }
        public IGitObjectDatabase ObjectDatabase { get; set; }
        public NoteCollection Notes { get; set; }
        public SubmoduleCollection Submodules { get; set; }
        public WorktreeCollection Worktrees { get; set; }
        public Rebase Rebase { get; private set; }

        public Ignore Ignore => throw new NotImplementedException();

        public IGitNetwork Network { get; set; }

        public StashCollection Stashes => throw new NotImplementedException();
    }
}
