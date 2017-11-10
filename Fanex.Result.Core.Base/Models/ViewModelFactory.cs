namespace Fanex.Result.Core.Models
{
    using System.Collections.Generic;
    using Base;
    using Base.ExcelModels;
    using Base.ViewModels;

#pragma warning disable S1694 // An abstract class should have both abstract and concrete methods

    public abstract class ViewModelFactory
#pragma warning restore S1694 // An abstract class should have both abstract and concrete methods
    {
        public abstract IResultViewModel GetViewModel<T>(T resultData, ResultSetting setting);

        public abstract IResultViewModel GetOutrightViewModel(object resultData, ResultSetting setting);

        public abstract IResultViewModel GetParlayViewModel(object resultData, ResultSetting setting);

        public abstract IExcelModel GetExcelViewModel<T>(IList<T> dataList, ResultSetting setting);
    }
}