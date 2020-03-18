namespace Fanex.Result.SAGaming.Tests
{
    using Core.SAGaming;
    using NUnit.Framework;

    [TestFixture]
    public class SAGamingCasinoExceptionTests
    {
        [Test]
        public void Message_Always_GetMessage()
        {
            // Act
            var exception = new SAGamingException("test");

            // Assert
            Assert.AreEqual("SAGaming Result API Exception. Exception: test", exception.Message);
        }
    }
}