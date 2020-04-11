namespace Fanex.Result.SexyGaming.Tests
{
    using Core.SexyGaming;
    using Fanex.Result.Core.BBin;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class BuilderServiceTest
    {
        private BBinResultBuilder _targetBuilder;

        [SetUp]
        public void Setup()
        {
            _targetBuilder = Substitute.For<BBinResultBuilder>();
            _targetBuilder.GetTemplatePath.Returns(string.Empty);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Render_WhenInvalidTransDesc_ThrowAllBetCasinoException(string transDesc)
        {
            // Arrange
            var matchCode = string.Empty;
            var betType = 3001;
            var expectedExceptionMessage = "BBin Result API Exception. Exception: tranDesc is invalid!";

            // Assert
            Assert.That(
                () => _targetBuilder.Render(transDesc, matchCode, betType),
                Throws.Exception.TypeOf<BBinException>().With.Property("Message").EqualTo(expectedExceptionMessage));
        }

        [TestCase(0)]
        public void Render_WhenInvalidBetType_ThrowAllBetCasinoException(int betType)
        {
            // Arrange
            var transDesc = "transDesc";
            var matchCode = string.Empty;
            var expectedExceptionMessage = "BBin Result API Exception. Exception: betType is invalid!";

            // Assert
            Assert.That(
                () => _targetBuilder.Render(transDesc, matchCode, betType),
                Throws.Exception.TypeOf<BBinException>().With.Property("Message").EqualTo(expectedExceptionMessage));
        }

        [Test]
        public void Render_WhenInvalidImagePathConfig_ThrowAllBetCasinoException()
        {
            // Arrange
            var transDesc = "transDesc";
            var matchCode = string.Empty;
            var betType = 3001;
            var expectedMessage = "SexyGaming Result API Exception. Exception: not found SexyGaming image path configuration!";

            // Assert
            Assert.That(
                () => _targetBuilder.Render(transDesc, matchCode, betType),
                Throws.Exception.TypeOf<BBinException>().With.Property("Message").Contains(expectedMessage));
        }

        [Test]
        public void Render_WhenValidImagePathConfig_ReturnResultHtml()
        {
            // Arrange
            var transDesc = "transDesc";
            var matchCode = string.Empty;
            var betType = 3001;
            var resultPopup = Substitute.For<IResultPopup>();
            resultPopup.genResultPopHtml(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>()).Returns("Test");
            var builder = new BBinResultBuilder(resultPopup);

            // Assert
            Assert.AreEqual("Test", builder.Render(transDesc, matchCode, betType));
        }

        [Test]
        public void Render_WhenValidImagePathConfig_WhenHasException_ThrowException()
        {
            // Arrange
            var transDesc = "transDesc";
            var matchCode = string.Empty;
            var betType = 3001;
            var builder = new BBinResultBuilder();

            // Act
            var testDelegate = new TestDelegate(() => builder.Render(transDesc, matchCode, betType));

            // Assert
            Assert.Throws<BBinException>(testDelegate);
        }

        [Test]
        public void GetTemplatePath_Always_GetFromAppSettings()
        {
            // Act
            var builder = new BBinResultBuilder();

            // Assert
            Assert.AreEqual("Testpath", builder.GetTemplatePath);
        }
    }
}