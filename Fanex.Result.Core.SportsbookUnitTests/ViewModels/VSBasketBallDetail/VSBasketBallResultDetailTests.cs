namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels.VSBasketBallDetail
{
    using System.Collections.Generic;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Sportsbook.DataDomain;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Fanex.Result.Core.Sportsbook.Utilities;
    using Fanex.Result.Core.Sportsbook.ViewModels.BasketBallDetail;
    using Fanex.Result.Core.Sportsbook.ViewModels.VSBasketBallDetail;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class VSBasketBallResultDetailTest
    {
        private Sportsbook _sportsbook;
        private ResultSetting _setting;
        private ISportsbookRefDataHelper _sportsbookRefDataHelper;
        private CompareLogic _compareLogic;

        [SetUp]
        public void Setup()
        {
            _sportsbook = new Sportsbook
            {
                HomeId = 1,
                AwayId = 2,
                GameDetails = new List<GameDetail>
                {
                    new GameDetail {ResourceId = VSBasketBallResourceID.FTRaceTo20Points, HomeValue = "h", SettlementStatusID = 4},
                    new GameDetail {ResourceId = VSBasketBallResourceID.FTRaceTo40Points, HomeValue = "a", SettlementStatusID = 4},
                    new GameDetail {ResourceId = GameResourceID.FinalScore, HomeValue = "1", AwayValue = "2", SettlementStatusID = 0},
                }
            };
            _setting = new ResultSetting { LanguageCode = "en-US", VisibleDetailedLink = true };
            _sportsbookRefDataHelper = Substitute.For<ISportsbookRefDataHelper>();

            _compareLogic = new CompareLogic();
        }

        [Test]
        public void CreateGameDetailItem_DetailItemContainVSBasketBallResourseId_GenerateCorrectGameDetailItem()
        {
            // Arrange
            _sportsbookRefDataHelper.GetBetTypeNameById("2809", "en", "20").Returns("FTRaceTo20Points");
            _sportsbookRefDataHelper.GetBetTypeNameById("2809", "en", "40").Returns("FTRaceTo40Points");
            _sportsbookRefDataHelper.GetTeamName(1, Arg.Any<string>(), Arg.Any<string>()).Returns("HomeTeam");
            _sportsbookRefDataHelper.GetTeamName(2, Arg.Any<string>(), Arg.Any<string>()).Returns("AwayTeam");

            var expectGameDetailItems = new SafeList<BasketBallGameDetailItem>
            {
                new BasketBallGameDetailItem { ResourceName = "FTRaceTo20Points", ResourceID = "280920", HomeValue = "HomeTeam", AwayValue = "-", IsHome = true },
                new BasketBallGameDetailItem { ResourceName = "FTRaceTo40Points", ResourceID = "280940", HomeValue = "AwayTeam", AwayValue = "-", IsAway = true },
                new BasketBallGameDetailItem { ResourceID = "00", HomeValue = "-", AwayValue = "-", IsHome = false }
            };

            // Act
            var resultDetail = new VSBasketBallResultDetail(_sportsbook, _setting, _sportsbookRefDataHelper);

            // Assert
            Assert.True(_compareLogic.Compare(expectGameDetailItems, resultDetail.GetDetailItems()).AreEqual);
        }
    }
}