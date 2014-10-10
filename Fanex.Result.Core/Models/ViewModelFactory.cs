// -----------------------------------------------------------------------
// <copyright file="ViewModelFactory.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System.Collections.Generic;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.ExcelModels;
    using Fanex.Result.Core.ViewModels;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal abstract class ViewModelFactory
    {
        public abstract IResultViewModel GetViewModel(object obj, ResultSetting setting, LangResources resource);

        public abstract object GetOutRightViewModel(object obj, ResultSetting setting, LangResources resource);

        public abstract object GetParlayViewModel(object obj, ResultSetting setting, LangResources resource);

        public abstract ExcelBase GetExcelViewModel<T>(IList<T> dataList, ResultSetting setting, LangResources resource);
    }
}