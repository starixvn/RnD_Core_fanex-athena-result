namespace Fanex.Result.Core.Models
{
    using System.Collections.Generic;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.ExcelModels;
    using Fanex.Result.Core.Base.ViewModels;

    public abstract class ViewModelFactory
    {
        public abstract IResultViewModel GetViewModel<T>(T resultData, ResultSetting setting);

        public abstract IResultViewModel GetOutrightViewModel(object resultData, ResultSetting setting);

        public abstract IResultViewModel GetParlayViewModel(object resultData, ResultSetting setting);

        public abstract IExcelModel GetExcelViewModel<T>(IList<T> dataList, ResultSetting setting);
    }
}