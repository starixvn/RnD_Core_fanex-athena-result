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
    using Fanex.Result.Core.DTO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IBuilder
    {
        string HtmlBuilder<T>(IList<T> dataList, ResultSetting setting);

        string HtmlGameDetailBuilder(Sportsbook data, ResultSetting setting);

        string PopUpParalyBuilder<T>(IList<T> dataList, ResultSetting setting);

        string PopUpHtmlBuilder<T>(IList<T> dataList, ResultSetting setting);

        MemoryStream ExcelBuilder<T>(IList<T> dataList, ResultSetting setting);

        string OutRightHtmlBuilder<T>(IList<T> dataList, ResultSetting setting);

        MemoryStream OutRightExcelBuilder<T>(IList<T> dataList, ResultSetting setting);
    }
}