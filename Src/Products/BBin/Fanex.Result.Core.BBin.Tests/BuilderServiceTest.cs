namespace Fanex.Result.BBin.Tests
{
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
        
        [Test]
        public void Render_WhenInvalidImagePathConfig_ThrowAllBetCasinoException()
        {
            // Arrange
            var transDesc = "transDesc";
            var matchCode = string.Empty;
            var expectedMessage = "BBin Result API Exception. Exception: not found BBin image path configuration!";

            // Assert
            Assert.That(
                () => _targetBuilder.Render(transDesc, matchCode),
                Throws.Exception.TypeOf<BBinException>().With.Property("Message").Contains(expectedMessage));
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