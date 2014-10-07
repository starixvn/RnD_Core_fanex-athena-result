namespace MvcExample.Controllers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.Result.Core;
    using Fanex.Result.Core.DTO;
    using MvcExample.Models;
    using MvcExample.Models.Criteria;

    /// <summary>
    /// Normal result page.
    /// </summary>
    public class NormalReportController : Controller
    {
        private readonly IResultService _resultService;

        public NormalReportController()
        {
            _resultService = new ResultService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gen html result.
        /// </summary>
        /// <param name="date">The result date.</param>
        /// <param name="sportId">The sport ID.</param>
        /// <param name="leagueId">The league ID.</param>
        /// <param name="languageCode">The language code.</param>
        /// <returns>The html table result.</returns>
        [SuppressMessage(
          "StyleCop.CSharp.Nexcel.NexcelCustomRules",
          "SP2101:MethodMustNotContainMoreLinesThan",
          Justification = "Reviewed. Suppression is OK here.")]
        [HttpPost]
        public string GenHtml(DateTime date, short sportId, int leagueId, string languageCode)
        {
            var sportType = (SportType)sportId;
            var resultSetting = new ResultSetting { SportType = sportType, LanguageCode = languageCode };
            var htmlResult = string.Empty;
            switch (sportType)
            {
                case SportType.HorseRacing:
                case SportType.Greyhound:
                case SportType.Harness:
                case SportType.VirtualHorseRacing:
                case SportType.VirtualGreyhounds:
                case SportType.VirtualMotorRacing:
                case SportType.VirtualSpeedway:
                case SportType.VirtualCycling:
                    var dataRc = _resultService.GetRacingResult(new GetRacingResultCriteria
                    {
                        Sporttype = sportId,
                        FromDate = date,
                        isShowAll = true,
                        LeagueID = leagueId
                    });
                    htmlResult = ResultEngine.HtmlParser<Racing>(dataRc.ToList(), resultSetting);
                    break;

                case SportType.LiveCasino:
                    var dataLC = _resultService.GetLiveCasinoResult(new GetLiveCasinoResultCriteria
                    {
                        FromDate = date,
                        sporttype = (byte)sportId,
                        isShowAll = true,
                        LeagueID = leagueId
                    });
                    htmlResult = ResultEngine.HtmlParser<LiveCasino>(dataLC.ToList(), resultSetting);

                    break;

                case SportType.NumberGame:
                    var dataNg = _resultService.GetNumberGameResult(new GetNumberGameResultCriteria
                    {
                        FromDate = date,
                        LeagueID = leagueId,
                        ToDate = date
                    });
                    htmlResult = ResultEngine.HtmlParser<NumberGame>(dataNg.ToList(), resultSetting);

                    break;

                default:
                    var dataSb = _resultService.GetSportbookResult(new GetSportbookResultCriteria
                    {
                        FromDate = date,
                        sporttype = (byte)sportId,
                        isShowAll = true,
                        LeagueID = leagueId
                    });

                    htmlResult = ResultEngine.HtmlParser<Sportbook>(dataSb.ToList(), resultSetting);

                    break;
            }

            return htmlResult;
        }

        /// <summary>
        /// Gen Excel result.
        /// </summary>
        /// <param name="date">The result date.</param>
        /// <param name="sportId">The sport ID.</param>
        /// <param name="leagueId">The league ID.</param>
        /// <param name="languageCode">The language code.</param>
        /// <returns>The Excel file.</returns>
        [SuppressMessage(
          "StyleCop.CSharp.Nexcel.NexcelCustomRules",
          "SP2101:MethodMustNotContainMoreLinesThan",
          Justification = "Reviewed. Suppression is OK here.")]
        [HttpGet]
        public ActionResult GenExcel(DateTime date, short sportId, int leagueId, string languageCode)
        {
            var sportType = (SportType)sportId;
            var resultSetting = new ResultSetting
            {
                SportType = sportType,
                LanguageCode = languageCode,
                ExcelTitle = "Result report",
                ExcelTemplatePath = Server.MapPath(@"~/App_Data/Fanex.Result/")
            };
            MemoryStream excelResult = null;
            switch (sportType)
            {
                case SportType.HorseRacing:
                case SportType.Greyhound:
                case SportType.Harness:
                case SportType.VirtualHorseRacing:
                case SportType.VirtualGreyhounds:
                case SportType.VirtualMotorRacing:
                case SportType.VirtualSpeedway:
                case SportType.VirtualCycling:
                    var dataRc = _resultService.GetRacingResult(new GetRacingResultCriteria
                    {
                        Sporttype = sportId,
                        FromDate = date,
                        isShowAll = true,
                        LeagueID = leagueId
                    });
                    excelResult = ResultEngine.ExcelParser<Racing>(dataRc.ToList(), resultSetting);
                    break;

                case SportType.LiveCasino:
                    var dataLC = _resultService.GetLiveCasinoResult(new GetLiveCasinoResultCriteria
                    {
                        FromDate = date,
                        sporttype = (byte)sportId,
                        isShowAll = true,
                        LeagueID = leagueId
                    });
                    excelResult = ResultEngine.ExcelParser<LiveCasino>(dataLC.ToList(), resultSetting);

                    break;

                case SportType.NumberGame:
                    var dataNg = _resultService.GetNumberGameResult(new GetNumberGameResultCriteria
                    {
                        FromDate = date,
                        LeagueID = leagueId,
                        ToDate = date
                    });
                    excelResult = ResultEngine.ExcelParser<NumberGame>(dataNg.ToList(), resultSetting);

                    break;

                default:
                    var dataSb = _resultService.GetSportbookResult(new GetSportbookResultCriteria
                    {
                        FromDate = date,
                        sporttype = (byte)sportId,
                        isShowAll = true,
                        LeagueID = leagueId
                    });
                    excelResult = ResultEngine.ExcelParser<Sportbook>(dataSb.ToList(), resultSetting);

                    break;
            }

            return File(excelResult.ToArray(), "application/vnd.ms-excel", "result-report.xls");
        }

        /// <summary>
        /// Gen JSON result.
        /// </summary>
        /// <param name="date">The result date.</param>
        /// <param name="sportId">The sport ID.</param>
        /// <param name="leagueId">The league ID.</param>
        /// <param name="languageCode">The language code.</param>
        /// <returns>The JSON  result.</returns>
        [SuppressMessage(
          "StyleCop.CSharp.Nexcel.NexcelCustomRules",
          "SP2101:MethodMustNotContainMoreLinesThan",
          Justification = "Reviewed. Suppression is OK here.")]
        [HttpPost]
        public string GenJson(DateTime date, short sportId, int leagueId, string languageCode)
        {
            var sportType = (SportType)sportId;
            var resultSetting = new ResultSetting
            {
                SportType = sportType,
                LanguageCode = languageCode
            };
            string jsonResult = string.Empty;
            switch (sportType)
            {
                case SportType.HorseRacing:
                case SportType.Greyhound:
                case SportType.Harness:
                case SportType.VirtualHorseRacing:
                case SportType.VirtualGreyhounds:
                case SportType.VirtualMotorRacing:
                case SportType.VirtualSpeedway:
                case SportType.VirtualCycling:
                    var dataRc = _resultService.GetRacingResult(new GetRacingResultCriteria
                    {
                        Sporttype = sportId,
                        FromDate = date,
                        isShowAll = true,
                        LeagueID = leagueId
                    });
                    jsonResult = ResultEngine.JsonParser<Racing>(dataRc.ToList(), resultSetting);
                    break;

                case SportType.LiveCasino:
                    var dataLC = _resultService.GetLiveCasinoResult(new GetLiveCasinoResultCriteria
                    {
                        FromDate = date,
                        sporttype = (byte)sportId,
                        isShowAll = true,
                        LeagueID = leagueId
                    });
                    jsonResult = ResultEngine.JsonParser<LiveCasino>(dataLC.ToList(), resultSetting);

                    break;

                case SportType.NumberGame:
                    var dataNg = _resultService.GetNumberGameResult(new GetNumberGameResultCriteria
                    {
                        FromDate = date,
                        LeagueID = leagueId,
                        ToDate = date
                    });
                    jsonResult = ResultEngine.JsonParser<NumberGame>(dataNg.ToList(), resultSetting);

                    break;

                default:
                    var dataSb = _resultService.GetSportbookResult(
                        new GetSportbookResultCriteria
                        {
                            FromDate = date,
                            sporttype = (byte)sportId,
                            isShowAll = true,
                            LeagueID = leagueId
                        });
                    jsonResult = ResultEngine.JsonParser<Sportbook>(dataSb.ToList(), resultSetting);

                    break;
            }

            return jsonResult;
        }

        [HttpGet]
        public ActionResult GetSports(GetSportByDateCriteria criteria)
        {
            return Json(_resultService.GetSports(criteria), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetLeagues(GetLeaguesBySportCriteria criteria)
        {
            return Json(_resultService.GetLeagues(criteria), JsonRequestBehavior.AllowGet);
        }
    }
}