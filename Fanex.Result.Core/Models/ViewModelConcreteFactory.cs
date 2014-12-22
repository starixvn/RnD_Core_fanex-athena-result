// -----------------------------------------------------------------------
// <copyright file="ViewModelConcreteFactory.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.ExcelModels;
    using Fanex.Result.Core.Exception;
    using Fanex.Result.Core.Extension;
    using Fanex.Result.Core.ViewModels;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class ViewModelConcreteFactory : ViewModelFactory
    {
        private const string EXTNAME = "ViewModel";
        private const string EXCELEXTNAME = "Excel";
        private const string NAMESPACE = "Fanex.Result.Core.ViewModels.";
        private const string EXCELNAMESPACE = "Fanex.Result.Core.ExcelModels.";

        private static readonly object SynceObject = new object();

        private static ViewModelFactory instance;

        private ViewModelConcreteFactory()
        {
        }

        public static ViewModelFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SynceObject)
                    {
                        if (instance == null)
                        {
                            instance = new ViewModelConcreteFactory();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Get view model.
        /// Used for render html result.
        /// </summary>
        /// <param name="obj">The DTO type.</param>
        /// <param name="setting">The result setting.</param>
        /// <param name="resource">The language resource.</param>
        /// <returns>The view model.</returns>
        public override IResultViewModel GetViewModel(object obj, ResultSetting setting, LangResources resource)
        {
            if ((int)setting.SportType <= 0)
            {
                throw new ObjectNullException("Sport Type");
            }

            var dtoName = setting.SportType.StringMe();
            var viewModel = Type.GetType(NAMESPACE + dtoName + EXTNAME);

            if (viewModel == null)
            {
                throw new ObjectNullException(dtoName);
            }

            return (IResultViewModel)Activator.CreateInstance(viewModel, obj, setting, resource);
        }

        public override IResultViewModel GetOutRightViewModel(object obj, ResultSetting setting, LangResources resource)
        {
            return new OutRightViewModel((OutRight)obj, setting, resource);
        }

        public override IResultViewModel GetParlayViewModel(object obj, ResultSetting setting, LangResources resource)
        {
            return new ParlayViewModel((Parlay)obj, setting, resource);
        }

        /// <summary>
        /// Get Excel View Model.
        /// </summary>
        /// <typeparam name="T">The Result DTO.</typeparam>
        /// <param name="dataList">The data list.</param>
        /// <param name="setting">The result setting.</param>
        /// <param name="resource">The resource.</param>
        /// <returns>The excel view model.</returns>
        public override IExcelModel GetExcelViewModel<T>(IList<T> dataList, ResultSetting setting, LangResources resource)
        {
            if ((int)setting.SportType <= 0)
            {
                throw new ObjectNullException("Sport Type");
            }

            var dtoName = setting.SportType.StringMe();

            var realTemplatePath = setting.ExcelTemplatePath + dtoName + ".xls";
            var dataMaped = dataList.Select(i => this.GetViewModel(i, setting, resource)).ToList();
            var excelViewModel = Type.GetType(EXCELNAMESPACE + dtoName + EXCELEXTNAME);
            if (excelViewModel == null)
            {
                throw new ObjectNullException(dtoName);
            }

            return (IExcelModel)Activator.CreateInstance(excelViewModel, realTemplatePath, setting, dataMaped, resource);
        }
    }
}