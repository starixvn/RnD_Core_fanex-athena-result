namespace Fanex.Result.Core.LeapGaming.Models
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Base.Exception;
    using Base.ViewModels;
    using Core.Models;
    using ViewModels;

    internal sealed class ViewModelConcreteFactory : ViewModelFactory
    {
        private static readonly Lazy<ViewModelFactory> LazyViewModel = new Lazy<ViewModelFactory>(() => new ViewModelConcreteFactory());

        private ViewModelConcreteFactory()
        {
        }

        public static ViewModelFactory Instance => LazyViewModel.Value;

        public override IResultViewModel GetViewModel<T>(T resultData, ResultSetting setting)
        {
            if ((int)setting.SportType <= 0)
            {
                throw new ObjectNullException("Sport Type");
            }

            var item = resultData as DTO.LeapGaming;
            var viewModel = new LeapGamingViewModel(item, setting);
            return (IResultViewModel)viewModel;
        }

        public override IResultViewModel GetOutrightViewModel(object resultData, ResultSetting setting)
        {
            throw new NotImplementedException();
        }

        public override IResultViewModel GetParlayViewModel(object resultData, ResultSetting setting)
        {
            throw new NotImplementedException();
        }

        public override Base.ExcelModels.IExcelModel GetExcelViewModel<T>(IList<T> dataList, ResultSetting setting)
        {
            throw new NotImplementedException();
        }
    }
}