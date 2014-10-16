// -----------------------------------------------------------------------
// <copyright file="ResultEngine.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
namespace Fanex.Result.Core
{
    using System.Collections.Generic;
    using System.IO;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Exception;
    using Fanex.Result.Core.Extension;
    using Fanex.Result.Core.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ResultEngine
    {
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
            ReCheckSetting<T>(dataList, setting);

            var dtoName = typeof(T).Name;
            if (setting.IsOutRight && !setting.IsPopUp)
            {
                return HtmlOutRightParser<T>(dataList, setting, dtoName);
            }

            if (setting.IsParlayPopUp)
            {
                return HtmlParlayParser<T>(dataList, setting, dtoName);
            }

            if (setting.IsPopUp)
            {
                return HtmlPopUpParser<T>(dataList, setting, dtoName);
            }

            if (!Validate.Instance.ValidateNormal(dtoName, setting.SportType))
            {
                throw new ObjectUnSupportedException(string.Format(ResourceConfig.SPORTTYPEARGUEMENTMES, dtoName));
            }

            return Utilities.Helper.RemoveWhiteSpace(
                _builder.HtmlBuilder(dataList, setting));
        }

        public static string HtmlGameDetailParser(Sportsbook data, ResultSetting setting)
        {
            if (setting.SportType != SportType.Tennis)
            {
                throw new ObjectUnSupportedException(string.Format(ResourceConfig.UNSUPPORTSPORTTYPEARGUEMENTMES, setting.SportType.StringMe()));
            }

            if (data == null || data.GameDetail.Count == 0)
            {
                throw new ObjectNullException("The data is null or detailed data is null");
            }

            if (string.IsNullOrEmpty(setting.LanguageCode))
            {
                setting.LanguageCode = ResourceConfig.ENGLISHLANGCODE;
            }
            else if (!Validate.Instance.ValidateResource(setting.LanguageCode))
            {
                throw new ObjectUnSupportedException(string.Format(ResourceConfig.LANGUAGEARGUEMENTMES, setting.LanguageCode));
            }

            if (string.IsNullOrEmpty(setting.ResourcePath))
            {
                setting.ResourcePath = System.AppDomain.CurrentDomain.BaseDirectory + ResourceConfig.RESOURCEPATH;
            }

            return _builder.HtmlGameDetailBuilder(data, setting);
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
            ReCheckSetting<T>(dataList, setting);

            var dtoName = typeof(T).Name;
            var builder = new Builder();
            if (setting.IsOutRight)
            {
                if (!Validate.Instance.ValidateOutRight(dtoName))
                {
                    throw new ObjectNullException(string.Format(ResourceConfig.ARGUEMENTMESOUTRIGHT, dtoName));
                }

                return builder.JSONOutRightBuilder(dataList, setting);
            }

            if (setting.IsParlayPopUp)
            {
                if (!Validate.Instance.ValidateParlay(dtoName))
                {
                    throw new ObjectNullException(string.Format(ResourceConfig.SPORTTYPEARGUEMENTMES, dtoName));
                }

                return builder.JSONParlayBuilder(dataList, setting);
            }

            if (!Validate.Instance.ValidateNormal(dtoName, setting.SportType))
            {
                throw new ObjectNullException(string.Format(ResourceConfig.SPORTTYPEARGUEMENTMES, dtoName));
            }

            return builder.JSONSportTypeBuilder(dataList, setting);
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
            ReCheckSetting<T>(dataList, setting);

            var dtoName = typeof(T).Name;
            if (setting.IsOutRight)
            {
                if (!Validate.Instance.ValidateOutRight(dtoName))
                {
                    throw new ObjectNullException(string.Format(ResourceConfig.ARGUEMENTMESOUTRIGHT, dtoName));
                }

                return _builder.OutRightExcelBuilder(dataList, setting);
            }

            if (setting.IsPopUp || setting.IsParlayPopUp)
            {
                return null;
            }

            if (!Validate.Instance.ValidateNormal(dtoName, setting.SportType))
            {
                throw new ObjectNullException(string.Format(ResourceConfig.SPORTTYPEARGUEMENTMES, dtoName));
            }

            return _builder.ExcelBuilder(dataList, setting);
        }

        #region HtmlParser-SubFunction

        /// <summary>
        /// Valid result setting and set default setting value.
        /// </summary>
        /// <typeparam name="T">The DTO type.</typeparam>
        /// <param name="dataList">The data list.</param>
        /// <param name="setting">The setting.</param>
        private static void ReCheckSetting<T>(IList<T> dataList, ResultSetting setting)
        {
            if (setting == null || dataList == null)
            {
                throw new ObjectNullException("The data is null or setting is null");
            }

            if (string.IsNullOrEmpty(setting.LanguageCode))
            {
                setting.LanguageCode = ResourceConfig.ENGLISHLANGCODE;
            }
            else if (!Validate.Instance.ValidateResource(setting.LanguageCode))
            {
                throw new ObjectUnSupportedException(string.Format(ResourceConfig.LANGUAGEARGUEMENTMES, setting.LanguageCode));
            }

            if (string.IsNullOrEmpty(setting.ExcelTemplatePath))
            {
                setting.ExcelTemplatePath = System.AppDomain.CurrentDomain.BaseDirectory + ResourceConfig.EXCELTEMPLATEPATH;
            }

            if (string.IsNullOrEmpty(setting.ResourcePath))
            {
                setting.ResourcePath = System.AppDomain.CurrentDomain.BaseDirectory + ResourceConfig.RESOURCEPATH;
            }
        }

        /// <summary>
        /// Render html for popup page.
        /// </summary>
        /// <typeparam name="T">The DTO object (FANEX.Result.Core.DTO).</typeparam>
        /// <param name="dataList">The DTO list.</param>
        /// <param name="setting">The result setting.</param>
        /// <param name="dtoName">The DTO name.</param>
        /// <param name="builder">The builder context.</param>
        /// <returns>The html string.</returns>
        private static string HtmlPopUpParser<T>(IList<T> dataList, ResultSetting setting, string dtoName)
        {
            if (!Validate.Instance.ValidateNormal(dtoName, setting.SportType))
            {
                throw new ObjectNullException(string.Format(ResourceConfig.SPORTTYPEARGUEMENTMES, dtoName));
            }

            return
                Utilities.Helper.RemoveWhiteSpace(
                _builder.PopUpHtmlBuilder(dataList, setting));
        }

        private static string HtmlParlayParser<T>(IList<T> dataList, ResultSetting setting, string dtoName)
        {
            if (!Validate.Instance.ValidateParlay(dtoName))
            {
                throw new ObjectNullException(string.Format(ResourceConfig.SPORTTYPEARGUEMENTMES, dtoName));
            }

            return Utilities.Helper.RemoveWhiteSpace(
                _builder.PopUpParalyBuilder(dataList, setting));
        }

        private static string HtmlOutRightParser<T>(IList<T> dataList, ResultSetting setting, string dtoName)
        {
            if (!Validate.Instance.ValidateOutRight(dtoName))
            {
                throw new ObjectNullException(string.Format(ResourceConfig.ARGUEMENTMESOUTRIGHT, dtoName));
            }

            return Utilities.Helper.RemoveWhiteSpace(
                _builder.OutRightHtmlBuilder(dataList, setting));
        }

        #endregion HtmlParser-SubFunction
    }
}