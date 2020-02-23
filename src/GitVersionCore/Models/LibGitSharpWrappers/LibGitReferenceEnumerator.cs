using System.Collections;
using System.Collections.Generic;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitReferenceEnumerator : IEnumerator<IGitReference>
    {
        public IEnumerator<LibGit2Sharp.Reference> Wrapped { get; }

        public LibGitReferenceEnumerator(IEnumerator<LibGit2Sharp.Reference> wrapped)
        {
            Wrapped = wrapped;
        }

        public bool MoveNext() => Wrapped.MoveNext();
        public void Reset() => Wrapped.Reset();
        public IGitReference Current => new LibGitDirectReference(Wrapped.Current);
        object IEnumerator.Current => Current;
        public void Dispose() => Wrapped.Dispose();
    }
}
