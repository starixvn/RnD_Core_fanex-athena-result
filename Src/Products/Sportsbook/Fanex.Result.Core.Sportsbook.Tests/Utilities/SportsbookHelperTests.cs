namespace Fanex.Result.Core.Sportsbook.Tests.Utilities
{
    using NUnit.Framework;
    using Sportsbook.Exceptions;
    using Sportsbook.Utilities;

    [TestFixture]
    public class SportsbookHelperTests
    {
        [TestCase("80101")]
        [TestCase("80501")]
        [TestCase("80199")]
        [TestCase("80231")]
        public void IsSetXGameYBetID_FromBetIdIsSetXGameY_ReturnsTrue(string betId)
        {
            // Act
            var result = SportsbookHelper.IsSetXGameYBetID(betId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("8")]
        [TestCase("805011")]
        [TestCase("abcd123")]
        [TestCase("80622")]
        public void IsSetXGameYBetID_FromBetIdIsNotSetXGameY_ReturnsFalse(string betId)
        {
            // Act
            var result = SportsbookHelper.IsSetXGameYBetID(betId);

            // Assert
            Assert.IsFalse(result);
        }

        [TestCase(1, "0122")]
        [TestCase(2, "0221")]
        [TestCase(3, "0399")]
        [TestCase(4, "0400")]
        [TestCase(5, "0515")]
        public void IsTennisGameResourceID_FromSetIndexIsFrom1To5AndValidResourceId_ReturnsTrue(int setIndex, string resourceId)
        {
            // Act
            var result = SportsbookHelper.IsTennisGameResourceID(setIndex, resourceId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase(1, "01222")]
        [TestCase(1, "012")]
        [TestCase(2, "0121")]
        [TestCase(3, "0599")]
        [TestCase(4, "0300")]
        [TestCase(5, "0415")]
        public void IsTennisGameResourceID_FromSetIndexIsFrom1To5AndInvalidResourceId_ReturnsFalse(int setIndex, string resourceId)
        {
            // Act
            var result = SportsbookHelper.IsTennisGameResourceID(setIndex, resourceId);

            // Assert
            Assert.IsFalse(result);
        }

        [TestCase("^\\d$", null)]
        [TestCase("^\\d$", "")]
        [TestCase(null, "0121")]
        [TestCase("", "0121")]
        public void IsValidResourceId_FromNullOrEmptyOfPatternOrResult_ReturnsFalse(string pattern, string resourceId)
        {
            // Act
            var result = SportsbookHelper.IsValidResourceId(pattern, resourceId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidResourceId_FromPatternContainsCaretAndIsMatchResourceId_ReturnsTrue()
        {
            // Arrange
            const string pattern = "^0[0-9]+$";
            const string resourceId = "0123";

            // Act
            var result = SportsbookHelper.IsValidResourceId(pattern, resourceId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidResourceId_FromPatternContainsCaretAndIsNotMatchResourceId_ReturnsFalse()
        {
            // Arrange
            const string pattern = "^0\\d+$";
            const string resourceId = "123";

            // Act
            var result = SportsbookHelper.IsValidResourceId(pattern, resourceId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidResourceId_FromPatternNotContainsCaretAndIsEqualResourceId_ReturnsTrue()
        {
            // Arrange
            const string pattern = "0123";
            const string resourceId = "0123";

            // Act
            var result = SportsbookHelper.IsValidResourceId(pattern, resourceId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidResourceId_FromPatternNotContainsCaretAndIsNotEqualResourceId_ReturnsFalse()
        {
            // Arrange
            var pattern = "0123";
            var resourceId = "0123a";

            // Act
            var result = SportsbookHelper.IsValidResourceId(pattern, resourceId);

            // Assert
            Assert.IsFalse(result);
        }

        [TestCase("22101")]
        [TestCase("22201")]
        [TestCase("22301")]
        [TestCase("22401")]
        [TestCase("22501")]
        public void IsFastMarketsResourceId_FromFastMarketsResourceId_ReturnsTrue(string resourceId)
        {
            // Act
            var result = SportsbookHelper.IsFastMarketsResourceId(resourceId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsFastMarketsResourceId_FromInvalidFastMarketsResourceId_ReturnsFalse()
        {
            // Arrange
            var resourceId = "22102";

            // Act
            var result = SportsbookHelper.IsFastMarketsResourceId(resourceId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GetResourceLang_FromEmptyKeyName_ReturnsEmpty()
        {
            // Arrange
            var keyName = string.Empty;

            // Act
            var resource = SportsbookHelper.GetResourceLang(keyName);

            // Assert
            Assert.AreEqual(string.Empty, resource);
        }

        [TestCase("135", "Penalty Shootout Yes/No")]
        [TestCase("160", "FT - Goal {0}")]
        [TestCase("164", "ET - Goal {0}")]
        [TestCase("168", "Who advances to next round")]
        [TestCase("16001", "FT - Goal 1")]
        [TestCase("16002", "FT - Goal 2")]
        [TestCase("16405", "ET - Goal 5")]
        [TestCase("16402", "ET - Goal 2")]
        [TestCase("16901", "Goal 1")]
        public void GetResourceLang_FromKeyNameIsIntegerAndNotMatchSoccerRangeResourceIdRegex_ReturnsExpectedResource(
            string keyName, string expectedResource)
        {
            // Act
            var resource = SportsbookHelper.GetResourceLang(keyName);

            // Assert
            Assert.AreEqual(expectedResource, resource);
        }

        [TestCase("abc")]
        [TestCase("&df")]
        [TestCase("")]
        [TestCase("-10")]
        [TestCase("90")]
        [TestCase("100")]
        public void CreateFastMarketsTimeRange_FromStartTimeIsNotInteger_ThrowsException(string startTime)
        {
            // Act
            TestDelegate testDelegate = () => SportsbookHelper.CreateFastMarketsTimeRange(string.Empty, startTime);

            // Assert
            Assert.Throws<SportsbookException>(testDelegate);
        }

        [TestCase("22102")]
        [TestCase("25102")]
        [TestCase("")]
        public void CreateFastMarketsTimeRange_FromStartTimeIsInRangeMatchTimeAndResourceIdIsNotFastMarkets_ReturnsEmpty(string resourceId)
        {
            // Arrange
            var startTime = 12.ToString();

            // Act
            var interval = SportsbookHelper.CreateFastMarketsTimeRange(resourceId, startTime);

            // Assert
            Assert.IsEmpty(interval);
        }

        [TestCase("22101", "0", "00:00-00:59")]
        [TestCase("22201", "9", "09:00-13:59")]
        [TestCase("22301", "45", "45:00-45:59")]
        [TestCase("22401", "70", "70:00-74:59")]
        [TestCase("22501", "89", "89:00-89:59")]
        public void CreateFastMarketsTimeRange_StartTimeIsInRangeMatchTimeAndResourceIdIsFastMarkets_ReturnsExpectedInterval(
            string resourceId,
            string startTime,
            string expectedInterval)
        {
            // Act
            var interval = SportsbookHelper.CreateFastMarketsTimeRange(resourceId, startTime);

            // Assert
            Assert.AreEqual(expectedInterval, interval);
        }
    }
}