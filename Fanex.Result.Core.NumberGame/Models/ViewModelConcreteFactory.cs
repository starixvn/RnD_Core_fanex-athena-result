// -----------------------------------------------------------------------
// <copyright file="ViewModelConcreteFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.NumberGame.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.ExcelModels;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Models;
    using Fanex.Result.Core.NumberGame.ExcelModels;
    using Fanex.Result.Core.NumberGame.ViewModels;

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

            var item = obj as DTO.NumberGame;
            var viewModel = new NumberGameViewModel(item, setting, resource);
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
            if ((int)setting.SportType <= 0)
            {
                throw new ObjectNullException("Sport Type");
            }

            var dtoName = setting.SportType.StringMe();

            var realTemplatePath = setting.ExcelTemplatePath + dtoName + ".xls";
            var dataMaped = dataList.Select(i => this.GetViewModel(i, setting, resource)).ToList();

            var viewModel = new NumberGameExcel(realTemplatePath, setting, dataMaped, resource);

            return viewModel as IExcelModel;
        }
    }
}