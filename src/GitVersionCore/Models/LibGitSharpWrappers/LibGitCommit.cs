using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using GitVersion.Models.Abstractions;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class LibGitCommit : IGitCommit, IEquatable<LibGitCommit>
    {
        public Commit Wrapped { get; }
        object IGitObject.Wrapped => Wrapped;

        public LibGitCommit(Commit commit)
        {
            Wrapped = commit;
        }

        public string Sha => Wrapped?.Sha;
        public IGitObjectId Id => new LibGitObjectId(Wrapped.Id);
        public IEnumerable<IGitCommit> Parents => Wrapped.Parents.Select(p => new LibGitCommit(p));
        public IGitSignature Committer => new LibGitSignature(Wrapped.Committer);
        public string Message => Wrapped.Message;

        public override bool Equals(object obj)
        {
            return obj switch
            {
                LibGitCommit other => Equals(other),
                _ => false
            };
        }

        public override string ToString() => GetType().Name + " :: " + Id.ToString(7);

        public bool Equals(LibGitCommit other)
        {
            return Equals(Wrapped, other?.Wrapped);
        }

        public override int GetHashCode()
        {
            return (Wrapped != null ? Wrapped.GetHashCode() : 0);
        }

        private Lazy<string> MessageShort => new Lazy<string>(() => Wrapped.MessageShort);

        private string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "{0} {1}",
                    Id.ToString(7),
                    MessageShort.Value);
            }
        }
    }
}
