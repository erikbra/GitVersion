using GitVersion.Models;

namespace GitVersionCore.Tests.Mocks
{
    public class MockMergeCommit : MockCommit
    {
        public MockMergeCommit(IGitObjectId id = null) : base(id)
        {
            ParentsEx.Add(null);
        }
    }
}
