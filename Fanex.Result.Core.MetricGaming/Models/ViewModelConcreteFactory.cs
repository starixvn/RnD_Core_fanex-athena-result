// -----------------------------------------------------------------------
// <copyright file="ViewModelConcreteFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.MetricGaming.Models
{
    using System;
    using System.Collections.Generic;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.ExcelModels;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.MetricGaming.ViewModels;
    using Fanex.Result.Core.Models;

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

        public override IResultViewModel GetViewModel<T>(T obj, ResultSetting setting, LangResources resource)
        {
            if ((int)setting.SportType <= 0)
            {
                throw new ObjectNullException("Sport Type");
            }

            var item = obj as DTO.MetricGaming;
            var viewModel = new SoccerViewModel(item, setting, resource);

            return (IResultViewModel)viewModel;
        }

        public override IResultViewModel GetOutRightViewModel(object resultData, ResultSetting setting, LangResources resource)
        {
            throw new NotImplementedException();
        }

        public override IResultViewModel GetParlayViewModel(object resultData, ResultSetting setting, LangResources resource)
        {
            throw new NotImplementedException();
        }

        public override IExcelModel GetExcelViewModel<T>(IList<T> dataList, ResultSetting setting, LangResources resource)
        {
            throw new NotImplementedException();
        }
    }
}