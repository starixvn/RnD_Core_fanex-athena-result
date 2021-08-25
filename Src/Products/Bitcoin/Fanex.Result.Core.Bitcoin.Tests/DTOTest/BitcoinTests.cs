namespace Fanex.Result.Core.Bitcoin.Tests.DTOTest
{
    using DTO;
    using NUnit.Framework;

    [TestFixture]
    internal class BitcoinTests
    {
        [Test]
        public void EventDate_Always_IntitGameResults()
        {
            // Arrange
            var bitcoin = new Bitcoin();

            // Assert
            Assert.IsNotNull(bitcoin.GameResults);
        }
    }
}