// -----------------------------------------------------------------------
// <copyright file="IExcelParser.cs" company="Nexcel">
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
    internal interface IExcelParser
    {
        MemoryStream Parser<T>(
            IList<T> dataList,
            SportType sportType,
            string lang,
            string templatePath,
            string excelTitle);

        MemoryStream ParserOutRight<T>(
            IList<T> dataList,
            string lang,
            string templatePath,
            string excelTitle);
    }
}