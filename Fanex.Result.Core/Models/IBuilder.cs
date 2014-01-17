// -----------------------------------------------------------------------
// <copyright file="IBuilder.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal interface IBuilder
    {
        string JSONSportTypeBuilder<T>(IList<T> dataList, SportType sportType, string language);

        string JSONParlayBuilder<T>(IList<T> dataList, string language);

        string JSONOutRightBuilder<T>(IList<T> dataList, string language);

        string HtmlBuilder<T>(IList<T> dataList, SportType sportType, string language);

        string PopUpParalyBuilder<T>(IList<T> dataList, string language);

        string PopUpHtmlBuilder<T>(IList<T> dataList, SportType sportType, bool isOutRight, string language);

        MemoryStream ExcelBuilder<T>(IList<T> dataList, SportType sportType, string language, string templatePath, string excelTitle);

        string OutRightHtmlBuilder<T>(IList<T> dataList, string language);

        MemoryStream OutRightExcelBuilder<T>(IList<T> dataList, string language, string templatePath, string excelTitle);
    }
}