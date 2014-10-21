// -----------------------------------------------------------------------
// <copyright file="ViewModelConcreteFactory.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.ExcelModels;
    using Fanex.Result.Core.Extension;
    using Fanex.Result.Core.ViewModels;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class ViewModelConcreteFactory : ViewModelFactory
    {
        private const string EXTNAME = "ViewModel";
        private const string EXCELEXTNAME = "Excel";
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
        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Reviewed.")]
        public override IResultViewModel GetViewModel(object obj, ResultSetting setting, LangResources resource)
        {
            if ((int)setting.SportType <= 0)
            {
                return null;
            }

            var viewModel = setting.SportType.StringMe() + EXTNAME;
            switch (viewModel)
            {
                case "BadmintonViewModel":
                    return new BadmintonViewModel((Sportsbook)obj, setting, resource);
                case "BaseBallViewModel":
                    return new BaseBallViewModel((Sportsbook)obj, setting, resource);
                case "BasketBallViewModel":
                    return new BasketBallViewModel((Sportsbook)obj, setting, resource);
                case "BilliardViewModel":
                    return new BilliardViewModel((Sportsbook)obj, setting, resource);
                case "HockeyViewModel":
                    return new HockeyViewModel((Sportsbook)obj, setting, resource);
                case "LiveCasinoViewModel":
                    return new LiveCasinoViewModel((LiveCasino)obj, setting, resource);
                case "NumberGameViewModel":
                    return new NumberGameViewModel((NumberGame)obj, setting, resource);
                case "OtherViewModel":
                    return new OtherViewModel((Sportsbook)obj, setting, resource);
                case "RacingViewModel":
                    return new RacingViewModel((Racing)obj, setting, resource);
                case "SoccerViewModel":
                    return new SoccerViewModel((Sportsbook)obj, setting, resource);
                case "TennisViewModel":
                    return new TennisViewModel((Sportsbook)obj, setting, resource);
                case "VSHRacingViewModel":
                    return new VSHRacingViewModel((Racing)obj, setting, resource);
                case "VSSpeedWayViewModel":
                    return new VSSpeedWayViewModel((Racing)obj, setting, resource);
                case "VSTennisViewModel":
                    return new VSTennisViewModel((Sportsbook)obj, setting, resource);
                default:
                    return null;
            }
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
            var templateName = setting.SportType.StringMe();
            var viewName = templateName + EXCELEXTNAME;
            var realTemplatePath = setting.ExcelTemplatePath + templateName + ".xls";
            IExcelModel result = null;
            switch (viewName)
            {
                case "BasketBallExcel":
                    result = new BasketballExcel(realTemplatePath, setting, ParserExcelData<T, BasketBallViewModel>(dataList, setting, resource), resource);
                    break;

                case "HockeyExcel":
                    result = new HockeyExcel(realTemplatePath, setting, ParserExcelData<T, HockeyViewModel>(dataList, setting, resource), resource);
                    break;

                case "TennisExcel":
                    result = new TennisExcel(realTemplatePath, setting, ParserExcelData<T, TennisViewModel>(dataList, setting, resource), resource);
                    break;

                case "BadmintonExcel":
                    result = new BadmintonExcel(realTemplatePath, setting, ParserExcelData<T, BadmintonViewModel>(dataList, setting, resource), resource);
                    break;

                case "BaseBallExcel":
                    result = new BaseBallExcel(realTemplatePath, setting, ParserExcelData<T, BaseBallViewModel>(dataList, setting, resource), resource);
                    break;

                case "BilliardExcel":

                    result = new BilliardExcel(realTemplatePath, setting, ParserExcelData<T, BilliardViewModel>(dataList, setting, resource), resource);
                    break;

                case "RacingExcel":

                    result = new RacingExcel(realTemplatePath, setting, ParserExcelData<T, RacingViewModel>(dataList, setting, resource), resource);
                    break;

                case "VSHRacingExcel":
                    result = new VSHRacingExcel(realTemplatePath, setting, ParserExcelData<T, VSHRacingViewModel>(dataList, setting, resource), resource);
                    break;

                case "VSSpeedWayExcel":
                    result = new VSSpeedWayExcel(realTemplatePath, setting, ParserExcelData<T, VSSpeedWayViewModel>(dataList, setting, resource), resource);
                    break;

                case "NumberGameExcel":
                    result = new NumberGameExcel(realTemplatePath, setting, ParserExcelData<T, NumberGameViewModel>(dataList, setting, resource), resource);
                    break;

                case "LiveCasinoExcel":
                    result = new LiveCasinoExcel(realTemplatePath, setting, ParserExcelData<T, LiveCasinoViewModel>(dataList, setting, resource), resource);
                    break;

                case "SoccerExcel":
                    result = new SoccerExcel(realTemplatePath, setting, ParserExcelData<T, SoccerViewModel>(dataList, setting, resource), resource);
                    break;

                case "VSTennisExcel":
                    result = new VSTennisExcel(realTemplatePath, setting, ParserExcelData<T, VSTennisViewModel>(dataList, setting, resource), resource);
                    break;

                default:
                    result = new OtherSportExcel(realTemplatePath, setting, ParserExcelData<T, OtherViewModel>(dataList, setting, resource), resource);
                    break;
            }

            return result;
        }

        private IList<T1> ParserExcelData<T, T1>(IList<T> dataList, ResultSetting setting, LangResources resource)
        {
            return
                dataList
                .Select(i => (T1)ViewModelConcreteFactory.Instance.GetViewModel(i, setting, resource))
                .ToList();
        }
    }
}