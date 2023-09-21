namespace Fanex.Result.AllBetCasino.Tests
{
    using Core.AllBetCasino;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class BuilderServiceTest
    {
        private AllbetResultBuilder _targetBuilder;

        [SetUp]
        public void Setup()
        {
            _targetBuilder = Substitute.For<AllbetResultBuilder>();
            _targetBuilder.GetTemplatePath.Returns(string.Empty);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Render_WhenInvalidTransDesc_ThrowAllBetCasinoException(string transDesc)
        {
            // Arrange
            var matchCode = string.Empty;
            var betType = "2401";
            var expectedExceptionMessage = "AllBetCasino Result API Exception. Exception: tranDesc is invalid!";

            // Assert
            Assert.That(
                () => _targetBuilder.Render(transDesc, matchCode, betType),
                Throws.Exception.TypeOf<AllBetCasinoException>().With.Property("Message").EqualTo(expectedExceptionMessage));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Render_WhenInvalidBetType_ThrowAllBetCasinoException(string betType)
        {
            // Arrange
            var transDesc = "transDesc";
            var matchCode = string.Empty;
            var expectedExceptionMessage = "AllBetCasino Result API Exception. Exception: betType is invalid!";

            // Assert
            Assert.That(
                () => _targetBuilder.Render(transDesc, matchCode, betType),
                Throws.Exception.TypeOf<AllBetCasinoException>().With.Property("Message").EqualTo(expectedExceptionMessage));
        }

        [Test]
        public void Render_WhenInvalidImagePathConfig_ThrowAllBetCasinoException()
        {
            // Arrange
            var transDesc = "transDesc";
            var matchCode = string.Empty;
            var betType = "2401";
            var expectedMessage = "AllBetCasino Result API Exception. Exception: not found AllBetCasino image path configuration!";

            // Assert
            Assert.That(
                () => _targetBuilder.Render(transDesc, matchCode, betType),
                Throws.Exception.TypeOf<AllBetCasinoException>().With.Property("Message").Contains(expectedMessage));
        }

        [Test]
        public void Render_WhenValidImagePathConfig_ReturnResultHtml()
        {
            // Arrange
            var transDesc = "transDesc";
            var matchCode = string.Empty;
            var betType = "2401";
            var resultPopup = Substitute.For<IResultPopup>();
            resultPopup.genResultPopHtml(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns("Test");
            var builder = new AllbetResultBuilder(resultPopup);

            // Assert
            Assert.AreEqual("Test", builder.Render(transDesc, matchCode, betType));
        }

        [Test]
        public void Render_WhenValidImagePathConfig_WhenHasException_ThrowException()
        {
            // Arrange
            var transDesc = "transDesc";
            var matchCode = string.Empty;
            var betType = "2401";
            var builder = new AllbetResultBuilder();

            // Act
            var testDelegate = new TestDelegate(() => builder.Render(transDesc, matchCode, betType));

            // Assert
            Assert.Throws<AllBetCasinoException>(testDelegate);
        }

        [Test]
        public void GetTemplatePath_Always_GetFromAppSettings()
        {
            // Act
            var builder = new AllbetResultBuilder();

            // Assert
            Assert.AreEqual("Testpath", builder.GetTemplatePath);
        }
    }
}