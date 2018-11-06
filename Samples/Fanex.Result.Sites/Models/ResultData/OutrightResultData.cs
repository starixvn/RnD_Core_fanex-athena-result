namespace MvcExample.Models.ResultData
{
    using System;
    using System.Collections.Generic;
    using System.Web.Hosting;
    using Criteria;
    using Fanex.Result.Core.Sportsbook.DTO;
    using ResultServices;

    public class OutRightResultData
    {
        private readonly OutRightResultService resultService;

        public OutRightResultData()
        {
            resultService = new OutRightResultService();
        }

        public IEnumerable<Outright> GetResult(DateTime date, bool getDataFromExcel)
        {
            if (getDataFromExcel)
            {
                return GetResultFromExcel("outright");
            }

            return resultService.GetResult(new GetOutRightResult
            {
                FromDate = date,
                isShowAll = false,
                ToDate = date
            });
        }

        public IEnumerable<Outright> GetResultFromExcel(string fileName)
            => resultService.GetResultFromExcel(HostingEnvironment.MapPath(string.Format(Constant.ExcelFilePath, fileName)));
    }
}