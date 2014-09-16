namespace MvcExample.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.Result.Core;
    using MvcExample.Models;

    public class OutRightReportController : Controller
    {
        private readonly IResultService _resultService;

        //
        // GET: /Home/
        public OutRightReportController()
        {
            _resultService = new ResultService();
        }

        //
        // GET: /OutRightReport/

        public ActionResult Index()
        {
            return View();
        }

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

            htmlResult = ResultEngine.HtmlParser<Fanex.Result.Core.DTO.OutRight>(
                    outRightData.ToList(),
                    new ResultSetting
                    {
                        LanguageCode = languageCode,
                        IsOutRight = true,
                        IsPopUp = false
                    });
            return htmlResult;
        }

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

            excelResult = ResultEngine.ExcelParser<Fanex.Result.Core.DTO.OutRight>(
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

        [HttpPost]
        public string GenJson(DateTime date, string languageCode)
        {
            string jsonResult = string.Empty;
            var outRightData = _resultService.GetOutRightResult(new Models.Criteria.GetOutRightResult
            {
                FromDate = date,
                isShowAll = true,
                ToDate = date
            });

            jsonResult = ResultEngine.JsonParser<Fanex.Result.Core.DTO.OutRight>(
                    outRightData.ToList(),
                    new ResultSetting
                    {
                        LanguageCode = languageCode,
                        IsOutRight = true,
                        IsPopUp = false,
                        ExcelTitle = "OutRight Result Report",
                        ExcelTemplatePath = Server.MapPath(@"~/App_Data/Fanex.Result/")
                    });

            return jsonResult;
        }
    }
}