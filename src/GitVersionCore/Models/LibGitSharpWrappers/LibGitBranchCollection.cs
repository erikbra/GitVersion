using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GitVersion.Models.Abstractions;
using LibGit2Sharp;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitBranchCollection : IGitBranchCollection
    {
        public LibGitBranchCollection(BranchCollection wrapped)
        {
            Wrapped = wrapped;
        }

        private BranchCollection Wrapped { get; }

        public IEnumerator<IGitBranch> GetEnumerator() => new LibGitBranchEnumerator(Wrapped.GetEnumerator());
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IGitBranch this[string name] => Wrap(Wrapped[name]);

        public IGitBranch Update(IGitBranch branch, params Action<IGitBranchUpdater>[] actions)
        {
            if (!(branch is LibGitBranch libGitBranch))
            {
                throw new ArgumentException(nameof(branch));
            }
            var wrappingActions = actions.Select<Action<IGitBranchUpdater>, Action<BranchUpdater>>(action => updater => { action(new LibGitBranchUpdater(updater)); });

            return Wrap(Wrapped.Update(libGitBranch.Wrapped, wrappingActions.ToArray()));
        }

        private static LibGitBranch Wrap(Branch branch) =>
            branch switch
            {
                Branch b => new LibGitBranch(b),
                null => null
            };
    }
}
