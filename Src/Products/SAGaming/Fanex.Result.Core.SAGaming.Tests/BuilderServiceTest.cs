namespace Fanex.Result.SAGaming.Tests
{
    using Core.SAGaming;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class BuilderServiceTest
    {
        private SAGamingResultBuilder _targetBuilder;

        [SetUp]
        public void Setup()
        {
            _targetBuilder = Substitute.For<SAGamingResultBuilder>();
            _targetBuilder.GetTemplatePath.Returns(string.Empty);
        }
                
        [Test]
        public void Render_WhenInvalidImagePathConfig_ThrowAllBetCasinoException()
        {
            // Arrange
            var transDesc = "transDesc";
            var matchCode = string.Empty;
            var betType = "3001";
            var expectedMessage = "SAGaming Result API Exception. Exception: not found SAGaming image path configuration!";

            // Assert
            Assert.That(
                () => _targetBuilder.Render(transDesc, matchCode, betType),
                Throws.Exception.TypeOf<SAGamingException>().With.Property("Message").Contains(expectedMessage));
        }

        [Test]
        public void Render_WhenValidImagePathConfig_ReturnResultHtml()
        {
            // Arrange
            var transDesc = "transDesc";
            var matchCode = string.Empty;
            var betType = "3001";
            var resultPopup = Substitute.For<IResultPopup>();
            resultPopup.genResultPopHtml(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns("Test");
            var builder = new SAGamingResultBuilder(resultPopup);

            // Assert
            Assert.AreEqual("Test", builder.Render(transDesc, matchCode, betType));
        }

        [Test]
        public void GetTemplatePath_Always_GetFromAppSettings()
        {
            // Act
            var builder = new SAGamingResultBuilder();

            // Assert
            Assert.AreEqual("Testpath", builder.GetTemplatePath);
        }
    }
}