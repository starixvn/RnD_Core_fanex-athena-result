using Fanex.Result.Core.AllBetCasino;
using NUnit.Framework;

namespace Fanex.Result.AllBetCasinoUnitTests
{
    [TestFixture]
    public class BuilderServiceTest
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" as 1243 sdf ")]
        [TestCase("43543645654 ")]
        public void Render_WhenInvalidTransId_ThrowAllBetCasinoException(string transId)
        {
            /// Arrange
            var expectedExceptionMessage = "AllBetCasino Result API Exception. Exception: The TransId is invalid!";
            var builder = new BuilderService();

            /// Assert
            Assert.That(() => builder.Render(transId),Throws.Exception.TypeOf<AllBetCasinoException>()
                .With.Property("Message").EqualTo(expectedExceptionMessage));
        }
        [TestCase]
        public void Render_WhenInvalidConfig_ThrowAllBetCasinoException()
        {
            /// Arrange

            /// Action

            /// Assert
        }

        [TestCase]
        public void Render_WhenValidConfigAndInputValue_ReturnValidHTMLResult()
        {
            /// Arrange

            /// Action

            /// Assert
        }
    }
}
