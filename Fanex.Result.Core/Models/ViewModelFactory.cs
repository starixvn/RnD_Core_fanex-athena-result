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

        public abstract IResultViewModel GetOutRightViewModel(object obj, ResultSetting setting, LangResources resource);

        public abstract IResultViewModel GetParlayViewModel(object obj, ResultSetting setting, LangResources resource);

        public abstract IExcelModel GetExcelViewModel<T>(IList<T> dataList, ResultSetting setting, LangResources resource);
    }
}