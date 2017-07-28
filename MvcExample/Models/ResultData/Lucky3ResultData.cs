namespace MvcExample.Models.ResultData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Hosting;
    using Criteria;
    using Fanex.Result.Core.Lucky3.DTO;
    using ResultServices;

    public class Lucky3ResultData
    {
        private readonly Lucky3ResultService resultService;

        public Lucky3ResultData()
        {
            resultService = new Lucky3ResultService();
        }

        public IEnumerable<Lucky3> GetNormalResult(DateTime date, int leagueId, bool getDataFromExcel)
        {
            if (getDataFromExcel)
            {
                return GetResultFromExcel();
            }

            return resultService.GetResult(new GetNumberGameResultCriteria
            {
                FromDate = date,
                LeagueID = leagueId,
                ToDate = date
            });
        }

        public IEnumerable<Lucky3> GetPopupResult(int matchId, bool getDataFromExcel)
        {
            if (getDataFromExcel)
            {
                var resultData = GetResultFromExcel();

                return resultData.Where(item => item.MatchId == matchId);
            }

            return resultService.GetResult(new GetNumberGamePopupResultCriteria { MatchID = matchId });
        }

        public IEnumerable<Lucky3> GetResultFromExcel()
            => resultService.GetResultFromExcel(HostingEnvironment.MapPath(string.Format(Constant.ExcelFilePath, "Lucky3")));
    }
}