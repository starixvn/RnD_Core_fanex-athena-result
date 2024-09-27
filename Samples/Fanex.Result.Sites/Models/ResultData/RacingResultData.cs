namespace MvcExample.Models.ResultData
{
    using System;
    using System.Collections.Generic;
    using System.Web.Hosting;
    using Criteria;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.VirtualRacing.DTO;
    using ResultServices;

    public class RacingResultData
    {
        private readonly RacingResultService resultService;

        public RacingResultData()
        {
            resultService = new RacingResultService();
        }        

        public IEnumerable<VirtualRacing> GetPopupResult(int licenseeId, int userId, int matchId, int? raceNo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VirtualRacing> GetNormalResult(short sportId, DateTime date, int leagueId)
            => resultService.GetResultFromExcel(HostingEnvironment.MapPath(string.Format(Constant.ExcelFilePath, (SportType)sportId)));
    }
}