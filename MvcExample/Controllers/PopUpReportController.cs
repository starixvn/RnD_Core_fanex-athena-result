namespace MvcExample.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.Result.Core;
    using MvcExample.Models;
    using MvcExample.Models.Criteria;
    using MvcExample.Models.MetaModel;

    public class PopUpReportController : Controller
    {
        private readonly IResultService _resultService = new ResultService();

        //
        // GET: /PopUpReport/
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

        private string GetPopUpData(ResultPopupMetaModel metalModel)
        {
            var sportType = (SportType)metalModel.SportType;
            var licenseeId = 1;
            var userId = 68;
            var setting = new ResultSetting
            {
                SportType = sportType,
                IsOutRight = metalModel.IsOutRight.HasValue && metalModel.IsOutRight.Value > 0,
                IsParlayPopUp = metalModel.Bettype == 9 || metalModel.Bettype == 29,
                IsPopUp = true,
                LanguageCode = metalModel.LanguageCode
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
                   UserID = userId
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