// -----------------------------------------------------------------------
// <copyright file="ExcelParser.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using Fanex.Result.Core.ExcelModels;
    using Fanex.Result.Core.ViewModels;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class ExcelParser : IExcelParser
    {
        /// <summary>
        /// Build excel stream.
        /// </summary>
        /// <typeparam name="T">The DTO object.</typeparam>
        /// <param name="dataList">The DTO list.</param>
        /// <param name="sportType">The sport type.</param>
        /// <param name="language">The language code.</param>
        /// <param name="templatePath">The physical template path.</param>
        /// <param name="excelTitle">The excel title.</param>
        /// <returns>The memory stream.</returns>
        [SuppressMessage(
           "StyleCop.CSharp.Nexcel.NexcelCustomRules",
           "SP2101:MethodMustNotContainMoreLinesThan",
           Justification = "Reviewed. Suppression is OK here.")]
        public MemoryStream Parser<T>(IList<T> dataList, SportType sportType, string language, string templatePath, string excelTitle)
        {
            var lang = Resources.Instance.GetLanguage(language);
            string realTemplatePath;
            switch (sportType)
            {
                case SportType.BasketBall:
                case SportType.FootBall:
                case SportType.NetBall:
                case SportType.Rugby:
                    return ParserBasketBall<T>(dataList, sportType, language, templatePath, excelTitle, lang, out realTemplatePath);
                case SportType.IceHockey:
                case SportType.FieldHockey:
                    return ParserHockey<T>(dataList, sportType, language, templatePath, excelTitle, lang, out realTemplatePath);
                case SportType.Tennis:
                    return ParserTennis<T>(dataList, sportType, language, templatePath, excelTitle, lang, out realTemplatePath);
                case SportType.VolleyBall:
                case SportType.Badminton:
                case SportType.Squash:
                    return ParserVolleyBall<T>(dataList, sportType, language, templatePath, excelTitle, lang, out realTemplatePath);
                case SportType.BaseBall:
                    return ParserBaseBall<T>(dataList, sportType, language, templatePath, excelTitle, lang, out realTemplatePath);
                case SportType.Billard:
                case SportType.Golf:
                case SportType.Motorsports:
                case SportType.Winter:
                case SportType.Cycling:
                case SportType.Fencing:
                case SportType.Judo:
                case SportType.ModernPentathlon:
                case SportType.Rowing:
                case SportType.Sailing:
                case SportType.Shooting:
                case SportType.Taekwondo:
                case SportType.Triathlon:
                case SportType.Wrestling:
                case SportType.HorseRacingFixedOdd:
                    return ParserBillard<T>(dataList, sportType, language, templatePath, excelTitle, lang, out realTemplatePath);
                case SportType.HorseRacing:
                case SportType.Greyhound:
                case SportType.Harness:
                    return ParserRacing<T>(dataList, sportType, language, templatePath, excelTitle, lang, out realTemplatePath);
                case SportType.VirtualHorseRacing:
                case SportType.VirtualGreyhounds:
                case SportType.VirtualMotorRacing:
                case SportType.VirtualCycling:
                    return ParserVirtualHorseRacing<T>(dataList, sportType, language, templatePath, excelTitle, lang, out realTemplatePath);
                case SportType.VirtualSpeedway:
                    return ParserVirtualSpeedWay<T>(dataList, sportType, language, templatePath, excelTitle, lang, out realTemplatePath);

                case SportType.NumberGame:
                    return ParserNumberGame<T>(dataList, sportType, language, templatePath, excelTitle, lang, out realTemplatePath);
                case SportType.LiveCasino:
                    return ParserLiveCasino<T>(dataList, sportType, language, templatePath, excelTitle, lang, out realTemplatePath);
                case SportType.Soccer:
                    return ParserSoccer<T>(dataList, sportType, language, templatePath, excelTitle, lang, out realTemplatePath);
                case SportType.VirtualTennis:
                    return
                    ParserVSTennis<T>(dataList, sportType, language, templatePath, excelTitle, lang, out realTemplatePath);
                default:
                    return ParserOther<T>(dataList, sportType, language, templatePath, excelTitle, lang, out realTemplatePath);
            }
        }

        /// <summary>
        /// Create excel file for outright bet-type.
        /// </summary>
        /// <typeparam name="T">The outright object.</typeparam>
        /// <param name="dataList">The outright data list.</param>
        /// <param name="language">The language code.</param>
        /// <param name="templatePath">The physical template path.</param>
        /// <param name="excelTitle">The excel title.</param>
        /// <returns>The memory stream.</returns>
        public MemoryStream ParserOutRight<T>(IList<T> dataList, string language, string templatePath, string excelTitle)
        {
            var lang = Resources.Instance.GetLanguage(language);
            string realTemplatePath;
            var otherSSport =
                    dataList
                    .Select(i => (OutRightViewModel)ViewModelConcreteFactory.Instance.GetOutRightViewModel(i, language, lang))
                    .ToList();
            realTemplatePath = templatePath + "Outright.xls";
            var outRight = new OutRightExcel(realTemplatePath, excelTitle, otherSSport, lang);
            return outRight.Export();
        }

        #region private method

        private static MemoryStream ParserOther<T>(
            IList<T> dataList,
            SportType sportType,
            string language,
            string templatePath,
            string excelTitle,
            DTO.LangResources lang,
            out string realTemplatePath)
        {
            var otherSSport =
             dataList
             .Select(i => (OtherViewModel)ViewModelConcreteFactory.Instance.GetViewModel(i, sportType, language, lang))
             .ToList();
            realTemplatePath = templatePath + "OtherSport.xls";
            var otherSport = new OtherSportExcel(realTemplatePath, excelTitle, otherSSport, lang);
            return otherSport.Export();
        }

        private static MemoryStream ParserVSTennis<T>(
            IList<T> dataList,
            SportType sportType,
            string language,
            string templatePath,
            string excelTitle,
            DTO.LangResources lang,
            out string realTemplatePath)
        {
            var viewModels =
               dataList
               .Select(i => (VSTennisViewModel)ViewModelConcreteFactory.Instance.GetViewModel(i, sportType, language, lang))
               .ToList();
            realTemplatePath = templatePath + "VSTennis.xls";
            var soccer = new VSTennisExcel(realTemplatePath, excelTitle, viewModels, lang);
            return soccer.Export();
        }

        private static MemoryStream ParserSoccer<T>(
            IList<T> dataList,
            SportType sportType,
            string language,
            string templatePath,
            string excelTitle,
            DTO.LangResources lang,
            out string realTemplatePath)
        {
            var viewModels =
               dataList
               .Select(i => (SoccerViewModel)ViewModelConcreteFactory.Instance.GetViewModel(i, sportType, language, lang))
               .ToList();
            realTemplatePath = templatePath + "Soccer.xls";
            var soccer = new SoccerExcel(realTemplatePath, excelTitle, viewModels, lang);
            return soccer.Export();
        }

        private static MemoryStream ParserLiveCasino<T>(
            IList<T> dataList,
            SportType sportType,
            string language,
            string templatePath,
            string excelTitle,
            DTO.LangResources lang,
            out string realTemplatePath)
        {
            var dataViewModel =
              dataList
              .Select(i => (LiveCasinoViewModel)ViewModelConcreteFactory.Instance.GetViewModel(i, sportType, language, lang))
              .ToList();
            realTemplatePath = templatePath + "LiveCasino.xls";
            var livecasino = new LiveCasinoExcel(realTemplatePath, excelTitle, dataViewModel, lang);
            return livecasino.Export();
        }

        private static MemoryStream ParserNumberGame<T>(
            IList<T> dataList,
            SportType sportType,
            string language,
            string templatePath,
            string excelTitle,
            DTO.LangResources lang,
            out string realTemplatePath)
        {
            var numberGameViewModels =
      dataList
      .Select(i => (NumberGameViewModel)ViewModelConcreteFactory.Instance.GetViewModel(i, sportType, language, lang))
      .ToList();
            realTemplatePath = templatePath + "NumberGame.xls";
            var numberGame = new NumberGameExcel(realTemplatePath, excelTitle, numberGameViewModels, lang);
            return numberGame.Export();
        }

        private static MemoryStream ParserVirtualHorseRacing<T>(
            IList<T> dataList,
            SportType sportType,
            string language,
            string templatePath,
            string excelTitle,
            DTO.LangResources lang,
            out string realTemplatePath)
        {
            var dataViewModels =
    dataList
    .Select(i => (VSHRacingViewModel)ViewModelConcreteFactory.Instance.GetViewModel(i, sportType, language, lang))
    .ToList();
            realTemplatePath = templatePath + "VSHRacing.xls";
            var ctx = new VSHRacingExcel(realTemplatePath, excelTitle, dataViewModels, lang);
            return ctx.Export();
        }

        private static MemoryStream ParserVirtualSpeedWay<T>(
            IList<T> dataList,
            SportType sportType,
            string language,
            string templatePath,
            string excelTitle,
            DTO.LangResources lang,
            out string realTemplatePath)
        {
            var dataViewModels =
   dataList
   .Select(i => (VSSpeedWayViewModel)ViewModelConcreteFactory.Instance.GetViewModel(i, sportType, language, lang))
   .ToList();
            realTemplatePath = templatePath + "VSSpeedWay.xls";
            var ctx = new VSSpeedWayExcel(realTemplatePath, excelTitle, dataViewModels, lang);
            return ctx.Export();
        }

        private static MemoryStream ParserRacing<T>(
            IList<T> dataList,
            SportType sportType,
            string language,
            string templatePath,
            string excelTitle,
            DTO.LangResources lang,
            out string realTemplatePath)
        {
            var racingViewModels =
       dataList
       .Select(i => (RacingViewModel)ViewModelConcreteFactory.Instance.GetViewModel(i, sportType, language, lang))
       .ToList();
            realTemplatePath = templatePath + "HorseRacing.xls";
            var racing = new RacingExcel(realTemplatePath, excelTitle, racingViewModels, lang);
            return racing.Export();
        }

        private static MemoryStream ParserBillard<T>(
            IList<T> dataList,
            SportType sportType,
            string language,
            string templatePath,
            string excelTitle,
            DTO.LangResources lang,
            out string realTemplatePath)
        {
            var billardViewModels =
         dataList
         .Select(i => (BilliardViewModel)ViewModelConcreteFactory.Instance.GetViewModel(i, sportType, language, lang))
         .ToList();
            realTemplatePath = templatePath + "Billiard.xls";
            var billiard = new BilliardExcel(realTemplatePath, excelTitle, billardViewModels, lang);
            return billiard.Export();
        }

        private static MemoryStream ParserBaseBall<T>(
            IList<T> dataList,
            SportType sportType,
            string language,
            string templatePath,
            string excelTitle,
            DTO.LangResources lang,
            out string realTemplatePath)
        {
            var baseBallViewModels =
             dataList
             .Select(i => (BaseBallViewModel)ViewModelConcreteFactory.Instance.GetViewModel(i, sportType, language, lang))
             .ToList();
            realTemplatePath = templatePath + "BaseBall.xls";
            var baseBall = new BaseBallExcel(realTemplatePath, excelTitle, baseBallViewModels, lang);
            return baseBall.Export();
        }

        private static MemoryStream ParserVolleyBall<T>(
            IList<T> dataList,
            SportType sportType,
            string language,
            string templatePath,
            string excelTitle,
            DTO.LangResources lang,
            out string realTemplatePath)
        {
            var badmintonViewModels =
              dataList
              .Select(i => (BadmintonViewModel)ViewModelConcreteFactory.Instance.GetViewModel(i, sportType, language, lang))
              .ToList();
            realTemplatePath = templatePath + "Badminton.xls";
            var badminton = new BadmintonExcel(realTemplatePath, excelTitle, badmintonViewModels, lang);
            return badminton.Export();
        }

        private static MemoryStream ParserTennis<T>(
            IList<T> dataList,
            SportType sportType,
            string language,
            string templatePath,
            string excelTitle,
            DTO.LangResources lang,
            out string realTemplatePath)
        {
            var tennisViewModels =
             dataList
             .Select(i => (TennisViewModel)ViewModelConcreteFactory.Instance.GetViewModel(i, sportType, language, lang))
             .ToList();
            realTemplatePath = templatePath + "Tennis.xls";
            var tennis = new TennisExcel(realTemplatePath, excelTitle, tennisViewModels, lang);
            return tennis.Export();
        }

        private static MemoryStream ParserHockey<T>(
            IList<T> dataList,
            SportType sportType,
            string language,
            string templatePath,
            string excelTitle,
            DTO.LangResources lang,
            out string realTemplatePath)
        {
            var hockeyViewModels =
                dataList.Select(i => (HockeyViewModel)ViewModelConcreteFactory.Instance.GetViewModel(i, sportType, language, lang)).ToList();
            realTemplatePath = templatePath + "Hockey.xls";
            var hockey = new HockeyExcel(realTemplatePath, excelTitle, hockeyViewModels, lang);
            return hockey.Export();
        }

        private static MemoryStream ParserBasketBall<T>(
            IList<T> dataList,
            SportType sportType,
            string language,
            string templatePath,
            string excelTitle,
            DTO.LangResources lang,
            out string realTemplatePath)
        {
            var basketBalllViewModels =
             dataList
             .Select(i => (BasketBallViewModel)ViewModelConcreteFactory.Instance.GetViewModel(i, sportType, language, lang))
             .ToList();
            realTemplatePath = templatePath + "Basketball.xls";
            var basket = new BasketballExcel(realTemplatePath, excelTitle, basketBalllViewModels, lang);
            return basket.Export();
        }

        #endregion private method
    }
}