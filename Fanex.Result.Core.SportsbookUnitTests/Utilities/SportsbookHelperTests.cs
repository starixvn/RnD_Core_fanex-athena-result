namespace Fanex.Result.Core.Sportsbook.Tests.Utilities
{
    using NUnit.Framework;
    using Sportsbook.Utilities;

    [TestFixture]
    public class SportsbookHelperTests
    {
        [TestCase("")]
        [TestCase(null)]
        public void IsSetXGameYBetID_FromBetIdNullOrEmpty_ReturnsFalse(string betId)
        {
            // Act
            var result = SportsbookHelper.IsSetXGameYBetID(betId);

            // Assert
            Assert.IsFalse(result);
        }

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
            var pattern = "^0[0-9]+$";
            var resourceId = "0123";

            // Act
            var result = SportsbookHelper.IsValidResourceId(pattern, resourceId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidResourceId_FromPatternContainsCaretAndIsNotMatchResourceId_ReturnsFalse()
        {
            // Arrange
            var pattern = "^0\\d+$";
            var resourceId = "123";

            // Act
            var result = SportsbookHelper.IsValidResourceId(pattern, resourceId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidResourceId_FromPatternNotContainsCaretAndIsEqualResourceId_ReturnsTrue()
        {
            // Arrange
            var pattern = "0123";
            var resourceId = "0123";

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
        public void GetResourceLang_FromKeyNameIsIntegerAndNotMatchSoccerRangeResourceIdRegex_ReturnsExpectedResource(string keyName, string expectedResource)
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
        public void GetResourceLang_FromKeyNameIsIntegerAndMatchToSoccerRangeResourceIdRegex_ReturnsExpectedResource(string keyName, string expectedResource)
        {
            // Act
            var resource = SportsbookHelper.GetResourceLang(keyName);

            // Assert
            Assert.AreEqual(expectedResource, resource);
        }
    }
}