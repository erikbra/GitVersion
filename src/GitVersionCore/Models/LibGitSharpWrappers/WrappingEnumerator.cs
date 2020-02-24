using System;
using System.Collections;
using System.Collections.Generic;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class WrappingEnumerator<TWrapper, TTOWrap> : IEnumerator<TWrapper>
    {
        public IEnumerator<TTOWrap> Wrapped { get; }

        // TODO: Add cache here? Or in the repo?
        protected WrappingEnumerator(IEnumerator<TTOWrap> toWrap)
        {
            Wrapped = toWrap;
        }

        public bool MoveNext() => Wrapped.MoveNext();

        public void Reset() => Wrapped.Reset();

        public TWrapper Current => Wrap(Wrapped.Current);

        private static TWrapper Wrap(TTOWrap item)
        {
            return (TWrapper) Activator.CreateInstance(typeof(TWrapper), item);
        }

        object IEnumerator.Current => Current;
        public void Dispose() => Wrapped.Dispose();
    }
}
