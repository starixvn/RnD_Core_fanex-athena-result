namespace Fanex.Result.Core.Base.Tests.Extension
{
    using System.Collections.Generic;
    using System.Threading;
    using Base.Extension;
    using NUnit.Framework;

    [TestFixture]
    public class SafeEnumeratorTests
    {
        private SafeEnumerable<int> _safeEnumerable;
        private IEnumerator<int> _safeEnumerator;
        private object _lock;

        [SetUp]
        public void SetUp()
        {
            var list = new List<int> { 4, 3, 2, 1 };
            _lock = new object();
            _safeEnumerable = new SafeEnumerable<int>(list, _lock);
            _safeEnumerator = _safeEnumerable.GetEnumerator();
        }

        [Test]
        public void Dispose_Always_ReleaseLock()
        {
            // Act
            _safeEnumerator.Dispose();

            // Assert
            Assert.IsFalse(Monitor.IsEntered(_lock));
        }

        [Test]
        public void MoveNext_Always_CurrentIsNextValue()
        {
            // Act
            _safeEnumerator.MoveNext();

            // Assert
            Assert.AreEqual(4, _safeEnumerator.Current);
        }

        [Test]
        public void Reset_Always_CurrentIsZero()
        {
            // Act
            _safeEnumerator.Reset();

            // Assert
            Assert.AreEqual(0, _safeEnumerator.Current);
        }
    }
}