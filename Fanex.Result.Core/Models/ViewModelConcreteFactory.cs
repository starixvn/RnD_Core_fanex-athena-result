// -----------------------------------------------------------------------
// <copyright file="ViewModelConcreteFactory.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System.Collections.Generic;
    using System.Linq;
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
        /// <param name="obj">The object.</param>
        /// <param name="sportType">The sport type.</param>
        /// <param name="languageCode">The language code.</param>
        /// <param name="resource">The language resource.</param>
        /// <returns>The view model.</returns>
        public override object GetViewModel(object obj, SportType sportType, string languageCode, LangResources resource)
        {
            if ((int)sportType <= 0)
            {
                return null;
            }

            var viewModel = sportType.StringMe() + EXTNAME;
            switch (viewModel)
            {
                case "BadmintonViewModel":
                    return new BadmintonViewModel((Sportbook)obj, sportType, languageCode, resource);
                case "BaseBallViewModel":
                    return new BaseBallViewModel((Sportbook)obj, sportType, languageCode, resource);
                case "BasketBallViewModel":
                    return new BasketBallViewModel((Sportbook)obj, sportType, languageCode, resource);
                case "BilliardViewModel":
                    return new BilliardViewModel((Sportbook)obj, sportType, languageCode, resource);
                case "HockeyViewModel":
                    return new HockeyViewModel((Sportbook)obj, sportType, languageCode, resource);
                case "LiveCasinoViewModel":
                    return new LiveCasinoViewModel((LiveCasino)obj, sportType, languageCode, resource);
                case "NumberGameViewModel":
                    return new NumberGameViewModel((NumberGame)obj, sportType, languageCode, resource);
                case "OtherViewModel":
                    return new OtherViewModel((Sportbook)obj, sportType, languageCode, resource);
                case "RacingViewModel":
                    return new RacingViewModel((Racing)obj, sportType, languageCode, resource);
                case "SoccerViewModel":
                    return new SoccerViewModel((Sportbook)obj, sportType, languageCode, resource);
                case "TennisViewModel":
                    return new TennisViewModel((Sportbook)obj, sportType, languageCode, resource);
                case "VSHRacingViewModel":
                    return new VSHRacingViewModel((Racing)obj, sportType, languageCode, resource);
                case "VSSpeedWayViewModel":
                    return new VSSpeedWayViewModel((Racing)obj, sportType, languageCode, resource);
                case "VSTennisViewModel":
                    return new VSTennisViewModel((Sportbook)obj, sportType, languageCode, resource);
                default:
                    return null;
            }
        }

        public override object GetOutRightViewModel(object obj, string languageCode, LangResources resource)
        {
            return new OutRightViewModel((OutRight)obj, languageCode, resource);
        }

        public override object GetParlayViewModel(object obj, string languageCode, LangResources resource)
        {
            return new ParlayViewModel((Parlay)obj, languageCode, resource);
        }

        public override ExcelBase GetExcelViewModel<T>(IList<T> dataList, ResultSetting setting, LangResources resource)
        {
            var templateName = setting.SportType.StringMe();
            var viewName = templateName + EXCELEXTNAME;
            var realTemplatePath = setting.ExcelTemplatePath + templateName + ".xls";
            switch (viewName)
            {
                case "BasketBallExcel":

                    return new BasketballExcel(realTemplatePath, setting.ExcelTitle, ParserExcelData<T, BasketBallViewModel>(dataList, setting.SportType, setting.LanguageCode, resource), resource);
                case "HockeyExcel":
                    return new HockeyExcel(realTemplatePath, setting.ExcelTitle, ParserExcelData<T, HockeyViewModel>(dataList, setting.SportType, setting.LanguageCode, resource), resource);

                case "TennisExcel":
                    return new TennisExcel(realTemplatePath, setting.ExcelTitle, ParserExcelData<T, TennisViewModel>(dataList, setting.SportType, setting.LanguageCode, resource), resource);

                case "BadmintonExcel":
                    return new BadmintonExcel(realTemplatePath, setting.ExcelTitle, ParserExcelData<T, BadmintonViewModel>(dataList, setting.SportType, setting.LanguageCode, resource), resource);

                case "BaseBallExcel":
                    return new BaseBallExcel(realTemplatePath, setting.ExcelTitle, ParserExcelData<T, BaseBallViewModel>(dataList, setting.SportType, setting.LanguageCode, resource), resource);

                case "BilliardExcel":

                    return new BilliardExcel(realTemplatePath, setting.ExcelTitle, ParserExcelData<T, BilliardViewModel>(dataList, setting.SportType, setting.LanguageCode, resource), resource);

                case "RacingExcel":

                    return new RacingExcel(realTemplatePath, setting.ExcelTitle, ParserExcelData<T, RacingViewModel>(dataList, setting.SportType, setting.LanguageCode, resource), resource);

                case "VSHRacingExcel":
                    return new VSHRacingExcel(realTemplatePath, setting.ExcelTitle, ParserExcelData<T, VSHRacingViewModel>(dataList, setting.SportType, setting.LanguageCode, resource), resource);

                case "VSSpeedWayExcel":
                    return new VSSpeedWayExcel(realTemplatePath, setting.ExcelTitle, ParserExcelData<T, VSSpeedWayViewModel>(dataList, setting.SportType, setting.LanguageCode, resource), resource);

                case "NumberGameExcel":
                    return new NumberGameExcel(realTemplatePath, setting.ExcelTitle, ParserExcelData<T, NumberGameViewModel>(dataList, setting.SportType, setting.LanguageCode, resource), resource);

                case "LiveCasinoExcel":
                    return new LiveCasinoExcel(realTemplatePath, setting.ExcelTitle, ParserExcelData<T, LiveCasinoViewModel>(dataList, setting.SportType, setting.LanguageCode, resource), resource);

                case "SoccerExcel":
                    return new SoccerExcel(realTemplatePath, setting.ExcelTitle, ParserExcelData<T, SoccerViewModel>(dataList, setting.SportType, setting.LanguageCode, resource), resource);

                case "VSTennisExcel":
                    return new VSTennisExcel(realTemplatePath, setting.ExcelTitle, ParserExcelData<T, VSTennisViewModel>(dataList, setting.SportType, setting.LanguageCode, resource), resource);

                default:
                    return new OtherSportExcel(realTemplatePath, setting.ExcelTitle, ParserExcelData<T, OtherViewModel>(dataList, setting.SportType, setting.LanguageCode, resource), resource);
            }
        }

        private IList<T1> ParserExcelData<T, T1>(IList<T> dataList, SportType sportType, string languageCode, LangResources resource)
        {
            return
                dataList
                .Select(i => (T1)ViewModelConcreteFactory.Instance.GetViewModel(i, sportType, languageCode, resource))
                .ToList();
        }
    }
}