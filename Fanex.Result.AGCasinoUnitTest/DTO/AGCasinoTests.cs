namespace Fanex.Result.AGCasino.Tests.DTO
{
    using NUnit.Framework;
    using Core.AGCasino.DTO;

    [TestFixture]
    public class AGCasinoTests
    {
        [Test]
        public void Bettype_Always_GetSetValue()
        {
            // Arrange
            var model = new AGCasino();

            // Act
            model.Bettype = 1;

            // Assert
            Assert.AreEqual(1, model.Bettype);
        }

        [Test]
        public void Result_Always_GetSetValue()
        {
            // Arrange
            var model = new AGCasino();

            // Act
            model.Result = "result";

            // Assert
            Assert.AreEqual("result", model.Result);
        }
    }
}