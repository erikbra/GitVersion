using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitCommit : IGitCommit
    {
        private Commit _wrapped;

        public LibGitCommit(Commit commit)
        {
            _wrapped = commit;
        }

        public string Sha => _wrapped.Sha;
        public IGitObjectId Id => new LibGitObjectId(_wrapped.Id);
        public IEnumerable<IGitCommit> Parents => _wrapped.Parents.Select(p => new LibGitCommit(p));
        public IGitSignature Committer => new LibGitSignature(_wrapped.Committer);
        public string Message => _wrapped.Message;
    }
}
