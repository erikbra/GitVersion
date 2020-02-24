using System;
using System.Collections;
using System.Collections.Generic;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class CachedWrappingEnumerator<TWrapper, TTOWrap> : IEnumerator<TWrapper>
    {
        public IEnumerator<TTOWrap> Wrapped { get; set; }
        //public IEnumerator<TTOWrap> Wrapped => WrappedObject.GetEnumerator();

        private int _current = -1;
        private IList<TWrapper> _items = new List<TWrapper>();

        // TODO: Add cache here? Or in the repo?
        protected CachedWrappingEnumerator(IEnumerable<TTOWrap> toWrap)
        {
            //WrappedObject = toWrap;
            Wrapped = toWrap.GetEnumerator();
            Reset();
        }

        public IEnumerable<TTOWrap> WrappedObject { get; set; }

        public bool MoveNext()
        {
            var moveNext = Wrapped.MoveNext();
            if (moveNext)
            {
                _current++;
                //var wrapped = GetWrappedCurrent();
                //_items.Add(wrapped);
            }
            return moveNext;
        }

        public void Reset()
        {
            _current = -1;
            Wrapped.Reset();
        }

        public TWrapper Current
        {
            get
            {
                if (_items.Count <= _current)
                {
                    var wrapped = GetWrappedCurrent();
                    _items.Add(wrapped);
                    return wrapped;
                }
                return _items[_current];
            }
        }

        private TWrapper GetWrappedCurrent()
        {
            return (TWrapper) Activator.CreateInstance(typeof(TWrapper), Wrapped.Current);
        }

        object IEnumerator.Current => Current;
        public void Dispose() => Wrapped.Dispose();
    }
}
