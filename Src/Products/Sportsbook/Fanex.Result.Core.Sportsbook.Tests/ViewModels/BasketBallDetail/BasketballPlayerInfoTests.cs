namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels.BasketBallDetail
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using Sportsbook.ViewModels.BasketBallDetail;

    [TestFixture]
    public class BasketballPlayerInfoTests
    {
        [Test]
        public void Constructor_WithNullJson_InitializesEmptyProperties()
        {
            // Act
            var playerInfo = new BasketballPlayerInfo(null);

            // Assert
            Assert.IsNull(playerInfo.PlayerId);
            Assert.IsNull(playerInfo.PlayerName);
            Assert.IsNull(playerInfo.TeamId);
            Assert.IsNotNull(playerInfo.ResultDetailItems);
            Assert.AreEqual(0, playerInfo.ResultDetailItems.Count);
        }

        [Test]
        public void Constructor_WithEmptyJson_InitializesEmptyProperties()
        {
            // Act
            var playerInfo = new BasketballPlayerInfo(string.Empty);

            // Assert
            Assert.IsNull(playerInfo.PlayerId);
            Assert.IsNull(playerInfo.PlayerName);
            Assert.IsNull(playerInfo.TeamId);
            Assert.IsNotNull(playerInfo.ResultDetailItems);
            Assert.AreEqual(0, playerInfo.ResultDetailItems.Count);
        }

        [Test]
        public void Constructor_WithWhitespaceJson_InitializesEmptyProperties()
        {
            // Act
            var playerInfo = new BasketballPlayerInfo("   ");

            // Assert
            Assert.IsNull(playerInfo.PlayerId);
            Assert.IsNull(playerInfo.PlayerName);
            Assert.IsNull(playerInfo.TeamId);
            Assert.IsNotNull(playerInfo.ResultDetailItems);
            Assert.AreEqual(0, playerInfo.ResultDetailItems.Count);
        }

        [Test]
        public void Constructor_WithValidJson_WithTwoCommaSeparatedValues_SetsPlayerIdAndTeamId()
        {
            // Arrange
            var json = "player123,team456";

            // Act
            var playerInfo = new BasketballPlayerInfo(json);

            // Assert
            Assert.AreEqual("player123", playerInfo.PlayerId);
            Assert.AreEqual("team456", playerInfo.TeamId);
            Assert.IsNull(playerInfo.PlayerName);
            Assert.IsNotNull(playerInfo.ResultDetailItems);
            Assert.AreEqual(0, playerInfo.ResultDetailItems.Count);
        }

        [Test]
        public void Constructor_WithValidJson_WithUppercaseValues_ConvertsToLowercase()
        {
            // Arrange
            var json = "PLAYER123,TEAM456";

            // Act
            var playerInfo = new BasketballPlayerInfo(json);

            // Assert
            Assert.AreEqual("player123", playerInfo.PlayerId);
            Assert.AreEqual("team456", playerInfo.TeamId);
        }

        [Test]
        public void Constructor_WithValidJson_WithMixedCaseValues_ConvertsToLowercase()
        {
            // Arrange
            var json = "Player123,Team456";

            // Act
            var playerInfo = new BasketballPlayerInfo(json);

            // Assert
            Assert.AreEqual("player123", playerInfo.PlayerId);
            Assert.AreEqual("team456", playerInfo.TeamId);
        }

        [Test]
        public void Constructor_WithValidJson_WithWhitespaceAroundValues_DoesNotTrimWhitespace()
        {
            // Arrange
            var json = " player123 , team456 ";

            // Act
            var playerInfo = new BasketballPlayerInfo(json);

            // Assert
            Assert.AreEqual(" player123 ", playerInfo.PlayerId);
            Assert.AreEqual(" team456 ", playerInfo.TeamId);
        }

        [Test]
        public void Constructor_WithValidJson_WithOnlyOneValue_DoesNotSetPlayerIdOrTeamId()
        {
            // Arrange
            var json = "player123";

            // Act
            var playerInfo = new BasketballPlayerInfo(json);

            // Assert
            Assert.IsNull(playerInfo.PlayerId);
            Assert.IsNull(playerInfo.TeamId);
            Assert.IsNotNull(playerInfo.ResultDetailItems);
            Assert.AreEqual(0, playerInfo.ResultDetailItems.Count);
        }

        [Test]
        public void Constructor_WithValidJson_WithMoreThanTwoValues_DoesNotSetPlayerIdOrTeamId()
        {
            // Arrange
            var json = "player123,team456,extravalue";

            // Act
            var playerInfo = new BasketballPlayerInfo(json);

            // Assert
            Assert.IsNull(playerInfo.PlayerId);
            Assert.IsNull(playerInfo.TeamId);
            Assert.IsNotNull(playerInfo.ResultDetailItems);
            Assert.AreEqual(0, playerInfo.ResultDetailItems.Count);
        }

        [Test]
        public void Constructor_WithValidJson_WithEmptyValues_DoesNotSetProperties()
        {
            // Arrange
            var json = ",";

            // Act
            var playerInfo = new BasketballPlayerInfo(json);

            // Assert
            Assert.IsNull(playerInfo.PlayerId);
            Assert.IsNull(playerInfo.TeamId);
            Assert.IsNotNull(playerInfo.ResultDetailItems);
            Assert.AreEqual(0, playerInfo.ResultDetailItems.Count);
        }

        [Test]
        public void Constructor_WithValidJson_WithFirstValueEmpty_DoesNotSetProperties()
        {
            // Arrange
            var json = ",team456";

            // Act
            var playerInfo = new BasketballPlayerInfo(json);

            // Assert
            Assert.IsNull(playerInfo.PlayerId);
            Assert.IsNull(playerInfo.TeamId);
        }

        [Test]
        public void Constructor_WithValidJson_WithSecondValueEmpty_DoesNotSetProperties()
        {
            // Arrange
            var json = "player123,";

            // Act
            var playerInfo = new BasketballPlayerInfo(json);

            // Assert
            Assert.IsNull(playerInfo.PlayerId);
            Assert.IsNull(playerInfo.TeamId);
        }

        [Test]
        public void Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            var playerInfo = new BasketballPlayerInfo("test,test");
            var expectedPlayerName = "John Doe";
            var expectedResultDetailItems = new List<BasketBallGameDetailItem>();

            // Act
            playerInfo.PlayerName = expectedPlayerName;
            playerInfo.ResultDetailItems = expectedResultDetailItems;

            // Assert
            Assert.AreEqual(expectedPlayerName, playerInfo.PlayerName);
            Assert.AreEqual(expectedResultDetailItems, playerInfo.ResultDetailItems);
        }

        [Test]
        public void ResultDetailItems_IsInitializedAsEmptyList()
        {
            // Act
            var playerInfo = new BasketballPlayerInfo("player123,team456");

            // Assert
            Assert.IsNotNull(playerInfo.ResultDetailItems);
            Assert.IsInstanceOf<List<BasketBallGameDetailItem>>(playerInfo.ResultDetailItems);
            Assert.AreEqual(0, playerInfo.ResultDetailItems.Count);
        }
    }

    [TestFixture]
    public class BasketballPlayerInfosTests
    {
        [Test]
        public void Constructor_WithNullJson_InitializesEmptyPlayersList()
        {
            // Act
            var playerInfos = new BasketballPlayerInfos(null);

            // Assert
            Assert.IsNotNull(playerInfos.Players);
            Assert.AreEqual(0, playerInfos.Players.Count);
        }

        [Test]
        public void Constructor_WithEmptyJson_InitializesEmptyPlayersList()
        {
            // Act
            var playerInfos = new BasketballPlayerInfos(string.Empty);

            // Assert
            Assert.IsNotNull(playerInfos.Players);
            Assert.AreEqual(0, playerInfos.Players.Count);
        }

        [Test]
        public void Constructor_WithWhitespaceJson_CreatesPlayerInfoWithNullValues()
        {
            // Act
            var playerInfos = new BasketballPlayerInfos("   ");

            // Assert
            Assert.IsNotNull(playerInfos.Players);
            Assert.AreEqual(1, playerInfos.Players.Count);
            Assert.IsNull(playerInfos.Players[0].PlayerId);
            Assert.IsNull(playerInfos.Players[0].TeamId);
        }

        [Test]
        public void Constructor_WithValidJson_WithSinglePlayer_CreatesOnePlayerInfo()
        {
            // Arrange
            var json = "player123,team456";

            // Act
            var playerInfos = new BasketballPlayerInfos(json);

            // Assert
            Assert.IsNotNull(playerInfos.Players);
            Assert.AreEqual(1, playerInfos.Players.Count);
            Assert.AreEqual("player123", playerInfos.Players[0].PlayerId);
            Assert.AreEqual("team456", playerInfos.Players[0].TeamId);
        }

        [Test]
        public void Constructor_WithValidJson_WithMultiplePlayers_CreatesMultiplePlayerInfos()
        {
            // Arrange
            var json = "player1,team1;player2,team2;player3,team3";

            // Act
            var playerInfos = new BasketballPlayerInfos(json);

            // Assert
            Assert.IsNotNull(playerInfos.Players);
            Assert.AreEqual(3, playerInfos.Players.Count);
            Assert.AreEqual("player1", playerInfos.Players[0].PlayerId);
            Assert.AreEqual("team1", playerInfos.Players[0].TeamId);
            Assert.AreEqual("player2", playerInfos.Players[1].PlayerId);
            Assert.AreEqual("team2", playerInfos.Players[1].TeamId);
            Assert.AreEqual("player3", playerInfos.Players[2].PlayerId);
            Assert.AreEqual("team3", playerInfos.Players[2].TeamId);
        }

        [Test]
        public void Constructor_WithValidJson_WithUppercaseValues_ConvertsToLowercase()
        {
            // Arrange
            var json = "PLAYER1,TEAM1;PLAYER2,TEAM2";

            // Act
            var playerInfos = new BasketballPlayerInfos(json);

            // Assert
            Assert.AreEqual(2, playerInfos.Players.Count);
            Assert.AreEqual("player1", playerInfos.Players[0].PlayerId);
            Assert.AreEqual("team1", playerInfos.Players[0].TeamId);
            Assert.AreEqual("player2", playerInfos.Players[1].PlayerId);
            Assert.AreEqual("team2", playerInfos.Players[1].TeamId);
        }

        [Test]
        public void Constructor_WithValidJson_WithMixedCaseValues_ConvertsToLowercase()
        {
            // Arrange
            var json = "Player1,Team1;Player2,Team2";

            // Act
            var playerInfos = new BasketballPlayerInfos(json);

            // Assert
            Assert.AreEqual(2, playerInfos.Players.Count);
            Assert.AreEqual("player1", playerInfos.Players[0].PlayerId);
            Assert.AreEqual("team1", playerInfos.Players[0].TeamId);
            Assert.AreEqual("player2", playerInfos.Players[1].PlayerId);
            Assert.AreEqual("team2", playerInfos.Players[1].TeamId);
        }

        [Test]
        public void Constructor_WithValidJson_WithWhitespaceAroundValues_DoesNotTrimWhitespace()
        {
            // Arrange
            var json = " player1 , team1 ; player2 , team2 ";

            // Act
            var playerInfos = new BasketballPlayerInfos(json);

            // Assert
            Assert.AreEqual(2, playerInfos.Players.Count);
            Assert.AreEqual(" player1 ", playerInfos.Players[0].PlayerId);
            Assert.AreEqual(" team1 ", playerInfos.Players[0].TeamId);
            Assert.AreEqual(" player2 ", playerInfos.Players[1].PlayerId);
            Assert.AreEqual(" team2 ", playerInfos.Players[1].TeamId);
        }

        [Test]
        public void Constructor_WithValidJson_WithEmptySegments_IgnoresEmptySegments()
        {
            // Arrange
            var json = "player1,team1;;player2,team2;";

            // Act
            var playerInfos = new BasketballPlayerInfos(json);

            // Assert
            Assert.AreEqual(2, playerInfos.Players.Count);
            Assert.AreEqual("player1", playerInfos.Players[0].PlayerId);
            Assert.AreEqual("team1", playerInfos.Players[0].TeamId);
            Assert.AreEqual("player2", playerInfos.Players[1].PlayerId);
            Assert.AreEqual("team2", playerInfos.Players[1].TeamId);
        }

        [Test]
        public void Constructor_WithValidJson_WithOnlyWhitespaceSegments_DoesNotIgnoreWhitespaceSegments()
        {
            // Arrange
            var json = "player1,team1;   ;player2,team2";

            // Act
            var playerInfos = new BasketballPlayerInfos(json);

            // Assert
            Assert.AreEqual(3, playerInfos.Players.Count);
            Assert.AreEqual("player1", playerInfos.Players[0].PlayerId);
            Assert.AreEqual("team1", playerInfos.Players[0].TeamId);
            // The whitespace segment creates a player info with null values
            Assert.IsNull(playerInfos.Players[1].PlayerId);
            Assert.IsNull(playerInfos.Players[1].TeamId);
            Assert.AreEqual("player2", playerInfos.Players[2].PlayerId);
            Assert.AreEqual("team2", playerInfos.Players[2].TeamId);
        }

        [Test]
        public void Constructor_WithValidJson_WithInvalidPlayerFormat_StillCreatesPlayerInfoObjects()
        {
            // Arrange
            var json = "player1,team1;invalidformat;player2,team2";

            // Act
            var playerInfos = new BasketballPlayerInfos(json);

            // Assert
            Assert.AreEqual(3, playerInfos.Players.Count);
            Assert.AreEqual("player1", playerInfos.Players[0].PlayerId);
            Assert.AreEqual("team1", playerInfos.Players[0].TeamId);
            // The invalid format should create a player info with null values
            Assert.IsNull(playerInfos.Players[1].PlayerId);
            Assert.IsNull(playerInfos.Players[1].TeamId);
            Assert.AreEqual("player2", playerInfos.Players[2].PlayerId);
            Assert.AreEqual("team2", playerInfos.Players[2].TeamId);
        }

        [Test]
        public void Players_Property_CanBeSetAndRetrieved()
        {
            // Arrange
            var playerInfos = new BasketballPlayerInfos("test,test");
            var expectedPlayers = new List<BasketballPlayerInfo>
            {
                new BasketballPlayerInfo("player1,team1"),
                new BasketballPlayerInfo("player2,team2")
            };

            // Act
            playerInfos.Players = expectedPlayers;

            // Assert
            Assert.AreEqual(expectedPlayers, playerInfos.Players);
            Assert.AreEqual(2, playerInfos.Players.Count);
        }

        [Test]
        public void Players_IsInitializedAsEmptyList()
        {
            // Act
            var playerInfos = new BasketballPlayerInfos("player123,team456");

            // Assert
            Assert.IsNotNull(playerInfos.Players);
            Assert.IsInstanceOf<List<BasketballPlayerInfo>>(playerInfos.Players);
        }
    }
}
