namespace MvcExample.Models.ResultData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Hosting;
    using Criteria;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Sportsbook.DTO;
    using MetaModel;
    using ResultServices;

    public class SportsbookResultData
    {
        private readonly SportsbookResultService resultService;

        public SportsbookResultData()
        {
            resultService = new SportsbookResultService();
        }

        public IEnumerable<Sportsbook> GetNormalResult(DateTime date, short sportId, int leagueId, bool getDataFromExcel)
        {
            if (getDataFromExcel)
            {
                return GetResultFromExcel((SportType)sportId);
            }

            return resultService.GetResult(new GetSportbookResultCriteria
            {
                FromDate = date,
                sporttype = (byte)sportId,
                isShowAll = true,
                LeagueID = leagueId,
                MoveBetsDay = new BaseResultService().GetBalanceUpDay().MoveBetsDay
            });
        }

        public IEnumerable<Sportsbook> GetPopupResult(SportType sportType, int licenseeId, int userId, ResultPopupMetaModel metaModel)
        {
            IEnumerable<Sportsbook> data;

            if (metaModel.GetDataFromExcel)
            {
                data = GetResultFromExcel(sportType);
                data = data.Where(item => item.Bettype == metaModel.Bettype);
            }
            else
            {
                data = resultService.GetResult(new GetSportbookPopupResultCriteria
                {
                    LicenseeID = licenseeId,
                    MatchID = metaModel.MatchId,
                    UserID = userId,
                    BetId = metaModel.BetId,
                    Bettype = metaModel.Bettype
                });
            }

            if (sportType == SportType.BasketBall)
            {
                GenerateGameDetailForBasketBall(metaModel, data);
            }

            return data;
        }

        public IEnumerable<Sportsbook> GetResultFromExcel(SportType sportType)
            => resultService.GetResultFromExcel(HostingEnvironment.MapPath(string.Format(Constant.ExcelFilePath, sportType)));

        private static void GenerateGameDetailForBasketBall(ResultPopupMetaModel metaModel, IEnumerable<Sportsbook> resultData)
        {
            var resourceId = metaModel.BetId.ToString().Length > 2 ? metaModel.BetId.ToString().Remove(0, 1) : string.Empty;

            foreach (var sportsbook in resultData)
            {
                sportsbook.GameDetails = sportsbook.GameDetails.Where(d => d.ResourceId.Contains(metaModel.Bettype.ToString()) && d.ResourceId.Contains(resourceId)).ToList();
            }
        }
    }
}