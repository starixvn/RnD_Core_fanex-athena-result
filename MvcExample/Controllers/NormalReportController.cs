namespace MvcExample.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Web.Mvc;
    using Fanex.Common.Extensions;
    using Fanex.Result.Core;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.GoldDeluxe.DTO;
    using Fanex.Result.Core.LiveCasino.DTO;
    using Fanex.Result.Core.NumberGame.DTO;
    using Fanex.Result.Core.Racing.DTO;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Models;
    using Models.Criteria;

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
        /// <param name="showMatchCode">Visible match code column.</param>
        /// <returns>The html table result.</returns>
        [SuppressMessage(
          "StyleCop.CSharp.StylecopPlus.Rules",
          "SP2101:MethodMustNotContainMoreLinesThan",
          Justification = "Reviewed. Suppression is OK here.")]
        [HttpPost]
        public string GenHtml(DateTime date, short sportId, int leagueId, string languageCode, bool showMatchCode)
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
                EnableCheckScoreCalculated = true
            };
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

                    htmlResult = ResultEngine.HtmlParser(dataLC.ToList(), resultSetting);
                    break;

                case SportType.NumberGame:
                    var dataNg = _resultService.GetNumberGameResult(new GetNumberGameResultCriteria
                    {
                        FromDate = date,
                        LeagueID = leagueId,
                        ToDate = date
                    });

                    htmlResult = ResultEngine.HtmlParser(dataNg.ToList(), resultSetting);
                    break;

                case SportType.GoldDeluxe:
                    var dataGd = _resultService.GetGoldDeluxePopUpResult(new GetGolDeluxePopUpResultCriteria
                    {
                        TransID = 7396592187,
                        Bettype = 1701
                    });

                    htmlResult = ResultEngine.HtmlParser(dataGd.ToList(), resultSetting);
                    break;

                default:

                    var dataSb = _resultService.GetSportbookResult(new GetSportbookResultCriteria
                    {
                        FromDate = date,
                        sporttype = (byte)sportId,
                        isShowAll = true,
                        LeagueID = leagueId,
                        MoveBetsDay = _resultService.GetBalanceUpDay().MoveBetsDay
                    });

                    htmlResult = ResultEngine.HtmlParser(dataSb.ToList(), resultSetting);
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
        public string GenHtmlGameDetail(int matchId, int sportId, string languageCode, bool showMatchCode)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);

            var detailedData = _resultService.GetGameDetail(new GetGameDetailCriteria { MatchId = matchId });

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
                        EnableCheckScoreCalculated = true
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
        [SuppressMessage(
          "StyleCop.CSharp.StylecopPlus.Rules",
          "SP2101:MethodMustNotContainMoreLinesThan",
          Justification = "Reviewed. Suppression is OK here.")]
        [HttpGet]
        public ActionResult GenExcel(DateTime date, short sportId, int leagueId, string languageCode, bool showMatchCode)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);

            var sportType = (SportType)sportId;
            var resultSetting = new ResultSetting
            {
                SportType = sportType,
                LanguageCode = languageCode,
                ExcelTitle = "Result report",
                ExcelTemplatePath = Server.MapPath(@"~/App_Data/Fanex.Result/"),
                VisibleMatchCodeIDColumn = showMatchCode,
                HighlightWinnerTeam = true,
                EnableCheckScoreCalculated = true
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

                case SportType.GoldDeluxe:
                    var dataGd = _resultService.GetGoldDeluxePopUpResult(new GetGolDeluxePopUpResultCriteria
                    {
                        TransID = 7201376080,
                        Bettype = 1701
                    });

                    excelResult = ResultEngine.ExcelParser<GoldDeluxe>(dataGd.ToList(), resultSetting);
                    break;

                default:
                    var dataSb = _resultService.GetSportbookResult(new GetSportbookResultCriteria
                    {
                        FromDate = date,
                        sporttype = (byte)sportId,
                        isShowAll = true,
                        LeagueID = leagueId,
                        MoveBetsDay = _resultService.GetBalanceUpDay().MoveBetsDay
                    });

                    excelResult = ResultEngine.ExcelParser<Sportsbook>(dataSb.ToList(), resultSetting);

                    break;
            }

            return File(excelResult.ToArray(), "application/vnd.ms-excel", "result-report.xls");
        }

        [HttpGet]
        public ActionResult GetSports(GetSportByDateCriteria criteria)
        {
            var moveBetsDay = _resultService.GetBalanceUpDay().MoveBetsDay;
            criteria.MoveBetsDay = moveBetsDay;

            return Json(_resultService.GetSports(criteria), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetLeagues(GetLeaguesBySportCriteria criteria)
        {
            criteria.MoveBetsDay = _resultService.GetBalanceUpDay().MoveBetsDay;
            return Json(_resultService.GetLeagues(criteria), JsonRequestBehavior.AllowGet);
        }

        public static readonly List<GameDetail> BasketBallGameDetails = new List<GameDetail>
            {
               new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "Q1",
                    HomeValue = "10",
                    AwayValue = "8",
                },
               new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "Q2",
                    HomeValue = "12",
                    AwayValue = "19",
                },
               new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "Q3",
                    HomeValue = "15",
                    AwayValue = "20",
                    SettlementStatusID = 1
                },
               new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "Q4",
                    HomeValue = "17",
                    AwayValue = "8",
                    SettlementStatusID = 1
                },
               new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "OT",
                    HomeValue = "5",
                    AwayValue = "3",
                },

               new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "604",
                    HomeValue = "h",
                    AwayValue = "",
                    SettlementStatusID = 1
                },

               new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "605",
                    HomeValue = "a",
                    AwayValue = "",
                },
               new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "60620",
                    HomeValue = "h",
                    AwayValue = "",
                },
               new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "60625",
                    HomeValue = "h",
                    AwayValue = "",
                },
               new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "60630",
                    HomeValue = "h",
                    AwayValue = "",
                },
               new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "60720",
                    HomeValue = "h",
                    AwayValue = "",
                },
               new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "60725",
                    HomeValue = "h",
                    AwayValue = "",
                },
               new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "60730",
                    HomeValue = "h",
                    AwayValue = "",
                },
                new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "6130110",
                    HomeValue = "h",
                    AwayValue = "",
                },
                new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "6130115",
                    HomeValue = "h",
                    AwayValue = "",
                },
                new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "6130120",
                    HomeValue = "h",
                    AwayValue = "",
                },
                new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "6130410",
                    HomeValue = "h",
                    AwayValue = "",
                },
                new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "6130415",
                    HomeValue = "h",
                    AwayValue = "",
                },
                new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "6130420",
                    HomeValue = "h",
                    AwayValue = "",
                },
                new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "61701",
                    HomeValue = "h",
                    AwayValue = "",
                },
                new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "61702",
                    HomeValue = "n",
                    AwayValue = "",
                },
                new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "61703",
                    HomeValue = "a",
                    AwayValue = "",
                },
                 new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "6130310",
                    HomeValue = "h",
                    AwayValue = "",
                },
                 new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "6130315",
                    HomeValue = "h",
                    AwayValue = "",
                },
                 new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "6130320",
                    HomeValue = "h",
                    AwayValue = "",
                },
                 new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "6130210",
                    HomeValue = "h",
                    AwayValue = "",
                },
                 new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "6130215",
                    HomeValue = "h",
                    AwayValue = "",
                },
                 new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "6130220",
                    HomeValue = "h",
                    AwayValue = "",
                },
                 new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "00",
                    HomeValue = "59",
                    AwayValue = "58",
                },
                 new GameDetail {
                    MatchId = 22056655,
                    ResourceId = "01",
                    HomeValue = "22",
                    AwayValue = "27",
                },
            };
    }
}