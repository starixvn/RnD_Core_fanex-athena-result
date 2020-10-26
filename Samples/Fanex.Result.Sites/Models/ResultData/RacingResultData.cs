namespace MvcExample.Models.ResultData
{
    using System;
    using System.Collections.Generic;
    using System.Web.Hosting;
    using Criteria;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Racing.DTO;
    using ResultServices;

    public class RacingResultData
    {
        private readonly RacingResultService resultService;

        public RacingResultData()
        {
            resultService = new RacingResultService();
        }

        public IEnumerable<Racing> GetNormalResult(short sportId, DateTime date, int leagueId)
        {
            return resultService.GetResult(new GetRacingResultCriteria
            {
                MoveBetsDay = BaseResultService.GetBalanceUpDay().MoveBetsDay,
                Sporttype = sportId,
                FromDate = date,
                isShowAll = true,
                LeagueID = leagueId
            });
        }

        public IEnumerable<Racing> GetPopupResult(int licenseeId, int userId, int matchId, int? raceNo)
        {
            return resultService.GetResult(new GetRacingPopupResultCriteria
            {
                LicenseeID = licenseeId,
                UserID = userId,
                MatchID = matchId,
                RaceNo = raceNo.HasValue ? raceNo.Value.ToString() : null
            });
        }
    }
}