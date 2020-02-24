using System;
using GitVersion.Models.Abstractions;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitSignature: IGitSignature
    {
        private Signature _wrapped;

        public LibGitSignature(string name, string email, DateTimeOffset when)
        {
            _wrapped = new Signature(name, email, when);
        }

        public LibGitSignature(Signature signature)
        {
            _wrapped = signature;
        }

        public DateTimeOffset When => _wrapped.When;
    }
}
