namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using System;
    using Base;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.ViewModels;

    [TestFixture]
    public class SportsbookBaseViewModelTests
    {
        private ResultSetting _setting;
        private Sportsbook _sportsbook;
        private IRefDataHelper _refDataHelper;

        [SetUp]
        public void SetUp()
        {
            _sportsbook = new Sportsbook();
            _setting = new ResultSetting();
            _refDataHelper = Substitute.For<IRefDataHelper>();
        }

        [Test]
        public void GetHomeAwaySet_Always_ReturnsHyphen()
        {
            // Act
            var viewModel = new SportsbookBaseViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual("-", viewModel.GetHomeAwaySet(0, false));
        }

        [Test]
        public void VisibleDetailedLink_Always_GetFromSetting()
        {
            // Arrange
            _setting.VisibleDetailedLink = true;

            // Act
            var viewModel = new SportsbookBaseViewModel(_sportsbook, _setting);

            // Assert
            Assert.True(viewModel.VisibleDetailedLink);
        }

        [Test]
        public void KickOffTime_Always_GetFormatedKickOffTime()
        {
            // Arrange
            _setting.DateTimeFormat = "{0:MM/dd/yyyy<br/>hh:mm:ss tt}";
            _sportsbook.KickOffTime = new DateTime(2016, 10, 01, 01, 01, 10, 900);

            // Act
            var viewModel = new SportsbookBaseViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual("10/01/2016<br/>01:01:10 AM", viewModel.KickOffTime);
        }

        [Test]
        public void LeagueId_Always_GetDTO()
        {
            // Arrange
            _sportsbook.LeagueId = 1;

            // Act
            var viewModel = new SportsbookBaseViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(1, viewModel.LeagueId);
        }

        [Test]
        public void LeagueName_Always_GetFromRefData()
        {
            // Arrange
            _sportsbook.LeagueId = 1;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetLeagueName(1, "en", Arg.Any<string>()).Returns("League");

            // Act
            var viewModel = new SportsbookBaseViewModel(_sportsbook, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("League", viewModel.LeagueName);
        }

        [TestCase("Running", false, true, "Running")]
        [TestCase("Running", true, true, "Pending")]
        [TestCase("Internal", false, true, "Internal")]
        [TestCase("Internal", false, true, "Internal")]
        [TestCase("Internal", false, true, "Internal")]
        [TestCase("Internal", false, false, "Running")]
        [TestCase("Completed", false, false, "Completed")]
        public void EventStatus_Always_ReturnEventStatus(string status, bool isPending, bool visibleInternal, string expectedStatus)
        {
            // Arrange
            _sportsbook.EventStatus = status;
            _sportsbook.IsPending = isPending;
            _setting.VisibleInternalEventStatus = visibleInternal;

            // Act
            var viewModel = new SportsbookBaseViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(expectedStatus, viewModel.EventStatus);
        }

        [Test]
        public void VisibleMatchResult_Always_GetDefaultValue()
        {
            // Act
            var viewModel = new SportsbookBaseViewModel(_sportsbook, _setting, _refDataHelper);

            // Assert
            Assert.True(viewModel.VisibleMatchResult);
        }

        [Test]
        public void VisibleGameDetail_Always_GetDefaultValue()
        {
            // Act
            var viewModel = new SportsbookBaseViewModel(_sportsbook, _setting, _refDataHelper);

            // Assert
            Assert.False(viewModel.VisibleGameDetail);
        }

        [Test]
        public void MatchCode_Always_GetDTO()
        {
            // Arrange
            _sportsbook.MatchCode = "1";

            // Act
            var viewModel = new SportsbookBaseViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual("1", viewModel.MatchCode);
        }

        [Test]
        public void MatchId_Always_GetDTO()
        {
            // Arrange
            _sportsbook.MatchId = 1;

            // Act
            var viewModel = new SportsbookBaseViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual("1", viewModel.MatchId);
        }

        [TestCase(true, "HomeTeam(N)")]
        [TestCase(false, "HomeTeam")]
        public void HomeTeam_Always_GetFromRefData(bool isNeutral, string expectedResult)
        {
            // Arrange
            _sportsbook.HomeId = 1;
            _sportsbook.IsNeutral = isNeutral;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetTeamName(1, "en", string.Empty).Returns("HomeTeam");

            // Act
            var viewModel = new SportsbookBaseViewModel(_sportsbook, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.HomeTeam);
        }

        [Test]
        public void AwayTeam_Always_GetFromRefData()
        {
            // Arrange
            _sportsbook.AwayId = 1;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetTeamName(1, "en", string.Empty).Returns("AwayTeam");

            // Act
            var viewModel = new SportsbookBaseViewModel(_sportsbook, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("AwayTeam", viewModel.AwayTeam);
        }

        [Test]
        public void IsPending_Always_GetDTO()
        {
            // Arrange
            _sportsbook.IsPending = false;

            // Act
            var viewModel = new SportsbookBaseViewModel(_sportsbook, _setting);

            // Assert
            Assert.False(viewModel.IsPending);
        }

        [Test]
        public void HomeWin_SettingNotHighlightTeamWinner_ReturnEmpty()
        {
            // Arrange
            _setting.HighlightWinnerTeam = false;

            // Act
            var viewModel = new SportsbookBaseViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(string.Empty, viewModel.HomeWin);
        }

        [Test]
        public void HomeWin_FinalHomeScoreIsNull_ReturnEmpty()
        {
            // Arrange
            _setting.HighlightWinnerTeam = true;
            _sportsbook.FinalHomeScore = null;

            // Act
            var viewModel = new SportsbookBaseViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(string.Empty, viewModel.HomeWin);
        }

        [Test]
        public void HomeWin_FinalAwayScoreIsNull_ReturnEmpty()
        {
            // Arrange
            _setting.HighlightWinnerTeam = true;
            _sportsbook.FinalHomeScore = 1;
            _sportsbook.FinalAwayScore = null;

            // Act
            var viewModel = new SportsbookBaseViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(string.Empty, viewModel.HomeWin);
        }

        [TestCase(2, 1, "win")]
        [TestCase(1, 2, "")]
        public void HomeWin_HighLightTeamWinner_And_ScoreHasValue_ReturnEmpty(int? homeScore, int? awayScore, string expectedResult)
        {
            // Arrange
            _setting.HighlightWinnerTeam = true;
            _sportsbook.FinalHomeScore = homeScore;
            _sportsbook.FinalAwayScore = awayScore;

            // Act
            var viewModel = new SportsbookBaseViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.HomeWin);
        }

        [TestCase(1, 2, "win")]
        [TestCase(2, 1, "")]
        public void AwayWin_HighLightTeamWinner_And_ScoreHasValue_ReturnEmpty(int? homeScore, int? awayScore, string expectedResult)
        {
            // Arrange
            _setting.HighlightWinnerTeam = true;
            _sportsbook.FinalHomeScore = homeScore;
            _sportsbook.FinalAwayScore = awayScore;

            // Act
            var viewModel = new SportsbookBaseViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.AwayWin);
        }
    }
}