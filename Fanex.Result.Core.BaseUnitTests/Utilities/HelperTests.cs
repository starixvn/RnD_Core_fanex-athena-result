namespace Fanex.Result.Core.Base.Tests.Utilities
{
    using System;
    using Base.Utilities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class HelperTests
    {
        [Test]
        public void FullDateFormat_FromNullFormatDateTime_ReturnsEmpty()
        {
            // Act
            var formattedDateTime = Helper.FullDateFormat(null, Arg.Any<string>());

            // Assert
            Assert.AreEqual(string.Empty, formattedDateTime);
        }

        [Test]
        public void FullDateFormat_FromDBNullFormatDateTime_ReturnsEmpty()
        {
            // Act
            var formattedDateTime = Helper.FullDateFormat(Convert.DBNull, Arg.Any<string>());

            // Assert
            Assert.AreEqual(string.Empty, formattedDateTime);
        }

        [TestCase("{0:MM/dd/yyyy<br/>hh:mm:ss tt}", "10/01/2016<br/>01:01:10 AM")]
        [TestCase("{0:yyyy-MMM-dd hh:mm:ss.fff tt}", "2016-Oct-01 01:01:10.900 AM")]
        public void FullDateFormat_FromFormatPattern_ReturnsFormattedDateTime(string formatPattern, string expectedFormattedDateTime)
        {
            // Arrange
            var dateTime = new DateTime(2016, 10, 01, 01, 01, 10, 900);

            // Act
            var formattedDateTime = Helper.FullDateFormat(dateTime, formatPattern);

            // Assert
            Assert.AreEqual(expectedFormattedDateTime, formattedDateTime);
        }

        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase("<div> </div>", "<div></div>")]
        [TestCase("<div>      </div>", "<div></div>")]
        [TestCase(" <div></div> ", "<div></div>")]
        [TestCase("        <div>      </div>", "<div></div>")]
        public void RemoveWhiteSpace_FromHtml_ReturnsExpectedRemovedWSHtml(string html, string expectedRemovedWSHtml)
        {
            // Act
            var removedWSHtml = Helper.RemoveWhiteSpace(html);

            // Assert
            Assert.AreEqual(expectedRemovedWSHtml, removedWSHtml);
        }

        [TestCase(1.001, 3, "1.001")]
        [TestCase(1.001, 2, "1.00")]
        [TestCase(1.001, 5, "1.00100")]
        [TestCase(1, 0, "1")]
        public void DecFormat_FromValueAndDecDigits_ReturnsFormattedDecimal(decimal value, byte decDigits, string expectedFormattedDecimal)
        {
            // Act
            var formattedDec = Helper.DecFormat(value, decDigits);

            // Assert
            Assert.AreEqual(expectedFormattedDecimal, formattedDec);
        }

        [TestCase(0, "00")]
        [TestCase(5, "05")]
        [TestCase(51, "51")]
        [TestCase(151, "151")]
        public void FormatTwoDigit_FromNumber_ReturnsExpectedTwoDigitsNumber(int number, string expectedNumber)
        {
            // Act
            var twoDigitsNumber = Helper.FormatTwoDigit(number);

            // Assert
            Assert.AreEqual(expectedNumber, twoDigitsNumber);
        }

        [TestCase("Hello <br />Beautiful<br/> World<br/>  ", "Hello \nBeautiful\n World\n  ")]
        [TestCase("Hello <b>Beautiful</b> World", "Hello Beautiful World")]
        [TestCase("Hello <b>Beautiful<br/> World</b>", "Hello Beautiful\n World")]
        public void ToRtfString_FromHtml_ReturnsExpectedRTFString(string html, string expectedRtfString)
        {
            // Act
            var rtfString = Helper.ToRtfString(html);

            // Assert
            Assert.AreEqual(expectedRtfString, rtfString);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("abc")]
        ////[TestCase("123a")]
        ////[TestCase("1.23")]
        public void IsIntegerNumber_FromInvalidInputValue_ReturnsFalse(string inputValue)
        {
            // Act
            var isIntegerNumber = Helper.IsIntegerNumber(inputValue);

            // Assert
            Assert.IsFalse(isIntegerNumber);
        }

        [TestCase("123")]
        [TestCase("010")]
        public void IsIntegerNumber_FromValidInputValue_ReturnsTrue(string inputValue)
        {
            // Act
            var isIntegerNumber = Helper.IsIntegerNumber(inputValue);

            // Assert
            Assert.IsTrue(isIntegerNumber);
        }
    }
}