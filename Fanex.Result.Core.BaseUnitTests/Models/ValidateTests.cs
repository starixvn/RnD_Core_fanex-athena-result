namespace Fanex.Result.Core.Base.Tests.Models
{
    using Base;
    using Base.Models;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class ValidateTests
    {
        [Test]
        public void ValidateNormal_FromNullDtoName_ReturnsFalse()
        {
            // Arrange
            string dtoName = null;

            // Act
            var isValidated = Validate.Instance.ValidateNormal(dtoName, Arg.Any<SportType>());

            // Assert
            Assert.IsFalse(isValidated);
        }

        [Test]
        public void ValidateNormal_FromNotNullDtoNameAndNotDefinedSportType_ReturnsFalse()
        {
            // Arrange
            string dtoName = "dtoName";

            // Act
            var isValidated = Validate.Instance.ValidateNormal(dtoName, (SportType)1000000);

            // Assert
            Assert.IsFalse(isValidated);
        }

        [TestCase("Racing", 151)]
        [TestCase("Racing", 181)]
        [TestCase("Racing", 182)]
        [TestCase("NumberGame", 161)]
        [TestCase("LiveCasino", 162)]
        [TestCase("GoldDeluxe", 255)]
        [TestCase("AGCasino", 250)]
        [TestCase("AllBetCasino", 211)]
        [TestCase("Sportsbook", 1)]
        [TestCase("Sportsbook", 44)]
        public void ValidateNormal_FromValidData_ReturnsTrue(string dtoName, SportType sportType)
        {
            // Act
            var isValidated = Validate.Instance.ValidateNormal(dtoName, sportType);

            // Assert
            Assert.IsTrue(isValidated);
        }

        [TestCase("Racing", 161)]
        [TestCase("NumberGame", 151)]
        [TestCase("LiveCasino", 255)]
        [TestCase("GoldDeluxe", 162)]
        [TestCase("AGCasino", 1)]
        [TestCase("AllBetCasino", 250)]
        [TestCase("Sportsbook", 211)]
        public void ValidateNormal_FromInvalidData_ReturnsFalse(string dtoName, SportType sportType)
        {
            // Act
            var isValidated = Validate.Instance.ValidateNormal(dtoName, sportType);

            // Assert
            Assert.IsFalse(isValidated);
        }
    }
}