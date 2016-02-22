using Fanex.Result.Core.AllBetCasino;
using NSubstitute;
using NUnit.Framework;

namespace Fanex.Result.AllBetCasinoUnitTests
{
    [TestFixture]
    public class BuilderServiceTest
    {
        private BuilderService _targetBuilder;
        [SetUp]
        public void Setup()
        {
            _targetBuilder = Substitute.For<BuilderService>();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" as 1243 sdf ")]
        [TestCase("43543645654 ")]
        public void Render_WhenInvalidTransId_ThrowAllBetCasinoException(string transId)
        {
            /// Arrange
            var expectedExceptionMessage = "AllBetCasino Result API Exception. Exception: The TransId is invalid!";
            /// Assert
            Assert.That(() => _targetBuilder.Render(transId),
                Throws.Exception.TypeOf<AllBetCasinoException>().With.Property("Message").EqualTo(expectedExceptionMessage));
        }

        [Test]
        public void Render_WhenInvalidConfig_ThrowAllBetCasinoException()
        {
            /// Arrange
            var transId = "123456789";
            var expectedMessage = "Not found [Fanex.Result.Core.3rdLiveCasino] connection string Or [Fanex.Result.Core.3rdLiveCasinoTplPath] template path";
            _targetBuilder.GetConnectionString.Returns("");
            _targetBuilder.GetTemplatePath.Returns("");
            /// Assert
            Assert.That(() => _targetBuilder.Render(transId), Throws.Exception.TypeOf<AllBetCasinoException>()
                .With.Property("Message").Contains(expectedMessage));
        }

        [Test]
        public void Render_WhenValidConfigAndInputValue_Return3rdException()
        {
            /// Arrange
            var transId = "12345";
            _targetBuilder.GetConnectionString.Returns("A connection string");
            _targetBuilder.GetTemplatePath.Returns("A template path");

            /// Assert
            Assert.Throws<AllBetCasinoException>(() => _targetBuilder.Render(transId));
        }
    }
}
