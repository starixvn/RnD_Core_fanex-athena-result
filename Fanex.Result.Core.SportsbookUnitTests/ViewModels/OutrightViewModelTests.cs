namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using System;
    using Base;
    using DTO;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.Utilities;
    using Sportsbook.ViewModels;

    [TestFixture]
    public class OutrightViewModelTests
    {
        private Outright _outright;
        private ResultSetting _setting;
        private ISportsbookRefDataHelper _refDataHelper;

        [SetUp]
        public void Setup()
        {
            _outright = new Outright();
            _setting = new ResultSetting();
            _refDataHelper = Substitute.For<ISportsbookRefDataHelper>();
        }

        [Test]
        public void SportId_Always_ReturnSportType()
        {
            // Arrange
            _outright.SportType = (int)SportType.Soccer;

            // Act
            var viewModel = new OutrightViewModel(_outright, _setting);

            // Assert
            Assert.AreEqual((int)SportType.Soccer, viewModel.SportId);
        }

        [Test]
        public void LeagueId_Always_ReturnLeagueId()
        {
            // Arrange
            _outright.LeagueId = 1;

            // Act
            var viewModel = new OutrightViewModel(_outright, _setting);

            // Assert
            Assert.AreEqual(1, viewModel.LeagueId);
        }

        [Test]
        public void LeagueName_Always_ReturnLeagueNameByLanguage()
        {
            // Arrange
            _outright.LeagueId = 1;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetLeagueName(1, "en", Arg.Any<string>()).Returns("LeagueName");

            // Act
            var viewModel = new OutrightViewModel(_outright, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("LeagueName", viewModel.LeagueName);
        }

        [Test]
        public void SportName_Always_ReturnSportNameByLanguage()
        {
            // Arrange
            _outright.SportType = 1;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetSportName(1, "en", Arg.Any<string>()).Returns("Sport");

            // Act
            var viewModel = new OutrightViewModel(_outright, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("Sport", viewModel.SportName);
        }

        [Test]
        public void Team_Always_ReturnTeamNameByLanguage()
        {
            // Arrange
            _outright.TeamId = 1;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetTeamName(1, "en", Arg.Any<string>()).Returns("TeamName");

            // Act
            var viewModel = new OutrightViewModel(_outright, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("TeamName", viewModel.Team);
        }

        [TestCase("Running", "Running")]
        [TestCase("", "-")]
        [TestCase("running", "Running")]
        public void EventStatus_Always_ReturnEventStatus(string status, string expectedStatus)
        {
            // Arrange
            _outright.Status = status;

            // Act
            var viewModel = new OutrightViewModel(_outright, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual(expectedStatus, viewModel.EventStatus);
        }

        [Test]
        public void KickOffTime_Always_ReturnFullDateFormatTime()
        {
            // Arrange
            _outright.EventDate = new DateTime(2016, 10, 01, 01, 01, 10, 900);
            _setting.DateTimeFormat = "{0:MM/dd/yyyy<br/>hh:mm:ss tt}";

            // Act
            var viewModel = new OutrightViewModel(_outright, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("10/01/2016<br/>01:01:10 AM", viewModel.KickOffTime);
        }
    }
}