using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitBranchCollection: IGitBranchCollection
    {
        private IEnumerator<IGitBranch> _enumerator;

        public LibGitBranchCollection(BranchCollection wrapped)
        {
            Wrapped = wrapped;
            _enumerator = new LibGitBranchEnumerator(Wrapped);
        }

        private BranchCollection Wrapped { get; }

        public IEnumerator<IGitBranch> GetEnumerator() => _enumerator;
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IGitBranch this[string name] => new LibGitBranch(Wrapped[name]);

        public IGitBranch Update(IGitBranch branch, params Action<IGitBranchUpdater>[] actions)
        {
            if (!(branch is LibGitBranch libGitBranch))
            {
                throw new ArgumentException(nameof(branch));
            }

            var wrappingActions = actions.Select <Action<IGitBranchUpdater>, Action<BranchUpdater>>(action => updater => { action(new LibGitBranchUpdater(updater)); });

            return new LibGitBranch(Wrapped.Update(libGitBranch.Wrapped, wrappingActions.ToArray()));
        }
    }
}
