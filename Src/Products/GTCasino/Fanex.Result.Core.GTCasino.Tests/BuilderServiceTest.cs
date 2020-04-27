namespace Fanex.Result.GTCasino.Tests
{
    using Fanex.Result.Core.GTCasino;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class BuilderServiceTest
    {
        private GTCasinoResultBuilder _targetBuilder;

        [SetUp]
        public void Setup()
        {
            _targetBuilder = Substitute.For<GTCasinoResultBuilder>();
            _targetBuilder.GetTemplatePath.Returns(string.Empty);
        }
        
        [Test]
        public void Render_WhenInvalidImagePathConfig_ThrowAllBetCasinoException()
        {
            // Arrange
            var transDesc = "transDesc";
            var matchCode = string.Empty;
            var betType = "3201";
            var expectedMessage = "GTCasino Result API Exception. Exception: not found GTCasino image path configuration!";

            // Assert
            Assert.That(
                () => _targetBuilder.Render(transDesc, matchCode, betType),
                Throws.Exception.TypeOf<GTCasinoException>().With.Property("Message").Contains(expectedMessage));
        }

        [Test]
        public void Render_WhenValidImagePathConfig_ReturnResultHtml()
        {
            // Arrange
            var transDesc = "transDesc";
            var matchCode = string.Empty;
            var betType = "3201";
            var resultPopup = Substitute.For<IResultPopup>();
            resultPopup.genResultPopHtml(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns("Test");
            var builder = new GTCasinoResultBuilder(resultPopup);

            // Assert
            Assert.AreEqual("Test", builder.Render(transDesc, matchCode, betType));
        }        

        [Test]
        public void GetTemplatePath_Always_GetFromAppSettings()
        {
            // Act
            var builder = new GTCasinoResultBuilder();

            // Assert
            Assert.AreEqual("Testpath", builder.GetTemplatePath);
        }
    }
}