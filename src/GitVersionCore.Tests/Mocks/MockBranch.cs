using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GitVersion.Models;
using GitVersion.Models.LibGitSharpWrappers;
using LibGit2Sharp;

namespace GitVersionCore.Tests.Mocks
{
    public class MockBranch : IGitBranch, ICollection<IGitCommit>
    {
        object IGitObject.Wrapped => this;

        public MockBranch(string friendlyName)
        {
            this.friendlyName = friendlyName;
            CanonicalName = friendlyName;
        }

        public MockBranch(string friendlyName, string canonicalName)
        {
            this.friendlyName = friendlyName;
            this.CanonicalName = canonicalName;
        }

        public MockBranch()
        {

        }

        private readonly MockCommitLog commits = new MockCommitLog();
        private readonly string friendlyName;
        public string FriendlyName => friendlyName;
        public bool IsRemote { get; }

        IGitCommitLog IGitBranch.Commits => commits;

        public string NameWithoutRemote() => this.FriendlyName;
        public bool IsDetachedHead() => false;
        public bool IsSameBranch(IGitBranch argBranch) => false;

        public IGitCommitLog Commits => commits;
        public IGitCommit Tip => commits.First();
        public bool IsTracking => true;

        public string CanonicalName { get; }

        public override int GetHashCode()
        {
            return friendlyName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }

        public IEnumerator<IGitCommit> GetEnumerator()
        {
            return commits.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IGitCommit item)
        {
            commits.Add(item);
        }

        public void Clear()
        {
            commits.Clear();
        }

        public bool Contains(IGitCommit item)
        {
            return commits.Contains(item);
        }

        public void CopyTo(IGitCommit[] array, int arrayIndex)
        {
            commits.CopyTo(array, arrayIndex);
        }

        public bool Remove(IGitCommit item)
        {
            return commits.Remove(item);
        }

        public int Count => commits.Count;

        public bool IsReadOnly => false;
        public string Sha { get; }
    }
}
