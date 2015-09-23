// -----------------------------------------------------------------------
// <copyright file="ViewModelConcreteFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.AGCasino.Models
{
    using System;
    using System.Collections.Generic;
    using Fanex.Result.Core.AGCasino.ViewModels;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
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

        public override IResultViewModel GetViewModel<T>(T obj, ResultSetting setting, LangResources resource)
        {
            if ((int)setting.SportType <= 0)
            {
                throw new ObjectNullException("Sport Type");
            }

            var item = obj as DTO.AGCasino;
            var viewModel = new AGCasinoViewModel(item, setting, resource);
            return (IResultViewModel)viewModel;
        }

        public override IResultViewModel GetOutRightViewModel(object obj, ResultSetting setting, LangResources resource)
        {
            throw new NotImplementedException();
        }

        public override IResultViewModel GetParlayViewModel(object obj, ResultSetting setting, LangResources resource)
        {
            throw new NotImplementedException();
        }

        public override Base.ExcelModels.IExcelModel GetExcelViewModel<T>(IList<T> dataList, ResultSetting setting, LangResources resource)
        {
            throw new NotImplementedException();
        }
    }
}