namespace MvcExample.Models.ResultData
{
    using System;
    using System.Collections.Generic;
    using Criteria;
    using Fanex.Result.Core.LiveCasino.DTO;
    using ResultServices;

    public class LiveCasinoResultData
    {
        private readonly LiveCasinoResultService resultService;

        public LiveCasinoResultData()
        {
            resultService = new LiveCasinoResultService();
        }

        public IEnumerable<LiveCasino> GetNormalResult(short sportId, DateTime date, int leagueId)
        {
            return resultService.GetResult(new GetLiveCasinoResultCriteria
            {
                FromDate = date,
                sporttype = (byte)sportId,
                isShowAll = true,
                LeagueID = leagueId
            });
        }

        public IEnumerable<LiveCasino> GetPopupResult(int licenseeId, int userId, int matchId)
        {
            return resultService.GetResult(new GetLiveCasinoPopupResultCriteria
            {
                LicenseeID = licenseeId,
                MatchID = matchId,
                UserID = userId
            });
        }
    }
}