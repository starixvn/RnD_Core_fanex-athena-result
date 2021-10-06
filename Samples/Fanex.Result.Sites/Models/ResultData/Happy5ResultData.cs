namespace MvcExample.Models.ResultData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Hosting;
    using Criteria;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Happy5.DTO;
    using Fanex.Result.Core.Sportsbook.DTO;
    using MetaModel;
    using ResultServices;


    public class Happy5ResultData
    {
        private readonly Happy5ResultService resultService;


        public Happy5ResultData()
        {
            resultService = new Happy5ResultService();
        }

        public IEnumerable<Happy5> GetNormalResult(DateTime date, int leagueId, int sportType, bool getDataFromExcel)
        {
            var data = GetResultFromExcel();

            return data;
        }

        public IEnumerable<Happy5> GetPopupResult(int matchId, bool getDataFromExcel)
        {
            if (getDataFromExcel)
            {
                var resultData = GetResultFromExcel();

                return resultData.Where(item => item.MatchId == matchId);
            }

            return resultService.GetResult(new GetNumberGamePopupResultCriteria { MatchID = matchId });
        }

        public IEnumerable<Happy5> GetResultFromExcel()
            => resultService.GetResultFromExcel(HostingEnvironment.MapPath(string.Format(Constant.ExcelFilePath, "Happy5")));

    }
}