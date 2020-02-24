using System;
using System.Collections.Generic;
using System.Linq;
using GitVersion.Models.Abstractions;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitRemote: IGitRemote
    {
        public LibGitRemote(LibGit2Sharp.Remote wrapped)
        {
            Wrapped = wrapped;
        }

        public LibGit2Sharp.Remote Wrapped { get;}
        public string Name => Wrapped.Name;
        public IEnumerable<IGitRefSpec> FetchRefSpecs => Wrapped.FetchRefSpecs.Select(r => new LibGitRefSpec(r));
        public Uri Url => new Uri(Wrapped.Url);
    }
}
