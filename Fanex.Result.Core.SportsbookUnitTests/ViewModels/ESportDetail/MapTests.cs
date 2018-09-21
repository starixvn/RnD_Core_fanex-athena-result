namespace Fanex.Result.Core.Sportsbook.Tests.ViewModels.ESportDetail
{
    using System.Collections.Generic;
    using System.Linq;
    using DTO;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.ESport.DTO;
    using Fanex.Result.Core.Sportsbook.Utilities;
    using Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail;
    using NSubstitute;
    using NUnit.Framework;

    public class MapTests
    {
        private Sportsbook _sportsbook;
        private ResultSetting _setting;
        private IResultDetail<MapResult> _resultDetail;
        private IRefDataHelper _refDataHelper;
        private ISportsbookHelperWraper _sportsbookHelperWraper;

        [SetUp]
        public void Setup()
        {
            _sportsbook = new Sportsbook();
            _setting = new ResultSetting();
            _resultDetail = Substitute.For<IResultDetail<MapResult>>();
            _resultDetail.GetDetailItems().Returns(new SafeList<MapResult>());
            _refDataHelper = Substitute.For<IRefDataHelper>();
            _sportsbookHelperWraper = Substitute.For<ISportsbookHelperWraper>();
            ResultCoreManager.RegisterRefDataHelper(_refDataHelper);
        }

        [TestCase("900601", "h", "", 4, "home")]
        [TestCase("900601", "a", "", 4, "away")]
        [TestCase("900601", "n", "", 4, "None")]
        [TestCase("903702", "h", "", 4, "home")]
        [TestCase("903702", "a", "", 4, "away")]
        [TestCase("903702", "n", "", 4, "None")]
        [TestCase("90730201", "n", "", 4, "No")]
        [TestCase("90730101", "y", "", 4, "Yes")]
        [TestCase("905801", "1", "2", 4, "1 min 2 sec")]
        [TestCase("905801", "1", "2", 1, "Refund")]
        [TestCase("903702", "n", "", 1, "Refund")]
        [TestCase("90730201", "n", "", 1, "Refund")]
        [TestCase("906402", "d", "", 4, "Draw")]
        public void GetDetailItems_SingleResult_ReturnFormatedResult(
            string resourceId,
            string homeValue,
            string awayValue,
            int settlementStatusId,
            string expectedResult)
        {
            _sportsbook.GameDetails = new List<GameDetail>
            {
                new GameDetail
                {
                    HomeValue = homeValue,
                    AwayValue = awayValue,
                    ResourceId = resourceId,
                    SettlementStatusID = settlementStatusId,
                    MatchId = 1
                }
            };
            _sportsbook.HomeId = 1;
            _sportsbook.AwayId = 2;
            _refDataHelper
                .GetBetTypeNameById(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns($"bettypeName_{expectedResult}");
            _refDataHelper.GetTeamName(_sportsbook.HomeId, Arg.Any<string>(), string.Empty).Returns("home");
            _refDataHelper.GetTeamName(_sportsbook.AwayId, Arg.Any<string>(), string.Empty).Returns("away");

            var map = new Map(_sportsbook, _setting, _refDataHelper, _sportsbookHelperWraper);

            var detailItems = map.GetDetailItems();

            Assert.AreEqual(expectedResult, detailItems.ElementAt(0).HomeValue);
        }

        [TestCase("900201", "2", "1", 4, "2", "1")]
        [TestCase("905201", "1", "1", 4, "1", "1")]
        [TestCase("905203", "2", "1", 1, "Refund", "Refund")]
        public void GetDetailItems_DoubleResult_ReturnFormatedResult(
            string resourceId,
            string homeValue,
            string awayValue,
            int settlementStatusId,
            string expectedHomeResult,
            string expectedAwayResult)
        {
            _sportsbook.GameDetails = new List<GameDetail>
            {
                new GameDetail
                {
                    HomeValue = homeValue,
                    AwayValue = awayValue,
                    ResourceId = resourceId,
                    SettlementStatusID = settlementStatusId,
                    MatchId = 1
                }
            };

            var map = new Map(_sportsbook, _setting, _refDataHelper, _sportsbookHelperWraper);

            var detailItems = map.GetDetailItems();

            Assert.AreEqual(expectedHomeResult, detailItems.ElementAt(0).HomeValue);
            Assert.AreEqual(expectedAwayResult, detailItems.ElementAt(0).AwayValue);
        }

        [TestCase("900201", "Map 1 Total Kills Score")]
        [TestCase("90690102", "Map 1 Round 2 Total Kills")]
        [TestCase("901203", "Map 3 Roshans Score")]
        public void GetDetailItems_RemotingRefDoesNotSupport_ReturnRexResource(
            string resourceId, 
            string expectedResult)
        {
            _sportsbook.GameDetails = new List<GameDetail>
            {
                new GameDetail
                {
                    ResourceId = resourceId,
                    SettlementStatusID = 4,
                    MatchId = 1
                }
            };

            var map = new Map(_sportsbook, _setting, _refDataHelper, _sportsbookHelperWraper);

            var detailItems = map.GetDetailItems();

            Assert.AreEqual(expectedResult, detailItems.ElementAt(0).ResourceName);
        }
    }
}