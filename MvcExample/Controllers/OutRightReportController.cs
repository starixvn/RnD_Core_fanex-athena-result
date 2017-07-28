namespace MvcExample.Controllers
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Web.Mvc;
    using Fanex.Result.Core;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Models.Criteria;
    using Models.ResultServices;

    /// <summary>
    /// OutRight result page.
    /// </summary>
    public class OutRightReportController : Controller
    {
        private readonly IResultService<Outright> resultService;

        public OutRightReportController()
        {
            resultService = new OutRightResultService();
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gen HTML result.
        /// </summary>
        /// <param name="date">The result date.</param>
        /// <param name="languageCode">The language code.</param>
        /// <returns>The html result.</returns>
        [HttpPost]
        public string GenHtml(DateTime date, string languageCode)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);

            string htmlResult = string.Empty;
            var outRightData = resultService.GetResult(new GetOutRightResult
            {
                FromDate = date,
                isShowAll = true,
                ToDate = date
            });

            htmlResult = ResultEngine.HtmlParser<Outright>(
                    outRightData.ToList(),
                    new ResultSetting
                    {
                        LanguageCode = languageCode,
                        IsOutright = true,
                        IsPopUp = false
                    });
            return htmlResult;
        }

        /// <summary>
        /// Gen Excel result.
        /// </summary>
        /// <param name="date">The result date.</param>
        /// <param name="languageCode">The language code.</param>
        /// <returns>The Excel result.</returns>
        [HttpGet]
        public ActionResult GenExcel(DateTime date, string languageCode)
        {
            MemoryStream excelResult = null;
            var outRightData = resultService.GetResult(new GetOutRightResult
            {
                FromDate = date,
                isShowAll = true,
                ToDate = date
            });

            excelResult = ResultEngine.ExcelParser<Outright>(
                    outRightData.ToList(),
                    new ResultSetting
                    {
                        LanguageCode = languageCode,
                        IsOutright = true,
                        IsPopUp = false,
                        ExcelTitle = "OutRight Result Report",
                        ExcelTemplatePath = Server.MapPath(@"~/App_Data/Fanex.Result/")
                    });

            return File(excelResult.ToArray(), "application/vnd.ms-excel", "result-report.xls");
        }
    }
}