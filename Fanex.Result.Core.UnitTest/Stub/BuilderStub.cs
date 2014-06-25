namespace Fanex.Result.Core.UnitTest.Stub
{
    using System.Collections.Generic;
    using System.IO;
    using Fanex.Result.Core.Models;

    /// <summary>
    /// Define stub builder.
    /// </summary>
    public class BuilderStub : IBuilder
    {
        /// <summary>
        /// Render excel for outright report.
        /// </summary>
        /// <typeparam name="T">The DTO object.</typeparam>
        /// <param name="dataList">The DTO list.</param>
        /// <param name="language">The language code.</param>
        /// <param name="templatePath">The physical template path.</param>
        /// <param name="excelTitle">The excel title.</param>
        /// <returns>The memory stream.</returns>
        public MemoryStream OutRightExcelBuilder<T>(IList<T> dataList, string language, string templatePath, string excelTitle)
        {
            return new MemoryStream();
        }

        public string JSONSportTypeBuilder<T>(IList<T> dataList, SportType sportType, string language)
        {
            return "Test";
        }

        public string JSONParlayBuilder<T>(IList<T> dataList, string language)
        {
            return "Test";
        }

        public string JSONOutRightBuilder<T>(IList<T> dataList, string language)
        {
            return "Test";
        }

        public string HtmlBuilder<T>(IList<T> dataList, SportType sportType, string language)
        {
            return "Test";
        }

        public string PopUpParalyBuilder<T>(IList<T> dataList, string language)
        {
            return "Test";
        }

        public string PopUpHtmlBuilder<T>(IList<T> dataList, SportType sportType, bool isOutRight, string language)
        {
            return "Test";
        }

        public MemoryStream ExcelBuilder<T>(IList<T> dataList, SportType sportType, string language, string templatePath, string excelTitle)
        {
            return new MemoryStream();
        }

        public string OutRightHtmlBuilder<T>(IList<T> dataList, string language)
        {
            return "Test";
        }
    }
}