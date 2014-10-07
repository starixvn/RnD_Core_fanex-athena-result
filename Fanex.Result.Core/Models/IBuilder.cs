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
        string JSONSportTypeBuilder<T>(IList<T> dataList, ResultSetting setting);

        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Reviewed")]
        string JSONParlayBuilder<T>(IList<T> dataList, ResultSetting setting);

        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Reviewed")]
        string JSONOutRightBuilder<T>(IList<T> dataList, ResultSetting setting);

        string HtmlBuilder<T>(IList<T> dataList, ResultSetting setting);

        string PopUpParalyBuilder<T>(IList<T> dataList, ResultSetting setting);

        string PopUpHtmlBuilder<T>(IList<T> dataList, ResultSetting setting);

        MemoryStream ExcelBuilder<T>(IList<T> dataList, ResultSetting setting);

        string OutRightHtmlBuilder<T>(IList<T> dataList, ResultSetting setting);

        MemoryStream OutRightExcelBuilder<T>(IList<T> dataList, ResultSetting setting);
    }
}