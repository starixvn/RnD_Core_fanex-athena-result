namespace MvcExample.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.Result.Core;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Sportsbook.DTO;
    using MvcExample.Models;

    /// <summary>
    /// OutRight result page.
    /// </summary>
    public class OutRightReportController : Controller
    {
        private readonly IResultService _resultService;

        public OutRightReportController()
        {
            _resultService = new ResultService();
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
            string htmlResult = string.Empty;
            var outRightData = _resultService.GetOutRightResult(new Models.Criteria.GetOutRightResult
            {
                FromDate = date,
                isShowAll = true,
                ToDate = date
            });

            htmlResult = ResultEngine.HtmlParser<OutRight>(
                    outRightData.ToList(),
                    new ResultSetting
                    {
                        LanguageCode = languageCode,
                        IsOutRight = true,
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
            var outRightData = _resultService.GetOutRightResult(new Models.Criteria.GetOutRightResult
            {
                FromDate = date,
                isShowAll = true,
                ToDate = date
            });

            excelResult = ResultEngine.ExcelParser<OutRight>(
                    outRightData.ToList(),
                    new ResultSetting
                    {
                        LanguageCode = languageCode,
                        IsOutRight = true,
                        IsPopUp = false,
                        ExcelTitle = "OutRight Result Report",
                        ExcelTemplatePath = Server.MapPath(@"~/App_Data/Fanex.Result/")
                    });

            return File(excelResult.ToArray(), "application/vnd.ms-excel", "result-report.xls");
        }
    }
}