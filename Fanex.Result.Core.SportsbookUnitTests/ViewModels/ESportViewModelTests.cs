namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels
{
    using System.Collections.Generic;
    using DTO;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.ESport.DTO;
    using Fanex.Result.Core.Sportsbook.ViewModels;
    using Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail;
    using NSubstitute;
    using NUnit.Framework;

    public class ESportViewModelTests
    {
        private Sportsbook _sportsbook;
        private ResultSetting _setting;
        private IResultDetail<MapResult> _resultDetail;
        private IRefDataHelper _refDataHelper;

        [SetUp]
        public void Setup()
        {
            _sportsbook = new Sportsbook();
            _setting = new ResultSetting();
            _resultDetail = Substitute.For<IResultDetail<MapResult>>();
            _resultDetail.GetDetailItems().Returns(new SafeList<MapResult>());
            _refDataHelper = Substitute.For<IRefDataHelper>();
            ResultCoreManager.RegisterRefDataHelper(_refDataHelper);
        }

        [Test]
        public void GetHomeAwaySet_FromIndexIsGreaterThan9_ReturnsHyphen()
        {
            // Arrange
            var viewModel = new ESportViewModel(_sportsbook, new ResultSetting(), _resultDetail, _refDataHelper);

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(12, false);

            // Assert
            Assert.AreEqual("-", homeAwaySet);
        }

        [TestCase(0, true, "1")]
        [TestCase(1, true, "R")]
        [TestCase(2, true, "3")]
        [TestCase(3, true, "-")]
        [TestCase(4, true, "-")]
        [TestCase(5, true, "-")]
        [TestCase(6, true, "7")]
        [TestCase(7, true, "8")]
        [TestCase(8, true, "9")]
        [TestCase(9, true, "-")]
        [TestCase(10, true, "-")]
        [TestCase(0, false, "3")]
        [TestCase(1, false, "R")]
        [TestCase(2, false, "6")]
        [TestCase(3, false, "-")]
        [TestCase(4, false, "-")]
        [TestCase(5, false, "-")]
        [TestCase(6, false, "2")]
        [TestCase(7, false, "1")]
        [TestCase(8, false, "3")]
        [TestCase(9, false, "-")]
        [TestCase(10, false, "-")]
        public void GetHomeAwaySet_Alway_ReturnData(int mapIndex, bool isHome, string expectedValue)
        {
            // Arrange
            var viewModel = new ESportViewModel(StubSportsbook(), new ResultSetting(), null, _refDataHelper);

            // Act
            var homeAwaySet = viewModel.GetHomeAwaySet(mapIndex, isHome);

            // Assert
            Assert.AreEqual(expectedValue, homeAwaySet);
        }

        [Test]
        public void FinalHomeScore_GameDetailsHasData_ReturnDataFromGameDetails()
        {
            // Arrange
            var viewModel = new ESportViewModel(StubSportsbook(), new ResultSetting(), null, _refDataHelper);

            // Act
            var score = viewModel.FinalHomeScore;

            // Assert
            Assert.AreEqual("100", score);
        }

        [Test]
        public void FinalAwayScore_GameDetailsHasData_ReturnDataFromGameDetails()
        {
            // Arrange
            var viewModel = new ESportViewModel(StubSportsbook(), new ResultSetting(), null, _refDataHelper);

            // Act
            var score = viewModel.FinalAwayScore;

            // Assert
            Assert.AreEqual("75", score);
        }

        [Test]
        public void FinalHomeScore_GameDetailsDoesNotHasData_ReturnDataFromSportsbookDto()
        {
            // Arrange
            _sportsbook.FinalHomeScore = 2;
            var viewModel = new ESportViewModel(_sportsbook, new ResultSetting(), null, _refDataHelper);

            // Act
            var score = viewModel.FinalHomeScore;

            // Assert
            Assert.AreEqual("2", score);
        }

        [Test]
        public void FinalAwayScore_GameDetailsDoesNotHasData_ReturnDataFromSportsbookDto()
        {
            // Arrange
            _sportsbook.FinalAwayScore = 1;
            var viewModel = new ESportViewModel(_sportsbook, new ResultSetting(), null, _refDataHelper);

            // Act
            var score = viewModel.FinalAwayScore;

            // Assert
            Assert.AreEqual("1", score);
        }

        [Test]
        public void MoreResultDetailByMap_Always_ReturnGroupedMap()
        {
            // Arrange
            var viewModel = new ESportViewModel(StubSportsbook(), new ResultSetting(), null, _refDataHelper);

            // Act
            var moreResult = viewModel.MoreResultDetailByMap;

            // Assert
            Assert.AreEqual(3, moreResult.Count);
            Assert.AreEqual(50, moreResult[1].Count);
            Assert.AreEqual(2, moreResult[2].Count);
            Assert.AreEqual(1, moreResult[4].Count);
        }

        ////[Test]
        ////public void Ctor_Always_GroupMoreResultByMap()
        ////{
        ////    var sportsbookData = StubSportsbook();
        ////}

        public static Sportsbook StubSportsbook()
            => new Sportsbook
            {
                Bettype = 1,
                EventStatus = "completed",
                FinalAwayScore = 10,
                FinalHomeScore = 11,
                GameDetails = StubGameDetails()
            };

        public static List<GameDetail> StubGameDetails()
            => new List<GameDetail>
            {
                new GameDetail { MatchId = 1, ResourceId = "900601", HomeValue = "A", AwayValue = "H", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "90070102", HomeValue = "N", AwayValue = "H", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "900201", HomeValue = "2", AwayValue = "4", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "90110102", HomeValue = "H", AwayValue = "H", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "900801", HomeValue = "1", AwayValue = "3", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "901501", HomeValue = "N", AwayValue = "H", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "901601", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "901701", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "901201", HomeValue = "4", AwayValue = "3", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "902101", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "902201", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "902301", HomeValue = "H", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "901801", HomeValue = "2", AwayValue = "1", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "90270101", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "902401", HomeValue = "2", AwayValue = "3", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "903101", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "903201", HomeValue = "H", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "903301", HomeValue = "6", AwayValue = "3", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "902801", HomeValue = "H", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "903701", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "903801", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "903901", HomeValue = "1", AwayValue = "3", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "903401", HomeValue = "H", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "904301", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "904401", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "904501", HomeValue = "1", AwayValue = "6", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "904001", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "904901", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "905001", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "905101", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "904601", HomeValue = "5", AwayValue = "2", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "905501", HomeValue = "H", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "905601", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "905701", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "905201", HomeValue = "3", AwayValue = "2", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "905801", HomeValue = "10", AwayValue = "1", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "90620102", HomeValue = "H", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "906301", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "906701", HomeValue = "H", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "906401", HomeValue = "D", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "905901", HomeValue = "3", AwayValue = "3", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "907401", HomeValue = "2", AwayValue = "1", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "906501", HomeValue = "1", AwayValue = "5", SettlementStatusID = 1 },
                new GameDetail { MatchId = 1, ResourceId = "906601", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "907501", HomeValue = "N", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "90680102", HomeValue = "H", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "90720102", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "90690102", HomeValue = "1", AwayValue = "5", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "90730102", HomeValue = "Y", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "90680101", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "00", HomeValue = "100", AwayValue = "75", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "M1", HomeValue = "1", AwayValue = "3", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "M2", HomeValue = "2", AwayValue = "4", SettlementStatusID = 1 },
                new GameDetail { MatchId = 1, ResourceId = "M3", HomeValue = "3", AwayValue = "6", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "M4", HomeValue = "4", AwayValue = "3", SettlementStatusID = 0 },
                new GameDetail { MatchId = 1, ResourceId = "M7", HomeValue = "7", AwayValue = "2", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "M8", HomeValue = "8", AwayValue = "1", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "M9", HomeValue = "9", AwayValue = "3", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "90680401", HomeValue = "A", AwayValue = "A", SettlementStatusID = 0 },
                new GameDetail { MatchId = 1, ResourceId = "90690202", HomeValue = "1", AwayValue = "4", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "90730202", HomeValue = "Y", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "90730201", HomeValue = "Y", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "90730203", HomeValue = "Y", AwayValue = "A", SettlementStatusID = 4 },
                new GameDetail { MatchId = 1, ResourceId = "90680401", HomeValue = "A", AwayValue = "A", SettlementStatusID = 4 }
            };
    }
}