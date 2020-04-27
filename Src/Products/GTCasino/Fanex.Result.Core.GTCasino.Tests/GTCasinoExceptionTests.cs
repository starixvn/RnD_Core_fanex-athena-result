namespace Fanex.Result.GTCasino.Tests
{
    using Fanex.Result.Core.GTCasino;
    using NUnit.Framework;

    [TestFixture]
    public class GTCasinoExceptionTests
    {
        [Test]
        public void Message_Always_GetMessage()
        {
            // Act
            var exception = new GTCasinoException("test");

            // Assert
            Assert.AreEqual("GTCasino Result API Exception. Exception: test", exception.Message);
        }
    }
}