namespace MvcExample.Controllers
{
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Web.Mvc;
    using Fanex.Result.Core;
    using Fanex.Result.Core.Base;
    using Models.Criteria;
    using Models.MetaModel;
    using Models.ResultData;
    using Models.ResultServices;

    /// <summary>
    /// POPUP result page.
    /// </summary>
    public class PopUpReportController : Controller
    {
        private const int MixParlayBettype = 9;
        private const int SystemParlayBettype = 29;
        private const int MetricGaming801 = 801;
        private const int MixParlayVirtualSport = 2799;

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string GenHtml(ResultPopupMetaModel metalModel)
        {
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(metalModel.LanguageCode);

            return GetPopUpData(metalModel);
        }

        /// <summary>
        /// Get result data for POPUP page.
        /// </summary>
        /// <param name="metalModel">The metal model.</param>
        /// <returns>The html result.</returns>
        private string GetPopUpData(ResultPopupMetaModel metalModel)
        {
            var sportType = (SportType)metalModel.SportType;
            var licenseeId = 1;
            var userId = 68;
            var setting = new ResultSetting
            {
                SportType = sportType,
                IsOutright = metalModel.IsOutRight.HasValue && metalModel.IsOutRight.Value > 0,
                IsParlayPopUp = metalModel.Bettype == MixParlayBettype ||
                    metalModel.Bettype == SystemParlayBettype ||
                    metalModel.Bettype == MixParlayVirtualSport,
                IsPopUp = true,
                VisiblePopUpTitle = true,
                LanguageCode = metalModel.LanguageCode,
                VisibleMatchCodeIDColumn = false,
                VisibleFreeHeader = true,
                VisibleDetailedLink = true,
                EnableCheckScoreCalculated = true,
                IsPopupOpenFromBetList = true
            };

            if (setting.IsParlayPopUp)
            {
                if (metalModel.RefNo.HasValue)
                {
                    var parlayData = new ParlayResultService().GetResult(new GetParlayResultCriteria
                    {
                        refno = metalModel.RefNo.Value
                    });

                    return ResultEngine.HtmlParser(parlayData.ToList(), setting);
                }
            }
            else
            {
                return GenNormalPopUp(metalModel, sportType, licenseeId, userId, setting);
            }

            return string.Empty;
        }

        /// <summary>
        /// Get normal POPUP result.
        /// </summary>
        /// <param name="metalModel">The metal model.</param>
        /// <param name="sportType">The sport type.</param>
        /// <param name="licenseeId">The licensee ID.</param>
        /// <param name="userId">The user ID.</param>
        /// <param name="setting">The result setting.</param>
        /// <returns>The html result.</returns>
        private static string GenNormalPopUp(ResultPopupMetaModel metalModel, SportType sportType, int licenseeId, int userId, ResultSetting setting)
        {
            switch (sportType)
            {
                case SportType.HorseRacing:
                case SportType.Greyhound:
                case SportType.Harness:
                case SportType.VirtualHorseRacing:
                case SportType.VirtualMotorRacing:
                case SportType.VirtualGreyhounds:
                case SportType.VirtualSpeedway:
                case SportType.VirtualCycling:
                    if (metalModel.RaceNo.HasValue)
                    {
                        return ResultEngine.HtmlParser(
                            new RacingResultData().GetPopupResult(licenseeId, userId, metalModel.MatchId, metalModel.RaceNo).ToList(), setting);
                    }
                    break;

                case SportType.GoldDeluxe:
                    return ResultEngine.HtmlParser(
                        new GoldDeluxeResultData().GetPopupResult(metalModel.RefNo, metalModel.Bettype, metalModel.GetDataFromExcel).ToList(), setting);

                case SportType.AGCasino:
                    return ResultEngine.HtmlParser(
                        new AGCasinoResultData().GetPopupResult(metalModel.RefNo, metalModel.Bettype).ToList(), setting);

                case SportType.AllBetCasino:
                    return new AllbetResultData().GetPopupResult(metalModel.Bettype, metalModel.LanguageCode);

                case SportType.MicroGamingLC:
                    return new MicroGamingResultData().GetPopupResult();

                case SportType.NumberGame:
                    return ResultEngine.HtmlParser(
                        new NumberGameResultData().GetPopupResult(metalModel.MatchId).ToList(), setting);

                case SportType.Happy5:
                    return ResultEngine.HtmlParser(
                        new Happy5ResultData().GetPopupResult(metalModel.MatchId, metalModel.GetDataFromExcel).ToList(), setting);

                default:
                    if (metalModel.Bettype == MetricGaming801)
                    {
                        return ResultEngine.HtmlParser(
                            new MetricGamingResultData().GetPopupResult(metalModel.BetId).ToList(), setting);
                    }

                    var data = new SportsbookResultData().GetPopupResult(sportType, licenseeId, userId, metalModel);
                    data.ToList().ForEach(x => x.Bettype = metalModel.Bettype);
                    data.ToList().ForEach(x => x.BetID = metalModel.BetId.ToString());
                    data.ToList().ForEach(x => x.Handicap2 = metalModel.Handicap2);

                    if (metalModel.GetDataFromExcel)
                    {
                        data.ToList()
                            .ForEach(x => x.GameDetails
                                = x.GameDetails.Where(d => d.ResourceId.StartsWith(metalModel.Bettype.ToString()) || d.ResourceId.Contains("CNT")).ToList());
                    }

                    return ResultEngine.HtmlParser(data.ToList(), setting);
            }

            return string.Empty;
        }
    }
}