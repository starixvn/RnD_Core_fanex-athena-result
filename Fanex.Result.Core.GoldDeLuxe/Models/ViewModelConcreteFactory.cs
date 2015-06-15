// -----------------------------------------------------------------------
// <copyright file="ViewModelConcreteFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.GoldDeLuxe.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Result.Core.Models;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Base.DTO;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.GoldDeLuxe.ViewModels;
    using Fanex.Result.Core.GoldDeLuxe.DTO;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Exception;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class ViewModelConcreteFactory : ViewModelFactory
    {
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
        public override IResultViewModel GetViewModel<T>(T obj, ResultSetting setting, LangResources resource)
        {
            if ((int)setting.SportType <= 0)
            {
                throw new ObjectNullException("Sport Type");
            }
            var item = obj as DTO.GoldDeluxe;
            var viewModel = new GoldDeluxeViewModel(item, setting, resource);
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
