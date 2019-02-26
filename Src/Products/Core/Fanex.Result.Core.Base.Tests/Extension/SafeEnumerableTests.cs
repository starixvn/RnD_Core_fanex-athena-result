namespace Fanex.Result.Core.Base.Tests.Extension
{
    using System.Collections.Generic;
    using System.Threading;
    using Base.Extension;
    using NUnit.Framework;

    [TestFixture]
    public class SafeEnumerableTests
    {
        private SafeEnumerable<int> _safeEnumerable;
        private object _lock;

        [SetUp]
        public void SetUp()
        {
            var list = new List<int> { 4, 3, 2, 1 };
            _lock = new object();
            _safeEnumerable = new SafeEnumerable<int>(list, _lock);
        }

        [Test]
        public void GetEnumerator_Always_ReturnsSafeEnumeratorType()
        {
            // Act
            var safeEnumerator = _safeEnumerable.GetEnumerator();

            // Assert
            var expectedType = "SafeEnumerator`1";
            Assert.AreEqual(expectedType, safeEnumerator.GetType().Name);
        }

        [Test]
        public void GetEnumerator_Always_EnterdLock()
        {
            // Act
            _safeEnumerable.GetEnumerator();

            // Assert
            Assert.IsTrue(Monitor.IsEntered(_lock));
        }
    }
}