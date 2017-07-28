namespace MvcExample.Models.ResultData
{
    using System;
    using System.Collections.Generic;
    using Criteria;
    using Fanex.Result.Core.AGCasino.DTO;
    using ResultServices;

    public class AGCasinoResultData
    {
        private readonly CasinoResultService resultService;

        public AGCasinoResultData()
        {
            resultService = new CasinoResultService();
        }

        public IEnumerable<AGCasino> GetPopupResult(long? refNo, int betType)
        {
            return resultService.GetResult(new GetAGCasinoPopUpResultCriteria
            {
                TransID = refNo.HasValue ? refNo.Value : 0,
                Bettype = betType
            });
        }
    }
}