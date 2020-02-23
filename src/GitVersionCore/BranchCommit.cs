using GitVersion.Models;
using LibGit2Sharp;

namespace GitVersion
{
    /// <summary>
    /// A Commit, together with the branch to which the Commit belongs.
    /// </summary>
    public struct BranchCommit
    {
        public static readonly BranchCommit Empty = new BranchCommit();

        public BranchCommit(IGitCommit commit, IGitBranch branch) : this()
        {
            Branch = branch;
            Commit = commit;
        }

        public IGitBranch Branch { get; }
        public IGitCommit Commit { get; }

        public bool Equals(BranchCommit other)
        {
            return Equals(Branch, other.Branch) && Equals(Commit, other.Commit);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is BranchCommit IGitCommit && Equals(IGitCommit);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Branch != null ? Branch.GetHashCode() : 0) * 397) ^ (Commit != null ? Commit.GetHashCode() : 0);
            }
        }

        public static bool operator ==(BranchCommit left, BranchCommit right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BranchCommit left, BranchCommit right)
        {
            return !left.Equals(right);
        }
    }
}
