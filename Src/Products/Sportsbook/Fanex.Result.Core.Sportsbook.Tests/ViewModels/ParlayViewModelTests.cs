namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using System.Collections.Generic;
    using Base;
    using Base.Exception;
    using Base.Utilities;
    using Base.ViewModels;
    using DataDomain;
    using DTO;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.ViewModels;
    using TestUtils;

    [TestFixture]
    public class ParlayViewModelTests
    {
        private Parlay _parlay;
        private ResultSetting _setting;
        private IRefDataHelper _refDataHelper;

        [SetUp]
        public void Setup()
        {
            _parlay = new Parlay();
            _setting = new ResultSetting();
            _refDataHelper = Substitute.For<IRefDataHelper>();
        }

        [Test]
        public void Render_FromSportIdIsZero_ReturnsEmpty()
        {
            // Arrage
            _parlay.SportId = 0;
            var viewModel = new ParlayViewModel(_parlay, _setting);

            // Act
            var result = viewModel.Render(new RazorBinderModel());

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void Render_FromInvalidSportIdNotZero_ThrowsException()
        {
            // Arrage
            _parlay.SportId = 99999;
            var viewModel = new ParlayViewModel(_parlay, _setting);

            // Act
            TestDelegate testDelegate = () => viewModel.Render(new RazorBinderModel());

            // Assert
            Assert.Throws<ResultCoreException>(testDelegate);
        }

        [Test]
        public void Render_TemplateContentIsNullOrEmpty_ReturnsEmpty()
        {
            // Arrage
            _parlay.SportId = (int)SportType.Soccer;
            var viewModel = new ParlayViewModel(_parlay, _setting);

            // Act
            var result = viewModel.Render(new RazorBinderModel());

            // Assert
            Assert.AreEqual(TemplateFileUtils.GetHtmlContentFromTestTemplate("Soccer.html"), TemplateFileUtils.RemoveEndLineSymbolHtml(result));
        }

        [Test]
        public void SportName_Always_GetSportNameByLanguage()
        {
            // Arrange
            _parlay.SportId = 1;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetSportName(1, "en-US", Arg.Any<string>()).Returns("SportName");

            // Act
            var viewModel = new ParlayViewModel(_parlay, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("SportName", viewModel.SportName);
        }

        [Test]
        public void LeaugeName_Always_GetLeagueNameByLanguage()
        {
            // Arrange
            _parlay.LeagueId = 1;
            _setting.LanguageCode = "en-US";
            _refDataHelper.GetLeagueName(1, "en-US", Arg.Any<string>()).Returns("League");

            // Act
            var viewModel = new ParlayViewModel(_parlay, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual("League", viewModel.LeagueName);
        }

        [Test]
        public void LeaugeId_Always_ReturnLeagueId()
        {
            // Arrange
            _parlay.LeagueId = 1;

            // Act
            var viewModel = new ParlayViewModel(_parlay, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual(1, viewModel.LeagueId);
        }

        [Test]
        public void SportId_Always_ReturnSportId()
        {
            // Arrange
            _parlay.SportId = 1;

            // Act
            var viewModel = new ParlayViewModel(_parlay, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual(1, viewModel.SportId);
        }

        [Test]
        public void MatchItemView_OnlyMatchItemViewModelIsNull_ReturnNewItemViewModel()
        {
            // Arrange
            _parlay.SportId = (int)SportType.Soccer;
            _parlay.Sportbook = new List<Sportsbook>()
            {
                new Sportsbook{ GameDetails = new List<GameDetail>(){CreateGameDetailWithValidGameResourceId() }},
                new Sportsbook()
            };

            // Act
            var viewModel = new ParlayViewModel(_parlay, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual(1, viewModel.MatchItemView.Count);
        }

        [Test]
        public void GameDetailItemView_OnlyGameDetailItemViewModelIsNull_ReturnNewItemViewModel()
        {
            // Arrange
            _parlay.SportId = (int)SportType.Tennis;
            _parlay.Sportbook = new List<Sportsbook>()
            {
                new Sportsbook { Bettype = BetTypeID.SetXGameYWinner, BetID = "80501" },
                new Sportsbook()
            };

            // Act
            var viewModel = new ParlayViewModel(_parlay, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual(1, viewModel.GameDetailItemView.Count);
        }

        [Test]
        public void BothMatchAndDetailItemView_BothMatchAndDetailItemViewModelIsNull_ReturnNewItemViewModel()
        {
            // Arrange
            _parlay.SportId = (int)SportType.Soccer;
            _parlay.Sportbook = new List<Sportsbook>()
            {
                new Sportsbook{ GameDetails = new List<GameDetail>(){ CreateGameDetailWithValidGameResourceId() }},
                new Sportsbook()
            };

            // Act
            var viewModel = new ParlayViewModel(_parlay, _setting, _refDataHelper);

            // Assert
            Assert.AreEqual(1, viewModel.BothMatchAndDetailItemView.Count);
        }

        private GameDetail CreateGameDetailWithValidGameResourceId()
            => new GameDetail { ResourceId = GameResourceID.FirstTenMinutes1X2 };
    }
}