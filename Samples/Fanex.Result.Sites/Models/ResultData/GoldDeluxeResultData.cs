namespace MvcExample.Models.ResultData
{
    using System.Collections.Generic;
    using System.Web.Hosting;
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

        public IEnumerable<GoldDeluxe> GetNormalResult(bool isFromExcel)
        {
            if (isFromExcel)
            {
                return GetResultFromExcel();
            }

            return resultService.GetResult(new GetGolDeluxePopUpResultCriteria
            {
                TransID = 7396592187,
                Bettype = 1701
            });
        }

        public IEnumerable<GoldDeluxe> GetPopupResult(long? refNo, int betType, bool isFromExcel)
        {
            if (isFromExcel)
            {
                return GetResultFromExcel();
            }

            return resultService.GetResult(new GetGolDeluxePopUpResultCriteria
            {
                TransID = refNo.HasValue ? refNo.Value : 0,
                Bettype = betType
            });
        }

        public IEnumerable<GoldDeluxe> GetResultFromExcel()
            => resultService.GetResultFromExcel(HostingEnvironment.MapPath(string.Format(Constant.ExcelFilePath, nameof(GoldDeluxe))));
    }
}