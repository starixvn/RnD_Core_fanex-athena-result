namespace Fanex.Result.Core.Sportsbook.Models
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
    using DTO;
    using ViewModels;

    internal sealed class ViewModelConcreteFactory : ViewModelFactory
    {
        private const string EXTNAME = "ViewModel";
        private const string EXCELEXTNAME = "Excel";
        private const string NAMESPACE = "Fanex.Result.Core.Sportsbook.ViewModels.";
        private const string EXCELNAMESPACE = "Fanex.Result.Core.Sportsbook.ExcelModels.";

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

            var dtoName = setting.SportType.StringMe();
            var viewModel = Type.GetType(NAMESPACE + dtoName + EXTNAME);

            if (viewModel == null)
            {
                throw new ObjectNullException(dtoName);
            }

            if (SportsUseBasketBallViewModel(setting.SportType))
            {
                return (IResultViewModel)Activator.CreateInstance(viewModel, resultData, setting, null);
            }

            if (viewModel == typeof(OtherViewModel) || viewModel == typeof(KabaddiViewModel))
            {
                return (IResultViewModel)Activator.CreateInstance(viewModel, resultData, setting, null);
            }

            return (IResultViewModel)Activator.CreateInstance(viewModel, resultData, setting);
        }

        public override IResultViewModel GetOutrightViewModel(object resultData, ResultSetting setting)
        {
            return new OutrightViewModel((Outright)resultData, setting);
        }

        public override IResultViewModel GetParlayViewModel(object resultData, ResultSetting setting)
        {
            return new ParlayViewModel((Parlay)resultData, setting);
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

            var excelViewModel = Type.GetType(EXCELNAMESPACE + dtoName + EXCELEXTNAME);

            if (excelViewModel == null)
            {
                throw new ObjectNullException(dtoName);
            }

            return (IExcelModel)Activator.CreateInstance(excelViewModel, realTemplatePath, setting, dataMaped);
        }

        private static bool SportsUseBasketBallViewModel(SportType sportType)
            => sportType == SportType.BasketBall ||
                sportType == SportType.NetBall ||
                sportType == SportType.FootBall ||
                sportType == SportType.Rugby;
    }
}