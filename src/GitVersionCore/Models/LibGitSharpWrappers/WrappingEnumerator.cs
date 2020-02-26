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
            Stats.Called(GetType().Name, "<ctor>");
        }

        public bool MoveNext() => Log(nameof(MoveNext), Wrapped.MoveNext());

        public void Reset()
        {
            Log(nameof(Reset));
            Wrapped.Reset();
        }

        public TWrapper Current => Log(nameof(Current), Wrap(Wrapped.Current));

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            Log(nameof(Dispose));
            Wrapped.Dispose();
        }

        private static TWrapper Wrap(TTOWrap item)
        {
            return (TWrapper) Activator.CreateInstance(typeof(TWrapper), item);
        }


        protected T Log<T>(string name, T value)
        {
            Stats.Called(GetType().Name, name);
            Stats.Called(GetType().Name, name, value);

            return value;
        }

        protected void Log(string name)
        {
            Stats.Called(GetType().Name, name);
        }

    }
}
