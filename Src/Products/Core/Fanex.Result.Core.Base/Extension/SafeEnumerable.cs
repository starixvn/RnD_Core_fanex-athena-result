namespace Fanex.Result.Core.Base.Extension
{
    using System.Collections;
    using System.Collections.Generic;

    public class SafeEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _inner;
        private readonly object _lock;

        public SafeEnumerable(IEnumerable<T> inner, object @lock)
        {
            _lock = @lock;
            _inner = inner;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SafeEnumerator<T>(_inner.GetEnumerator(), _lock);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}