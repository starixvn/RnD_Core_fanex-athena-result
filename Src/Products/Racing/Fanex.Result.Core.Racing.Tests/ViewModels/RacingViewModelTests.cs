namespace Fanex.Result.Core.Racing.Tests.ViewModels
{
    using System;
    using Base;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using NSubstitute;
    using NUnit.Framework;
    using Racing.ViewModels;

    [TestFixture]
    public class RacingViewModelTests
    {
        private Racing _racing;
        private ResultSetting _setting;
        private IRefDataHelper _refDataHelper;

        [SetUp]
        public void Setup()
        {
            _racing = new Racing();
            _setting = new ResultSetting { LanguageCode = "en-US" };
            _refDataHelper = Substitute.For<IRefDataHelper>();
            ResultCoreManager.RegisterRefDataHelper(_refDataHelper);
        }

        [Test]
        public void LeagueId_Always_GetFromRacing()
        {
            // Arrange
            _racing.LeagueId = 1;

            // Act
            var viewModel = new RacingViewModel(_racing, _setting);

            // Assert
            Assert.AreEqual(1, viewModel.LeagueId);
        }

        [Test]
        public void LeagueName_Always_GetFromRemotingRef()
        {
            // Arrange
            _racing.LeagueId = 1;
            _refDataHelper.GetLeagueName(1, "en-US", string.Empty).Returns("LeagueName");

            // Act
            var viewModel = new VSHRacingViewModel(_racing, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("LeagueName", viewModel.LeagueName);
        }

        [Test]
        public void KickOffTime_Always_GetFromRacing()
        {
            // Arrange
            _racing.KickOffTime = DateTime.Parse("10/10/2017");

            // Act
            var viewModel = new VSHRacingViewModel(_racing, _setting);

            // Assert
            Assert.AreEqual("10/10/2017<br/>12:00:00 AM", viewModel.KickOffTime);
        }

        [Test]
        public void EvenTime_Always_GetFromRacing()
        {
            // Arrange
            _racing.KickOffTime = DateTime.Parse("10/10/2017");

            // Act
            var viewModel = new VSHRacingViewModel(_racing, _setting);

            // Assert
            Assert.AreEqual(_racing.KickOffTime, viewModel.EvenTime);
        }

        [Test]
        public void Place_Always_GetFromRacing()
        {
            // Arrange
            _racing.Place = 1;

            // Act
            var viewModel = new VSHRacingViewModel(_racing, _setting);

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
            var viewModel = new VSHRacingViewModel(_racing, _setting);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.Placing);
        }

        [Test]
        public void HorseNo_Always_GetFromRacing()
        {
            // Arrange
            _racing.EngineNo = "1";

            // Act
            var viewModel = new VSHRacingViewModel(_racing, _setting);

            // Assert
            Assert.AreEqual("1", viewModel.HorseNo);
        }

        [Test]
        public void Result_Always_GetHorseTeamNameFromRef()
        {
            // Arrange
            _racing.EngineId = "1";
            _refDataHelper.GetHorseTeamName("1", "en-US", string.Empty).Returns("Team");

            // Act
            var viewModel = new VSHRacingViewModel(_racing, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("Team", viewModel.Result);
        }

        [Test]
        public void WinOdd_WinOddsIsGreaterThanZero_ReturnWinOdds()
        {
            // Arrange
            _racing.WinOdds = 1;

            // Act
            var viewModel = new VSHRacingViewModel(_racing, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("1.00", viewModel.WinOdd);
        }

        [Test]
        public void WinOdd_WinOddsIsZero_ReturnEmpty()
        {
            // Arrange
            _racing.WinOdds = 0;

            // Act
            var viewModel = new VSHRacingViewModel(_racing, _setting, _refDataHelper);

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
            var viewModel = new VSHRacingViewModel(_racing, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual(result, viewModel.PlaceOdd);
        }

        [Test]
        public void Status_Always_ReturnFromRacing()
        {
            // Arrange
            _racing.Status = "completed";

            // Act
            var viewModel = new VSHRacingViewModel(_racing, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("Completed", viewModel.Status);
        }

        [Test]
        public void RowCount_Always_ReturnFromRacing()
        {
            // Arrange
            _racing.RowCount = 10;

            // Act
            var viewModel = new VSHRacingViewModel(_racing, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual(10, viewModel.RowCount);
        }
    }
}