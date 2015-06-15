// -----------------------------------------------------------------------
// <copyright file="ViewModelFactory.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System.Collections.Generic;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.ExcelModels;
    using Fanex.Result.Core.Base.ViewModels;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class ViewModelFactory
    {
        public abstract IResultViewModel GetViewModel<T>(T resultData, ResultSetting setting, LangResources resource);

        public abstract IResultViewModel GetOutRightViewModel(object resultData, ResultSetting setting, LangResources resource);

        public abstract IResultViewModel GetParlayViewModel(object resultData, ResultSetting setting, LangResources resource);

        public abstract IExcelModel GetExcelViewModel<T>(IList<T> dataList, ResultSetting setting, LangResources resource);
    }
}