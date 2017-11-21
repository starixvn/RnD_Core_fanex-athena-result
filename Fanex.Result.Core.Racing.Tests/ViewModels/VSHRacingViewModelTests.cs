namespace Fanex.Result.Core.Racing.Tests.ViewModels
{
    using Base;
    using DTO;
    using NSubstitute;
    using NUnit.Framework;
    using Racing.ViewModels;
    using Utilities;

    [TestFixture]
    public class VSHRacingViewModelTests
    {
        private Racing _racing;
        private ResultSetting _setting;
        private IRacingRefDataHelper _refDataHelper;

        [SetUp]
        public void Setup()
        {
            _racing = new Racing();
            _setting = new ResultSetting { LanguageCode = "en-US" };
            _refDataHelper = Substitute.For<IRacingRefDataHelper>();
        }

        [Test]
        public void LeagueId_Always_GetFromRacing()
        {
            // Arrange
            _racing.LeagueId = 1;

            // Act
            var viewModel = new VSHRacingViewModel(_racing, _setting);

            // Assert
            Assert.AreEqual(1, viewModel.LeagueId);
        }

        [Test]
        public void LeagueName_Always_GetFromRemotingRef()
        {
            // Arrange
            _racing.LeagueId = 1;
            _refDataHelper.GetLeagueName(1, "en", string.Empty).Returns("LeagueName");

            // Act
            var viewModel = new VSHRacingViewModel(_racing, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("LeagueName", viewModel.LeagueName);
        }

        [Test]
        public void RaceName_Always_GetFromRacing()
        {
            // Arrange
            _racing.RaceName = 1;

            // Act
            var viewModel = new RacingViewModel(_racing, _setting);

            // Assert
            Assert.AreEqual("1", viewModel.RaceName);
        }

        [Test]
        public void Place_Always_GetFromRacing()
        {
            // Arrange
            _racing.Place = 1;

            // Act
            var viewModel = new RacingViewModel(_racing, _setting);

            // Assert
            Assert.AreEqual(1, viewModel.Place);
        }

        [TestCase(1, "1st")]
        [TestCase(2, "2nd")]
        [TestCase(3, "3rd")]
        [TestCase(4, "4th")]
        [TestCase(5, "5th")]
        public void Placing_ForEachPlace_ReturnExpectedPlace(int place, string expectedResult)
        {
            // Arrange
            _racing.Place = place;

            // Act
            var viewModel = new RacingViewModel(_racing, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.Placing);
        }

        [Test]
        public void HorseNo_Always_GetFromRacing()
        {
            // Arrange
            _racing.EngineNo = "1";

            // Act
            var viewModel = new RacingViewModel(_racing, _setting);

            // Assert
            Assert.AreEqual("1", viewModel.HorseNo);
        }

        [Test]
        public void Result_Always_GetHorseTeamNameFromRef()
        {
            // Arrange
            _racing.EngineId = "1";
            _refDataHelper.GetHorseTeamName("1", "en", string.Empty).Returns("Team");

            // Act
            var viewModel = new RacingViewModel(_racing, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("Team", viewModel.Result);
        }

        [Test]
        public void WinOdd_WinOddsIsGreaterThanZero_ReturnWinOdds()
        {
            // Arrange
            _racing.WinOdds = 1;

            // Act
            var viewModel = new RacingViewModel(_racing, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("1.00", viewModel.WinOdd);
        }

        [Test]
        public void WinOdd_WinOddsIsZero_ReturnEmpty()
        {
            // Arrange
            _racing.WinOdds = 0;

            // Act
            var viewModel = new RacingViewModel(_racing, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("", viewModel.WinOdd);
        }

        [TestCase(1, "1.00")]
        [TestCase(0, "")]
        public void PlaceOdd_WithOddValue_ReturnExpectedResult(decimal odds, string result)
        {
            // Arrange
            _racing.PlaceOdds = odds;

            // Act
            var viewModel = new RacingViewModel(_racing, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual(result, viewModel.PlaceOdd);
        }

        [Test]
        public void IsSCR_Always_ReturnFromRacing()
        {
            // Arrange
            _racing.IsSCR = true;

            // Act
            var viewModel = new RacingViewModel(_racing, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual(true, viewModel.IsSCR);
        }

        [Test]
        public void IsFavor_Always_ReturnFromRacing()
        {
            // Arrange
            _racing.IsFavor = true;

            // Act
            var viewModel = new RacingViewModel(_racing, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual(true, viewModel.IsFavor);
        }

        [Test]
        public void SCR_Always_ReturnFromRacing()
        {
            // Arrange
            _racing.SCROfMatch = "ccc";

            // Act
            var viewModel = new RacingViewModel(_racing, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("ccc", viewModel.SCR);
        }

        [Test]
        public void TF_Always_ReturnFromRacing()
        {
            // Arrange
            _racing.TFOfMatch = "ccc";

            // Act
            var viewModel = new RacingViewModel(_racing, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("ccc", viewModel.TF);
        }

        [Test]
        public void RowCount_Always_ReturnFromRacing()
        {
            // Arrange
            _racing.RowCount = 10;

            // Act
            var viewModel = new RacingViewModel(_racing, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual(10, viewModel.RowCount);
        }
    }
}