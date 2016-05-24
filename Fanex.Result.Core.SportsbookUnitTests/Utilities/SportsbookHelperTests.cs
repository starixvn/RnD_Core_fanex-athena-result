namespace Fanex.Result.Core.SportsbookUnitTests.Utilities
{
    using NUnit.Framework;
    using Sportsbook.Utilities;

    [TestFixture]
    public class SportsbookHelperTests
    {
        [TestCase("80101")]
        [TestCase("80501")]
        [TestCase("80199")]
        [TestCase("80231")]
        public void IsSetXGameYBetID_FromValidBetId_ReturnsTrue(string betId)
        {
            // Act
            var result = SportsbookHelper.IsSetXGameYBetID(betId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase("8")]
        [TestCase("805011")]
        [TestCase("abcd123")]
        [TestCase("80622")]
        public void IsSetXGameYBetID_FromInvalidBetId_ReturnsFalse(string betId)
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
        public void IsTennisGameResourceID_FromValidSetIndexAndValidResourceId(int setIndex, string resourceId)
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
        public void IsTennisGameResourceID_FromInvalidSetIndexAndInvalidResourceId(int setIndex, string resourceId)
        {
            // Act
            var result = SportsbookHelper.IsTennisGameResourceID(setIndex, resourceId);

            // Assert
            Assert.IsFalse(result);
        }

        [TestCase("^\\d$", "")]
        [TestCase("", "0121")]
        public void IsValidResourceId_FromNullOrEmptyOfPatternOrResult_ReturnsFalse(string pattern, string resourceId)
        {
            // Act
            var result = SportsbookHelper.IsValidResourceId(pattern, resourceId);

            // Assert
            Assert.IsFalse(result);
        }

        [TestCase("^\\d$", "123", false)]
        [TestCase("^\\d+$", "123", true)]
        [TestCase("^0[0-9]+$", "123", false)]
        [TestCase("^0[0-9]+$", "0123", true)]
        public void IsValidResourceId_FromPatternContainsCaretAndResourceIdIsNotNull_ReturnsExpectedResult(string pattern, string resourceId, bool expectedResult)
        {
            // Act
            var result = SportsbookHelper.IsValidResourceId(pattern, resourceId);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("0123", "0123a", false)]
        [TestCase("abc0", "abc0", true)]
        public void IsValidResourceId_FromPatternDoesNotContainsCaretAndResourceIdIsNotNull_ReturnsExpectedResult(string pattern, string resourceId, bool expectedResult)
        {
            // Act
            var result = SportsbookHelper.IsValidResourceId(pattern, resourceId);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetResourceLang_FromNullKeyName_ReturnsEmpty()
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
        public void GetResourceLang_FromKeyNameIsIntegerAndNotMatchToSoccerRangeResourceIdRegex_ReturnsExpectedResource(string keyName, string expectedResource)
        {
            // Act
            var resource = SportsbookHelper.GetResourceLang(keyName);

            // Assert
            Assert.AreEqual(expectedResource, resource);
        }

        [TestCase("16001", "FT - Goal 01")]
        [TestCase("16002", "FT - Goal 02")]
        [TestCase("16405", "ET - Goal 05")]
        [TestCase("16402", "ET - Goal 02")]
        [TestCase("16901", "Goal 01")]
        public void GetResourceLang_FromKeyNameMatchToSoccerRangeResourceIdRegex_ReturnsExpectedResource(string keyName, string expectedResource)
        {
            // Act
            var resource = SportsbookHelper.GetResourceLang(keyName);

            // Assert
            Assert.AreEqual(expectedResource, resource);
        }
    }
}