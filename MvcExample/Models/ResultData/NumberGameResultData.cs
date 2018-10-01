namespace MvcExample.Models.ResultData
{
    using System;
    using System.Collections.Generic;
    using System.Web.Hosting;
    using Criteria;
    using Fanex.Result.Core.NumberGame.DTO;
    using ResultServices;

    public class NumberGameResultData
    {
        private readonly NumberGameResultService resultService;

        public NumberGameResultData()
        {
            resultService = new NumberGameResultService();
        }

        public IEnumerable<NumberGame> GetNormalResult(DateTime date, int leagueId, bool getDataFromExcel)
        {
            if (getDataFromExcel)
            {
                var data = GetResultFromExcel();

                return data;
            }

            return resultService.GetResult(new GetNumberGameResultCriteria
            {
                FromDate = date,
                LeagueID = leagueId,
                ToDate = date
            });
        }

        public IEnumerable<NumberGame> GetPopupResult(int matchId)
        {
            return resultService.GetResult(new GetNumberGamePopupResultCriteria { MatchID = matchId });
        }

        public IEnumerable<NumberGame> GetResultFromExcel()
           => resultService.GetResultFromExcel(HostingEnvironment.MapPath(string.Format(Constant.ExcelFilePath, "NumberGame")));
    }
}