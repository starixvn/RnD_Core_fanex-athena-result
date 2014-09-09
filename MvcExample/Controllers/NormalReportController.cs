// -----------------------------------------------------------------------
// <copyright file="NormalReport.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
namespace MvcExample.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.Result.Core;
    using Fanex.Result.Core.DTO;
    using MvcExample.Models;
    using MvcExample.Models.Criteria;

    public class NormalReportController : Controller
    {
        private readonly IResultService _resultService;

        //
        // GET: /Home/
        public NormalReportController()
        {
            _resultService = new ResultService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Index(DateTime date, short sportId, int leagueId, string languageCode)
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
                        isShowAll = true,
                        LeagueID = leagueId,
                        ToDate = date
                    });
                    htmlResult = ResultEngine.HtmlParser<NumberGame>(dataNg.ToList(), resultSetting);

                    break;
                ///sportbook
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

        [HttpGet]
        public ActionResult GetSports(GetSportByDateCriteria criteria)
        {
            return Json(_resultService.GetSports(criteria),
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetLeagues(GetLeaguesBySportCriteria criteria)
        {
            return Json(_resultService.GetLeagues(criteria),
                JsonRequestBehavior.AllowGet);
        }
    }
}