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
    using MvcExample.Models.ResultData;

    public class OutRightReportController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string GenHtml(DateTime date, string languageCode, bool getDataFromExcel)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);
            var htmlResult = string.Empty;
            var outRightData = new OutRightResultData().GetResult(date, getDataFromExcel);

            htmlResult = ResultEngine.HtmlParser(
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
        public ActionResult GenExcel(DateTime date, string languageCode, bool getDataFromExcel)
        {
            MemoryStream excelResult = null;
            var outRightData = new OutRightResultData().GetResult(date, getDataFromExcel);

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