namespace MvcExample.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Web.Mvc;
    using Fanex.Result.Core;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Ionic.Zip;
    using Models.Criteria;
    using Models.ResultData;
    using Models.ResultServices;

    /// <summary>
    /// Normal result page.
    /// </summary>
    public class NormalReportController : Controller
    {
#pragma warning disable S1075 // URIs should not be hardcoded
        private const string ExcelTemplatePath = "~/App_Data/Fanex.Result/";
#pragma warning restore S1075 // URIs should not be hardcoded

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
        /// <param name="showMatchCode">Visible match code column.</param>
        /// <returns>The html table result.</returns>
        [HttpPost]
        public string GenHtml(DateTime date, short sportId, int leagueId, string languageCode, bool showMatchCode, bool getDataFromExcel)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);
            var sportType = (SportType)sportId;
            var resultSetting = new ResultSetting
            {
                SportType = sportType,
                LanguageCode = languageCode,
                VisibleMatchCodeIDColumn = showMatchCode,
                VisibleDetailedLink = true,
                VisibleFreeHeader = true,
                HighlightWinnerTeam = true,
            };
            var htmlResult = string.Empty;

            switch (sportType)
            {
                case SportType.VirtualHorseRacing:
                case SportType.VirtualGreyhounds:
                case SportType.VirtualMotorRacing:
                case SportType.VirtualSpeedway:
                case SportType.VirtualCycling:
                    htmlResult = ResultEngine.HtmlParser(
                        new RacingResultData().GetNormalResult(sportId, date, leagueId).ToList(), resultSetting);
                    break;

                case SportType.NumberGame:
                    htmlResult = ResultEngine.HtmlParser(
                        new NumberGameResultData().GetNormalResult(date, leagueId, getDataFromExcel).ToList(), resultSetting);
                    break;

                case SportType.GoldDeluxe:
                    htmlResult = ResultEngine.HtmlParser(
                        new GoldDeluxeResultData().GetNormalResult(getDataFromExcel).ToList(), resultSetting);
                    break;

                case SportType.Bitcoin:
                    htmlResult = ResultEngine.HtmlParser(
                        new BitcoinResultData().GetResult(sportId).ToList(), resultSetting);
                    break;

                case SportType.Happy5:
                    htmlResult = ResultEngine.HtmlParser(
                        new Happy5ResultData().GetNormalResult(date, leagueId, sportId, getDataFromExcel).ToList(), resultSetting);
                    break;
                default:
                    htmlResult = ResultEngine.HtmlParser(
                        new SportsbookResultData().GetNormalResult(date, sportId, leagueId, getDataFromExcel).ToList(), resultSetting);
                    break;
            }

            return htmlResult;
        }

        /// <summary>
        /// Generate tennis detailed result.
        /// </summary>
        /// <param name="matchId">The match ID.</param>
        /// <param name="sportId">The sport type.</param>
        /// <param name="languageCode">The language code.</param>
        /// <param name="showMatchCode">If true, show match code column.</param>
        /// <returns>The html result.</returns>
        [HttpGet]
        public string GenHtmlGameDetail(int matchId, int sportId, string languageCode, bool showMatchCode, bool getDataFromExcel)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);

            var detailedData = new SportsbookResultData().GetResultFromExcel((SportType)sportId).FirstOrDefault(i => i.MatchId == matchId);
            if (detailedData != null && detailedData.MatchId > 0)
            {
                return ResultEngine.HtmlGameDetailParser(
                    detailedData,
                    new ResultSetting
                    {
                        SportType = (SportType)sportId,
                        VisibleMatchCodeIDColumn = showMatchCode,
                        LanguageCode = languageCode,
                        HighlightWinnerTeam = true,
                        IsPopUp = true,
                        //IsPopupOpenFromBetList = true
                    });
            }

            return string.Empty;
        }

        /// <summary>
        /// Gen Excel result.
        /// </summary>
        /// <param name="date">The result date.</param>
        /// <param name="sportId">The sport ID.</param>
        /// <param name="leagueId">The league ID.</param>
        /// <param name="languageCode">The language code.</param>
        /// <param name="showMatchCode">Visible match code column.</param>
        /// <returns>The Excel file.</returns>
        [HttpGet]
        public ActionResult GenExcel(DateTime date, short sportId, int leagueId, string languageCode, bool showMatchCode, bool getDataFromExcel)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);

            var sportType = (SportType)sportId;
            var resultSetting = new ResultSetting
            {
                SportType = sportType,
                LanguageCode = languageCode,
                ExcelTitle = "Result report",
                ExcelTemplatePath = Server.MapPath(ExcelTemplatePath),
                VisibleMatchCodeIDColumn = showMatchCode,
                HighlightWinnerTeam = true
            };

            MemoryStream excelResult = null;

            switch (sportType)
            {
                case SportType.VirtualHorseRacing:
                case SportType.VirtualGreyhounds:
                case SportType.VirtualMotorRacing:
                case SportType.VirtualSpeedway:
                case SportType.VirtualCycling:
                    excelResult = ResultEngine.ExcelParser(
                        new RacingResultData().GetNormalResult(sportId, date, leagueId).ToList(), resultSetting);
                    break;

                case SportType.NumberGame:
                    excelResult = ResultEngine.ExcelParser(
                        new NumberGameResultData().GetNormalResult(date, leagueId, getDataFromExcel).ToList(), resultSetting);
                    break;

                case SportType.GoldDeluxe:
                    excelResult = ResultEngine.ExcelParser(
                        new GoldDeluxeResultData().GetNormalResult(getDataFromExcel).ToList(), resultSetting);
                    break;
                case SportType.Bitcoin:
                    excelResult = ResultEngine.ExcelParser(
                        new BitcoinResultData().GetResult(sportId).ToList(), resultSetting);
                    break;
                default:
                    excelResult = ResultEngine.ExcelParser(
                        new SportsbookResultData().GetNormalResult(date, sportId, leagueId, getDataFromExcel).ToList(), resultSetting);
                    break;
            }

            return File(excelResult.ToArray(), "application/vnd.ms-excel", "result-report.xls");
        }

        [HttpGet]
        public ActionResult GetSports(GetSportByDateCriteria criteria, bool getDataFromExcel = false)
        {
            var moveBetsDay = BaseResultService.GetBalanceUpDay().MoveBetsDay;
            criteria.MoveBetsDay = moveBetsDay;
            var resultService = new SportsResultService();

            if (getDataFromExcel)
            {
                return Json(resultService.GetResultFromExcel(string.Empty), JsonRequestBehavior.AllowGet);
            }

            return Json(resultService.GetResult(criteria), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetLeagues(GetLeaguesBySportCriteria criteria)
        {
            criteria.MoveBetsDay = BaseResultService.GetBalanceUpDay().MoveBetsDay;

            return Json(new LeaguesResultService().GetResult(criteria), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ExportExcelDataFile(short sportId = 1)
        {
            var sportName = (SportType)sportId;
            var outputStream = new MemoryStream();

            using (var zip = new ZipFile())
            {
                zip.AddFile(Server.MapPath(string.Format(Constant.ExcelFilePath, sportName)), "");

                if (sportName == SportType.Soccer)
                {
                    zip.AddFile(Server.MapPath(string.Format(Constant.ExcelFilePath, $"{sportName}detail")), "");
                }
                zip.Save(outputStream);
            }

            outputStream.Position = 0;
            return File(outputStream, "application/zip", "data.zip");
        }
    }
}