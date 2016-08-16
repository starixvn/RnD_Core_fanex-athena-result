using Fanex.Result.Core.AllBetCasino;
using NSubstitute;
using NUnit.Framework;

namespace Fanex.Result.AllBetCasino.Tests
{
    [TestFixture]
    public class BuilderServiceTest
    {
        private BuilderService _targetBuilder;

        [SetUp]
        public void Setup()
        {
            _targetBuilder = Substitute.For<BuilderService>();
            _targetBuilder.GetTemplatePath.Returns<string>(string.Empty);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Render_WhenInvalidTransDesc_ThrowAllBetCasinoException(string transDesc)
        {
            // Arrange
            var betCheck = string.Empty;
            var betType = "2401";
            var expectedExceptionMessage = "AllBetCasino Result API Exception. Exception: tranDesc is invalid!";

            // Assert
            Assert.That(
                () => _targetBuilder.Render(transDesc, betCheck, betType),
                Throws.Exception.TypeOf<AllBetCasinoException>().With.Property("Message").EqualTo(expectedExceptionMessage));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Render_WhenInvalidBetType_ThrowAllBetCasinoException(string betType)
        {
            // Arrange
            var transDesc = "transDesc";
            var betCheck = string.Empty;
            var expectedExceptionMessage = "AllBetCasino Result API Exception. Exception: betType is invalid!";

            // Assert
            Assert.That(
                () => _targetBuilder.Render(transDesc, betCheck, betType),
                Throws.Exception.TypeOf<AllBetCasinoException>().With.Property("Message").EqualTo(expectedExceptionMessage));
        }

        [Test]
        public void Render_WhenInvalidImagePathConfig_ThrowAllBetCasinoException()
        {
            /// Arrange
            var transDesc = "transDesc";
            var betCheck = string.Empty;
            var betType = "2401";
            var expectedMessage = "AllBetCasino Result API Exception. Exception: not found AllBetCasino image path configuration!";

            /// Assert
            Assert.That(
                () => _targetBuilder.Render(transDesc, betCheck, betType),
                Throws.Exception.TypeOf<AllBetCasinoException>().With.Property("Message").Contains(expectedMessage));
        }
    }
}