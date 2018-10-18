namespace Fanex.Result.Core.RemotingRef.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Fanex.RemotingRef;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class RefDataHelperTests
    {
        private RefDataHelper refDataHelper;
        private IRemotingRefService remotingRefService;

        [SetUp]
        public void SetUp()
        {
            remotingRefService = Substitute.For<IRemotingRefService>();
            refDataHelper = new RefDataHelper(remotingRefService);
        }

        [Test]
        public void GetBetTypeNameByIdWithResourceId_Always_ExecuteGetBetTypeName()
        {
            string betTypeId = "1";
            string lang = "en-US";
            string resourceId = "1";

            refDataHelper.GetBetTypeNameById(betTypeId, lang, resourceId);

            remotingRefService.Received().GetBetTypeName(betTypeId, lang, string.Empty, resourceId);
        }

        [Test]
        public void GetTeamName_Always_ExecuteGetTeamName()
        {
            string teamId = "1";
            string lang = "en-US";
            string defaultValue = "1";

            refDataHelper.GetTeamName(teamId, lang, defaultValue);

            remotingRefService.Received().GetTeamName(teamId, lang, defaultValue);
        }

        [Test]
        public void GetTeamNameWithTeamIdIsInt_Always_ExecuteGetTeamName()
        {
            int teamId = 1;
            string lang = "en-US";
            string defaultValue = "1";

            refDataHelper.GetTeamName(teamId, lang, defaultValue);

            remotingRefService.Received().GetTeamName("1", lang, defaultValue);
        }

        [Test]
        public void GetLeagueName_Always_ExecuteGetLeagueName()
        {
            int leagueId = 1;
            string lang = "en-US";
            string defaultValue = "1";

            refDataHelper.GetLeagueName(leagueId, lang, defaultValue);

            remotingRefService.Received().GetLeagueName("1", lang, defaultValue);
        }

        [Test]
        public void GetSportName_Always_ExecuteGetSportName()
        {
            int sportId = 1;
            string lang = "en-US";
            string defaultValue = "1";

            refDataHelper.GetSportName(sportId, lang, defaultValue);

            remotingRefService.Received().GetSportName("1", lang, defaultValue);
        }

        [Test]
        public void GetBetTypeNameById_Always_ExecuteGetBetTypeName()
        {
            string bettypeId = "1";
            string lang = "en-US";

            refDataHelper.GetBetTypeNameById(bettypeId, lang);

            remotingRefService.Received().GetBetTypeName("1", lang);
        }

        [Test]
        public void GetResourceDataByRefIdAndResourceId_Always_ExecuteGetResourceDataByRefIdAndResourceId()
        {
            string refId = "1";
            string resourceId = "1";
            string lang = "en-US";
            string defaultValue = "1";

            refDataHelper.GetResourceDataByRefIdAndResourceId(refId, resourceId, lang, defaultValue);

            remotingRefService.Received().GetResourceDataByRefIdAndResourceId(refId, resourceId, lang, defaultValue);
        }

        [Test]
        public void GetFastMarketResult_Always_ExecuteGetFastMarketsResult()
        {
            string resultId = "1";
            string lang = "en-US";

            refDataHelper.GetFastMarketResult(resultId, lang);

            remotingRefService.Received().GetFastMarketsResult(resultId, lang);
        }

        [Test]
        public void GetFastMarket225Result_Always_ExecuteGetFastMarkets225Result()
        {
            string resultId = "1";
            string lang = "en-US";

            refDataHelper.GetFastMarket225Result(resultId, lang);

            remotingRefService.Received().GetFastMarkets225Result(resultId, lang);
        }

        [Test]
        public void GetBetRadarMatchCodeFormat_Always_ExecuteGetBetRadarMatchCodeFormat()
        {
            string matchCode = "1";
            string lang = "en-US";

            refDataHelper.GetBetRadarMatchCodeFormat(matchCode, lang);

            remotingRefService.Received().GetBetRadarMatchCodeFormat(matchCode, lang);
        }

        [Test]
        public void GetHorseTeamName_Always_ExecuteGetHorseTeamName()
        {
            string codeId = "1";
            string lang = "en-US";
            string defaultValue = "1";

            refDataHelper.GetHorseTeamName(codeId, lang, defaultValue);

            remotingRefService.Received().GetHorseTeamName(codeId, lang, defaultValue);
        }

        [Test]
        public void GetRouletteBetItem_Always_ExecuteGetRouletteBetItem()
        {
            string itemId = "1";
            string lang = "en-US";
            string defaultValue = "1";

            refDataHelper.GetRouletteBetItem(itemId, lang, defaultValue);

            remotingRefService.Received().GetRouletteBetItem(itemId, lang, defaultValue);
        }

        [Test]
        public void GetCasinoBetItem_Always_ExecuteGetCasinoBetItem()
        {
            string itemId = "1";
            string lang = "en-US";
            string defaultValue = "1";

            refDataHelper.GetCasinoBetItem(itemId, lang, defaultValue);

            remotingRefService.Received().GetCasinoBetItem(itemId, lang, defaultValue);
        }
    }
}