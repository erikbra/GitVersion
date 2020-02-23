using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitCommit : IGitCommit
    {
        public Commit Wrapped { get; }
        object IGitObject.Wrapped => Wrapped;

        public LibGitCommit(Commit commit)
        {
            Wrapped = commit;
        }

        public string Sha => Wrapped?.Sha;
        public IGitObjectId Id => new LibGitObjectId(Wrapped.Id);
        public IEnumerable<IGitCommit> Parents => Wrapped?.Parents.Select(p => new LibGitCommit(p));
        public IGitSignature Committer => new LibGitSignature(Wrapped.Committer);
        public string Message => Wrapped.Message;

        public override bool Equals(object obj)
        {
            return obj switch
            {
                LibGitCommit other => other.Wrapped.Equals(Wrapped),
                _ => false
            };
        }
    }
}
