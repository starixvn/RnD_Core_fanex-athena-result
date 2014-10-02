// -----------------------------------------------------------------------
// <copyright file="ResultEngine.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
namespace Fanex.Result.Core
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using Fanex.Result.Core.Exception;
    using Fanex.Result.Core.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ResultEngine
    {
        private const string EXCELTEMPLATEPATH = "App_Data/Fanex.Result/";
        private const string ARGUEMENTMES = "Sport Type is invalid or DTO ({0}) doesn't match with sportType";
        private const string ARGUEMENTMESOUTRIGHT = "DTO ({0}) is invalid";

        private static IBuilder _builder;

        static ResultEngine()
        {
            Reset(null);
        }

        public static void Reset(IBuilder builder)
        {
            _builder = builder ?? new Builder();
        }

        /// <summary>
        /// Render html result.
        /// </summary>
        /// <typeparam name="T">The DTO object (FANEX.Result.Core.DTO).</typeparam>
        /// <param name="dataList">The DTO list.</param>
        /// <param name="setting">The result setting.</param>
        /// <returns>The html string.</returns>
        public static string HtmlParser<T>(IList<T> dataList, ResultSetting setting)
        {
            if (setting == null || dataList == null)
            {
                throw new ObjectNullException("The data is null or setting is null");
            }

            if (string.IsNullOrEmpty(setting.LanguageCode))
            {
                setting.LanguageCode = CultureInfo.CurrentUICulture.Name;
            }

            if (string.IsNullOrEmpty(setting.ExcelTemplatePath))
            {
                setting.ExcelTemplatePath = EXCELTEMPLATEPATH;
            }

            var dtoName = typeof(T).Name;
            var builder = new Builder();
            if (setting.IsOutRight && !setting.IsPopUp)
            {
                return HtmlOutRightParser<T>(dataList, setting, dtoName, builder);
            }

            if (setting.IsParlayPopUp)
            {
                return HtmlParlayParser<T>(dataList, setting, dtoName, builder);
            }

            if (setting.IsPopUp)
            {
                return HtmlPopUpParser<T>(dataList, setting, dtoName, builder);
            }

            if (!Validate.Instance.ValidateNormal(dtoName, setting.SportType))
            {
                throw new ObjectNullException(string.Format(ARGUEMENTMES, dtoName));
            }

            return Utilities.Helper.RemoveWhiteSpace(
                builder.HtmlBuilder(dataList, setting));
        }

        /// <summary>
        /// Render JSON result.
        /// </summary>
        /// <typeparam name="T">The DTO object (FANEX.Result.Core.DTO).</typeparam>
        /// <param name="dataList">The DTO list.</param>
        /// <param name="setting">The result setting.</param>
        /// <returns>The JSON string.</returns>
        public static string JsonParser<T>(IList<T> dataList, ResultSetting setting)
        {
            if (setting == null || dataList == null)
            {
                throw new ObjectNullException("The data is null or setting is null");
            }

            if (string.IsNullOrEmpty(setting.LanguageCode))
            {
                setting.LanguageCode = CultureInfo.CurrentUICulture.Name;
            }

            if (string.IsNullOrEmpty(setting.ExcelTemplatePath))
            {
                setting.ExcelTemplatePath = EXCELTEMPLATEPATH;
            }

            var dtoName = typeof(T).Name;
            var builder = new Builder();
            if (setting.IsOutRight)
            {
                if (!Validate.Instance.ValidateOutRight(dtoName))
                {
                    throw new ObjectNullException(string.Format(ARGUEMENTMESOUTRIGHT, dtoName));
                }

                return builder.JSONOutRightBuilder(dataList, setting.LanguageCode);
            }

            if (setting.IsParlayPopUp)
            {
                if (!Validate.Instance.ValidateParlay(dtoName))
                {
                    throw new ObjectNullException(string.Format(ARGUEMENTMES, dtoName));
                }

                return builder.JSONParlayBuilder(dataList, setting.LanguageCode);
            }

            if (!Validate.Instance.ValidateNormal(dtoName, setting.SportType))
            {
                throw new ObjectNullException(string.Format(ARGUEMENTMES, dtoName));
            }

            return builder.JSONSportTypeBuilder(dataList, setting.SportType, setting.LanguageCode);
        }

        /// <summary>
        /// Create excel file.
        /// </summary>
        /// <typeparam name="T">The DTO object (FANEX.Result.Core.DTO).</typeparam>
        /// <param name="dataList">The DTO list.</param>
        /// <param name="setting">The result setting.</param>
        /// <returns>The memory stream.</returns>
        public static MemoryStream ExcelParser<T>(IList<T> dataList, ResultSetting setting)
        {
            if (setting == null || dataList == null)
            {
                throw new ObjectNullException("The data is null or setting is null");
            }

            if (string.IsNullOrEmpty(setting.LanguageCode))
            {
                setting.LanguageCode = CultureInfo.CurrentUICulture.Name;
            }

            if (string.IsNullOrEmpty(setting.ExcelTemplatePath))
            {
                setting.ExcelTemplatePath = EXCELTEMPLATEPATH;
            }

            var dtoName = typeof(T).Name;
            if (setting.IsOutRight)
            {
                if (!Validate.Instance.ValidateOutRight(dtoName))
                {
                    throw new ObjectNullException(string.Format(ARGUEMENTMESOUTRIGHT, dtoName));
                }

                return _builder.OutRightExcelBuilder(dataList, setting.LanguageCode, setting.ExcelTemplatePath, setting.ExcelTitle);
            }

            if (setting.IsPopUp || setting.IsParlayPopUp)
            {
                return null;
            }

            if (!Validate.Instance.ValidateNormal(dtoName, setting.SportType))
            {
                throw new ObjectNullException(string.Format(ARGUEMENTMES, dtoName));
            }

            return _builder.ExcelBuilder(dataList, setting.SportType, setting.LanguageCode, setting.ExcelTemplatePath, setting.ExcelTitle);
        }

        #region HtmlParser-SubFunction

        /// <summary>
        /// Render html for popup page.
        /// </summary>
        /// <typeparam name="T">The DTO object (FANEX.Result.Core.DTO).</typeparam>
        /// <param name="dataList">The DTO list.</param>
        /// <param name="setting">The result setting.</param>
        /// <param name="dtoName">The DTO name.</param>
        /// <param name="builder">The builder context.</param>
        /// <returns>The html string.</returns>
        private static string HtmlPopUpParser<T>(IList<T> dataList, ResultSetting setting, string dtoName, Builder builder)
        {
            if (!Validate.Instance.ValidateNormal(dtoName, setting.SportType))
            {
                throw new ObjectNullException(string.Format(ARGUEMENTMES, dtoName));
            }

            return
                Utilities.Helper.RemoveWhiteSpace(
                builder.PopUpHtmlBuilder(dataList, setting));
        }

        private static string HtmlParlayParser<T>(IList<T> dataList, ResultSetting setting, string dtoName, Builder builder)
        {
            if (!Validate.Instance.ValidateParlay(dtoName))
            {
                throw new ObjectNullException(string.Format(ARGUEMENTMES, dtoName));
            }

            return Utilities.Helper.RemoveWhiteSpace(
                builder.PopUpParalyBuilder(dataList, setting));
        }

        private static string HtmlOutRightParser<T>(IList<T> dataList, ResultSetting setting, string dtoName, Builder builder)
        {
            if (!Validate.Instance.ValidateOutRight(dtoName))
            {
                throw new ObjectNullException(string.Format(ARGUEMENTMESOUTRIGHT, dtoName));
            }

            return Utilities.Helper.RemoveWhiteSpace(
                builder.OutRightHtmlBuilder(dataList, setting));
        }

        #endregion HtmlParser-SubFunction
    }
}