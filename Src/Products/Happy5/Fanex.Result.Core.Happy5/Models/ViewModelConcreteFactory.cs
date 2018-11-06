namespace Fanex.Result.Core.Happy5.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.ExcelModels;
    using Base.Exception;
    using Base.Extension;
    using Base.ViewModels;
    using Core.Models;
    using ViewModels;

    public sealed class ViewModelConcreteFactory : ViewModelFactory
    {
        private static readonly Lazy<ViewModelFactory> LazyViewModelService = new Lazy<ViewModelFactory>(() => new ViewModelConcreteFactory());

        public static ViewModelFactory Instance => LazyViewModelService.Value;

        public override IResultViewModel GetViewModel<T>(T resultData, ResultSetting setting)
        {
            CheckSportTypeIsValid((int)setting.SportType);

            return new Happy5ViewModel(resultData as DTO.Happy5, setting, ResultCoreManager.RefDataHelper);
        }

        public override IExcelModel GetExcelViewModel<T>(IList<T> dataList, ResultSetting setting)
        {
            CheckSportTypeIsValid((int)setting.SportType);

            var absoluteTemplatePath = $"{setting.ExcelTemplatePath}{setting.SportType.StringMe()}.xls";
            var resultDataList = dataList.Select(i => GetViewModel(i, setting)).ToSafeList();

            return new Happy5ExcelViewModel(absoluteTemplatePath, setting, resultDataList);
        }

        public override IResultViewModel GetOutrightViewModel(object resultData, ResultSetting setting)
        {
            throw new NotImplementedException();
        }

        public override IResultViewModel GetParlayViewModel(object resultData, ResultSetting setting)
        {
            throw new NotImplementedException();
        }

        private static void CheckSportTypeIsValid(int sportType)
        {
            if (sportType <= 0)
            {
                throw new ObjectNullException("Sport Type");
            }
        }
    }
}