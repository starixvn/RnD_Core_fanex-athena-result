namespace Fanex.Result.Core.UnitTest.Models
{
    using System;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Models;
    using Fanex.Result.Core.ViewModels;
    using NUnit.Framework;

    /// <summary>
    /// Apply unit testing for ViewModelConcreteFactory class.
    /// </summary>
    [TestFixture]
    public class ViewModelConcreteFactoryTest
    {
        private const string EXTNAME = "ViewModel";
        private const string NAMESPACE = "Fanex.Result.Core";
        private const string Language = "en-US";
        private ViewModelFactory _viewModelFactory = ViewModelConcreteFactory.Instance;
        private Sportbook _sportsBookInfo = null;

        /// <summary>
        /// Constructor function.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _sportsBookInfo = new Sportbook
            {
                AwayId = 1,
                HomeId = 2,
                BetStatus = string.Empty,
                LeagueId = 1,
                MatchId = 1,
                EventStatus = string.Empty,
                FinalAwayScore = 1,
                HtAwayScore = 2,
                Color = string.Empty,
                FinalHomeScore = 1,
                HtHomeScore = 2,
                EventDate = DateTime.Parse("01-01-2014"),
                IsNeutral = false,
                IsPending = false,
                KickOffTime = DateTime.Parse("01-01-2014"),
                MatchCode = "01",
                GoalRecord = string.Empty,
                ResultValue = "6-1 *30-40 40-30* *40-0 40-0* *40-0 40-0* *40-0",
                IsHTCal = true,
                Calculate = true,
                RefNo = "12"
            };
        }

        /// <summary>
        /// Testing view model sports book when sport type is not exist.
        /// </summary>
        [Test]
        public void GetViewModel_NotExistSportType_ReturnNull()
        {
            // Arrange
            var sportType = (SportType)(-1);
            var lang = Resources.Instance.GetLanguage(Language);

            // ACT
            var result = _viewModelFactory.GetViewModel(_sportsBookInfo, sportType, Language, lang);

            // Assert
            Assert.IsNull(result, "Sporttype is not existed.");
        }

        /// <summary>
        /// Testing view model sports book when sport type is exist.
        /// </summary>
        [Test]
        public void GetViewModel_ExistSportType_ReturnViewModelForThisSport()
        {
            // Arrange
            var sportType = (SportType)1;
            var lang = Resources.Instance.GetLanguage(Language);

            // ACT
            var result = _viewModelFactory.GetViewModel(_sportsBookInfo, sportType, Language, lang);

            // Assert
            Assert.IsInstanceOf<SoccerViewModel>(result, "Sport type is existed.");
        }

        /// <summary>
        /// Testing view model outright when DTO name is exist.
        /// </summary>
        [Test]
        public void GetOutRightViewModel_ReturnOutRightViewModel()
        {
            // Arrange
            var lang = Resources.Instance.GetLanguage(Language);
            var sportsBookOutrightInfo = new OutRight();

            // ACT
            var result = _viewModelFactory.GetOutRightViewModel(sportsBookOutrightInfo, Language, lang);

            // Assert
            Assert.IsInstanceOf<OutRightViewModel>(result, "DTO Outright name is existed.");
        }

        /// <summary>
        /// Test this method when DTO name is exist.
        /// </summary>
        [Test]
        public void GetParlayViewModel_ReturnParlayViewModel()
        {
            // Arrange
            var lang = Resources.Instance.GetLanguage(Language);
            var sportsBookParlayInfo = new Parlay();

            // ACT
            var result = _viewModelFactory.GetParlayViewModel(sportsBookParlayInfo, Language, lang);

            // Assert
            Assert.IsInstanceOf<ParlayViewModel>(result, "DTO Parlay name is existed.");
        }

        [TearDown]
        public void TearDown()
        {
            _sportsBookInfo = null;
        }
    }
}