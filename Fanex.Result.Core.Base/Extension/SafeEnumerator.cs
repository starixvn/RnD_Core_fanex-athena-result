namespace Fanex.Result.Core.Base.Extension
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;

    internal class SafeEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        private readonly object _lock;

        public SafeEnumerator(IEnumerator<T> inner, object @lock)
        {
            _inner = inner;
            _lock = @lock;

            Monitor.Enter(_lock);
        }

        public T Current
        {
            get { return _inner.Current; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public bool MoveNext()
        {
            return _inner.MoveNext();
        }

        public void Reset()
        {
            _inner.Reset();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Monitor.Exit(_lock);
            }
        }
    }
}