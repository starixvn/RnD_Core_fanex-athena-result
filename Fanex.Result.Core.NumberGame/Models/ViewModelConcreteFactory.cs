namespace Fanex.Result.Core.NumberGame.Models
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
    using ExcelModels;
    using ViewModels;

    internal sealed class ViewModelConcreteFactory : ViewModelFactory
    {
        private static readonly Lazy<ViewModelFactory> LazyViewModelService = new Lazy<ViewModelFactory>(() => new ViewModelConcreteFactory());

        private ViewModelConcreteFactory()
        {
        }

        public static ViewModelFactory Instance => LazyViewModelService.Value;

        public override IResultViewModel GetViewModel<T>(T resultData, ResultSetting setting)
        {
            if (setting.SportType <= 0)
            {
                throw new ObjectNullException("Sport Type");
            }

            var item = resultData as DTO.NumberGame;
            var viewModel = new NumberGameViewModel(item, setting);
            return viewModel;
        }

        public override IResultViewModel GetOutrightViewModel(object resultData, ResultSetting setting)
        {
            throw new NotImplementedException();
        }

        public override IResultViewModel GetParlayViewModel(object resultData, ResultSetting setting)
        {
            throw new NotImplementedException();
        }

        public override IExcelModel GetExcelViewModel<T>(IList<T> dataList, ResultSetting setting)
        {
            if (setting.SportType <= 0)
            {
                throw new ObjectNullException("Sport Type");
            }

            var dtoName = setting.SportType.StringMe();

            var realTemplatePath = setting.ExcelTemplatePath + dtoName + ".xls";
            var dataMaped = dataList.Select(i => GetViewModel(i, setting)).ToSafeList();

            var viewModel = new NumberGameExcel(realTemplatePath, setting, dataMaped);

            return viewModel as IExcelModel;
        }
    }
}