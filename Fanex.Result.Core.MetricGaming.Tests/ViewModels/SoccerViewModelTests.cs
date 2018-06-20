namespace Fanex.Result.Core.MetricGaming.Tests.ViewModels
{
    using Base;
    using Base.Utilities;
    using MetricGaming.DTO;
    using MetricGaming.ViewModels;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class SoccerViewModelTests
    {
        private MetricGaming _metricGaming;
        private ResultSetting _setting;
        private IRefDataHelper _refDataHelper;

        [SetUp]
        public void Setup()
        {
            _metricGaming = new MetricGaming();
            _setting = new ResultSetting() { LanguageCode = "en-US" };
            _refDataHelper = Substitute.For<IRefDataHelper>();
            ResultCoreManager.RegisterRefDataHelper(Substitute.For<IRefDataHelper>());
        }

        [Test]
        public void LeagueId_Always_ReturnLeagueId()
        {
            // Arrange
            _metricGaming.LeagueId = 1;

            // Act
            var viewModel = new SoccerViewModel(_metricGaming, _setting);

            // Assert
            Assert.AreEqual(1, viewModel.LeagueId);
        }

        [Test]
        public void HomeTeam_Always_GetFromRemotingRef()
        {
            // Arrange
            _metricGaming.HomeId = "1";
            _refDataHelper.GetTeamName("1", "en", string.Empty).Returns("Home Team");

            // Act
            var viewModel = new SoccerViewModel(_metricGaming, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("Home Team", viewModel.HomeTeam);
        }

        [Test]
        public void AwayTeam_Always_GetFromRemotingRef()
        {
            // Arrange
            _metricGaming.AwayId = "2";
            _refDataHelper.GetTeamName("2", "en", string.Empty).Returns("Away Team");

            // Act
            var viewModel = new SoccerViewModel(_metricGaming, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("Away Team", viewModel.AwayTeam);
        }

        [Test]
        public void GameId_Always_ReturnGameId()
        {
            // Arrange
            _metricGaming.GameId = 1;

            // Act
            var viewModel = new SoccerViewModel(_metricGaming, _setting);

            // Assert
            Assert.AreEqual(1, viewModel.GameId);
        }

        [Test]
        public void EventName_BetTypeTimeIsEmpty_ReturnEventName()
        {
            // Arrange
            _metricGaming.GameId = 1;
            _metricGaming.BetTypeTime = string.Empty;
            _refDataHelper.GetBetTypeNameById("1", "en", string.Empty).Returns("game name");

            // Act
            var viewModel = new SoccerViewModel(_metricGaming, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("game name ", viewModel.EventName);
        }

        [Test]
        public void EventName_BetTypeTimeIsNotEmpty_And_NotExistComma_ReturnEventNameWithComposeTime()
        {
            // Arrange
            _metricGaming.GameId = 1;
            _metricGaming.BetTypeTime = "1";
            _refDataHelper.GetBetTypeNameById("1", "en", string.Empty).Returns("game name");
            _refDataHelper.GetTeamName("1", "en", string.Empty).Returns("Away Team");

            // Act
            var viewModel = new SoccerViewModel(_metricGaming, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("game name 1", viewModel.EventName);
        }

        [Test]
        public void EventName_BetTypeTimeIsNotEmpty_ReturnEventNameWithComposeTime()
        {
            // Arrange
            _metricGaming.GameId = 1;
            _metricGaming.BetTypeTime = "1,57%";
            _refDataHelper.GetBetTypeNameById("1", "en", string.Empty).Returns("game name");
            _refDataHelper.GetTeamName("1", "en", string.Empty).Returns("Away Team");

            // Act
            var viewModel = new SoccerViewModel(_metricGaming, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("game name Away Team,57%", viewModel.EventName);
        }

        [Test]
        public void EventResult_GameResultIsEmpty_ReturnEmptyEventResult()
        {
            // Arrange
            _metricGaming.GameResult = string.Empty;

            // Act
            var viewModel = new SoccerViewModel(_metricGaming, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual(0, viewModel.EventResult.Count);
        }

        [Test]
        public void EventResult_GameResultIsNotEmpty_ReturnEventResult()
        {
            // Arrange
            _metricGaming.GameResult = "1,2,3";
            _metricGaming.OtherInfo = "22:30,12:10";
            _refDataHelper.GetResourceDataByRefIdAndResourceId("SuperLive", "1", "en", string.Empty).Returns("Corner Kick");
            _refDataHelper.GetResourceDataByRefIdAndResourceId("SuperLive", "2", "en", string.Empty).Returns("Goal Kick");
            _refDataHelper.GetResourceDataByRefIdAndResourceId("SuperLive", "3", "en", string.Empty).Returns("Off Side");

            // Act
            var viewModel = new SoccerViewModel(_metricGaming, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("Corner Kick 22:30", viewModel.EventResult[0]);
            Assert.AreEqual("Goal Kick 12:10", viewModel.EventResult[1]);
            Assert.AreEqual("Off Side ", viewModel.EventResult[2]);
        }

        [Test]
        public void EventResult_GameResultIsNotEmpty_OtherInfoIsEmpty_ReturnEventResult()
        {
            // Arrange
            _metricGaming.GameResult = "1";
            _metricGaming.OtherInfo = "";
            _refDataHelper.GetResourceDataByRefIdAndResourceId("SuperLive", "1", "en", string.Empty).Returns("Corner Kick");

            // Act
            var viewModel = new SoccerViewModel(_metricGaming, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("Corner Kick ", viewModel.EventResult[0]);
        }

        [Test]
        public void IsShowNoInfoRow_EventResultCountIsZero_ReturnTrue()
        {
            // Act
            var viewModel = new SoccerViewModel(_metricGaming, _setting, _refDataHelper);

            // Assert
            Assert.True(viewModel.IsShowNoInfoRow);
        }

        [TestCase(0, true)]
        [TestCase(1, false)]
        public void IsShowNoInfoRow_EventResultCountIsNotZero_WithSettlementStatus_ReturnExpectedResult(int statusId, bool expectedResult)
        {
            // Arrange
            _metricGaming.GameResult = "1,2,3";
            _metricGaming.OtherInfo = "22:30,12:10";
            _metricGaming.SettlementStatusId = statusId;
            _refDataHelper.GetResourceDataByRefIdAndResourceId("SuperLive", "1", "en", string.Empty).Returns("Corner Kick");
            _refDataHelper.GetResourceDataByRefIdAndResourceId("SuperLive", "2", "en", string.Empty).Returns("Goal Kick");

            // Act
            var viewModel = new SoccerViewModel(_metricGaming, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.IsShowNoInfoRow);
        }
    }
}