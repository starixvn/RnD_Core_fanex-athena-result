namespace Fanex.Result.Core.Base.Tests.Extension
{
    using System;
    using Base.Extension;
    using NUnit.Framework;

    [TestFixture]
    public class SafeListTests
    {
        private SafeList<int> _safeList;

        [SetUp]
        public void SetUp()
        {
            _safeList = new SafeList<int>();
            _safeList.Add(1);
            _safeList.Add(2);
            _safeList.Add(3);
        }

        [TestCase(0, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        public void Index_FromIndex_ReturnsValue(int index, int expectedValue)
        {
            // Act
            var value = _safeList[index];

            // Assert
            Assert.AreEqual(expectedValue, value);
        }

        [Test]
        public void Index_FromSetByIndex_ReturnsEqualSetValue()
        {
            // Arrange
            var index = 0;
            var value = 10;

            // Act
            _safeList[index] = value;

            // Assert
            Assert.AreEqual(10, _safeList[index]);
        }

        [Test]
        public void GetEnumerator_Always_ReturnsSafeEnumerator()
        {
            // Arrange
            _safeList = new SafeList<int>();

            // Act
            var enumerator = _safeList.GetEnumerator();

            // Assert
            var expectedType = "SafeEnumerator`1";
            Assert.AreEqual(expectedType, enumerator.GetType().Name);
        }

        [Test]
        public void Add_FromValue_ReturnsCount()
        {
            // Act
            _safeList.Add(10);

            // Assert
            Assert.AreEqual(4, _safeList.Count);
        }

        [Test]
        public void Clear_Always_ReturnsCountIsZero()
        {
            // Act
            _safeList.Clear();

            // Assert
            Assert.AreEqual(0, _safeList.Count);
        }

        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(10, false)]
        public void Contains_FromValue_ReturnsExpectedResult(int value, bool expectedResult)
        {
            // Act
            var isConstains = _safeList.Contains(value);

            // Assert
            Assert.AreEqual(expectedResult, isConstains);
        }

        [TestCase(0, 3)]
        [TestCase(1, 4)]
        public void CopyTo_FromStartIndex_ReturnsExpectedCount(int startIndex, int expectedCount)
        {
            // Arrange
            int[] copiedArray = new int[expectedCount];

            // Act
            _safeList.CopyTo(copiedArray, startIndex);

            // Assert
            Assert.AreEqual(expectedCount, copiedArray.Length);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void Remove_FromItemValue_ReturnsDoesNotContainsValue(int value)
        {
            // Act
            _safeList.Remove(value);

            // Assert
            Assert.IsFalse(_safeList.Contains(value));
        }

        [TestCase(1, 0)]
        [TestCase(2, 1)]
        [TestCase(-10, -1)]
        public void IndexOf_FromValue_ReturnsExpectedIndex(int value, int expectedIndex)
        {
            // Act
            var index = _safeList.IndexOf(value);

            // Assert
            Assert.AreEqual(expectedIndex, index);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void Insert_FromIndex_ReturnsValueOfIndex(int index)
        {
            // Arrange
            var value = 2;

            // Act
            _safeList.Insert(index, value);

            // Assert
            Assert.AreEqual(value, _safeList[index]);
        }

        [TestCase(1, 2)]
        [TestCase(2, 2)]
        public void RemoveAt_FromIndex_ReturnsExpectedCount(int index, int expectedCount)
        {
            // Act
            _safeList.RemoveAt(index);

            // Assert
            Assert.AreEqual(expectedCount, _safeList.Count);
        }

        [Test]
        public void AsReadOnly_Always_ReturnsCountEqualWithSafeListCount()
        {
            // Act
            var readOnlyList = _safeList.AsReadOnly();

            // Assert
            Assert.AreEqual(_safeList.Count, readOnlyList.Count);
        }

        [Test]
        public void Foreach_FromNullActionDelegate_ReturnsDoNothing()
        {
            // Arrange
            var value = 0;
            Action<int> sumAction = null;

            // Act
            _safeList.ForEach(sumAction);

            // Assert
            Assert.AreEqual(0, value);
        }

        [Test]
        public void Foreach_FromSumActionDelegate_ReturnsSumOfElement()
        {
            // Arrange
            var value = 0;
            Action<int> sumAction = (int item) => value += item;

            // Act
            _safeList.ForEach(sumAction);

            // Assert
            Assert.AreEqual(6, value);
        }

        [Test]
        public void Exists_FromNullMatchDelegate_ReturnsFalse()
        {
            // Act
            var isExists = _safeList.Exists(null);

            // Assert
            Assert.IsFalse(isExists);
        }

        [Test]
        public void Exists_FromRightMatchDelegate_ReturnsTrue()
        {
            // Act
            var isExists = _safeList.Exists((int item) => { return true; });

            // Assert
            Assert.IsTrue(isExists);
        }

        [Test]
        public void Exists_FromWrongMatchDelegate_ReturnsTrue()
        {
            // Act
            var isExists = _safeList.Exists((int item) => { return false; });

            // Assert
            Assert.IsFalse(isExists);
        }
    }
}