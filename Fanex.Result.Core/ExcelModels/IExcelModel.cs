// -----------------------------------------------------------------------
// <copyright file="IExcelModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ExcelModels
{
    using System.IO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IExcelModel
    {
        MemoryStream Export();
    }
}