// -----------------------------------------------------------------------
// <copyright file="ResultEngine.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
namespace Fanex.Result.Core
{
    using System.Collections.Generic;
    using System.IO;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.Models;
    using Fanex.Result.Core.Base.Utilities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ResultEngine
    {
        /// <summary>
        /// Render detail result by sport.
        /// </summary>
        /// <param name="data">The SPORTSBOOK data.</param>
        /// <param name="setting">The result setting.</param>
        /// <returns>The html result.</returns>
        public static string HtmlGameDetailParser(dynamic data, ResultSetting setting)
        {
            if (setting.SportType != SportType.Tennis && setting.SportType != SportType.Soccer)
            {
                throw new ObjectUnSupportedException(string.Format(CoreResourceConfig.UNSUPPORTSPORTTYPEARGUEMENTMES, setting.SportType.StringMe()));
            }

            if (data == null)
            {
                throw new ObjectNullException("The data is null");
            }

            if (data.GameDetails.Count == 0 && setting.SportType == SportType.Tennis)
            {
                throw new ObjectNullException("Game data is null");
            }

            if (string.IsNullOrEmpty(setting.LanguageCode))
            {
                setting.LanguageCode = CoreResourceConfig.ENGLISHLANGCODE;
            }
            else if (!Validate.Instance.ValidateResource(setting.LanguageCode))
            {
                throw new ObjectUnSupportedException(string.Format(CoreResourceConfig.LANGUAGEARGUEMENTMES, setting.LanguageCode));
            }

            if (string.IsNullOrEmpty(setting.ResourcePath))
            {
                setting.ResourcePath = System.AppDomain.CurrentDomain.BaseDirectory + CoreResourceConfig.RESOURCEPATH;
            }

            var builder = new Sportsbook.Models.Builder();
            return builder.HtmlGameDetailBuilder(data, setting);
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
            var builder = GetBuilder(dtoName);
            if (setting.IsOutRight)
            {
                if (!Validate.Instance.ValidateOutRight(dtoName))
                {
                    throw new ObjectNullException(string.Format(CoreResourceConfig.ARGUEMENTMESOUTRIGHT, dtoName));
                }

                return builder.OutRightExcelBuilder<T>(dataList, setting);
            }

            if (setting.IsPopUp || setting.IsParlayPopUp)
            {
                return null;
            }

            if (!Validate.Instance.ValidateNormal(dtoName, setting.SportType))
            {
                throw new ObjectNullException(string.Format(CoreResourceConfig.SPORTTYPEARGUEMENTMES, dtoName));
            }

            return builder.ExcelBuilder(dataList, setting);
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

            if (setting.IsOutRight)
            {
                return HtmlOutRightParser<T>(dataList, setting, dtoName);
            }

            if (setting.IsParlayPopUp)
            {
                return HtmlParlayParser<T>(dataList, setting, dtoName);
            }

            if (!Validate.Instance.ValidateNormal(dtoName, setting.SportType))
            {
                throw new ObjectUnSupportedException(string.Format(CoreResourceConfig.SPORTTYPEARGUEMENTMES, dtoName));
            }

            var builder = GetBuilder(dtoName);

            return
                Helper.RemoveWhiteSpace(
                builder.HtmlBuilder(dataList, setting));
        }

        private static string HtmlParlayParser<T>(IList<T> dataList, ResultSetting setting, string dtoName)
        {
            if (!Validate.Instance.ValidateParlay(dtoName))
            {
                throw new ObjectNullException(string.Format(CoreResourceConfig.SPORTTYPEARGUEMENTMES, dtoName));
            }

            var builder = GetBuilder(dtoName);
            return Helper.RemoveWhiteSpace(
                builder.PopUpParalyBuilder<T>(dataList, setting));
        }

        private static string HtmlOutRightParser<T>(IList<T> dataList, ResultSetting setting, string dtoName)
        {
            if (!Validate.Instance.ValidateOutRight(dtoName))
            {
                throw new ObjectNullException(string.Format(CoreResourceConfig.ARGUEMENTMESOUTRIGHT, dtoName));
            }

            var builder = GetBuilder(dtoName);
            return Helper.RemoveWhiteSpace(
                builder.OutRightHtmlBuilder<T>(dataList, setting));
        }

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
                setting.LanguageCode = CoreResourceConfig.ENGLISHLANGCODE;
            }
            else if (!Validate.Instance.ValidateResource(setting.LanguageCode))
            {
                throw new ObjectUnSupportedException(string.Format(CoreResourceConfig.LANGUAGEARGUEMENTMES, setting.LanguageCode));
            }

            if (string.IsNullOrEmpty(setting.ExcelTemplatePath))
            {
                setting.ExcelTemplatePath = System.AppDomain.CurrentDomain.BaseDirectory + CoreResourceConfig.EXCELTEMPLATEPATH;
            }

            if (string.IsNullOrEmpty(setting.ResourcePath))
            {
                setting.ResourcePath = System.AppDomain.CurrentDomain.BaseDirectory + CoreResourceConfig.RESOURCEPATH;
            }
        }

        private static IBuilder GetBuilder(string productType)
        {
            switch (productType)
            {
                case DTOType.SPORTSBOOK:
                    return new Sportsbook.Models.Builder();
                case DTOType.GOLDDELUXE:
                    return new GoldDeLuxe.Models.Builder();
                case DTOType.NUMBERGAME:
                    return new NumberGame.Models.Builder();
                case DTOType.LIVECASINO:
                    return new LiveCasino.Models.Builder();
                case DTOType.RACING:
                    return new Racing.Models.Builder();
                default:
                    return new Sportsbook.Models.Builder();
            }
        }
    }
}