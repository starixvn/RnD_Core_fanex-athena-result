namespace MvcExample.Controllers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.Result.Core;
    using MvcExample.Models;
    using MvcExample.Models.Criteria;
    using MvcExample.Models.MetaModel;

    /// <summary>
    /// POPUP result page.
    /// </summary>
    public class PopUpReportController : Controller
    {
        private const int MixParlayBettype = 9;
        private const int SystemParlayBettype = 29;

        private readonly IResultService _resultService = new ResultService();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string GenHtml(ResultPopupMetaModel metalModel)
        {
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
                IsOutRight = metalModel.IsOutRight.HasValue && metalModel.IsOutRight.Value > 0,
                IsParlayPopUp = metalModel.Bettype == MixParlayBettype || metalModel.Bettype == SystemParlayBettype,
                IsPopUp = true,
                VisiblePopUpTitle = true,
                LanguageCode = metalModel.LanguageCode,
                VisibleMatchCodeIDColumn = false,
                VisibleFreeHeader = true,
                VisibleDetailedLink = true,
            };
            if (setting.IsParlayPopUp)
            {
                if (metalModel.RefNo.HasValue)
                {
                    var parlayData = _resultService.GetParlayResult(new GetParlayResultCriteria
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
        [SuppressMessage(
          "StyleCop.CSharp.Nexcel.NexcelCustomRules",
          "SP2101:MethodMustNotContainMoreLinesThan",
          Justification = "Reviewed. Suppression is OK here.")]
        private string GenNormalPopUp(ResultPopupMetaModel metalModel, SportType sportType, int licenseeId, int userId, ResultSetting setting)
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
                        var dataRc = _resultService.GetRacingPopupResult(
                            new GetRacingPopupResultCriteria
                            {
                                LicenseeID = licenseeId,
                                UserID = userId,
                                MatchID = metalModel.MatchId,
                                RaceNo = metalModel.RaceNo.Value.ToString()
                            });

                        if (dataRc != null)
                        {
                            return ResultEngine.HtmlParser(dataRc.ToList(), setting);
                        }
                    }

                    break;

                case SportType.LiveCasino:

                    var dataLc = _resultService.GetLiveCasinoPopupResult(
                        new GetLiveCasinoPopupResultCriteria
                        {
                            LicenseeID = licenseeId,
                            MatchID = metalModel.MatchId,
                            UserID = userId
                        });

                    if (dataLc != null)
                    {
                        return ResultEngine.HtmlParser(dataLc.ToList(), setting);
                    }

                    break;

                case SportType.NumberGame:
                    var dataNg = _resultService.GetNumberGamePopupResult(new GetNumberGamePopupResultCriteria { MatchID = metalModel.MatchId });
                    if (dataNg != null)
                    {
                        return ResultEngine.HtmlParser(dataNg.ToList(), setting);
                    }

                    break;

                default:
                    var data = _resultService.GetSportbookPopupResult(
               new GetSportbookPopupResultCriteria
               {
                   LicenseeID = licenseeId,
                   MatchID = metalModel.MatchId,
                   UserID = userId,
                   BetId = metalModel.BetId,
                   Bettype = metalModel.Bettype
               });

                    if (data != null)
                    {
                        return ResultEngine.HtmlParser(data.ToList(), setting);
                    }

                    break;
            }

            return string.Empty;
        }
    }
}