// -----------------------------------------------------------------------
// <copyright file="IBuilder.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IBuilder
    {
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Reviewed")]
        string JSONSportTypeBuilder<T>(IList<T> dataList, SportType sportType, string language);

        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Reviewed")]
        string JSONParlayBuilder<T>(IList<T> dataList, string language);

        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Reviewed")]
        string JSONOutRightBuilder<T>(IList<T> dataList, string language);

        string HtmlBuilder<T>(IList<T> dataList, SportType sportType, string language);

        string PopUpParalyBuilder<T>(IList<T> dataList, string language);

        string PopUpHtmlBuilder<T>(IList<T> dataList, SportType sportType, bool isOutRight, string language);

        MemoryStream ExcelBuilder<T>(IList<T> dataList, SportType sportType, string language, string templatePath, string excelTitle);

        string OutRightHtmlBuilder<T>(IList<T> dataList, string language);

        MemoryStream OutRightExcelBuilder<T>(IList<T> dataList, string language, string templatePath, string excelTitle);
    }
}