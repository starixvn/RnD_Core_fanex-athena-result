namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using System.Collections.Generic;
    using Base;
    using DTO;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.ViewModels;

    [TestFixture]
    public class BadmintonViewModelTests
    {
        private IRefDataHelper refDataHelper;

        [SetUp]
        public void Setup()
        {
            refDataHelper = Substitute.For<IRefDataHelper>();
            ResultCoreManager.RegisterRefDataHelper(refDataHelper);
        }

        [Test]
        public void GetHomeAwaySet_FromIndexIsGreaterThan4_ReturnsHyphen()
        {
            // Arrange
            var viewModel = new BadmintonViewModel(new Sportsbook(), new ResultSetting());
            var index = 5;

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase(3, "")]
        [TestCase(3, null)]
        public void GetHomeAwaySet_FromIndexIsLessThan5AndGameDetailsIsEmptyOrEmpty_ReturnsHyphen(
            int index,
            string betStatus)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { GameDetails = new List<GameDetail>() };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(index, false);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase(4)]
        [TestCase(5)]
        public void GetHomeAwaySet_GameXNotExist_ReturnHyphen(int gameX)
        {
            // Arrange
            gameX--;
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="00", HomeValue="2", AwayValue="1", SettlementStatusID=4, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(gameX, true);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void GetHomeAwaySet_GameXMustRefund_ReturnRefund(int gameX)
        {
            // Arrange
            gameX--;
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=1, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=1, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=1, MatchId=1111},
                new GameDetail{ ResourceId="0104", HomeValue="31", AwayValue="30",SettlementStatusID=1, MatchId=1111},
                new GameDetail{ ResourceId="0105", HomeValue="31", AwayValue="30",SettlementStatusID=1, MatchId=1111},
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(gameX, true);

            // Assert
            Assert.AreEqual(LangResource.Refund, homeAwaySet);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void GetHomeAwaySet_GameXMustRunning_ReturnHyphen(int gameX)
        {
            // Arrange
            gameX--;
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=0, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=0, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=0, MatchId=1111},
                new GameDetail{ ResourceId="0104", HomeValue="31", AwayValue="30",SettlementStatusID=0, MatchId=1111},
                new GameDetail{ ResourceId="0105", HomeValue="31", AwayValue="30",SettlementStatusID=0, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(gameX, true);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [Test]
        public void TotalPointHome_Game2MustRefund_NotCountPointOfGame2()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=1, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="00", HomeValue="2", AwayValue="1", SettlementStatusID=4, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalPoint = viewModel.TotalPointHome;

            // Assert
            Assert.AreEqual("52", actualTotalPoint);
        }

        [Test]
        public void TotalPointHome_WhenTotalPointIsGreaterThanZero_ShowTotalPointHome()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="00", HomeValue="2", AwayValue="1", SettlementStatusID=4, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalPoint = viewModel.TotalPointHome;

            // Assert
            Assert.AreEqual("80", actualTotalPoint);
        }

        [Test]
        public void TotalPointHome_WhenTotalPointIsLessThanZero_ShowHyphen()
        {
            // Arrange
            var sportsbookDto = new Sportsbook();
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalPoint = viewModel.TotalPointHome;

            // Assert
            Assert.AreEqual("-", actualTotalPoint);
        }

        [Test]
        public void TotalPointHome_WhenTotalGamesRefund_ShowRefund()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="00", HomeValue="2", AwayValue="1", SettlementStatusID=1, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalPoint = viewModel.TotalPointHome;

            // Assert
            Assert.AreEqual(LangResource.Refund, actualTotalPoint);
        }

        [Test]
        public void TotalPointHome_WhenTotalGamesRunning_ShowHyphen()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="00", HomeValue="2", AwayValue="1", SettlementStatusID=0, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalPoint = viewModel.TotalPointHome;

            // Assert
            Assert.AreEqual("-", actualTotalPoint);
        }

        [Test]
        public void TotalPointAway_WhenTotalGamesRunning_ShowHyphen()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="00", HomeValue="2", AwayValue="1", SettlementStatusID=0, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalPoint = viewModel.TotalPointAway;

            // Assert
            Assert.AreEqual("-", actualTotalPoint);
        }

        [Test]
        public void TotalPointAway_WhenTotalGamesRefund_ShowRefund()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="00", HomeValue="2", AwayValue="1", SettlementStatusID=1, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actual = viewModel.TotalPointAway;

            // Assert
            Assert.AreEqual(LangResource.Refund, actual);
        }
        [Test]
        public void TotalPointAway_Game2MustRefund_NotCountPointOfGame2()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=1, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="00", HomeValue="2", AwayValue="1", SettlementStatusID=4, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalPoint = viewModel.TotalPointAway;

            // Assert
            Assert.AreEqual("44", actualTotalPoint);
        }

        [Test]
        public void TotalPointAway_WhenTotalPointIsLGreaterThanZero_ShowTotalAwayPoints()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="00", HomeValue="2", AwayValue="1", SettlementStatusID=4, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalPoint = viewModel.TotalPointAway;

            // Assert
            Assert.AreEqual("74", actualTotalPoint);
        }

        [Test]
        public void TotalPointAway_WhenTotalPointIsLessThanZero_ShowHyphen()
        {
            // Arrange
            var sportsbookDto = new Sportsbook { BetStatus = "0-0,0-0,0-0,0-0" };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalPoint = viewModel.TotalPointAway;

            // Assert
            Assert.AreEqual("-", actualTotalPoint);
        }

        [Test]
        public void TotalGamesAway_NotExistTotalGames_ShowHyphen()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=4, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalGames = viewModel.TotalGameAway;

            // Assert
            Assert.AreEqual("-", actualTotalGames);
        }

        [Test]
        public void TotalGamesAway_TotalGamesMustRefunt_ShowRefund()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="00", HomeValue="2", AwayValue="1", SettlementStatusID=1, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalGames = viewModel.TotalGameAway;

            // Assert
            Assert.AreEqual(LangResource.Refund, actualTotalGames);
        }

        [Test]
        public void TotalGamesAway_TotalGamesValid_ShowNumber()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="00", HomeValue="2", AwayValue="1", SettlementStatusID=4, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalGames = viewModel.TotalGameAway;

            // Assert
            Assert.AreEqual("1", actualTotalGames);
        }

        [Test]
        public void TotalGamesAway_TotalGamesIsRunning_ShowHyphen()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="00", HomeValue="2", AwayValue="1", SettlementStatusID=0, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalGames = viewModel.TotalGameAway;

            // Assert
            Assert.AreEqual("-", actualTotalGames);
        }

        [Test]
        public void TotalGamesHome_TotalGamesIsRunning_ShowHyphen()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="00", HomeValue="2", AwayValue="1", SettlementStatusID=0, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalGames = viewModel.TotalGameHome;

            // Assert
            Assert.AreEqual("-", actualTotalGames);
        }

        [Test]
        public void TotalGamesHome_TotalGamesValid_ShowNumber()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="00", HomeValue="2", AwayValue="1", SettlementStatusID=4, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalGames = viewModel.TotalGameHome;

            // Assert
            Assert.AreEqual("2", actualTotalGames);
        }

        [Test]
        public void TotalGamesHome_NotExistTotalGames_ShowHyphen()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=4, MatchId=1111},
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalGames = viewModel.TotalGameHome;

            // Assert
            Assert.AreEqual("-", actualTotalGames);
        }

        [Test]
        public void TotalGamesHome_TotalGamesMustRefunt_ShowRefund()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="00", HomeValue="2", AwayValue="1", SettlementStatusID=1, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actualTotalGames = viewModel.TotalGameHome;

            // Assert
            Assert.AreEqual(LangResource.Refund, actualTotalGames);
        }

        [Test]
        public void VisibleDetailedLink_HasDataOfBetType712_ReturnTrue()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="7120103", HomeValue="A", SettlementStatusID=4, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting { VisibleDetailedLink = true });

            // Act
            var actual = viewModel.VisibleDetailedLink;

            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void VisibleDetailedLink_HasDataOfBetType713_ReturnTrue()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="7130103", HomeValue="A", SettlementStatusID=4, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting { VisibleDetailedLink = true });

            // Act
            var actual = viewModel.VisibleDetailedLink;

            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void VisibleDetailedLink_NoDataOfBetType713Or712_ReturnFalse()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="00", HomeValue="2", AwayValue="1", SettlementStatusID=1, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actual = viewModel.VisibleDetailedLink;

            // Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void VisibleGameDetail_HasDataOfBetType713_ReturnTrue()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="7130103", HomeValue="A", SettlementStatusID=4, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting { VisibleDetailedLink = true });

            // Act
            var actual = viewModel.VisibleGameDetail;

            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void VisibleGameDetail_NoDataOfBetType713Or712_ReturnFalse()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="0101", HomeValue="21", AwayValue="14", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0102", HomeValue="28", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="0103", HomeValue="31", AwayValue="30",SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="00", HomeValue="2", AwayValue="1", SettlementStatusID=1, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actual = viewModel.VisibleGameDetail;

            // Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void ResultDetailsItems_MustOrderByAscByGameNumber()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="7130303", HomeValue="A", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="7130403", HomeValue="A", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="7130203", HomeValue="A", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="7130103", HomeValue="A", SettlementStatusID=4, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actual = viewModel.ResultDetailItems;

            // Assert
            Assert.AreEqual("7130103", actual[0].ResourceID);
            Assert.AreEqual("7130203", actual[1].ResourceID);
            Assert.AreEqual("7130303", actual[2].ResourceID);
            Assert.AreEqual("7130403", actual[3].ResourceID);
        }

        [Test]
        public void ResultDetailsItems_MustOrderByAscByBetType()
        {
            // Arrange
            var details = new List<GameDetail>
            {
                new GameDetail{ ResourceId="7130303", HomeValue="A", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="7130407", HomeValue="A", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="7120203", HomeValue="A", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="7120103", HomeValue="A", SettlementStatusID=4, MatchId=1111},
                new GameDetail{ ResourceId="7130105", HomeValue="A", SettlementStatusID=4, MatchId=1111}
            };
            var sportsbookDto = new Sportsbook { GameDetails = details };
            var viewModel = new BadmintonViewModel(sportsbookDto, new ResultSetting());

            // Act
            var actual = viewModel.ResultDetailItems;

            // Assert
            Assert.AreEqual("7120103", actual[0].ResourceID);
            Assert.AreEqual("7130105", actual[1].ResourceID);
            Assert.AreEqual("7120203", actual[2].ResourceID);
            Assert.AreEqual("7130303", actual[3].ResourceID);
            Assert.AreEqual("7130407", actual[4].ResourceID);
        }
    }
}