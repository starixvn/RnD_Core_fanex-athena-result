namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels.SoccerDetail
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using DataDomain;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using NSubstitute;
    using NUnit.Framework;
    using Sportsbook.Utilities;
    using Sportsbook.ViewModels.SoccerDetail;

    [TestFixture]
    public class FastMarketResultDetailTests
    {
        private IRefDataHelper _refDataHelper;
        private ISportsbookHelperWraper _sportsbookHelper;
        private Sportsbook _sportsbook;
        private ResultSetting _setting;

        [SetUp]
        public void SetUp()
        {
            _refDataHelper = Substitute.For<IRefDataHelper>();
            _refDataHelper.GetBetTypeNameById(Arg.Any<string>(), "en-US").Returns("bettype");
            _sportsbookHelper = Substitute.For<ISportsbookHelperWraper>();
            _sportsbook = new Sportsbook
            {
                BetID = "1"
            };
            _setting = new ResultSetting();
            _setting.LanguageCode = ConstantValue.EnglishLanguageCode;
            ResultCoreManager.RegisterRefDataHelper(Substitute.For<IRefDataHelper>());
        }

        [Test]
        public void GetDetailItems_Always_GetInFastMarketResourceId()
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail> {
                new GameDetail { ResourceId = GameResourceID.NextOneMinutes, HomeValue = "324234324234" },
            };
            var soccerResultDetail = new FastMarketResultDetail(_sportsbook, _setting, _refDataHelper, _sportsbookHelper);

            // Act
            var actualDetailItems = soccerResultDetail.GetDetailItems();

            // Assert
            Assert.AreEqual(1, actualDetailItems.Count);
        }

        [Test]
        public void CreateGameDetailItem_Always_CreateCorrectValue()
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail> {
                new GameDetail { ResourceId = GameResourceID.NextOneMinutes, HomeValue = "324234324234" },
            };
            _sportsbook.Bettype = 221;
            _sportsbook.BetID = "2";
            _refDataHelper.GetBetTypeNameById("221", "en-US").Returns("NextOneMinutes");
            var soccerResultDetail = new FastMarketResultDetail(_sportsbook, _setting, _refDataHelper, _sportsbookHelper);

            // Act
            var actualDetailItems = soccerResultDetail.GetDetailItems();

            // Assert
            Assert.AreEqual("22101", actualDetailItems.First().GroupCode);
            Assert.AreEqual("22101", actualDetailItems.First().ResourceID);
            Assert.AreEqual("NextOneMinutes", actualDetailItems.First().GroupName);
            Assert.AreEqual("02:00-02:59", actualDetailItems.First().TimeRange.First());
        }

        [Test]
        public void MapGameValueByPattern_221_IsFMTwo_CreateCorrectValue()
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail> {
                new GameDetail { ResourceId = GameResourceID.NextOneMinutes, HomeValue = "324234324234" },
                new GameDetail {
                    ResourceId = "221CNT02",
                    HomeValue = "44444444444444444444444444444444444444444444400000",
                    AwayValue = "54444444444444444444444444444444444444444444400000",
                    SettlementStatusID = EventStatusID.CompletedSettlementStatusID
                },
            };
            _sportsbook.Bettype = 221;
            _sportsbook.BetID = "45";
            _sportsbook.Handicap2 = 2;
            _refDataHelper.GetResourceDataByRefIdAndResourceId("FastMarket", "2", "en-US", string.Empty).Returns("Goal");
            var soccerResultDetail = new FastMarketResultDetail(_sportsbook, _setting, _refDataHelper, _sportsbookHelper);

            // Act
            var actualDetailItems = soccerResultDetail.GetDetailItems();

            // Assert
            Assert.AreEqual("Goal: 5", actualDetailItems.First().SituationValue.First());
        }

        [Test]
        public void MapGameValueByPattern_221_IsFMOne_CreateCorrectValue()
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail> {
                new GameDetail { ResourceId = GameResourceID.NextOneMinutes, HomeValue = "324234324234" },
            };
            _sportsbook.Bettype = 221;
            _sportsbook.BetID = "2";
            _sportsbook.Handicap2 = 2;
            _refDataHelper.GetFastMarketResult(Arg.Any<string>(), "en-US").Returns("Goal");
            var soccerResultDetail = new FastMarketResultDetail(_sportsbook, _setting, _refDataHelper, _sportsbookHelper);

            // Act
            var actualDetailItems = soccerResultDetail.GetDetailItems();

            // Assert
            Assert.AreEqual("Goal", actualDetailItems.First().SituationValue.First());
        }

        [Test]
        public void MapGameValueByPattern_222_IsFMTwo_CreateCorrectValue()
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail> {
                new GameDetail { ResourceId = GameResourceID.NextFiveMinutes, HomeValue = "324234324234" },
                new GameDetail {
                    ResourceId = "221CNT02",
                    HomeValue = "44444444444444444444444444444444444444444444400000",
                    AwayValue = "44444444444444444444444444444444444444444444400000",
                    SettlementStatusID = EventStatusID.CompletedSettlementStatusID
                },
            };
            _sportsbook.Bettype = 222;
            _sportsbook.BetID = "2";
            _sportsbook.Handicap2 = 2;
            _refDataHelper.GetResourceDataByRefIdAndResourceId("FastMarket", "2", "en-US", string.Empty).Returns("Goal");
            var soccerResultDetail = new FastMarketResultDetail(_sportsbook, _setting, _refDataHelper, _sportsbookHelper);

            // Act
            var actualDetailItems = soccerResultDetail.GetDetailItems();

            // Assert
            Assert.AreEqual("Goal: 4", actualDetailItems.First().SituationValue[0]);
            Assert.AreEqual("Goal: 4", actualDetailItems.First().SituationValue[1]);
            Assert.AreEqual("Goal: 4", actualDetailItems.First().SituationValue[2]);
            Assert.AreEqual("Goal: 4", actualDetailItems.First().SituationValue[3]);
            Assert.AreEqual("Goal: 4", actualDetailItems.First().SituationValue[4]);
            Assert.AreEqual("02:00-02:59", actualDetailItems.First().TimeRange[0]);
            Assert.AreEqual("03:00-03:59", actualDetailItems.First().TimeRange[1]);
            Assert.AreEqual("04:00-04:59", actualDetailItems.First().TimeRange[2]);
            Assert.AreEqual("05:00-05:59", actualDetailItems.First().TimeRange[3]);
            Assert.AreEqual("06:00-06:59", actualDetailItems.First().TimeRange[4]);
        }

        [Test]
        public void MapGameValueByPattern_222_IsFMOne_CreateCorrectValue()
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail> {
                new GameDetail { ResourceId = GameResourceID.NextFiveMinutes, HomeValue = "324234324234" },
            };
            _sportsbook.Bettype = 222;
            _sportsbook.BetID = "2";
            _sportsbook.Handicap2 = 2;
            _refDataHelper.GetFastMarketResult(Arg.Any<string>(), "en-US").Returns("Goal");
            var soccerResultDetail = new FastMarketResultDetail(_sportsbook, _setting, _refDataHelper, _sportsbookHelper);

            // Act
            var actualDetailItems = soccerResultDetail.GetDetailItems();

            // Assert
            Assert.AreEqual("Goal", actualDetailItems.First().SituationValue[0]);
            Assert.AreEqual("02:00-06:59", actualDetailItems.First().TimeRange[0]);
        }

        [Test]
        public void MapGameValueByPattern_225_CreateCorrectValue()
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail> {
                new GameDetail { ResourceId = GameResourceID.NextOneMinutes, HomeValue = "324234324234" },
                 new GameDetail {
                    ResourceId = "221CNT02",
                    HomeValue = "44444444444444444444444444444444444444444444400000",
                    AwayValue = "44444444444444444444444444444444444444444444400000",
                    SettlementStatusID = EventStatusID.CompletedSettlementStatusID
                },
            };
            _sportsbook.Bettype = 225;
            _sportsbook.BetID = "2";
            _sportsbook.Handicap2 = 2;
            _refDataHelper.GetFastMarket225Result(Arg.Any<string>(), "en-US").Returns("Goal");
            var soccerResultDetail = new FastMarketResultDetail(_sportsbook, _setting, _refDataHelper, _sportsbookHelper);

            // Act
            var actualDetailItems = soccerResultDetail.GetDetailItems();

            // Assert
            Assert.AreEqual("Goal", actualDetailItems.First().SituationValue[0]);
            Assert.AreEqual("02:00-02:59", actualDetailItems.First().TimeRange[0]);
        }

        [Test]
        public void MapGameValueByPattern_226_CreateCorrectValue()
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail> {
                new GameDetail { ResourceId = GameResourceID.WhichCombinationFirstOneMinutes, HomeValue = "324234324234" },
            };
            _sportsbook.Bettype = 226;
            _sportsbook.BetID = "2";
            _sportsbook.Handicap2 = 2;
            _refDataHelper.GetFastMarketResult(Arg.Any<string>(), "en-US").Returns("Goal");
            var soccerResultDetail = new FastMarketResultDetail(_sportsbook, _setting, _refDataHelper, _sportsbookHelper);

            // Act
            var actualDetailItems = soccerResultDetail.GetDetailItems();

            // Assert
            Assert.AreEqual("Goal", actualDetailItems.First().SituationValue[0]);
            Assert.AreEqual("02:00-02:59", actualDetailItems.First().TimeRange[0]);
        }

        [Test]
        public void MapGameValueByPattern_227_CreateCorrectValue()
        {
            // Arrange
            _sportsbook.GameDetails = new List<GameDetail> {
                new GameDetail { ResourceId = GameResourceID.WhichCombinationFirstFiveMinutes, HomeValue = "324234324234" },
            };
            _sportsbook.Bettype = 227;
            _sportsbook.BetID = "2";
            _sportsbook.Handicap2 = 2;
            _refDataHelper.GetFastMarketResult(Arg.Any<string>(), "en-US").Returns("Goal");
            var soccerResultDetail = new FastMarketResultDetail(_sportsbook, _setting, _refDataHelper, _sportsbookHelper);

            // Act
            var actualDetailItems = soccerResultDetail.GetDetailItems();

            // Assert
            Assert.AreEqual("Goal", actualDetailItems.First().SituationValue[0]);
            Assert.AreEqual("02:00-06:59", actualDetailItems.First().TimeRange[0]);
        }
    }
}