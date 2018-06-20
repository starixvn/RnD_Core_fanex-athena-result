namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using Base;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.ViewModels;

    [TestFixture]
    public class OtherViewModelTests
    {
        private IRefDataHelper _refDataHelper;
        private ResultSetting _resultSetting;

        [SetUp]
        public void Setup()
        {
            _resultSetting = new ResultSetting
            {
                LanguageCode = "en-US"
            };
            _refDataHelper = Substitute.For<IRefDataHelper>();
            _refDataHelper.GetBetRadarMatchCodeFormat(Arg.Any<string>(), Arg.Any<string>()).Returns("MatchCode");
        }

        [TestCase(1, "1")]
        [TestCase(null, "-")]
        public void HTHomeScore_ScoreHasValue_ShowScore(int? scoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { HtHomeScore = scoreValue, MatchCode = "124" };
            var viewModel = new OtherViewModel(sportsbookDto, _resultSetting, _refDataHelper);

            // Act
            var actualHTHomeScore = viewModel.HTHomeScore;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualHTHomeScore);
        }

        [TestCase(1, "1")]
        [TestCase(null, "-")]
        public void HTAwayScore_ScoreHasValue_ShowScore(int? scoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { HtAwayScore = scoreValue };
            var viewModel = new OtherViewModel(sportsbookDto, _resultSetting);

            // Act
            var actualHTAwayScore = viewModel.HTAwayScore;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualHTAwayScore);
        }

        [TestCase(1, "1")]
        [TestCase(null, "-")]
        public void FinalHomeScore_ScoreHasValue_ShowScore(int? scoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalHomeScore = scoreValue };
            var viewModel = new OtherViewModel(sportsbookDto, _resultSetting);

            // Act
            var actualFinalHomeScore = viewModel.FinalHomeScore;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualFinalHomeScore);
        }

        [TestCase(1, "1")]
        [TestCase(null, "-")]
        public void FinalAwayScore_ScoreHasValue_ShowScore(int? scoreValue, string expectedScoreValue)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { FinalAwayScore = scoreValue };
            var viewModel = new OtherViewModel(sportsbookDto, _resultSetting);

            // Act
            var actualFinalAwayScore = viewModel.FinalAwayScore;

            // Assert
            Assert.AreEqual(expectedScoreValue, actualFinalAwayScore);
        }

        [TestCase(18, "{2} 23, {0} 6666", "")]
        [TestCase(190, "{2} 23, {0} 6666", " (Match Day 23, Season 6666)")]
        [TestCase(191, "{5} 23, {10} 6666", " (Match Day 1 Groups E&F 23, Match Day 3 Groups C&D 6666)")]
        public void MatchInfo_IfSportTypeIsVirtualSports_AddMatchInfo(int sportType, string matchCode, string expectedMatchInfo)
        {
            // Arrange
            var sportsbookDto = new Sportsbook { HomeId = 1, MatchCode = matchCode };
            var resultSetting = new ResultSetting { SportType = (SportType)sportType, LanguageCode = "en-US" };
            _refDataHelper.GetTeamName(Arg.Any<int>(), Arg.Any<string>(), Arg.Any<string>()).Returns("Home Team Name");
            _refDataHelper.GetBetRadarMatchCodeFormat("{2} 23, {0} 6666", Arg.Any<string>()).Returns("Match Day 23, Season 6666");
            _refDataHelper.GetBetRadarMatchCodeFormat("{5} 23, {10} 6666", Arg.Any<string>()).Returns("Match Day 1 Groups E&F 23, Match Day 3 Groups C&D 6666");
            var viewModel = new OtherViewModel(sportsbookDto, resultSetting, _refDataHelper);

            // Act
            var actualHomeTeam = viewModel.MatchInfo;

            // Assert
            Assert.AreEqual(expectedMatchInfo, actualHomeTeam);
        }
    }
}