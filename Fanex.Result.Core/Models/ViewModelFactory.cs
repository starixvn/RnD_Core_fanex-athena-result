// -----------------------------------------------------------------------
// <copyright file="ViewModelFactory.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System.Collections.Generic;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.ExcelModels;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal abstract class ViewModelFactory
    {
        public abstract object GetViewModel(object obj, ResultSetting setting, LangResources resource);

        public abstract object GetOutRightViewModel(object obj, ResultSetting setting, LangResources resource);

        public abstract object GetParlayViewModel(object obj, ResultSetting setting, LangResources resource);

        public abstract ExcelBase GetExcelViewModel<T>(IList<T> dataList, ResultSetting setting, LangResources resource);
    }
}