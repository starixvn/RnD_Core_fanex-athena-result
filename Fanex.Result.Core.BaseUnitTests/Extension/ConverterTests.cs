namespace Fanex.Result.Core.Base.Tests.Extension
{
    using System;
    using Base.Extension;
    using NUnit.Framework;

    [TestFixture]
    public class ConverterTests
    {
        [TestCase("None123")]
        [TestCase("Hello")]
        public void To_FromInvalidObjectWithReturnTypeIsBool_ThrowsException(object item)
        {
            // Act
            TestDelegate testDelegate = () => Converter.To<bool>(item);

            // Assert
            Assert.Throws<FormatException>(testDelegate);
        }

        [TestCase(true, true)]
        [TestCase(false, false)]
        [TestCase(-123, true)]
        [TestCase(0, false)]
        [TestCase(0000, false)]
        [TestCase(1, true)]
        [TestCase(01, true)]
        [TestCase(12345, true)]
        [TestCase("false", false)]
        [TestCase("true", true)]
        public void To_FromValidObjectWithReturnTypeIsBool_ReturnsExpectedResult(object item, bool expectedResult)
        {
            // Act
            var convertedItem = Converter.To<bool>(item);

            // Assert
            Assert.AreEqual(expectedResult, convertedItem);
        }

        [Test]
        public void To_FromNullObjectWithReturnTypeIsBool_ReturnsFalse()
        {
            // Act
            var convertedItem = Converter.To<bool>(null);

            // Assert
            Assert.IsFalse(convertedItem);
        }

        [TestCase(true, "True")]
        [TestCase(false, "False")]
        [TestCase(-123, "-123")]
        [TestCase(0, "0")]
        [TestCase(012345, "12345")]
        [TestCase(012.34, "12.34")]
        [TestCase("This is a test", "This is a test")]
        public void To_FromValidObjectWithReturnTypeIsString_ReturnsExpectedResult(object item, string expectedResult)
        {
            // Act
            var convertedItem = Converter.To<string>(item);

            // Assert
            Assert.AreEqual(expectedResult, convertedItem);
        }

        [Test]
        public void To_FromNullObjectWithReturnTypeIsString_ReturnsNull()
        {
            // Act
            var convertedItem = Converter.To<string>(null);

            // Assert
            Assert.IsNull(convertedItem);
        }

        [TestCase(true, 1)]
        [TestCase(false, 0)]
        [TestCase("123", 123)]
        [TestCase("00123", 123)]
        [TestCase(-123.34, -123)]
        public void To_FromValidObjectWithReturnTypeIsInt_ReturnsExpectedResult(object item, int expectedResult)
        {
            // Act
            var convertedItem = Converter.To<int>(item);

            // Assert
            Assert.AreEqual(expectedResult, convertedItem);
        }

        [Test]
        public void To_FromNullObjectWithReturnTypeIsInt_ReturnsZero()
        {
            // Act
            var convertedItem = Converter.To<int>(null);

            // Assert
            Assert.AreEqual(0, convertedItem);
        }

        [TestCase(true, 1)]
        [TestCase(false, 0)]
        [TestCase("001.23", 1.23)]
        [TestCase("-123", -123)]
        [TestCase(-123.34, -123.34)]
        public void To_FromValidObjectWithReturnTypeIsDecimal_ReturnsExpectedResult(object item, decimal expectedResult)
        {
            // Act
            var convertedItem = Converter.To<decimal>(item);

            // Assert
            Assert.AreEqual(expectedResult, convertedItem);
        }

        [Test]
        public void To_FromNullObjectWithReturnTypeIsDecimal_ReturnsZero()
        {
            // Act
            var convertedItem = Converter.To<decimal>(null);

            // Assert
            Assert.AreEqual(0, convertedItem);
        }

        [Test]
        public void ToBool_ValueIsNull_ReturnDefaultValue()
        {
            // Act
            var actualValue = Converter.ToBool(null, false);

            // Assert
            Assert.False(actualValue);
        }

        [Test]
        public void ToBool_ValueIsDBNull_ReturnFalse()
        {
            // Act
            var actualValue = Converter.ToBool(DBNull.Value, true);

            // Assert
            Assert.True(actualValue);
        }

        [Test]
        public void ToBool_ValueIsNotNullAndWrongFormat_ReturnDefaultValue()
        {
            // Act
            var actualValue = Converter.ToBool(1, false);

            // Assert
            Assert.False(actualValue);
        }

        [Test]
        public void ToBool_ValueIsNotNullAndRightFormat_ReturnValue()
        {
            // Act
            var actualValue = Converter.ToBool(true, false);

            // Assert
            Assert.True(actualValue);
        }

        [Test]
        public void ToByteArray_CorrectFormat_ReturnByteArray()
        {
            // Arrange
            var item = new byte[] { 1, 2 };

            // Act
            var actualValue = Converter.ToByteArray(item);

            // Assert
            Assert.AreEqual(item, actualValue);
        }

        [Test]
        public void ToByteArray_WrongFormat_ReturnByteArray()
        {
            // Arrange
            var item = new int[] { 1, 2 };

            // Act
            TestDelegate testDelegate = () => Converter.ToByteArray(item);

            // Assert
            Assert.Throws<InvalidCastException>(testDelegate);
        }
    }
}