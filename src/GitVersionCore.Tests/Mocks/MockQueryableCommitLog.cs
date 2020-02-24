using System;
using System.Collections;
using System.Collections.Generic;
using GitVersion.Models.Abstractions;
using LibGit2Sharp;

namespace GitVersionCore.Tests.Mocks
{
    public class MockQueryableCommitLog : IQueryableGitCommitLog
    {
        private readonly IGitCommitLog commits;

        public MockQueryableCommitLog(IGitCommitLog commits)
        {
            this.commits = commits;
        }

        public IEnumerator<IGitCommit> GetEnumerator()
        {
            return commits.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public GitCommitSortStrategies SortedBy => throw new NotImplementedException();

        public IGitCommitLog QueryBy(GitCommitFilter filter)
        {
            return this;
        }

        public IEnumerable<IGitLogEntry> QueryBy(string path)
        {
            throw new NotImplementedException();
        }

        public IGitCommit FindMergeBase(IGitCommit first, IGitCommit second)
        {
            return null;
        }

        public IGitCommit FindMergeBase(IEnumerable<IGitCommit> commits, MergeBaseFindingStrategy strategy)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGitLogEntry> QueryBy(string path, GitCommitFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
