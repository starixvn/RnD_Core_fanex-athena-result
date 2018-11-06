namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using Base;
    using NUnit.Framework;
    using Sportsbook.ViewModels;

    [TestFixture]
    internal class MuayThaiViewModelTests
    {
        private DTO.Sportsbook _sportsbook;
        private ResultSetting _setting;

        [SetUp]
        public void Setup()
        {
            _sportsbook = new DTO.Sportsbook();
            _setting = new ResultSetting();
        }

        [TestCase(0, 0)]
        [TestCase(null, 0)]
        [TestCase(0, null)]
        [TestCase(0, 2)]
        public void IsValidScore_ScoreHasNoMuayThaiValue_ReturnFalse(int? homeScore, int? awayScore)
        {
            // Arrange
            _sportsbook.FinalAwayScore = homeScore;
            _sportsbook.FinalHomeScore = awayScore;

            // Act
            var muaythaiViewModel = new MuayThaiViewModel(_sportsbook, _setting);

            // Assert
            Assert.False(muaythaiViewModel.IsValidScore);
        }

        [TestCase(0, 5)]
        [TestCase(0, 1)]
        [TestCase(1, 5)]
        public void IsValidScore_ScoreHasMuayThaiValue_ReturnFalse(int? homeScore, int? awayScore)
        {
            // Arrange
            _sportsbook.FinalAwayScore = homeScore;
            _sportsbook.FinalHomeScore = awayScore;

            // Act
            var muaythaiViewModel = new MuayThaiViewModel(_sportsbook, _setting);

            // Assert
            Assert.True(muaythaiViewModel.IsValidScore);
        }

        [TestCase(1, 1, "Draw")]
        [TestCase(5, 1, "Won")]
        [TestCase(1, 5, "Lose")]
        [TestCase(0, 0, "0")]
        [TestCase(null, null, "-")]
        public void FinalHomeScore_IsValidScore_ReturnExpectedResult(int? homeScore, int? awayScore, string expectedResult)
        {
            // Arrange
            _sportsbook.FinalAwayScore = awayScore;
            _sportsbook.FinalHomeScore = homeScore;

            // Act
            var muaythaiViewModel = new MuayThaiViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(expectedResult, muaythaiViewModel.FinalHomeScore);
        }

        [TestCase(1, 1, "Draw")]
        [TestCase(5, 1, "Lose")]
        [TestCase(1, 5, "Won")]
        [TestCase(0, 0, "0")]
        [TestCase(null, null, "-")]
        public void FinalAwayScore_IsValidScore_ReturnExpectedResult(int? homeScore, int? awayScore, string expectedResult)
        {
            // Arrange
            _sportsbook.FinalAwayScore = awayScore;
            _sportsbook.FinalHomeScore = homeScore;

            // Act
            var muaythaiViewModel = new MuayThaiViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(expectedResult, muaythaiViewModel.FinalAwayScore);
        }

        [Test]
        public void KOInRound_IsNotValidScore_ReturnEmpty()
        {
            // Arrange
            _sportsbook.FinalAwayScore = null;
            _sportsbook.FinalHomeScore = null;

            // Act
            var muaythaiViewModel = new MuayThaiViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(string.Empty, muaythaiViewModel.KOInRound);
        }

        [Test]
        public void KOInRound_EventStatusIsNotCompleted_ReturnEmpty()
        {
            // Arrange
            _sportsbook.FinalAwayScore = 1;
            _sportsbook.FinalHomeScore = 5;
            _sportsbook.EventStatus = "Running";

            // Act
            var muaythaiViewModel = new MuayThaiViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(string.Empty, muaythaiViewModel.KOInRound);
        }

        [TestCase(5, 1, "5 rounds completed")]
        [TestCase(1, 5, "5 rounds completed")]
        [TestCase(1, 0, "KO in round 122")]
        public void KOInRound_IsValidScore_And_EventStatusIsCompleted_ReturnExpectedResult(int? homeScore, int? awayScore, string expectedResult)
        {
            // Arrange
            _sportsbook.FinalAwayScore = awayScore;
            _sportsbook.FinalHomeScore = homeScore;
            _sportsbook.EventStatus = "Completed";
            _sportsbook.BetStatus = "12;2";

            // Act
            var muaythaiViewModel = new MuayThaiViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(expectedResult, muaythaiViewModel.KOInRound);
        }

        [TestCase(5, 1, true, "win")]
        [TestCase(5, 1, false, "")]
        [TestCase(1, 5, true, "")]
        [TestCase(1, 5, false, "win")]
        public void GetWonTeam_IsHome_IsValidScore_ReturnWin(int? homeScore, int? awayScore, bool isHome, string expectedResult)
        {
            // Arrange
            _sportsbook.FinalHomeScore = homeScore;
            _sportsbook.FinalAwayScore = awayScore;
            _setting.HighlightWinnerTeam = true;

            // Act
            var muaythaiViewModel = new MuayThaiViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(expectedResult, muaythaiViewModel.GetWonTeam(isHome));
        }

        [TestCase(null, 1, true, "")]
        public void GetWonTeam_IsNotValidScore_ReturnEmpty(int? homeScore, int? awayScore, bool isHome, string expectedResult)
        {
            // Arrange
            _sportsbook.FinalHomeScore = homeScore;
            _sportsbook.FinalAwayScore = awayScore;
            _setting.HighlightWinnerTeam = true;

            // Act
            var muaythaiViewModel = new MuayThaiViewModel(_sportsbook, _setting);

            // Assert
            Assert.AreEqual(expectedResult, muaythaiViewModel.GetWonTeam(isHome));
        }
    }
}