namespace Fanex.Result.Core.Sportsbook.Tests.DTOTest
{
    using DTO;
    using NUnit.Framework;

    [TestFixture]
    public class GameDetailTests
    {
        [Test]
        public void MatchId_Always_GetSetValue()
        {
            // Arrange
            var gameDetail = new GameDetail { MatchId = 1 };

            // Act & Assert
            Assert.AreEqual(1, gameDetail.MatchId);
        }

        [Test]
        public void ResultStatusID_Always_GetSetValue()
        {
            // Arrange
            var gameDetail = new GameDetail { ResultStatusID = 1 };

            // Act & Assert
            Assert.AreEqual(1, gameDetail.ResultStatusID);
        }
    }
}