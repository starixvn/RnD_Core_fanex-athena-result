// -----------------------------------------------------------------------
// <copyright file="ViewModelConcreteFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.AllBetCasino.Models
{
    using System;
    using System.Collections.Generic;
    using Fanex.Result.Core.AllBetCasino.ViewModels;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class ViewModelConcreteFactory : ViewModelFactory
    {
        private static readonly Lazy<ViewModelFactory> LazyViewModelService = new Lazy<ViewModelFactory>(() => new ViewModelConcreteFactory());

        private ViewModelConcreteFactory()
        {
        }

        public static ViewModelFactory Instance
        {
            get
            {
                return LazyViewModelService.Value;
            }
        }

        public override IResultViewModel GetViewModel<T>(T obj, ResultSetting setting)
        {
            if ((int)setting.SportType <= 0)
            {
                throw new ObjectNullException("Sport Type");
            }

            var item = obj as DTO.AllBetCasino;
            var viewModel = new AllBetCasinoViewModel(item, setting);
            return (IResultViewModel)viewModel;
        }

        public override IResultViewModel GetOutrightViewModel(object obj, ResultSetting setting)
        {
            throw new NotImplementedException();
        }

        public override IResultViewModel GetParlayViewModel(object obj, ResultSetting setting)
        {
            throw new NotImplementedException();
        }

        public override Base.ExcelModels.IExcelModel GetExcelViewModel<T>(IList<T> dataList, ResultSetting setting)
        {
            throw new NotImplementedException();
        }
    }
}