using System;
using System.Collections.Generic;
using System.Diagnostics;
using GitVersion.Models;
using GitVersion.Models.LibGitSharpWrappers;
using LibGit2Sharp;

namespace GitVersionCore.Tests.Mocks
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class MockCommit : IGitCommit
    {
        private static int commitCount = 1;
        private static DateTimeOffset when = DateTimeOffset.Now;

        object IGitObject.Wrapped => this;

        public MockCommit(IGitObjectId id = null)
        {
            idEx = id ?? new LibGitObjectId(Guid.NewGuid().ToString().Replace("-", "") + "00000000");
            MessageEx = "Commit " + commitCount++;
            ParentsEx = new List<IGitCommit> { null };
            CommitterEx = new LibGitSignature("Joe", "Joe@bloggs.net", when);
            // Make sure each commit is a different time
            when = when.AddSeconds(1);
        }

        public string MessageEx;
        public string Message => MessageEx;

        public IGitSignature CommitterEx;
        public IGitSignature Committer => CommitterEx;

        private readonly IGitObjectId idEx;
        public IGitObjectId Id => idEx;

        public string Sha => idEx.Sha;

        public IList<IGitCommit> ParentsEx;
        public IEnumerable<IGitCommit> Parents => ParentsEx;

        // ReSharper disable once UnusedMember.Local
        private string DebuggerDisplay => MessageEx;
    }
}
