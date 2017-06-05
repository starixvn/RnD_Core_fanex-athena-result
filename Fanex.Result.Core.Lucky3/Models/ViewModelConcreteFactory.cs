namespace Fanex.Result.Core.Lucky3.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Models;
    using Base;
    using Base.ExcelModels;
    using Base.ViewModels;
    using Base.Exception;
    using Base.Extension;
    using ViewModels;

    internal sealed class ViewModelConcreteFactory : ViewModelFactory
    {
        private static readonly Lazy<ViewModelFactory> LazyViewModelService = new Lazy<ViewModelFactory>(() => new ViewModelConcreteFactory());

        public static ViewModelFactory Instance => LazyViewModelService.Value;

        public override IResultViewModel GetViewModel<T>(T resultData, ResultSetting setting)
        {
            CheckSportTypeIsValid((int)setting.SportType);

            return new Lucky3ViewModel(resultData as DTO.Lucky3, setting);
        }

        public override IExcelModel GetExcelViewModel<T>(IList<T> dataList, ResultSetting setting)
        {
            CheckSportTypeIsValid((int)setting.SportType);

            var absoluteTemplatePath = $"{setting.ExcelTemplatePath}{setting.SportType.StringMe()}.xls";
            var resultDataList = dataList.Select(i => GetViewModel(i, setting)).ToSafeList();

            return new Lucky3ExcelViewModel(absoluteTemplatePath, setting, resultDataList);
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