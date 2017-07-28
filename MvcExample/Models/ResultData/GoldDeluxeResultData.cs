namespace MvcExample.Models.ResultData
{
    using System;
    using System.Collections.Generic;
    using Criteria;
    using Fanex.Result.Core.GoldDeluxe.DTO;
    using ResultServices;

    public class GoldDeluxeResultData
    {
        private readonly GoldDeluxeResultService resultService;

        public GoldDeluxeResultData()
        {
            resultService = new GoldDeluxeResultService();
        }

        public IEnumerable<GoldDeluxe> GetNormalResult()
        {
            return new GoldDeluxeResultService().GetResult(new GetGolDeluxePopUpResultCriteria
            {
                TransID = 7396592187,
                Bettype = 1701
            });
        }

        public IEnumerable<GoldDeluxe> GetPopupResult(long? refNo, int betType)
        {
            return new GoldDeluxeResultService().GetResult(new GetGolDeluxePopUpResultCriteria
            {
                TransID = refNo.HasValue ? refNo.Value : 0,
                Bettype = betType
            });
        }
    }
}