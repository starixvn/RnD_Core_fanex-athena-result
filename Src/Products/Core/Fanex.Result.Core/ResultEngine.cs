namespace Fanex.Result.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Caching;
    using System.Web.Compilation;
    using Base;
    using Base.DataDomain;
    using Base.Exception;
    using Base.Extension;
    using Base.Models;
    using Base.Utilities;

    public static class ResultEngine
    {
        private const string PluginPattern = "Fanex.Result.Core.{0}";
        private const string BuilderNamespacePattern = "Fanex.Result.Core.{0}.Models.Builder";
        private const string CacheKey = "Fanex.Result.Core.Plugin.CacheKey";

#pragma warning disable S1541 // Methods and properties should not be too complex
        public static string HtmlGameDetailParser(dynamic data, ResultSetting setting)
#pragma warning restore S1541 // Methods and properties should not be too complex
        {
#pragma warning disable S1067 // Expressions should not be too complex
            if (setting.SportType != SportType.Tennis &&
                setting.SportType != SportType.Soccer &&
                setting.SportType != SportType.BasketBall &&
                setting.SportType != SportType.VirtualBasketBall &&
                setting.SportType != SportType.ESport &&
                setting.SportType != SportType.Badminton)
#pragma warning restore S1067 // Expressions should not be too complex
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
            else
            {
                if (!Validate.Instance.ValidateResource(setting.LanguageCode))
                {
                    throw new ObjectUnSupportedException(string.Format(CoreResourceConfig.LANGUAGEARGUEMENTMES, setting.LanguageCode));
                }
            }

            var builder = GetBuilder(DTOType.SPORTSBOOK);
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
            ReCheckSetting(dataList, setting);

            var dtoName = typeof(T).Name;

            if (setting.IsOutright)
            {
                return ExcelOutrightParser(dataList, setting, dtoName);
            }

            if (setting.IsPopUp || setting.IsParlayPopUp)
            {
                return null;
            }

            if (!Validate.Instance.ValidateNormal(dtoName, setting.SportType))
            {
                throw new ObjectNullException(string.Format(CoreResourceConfig.SPORTTYPEARGUEMENTMES, dtoName));
            }

            var builder = GetBuilder(dtoName);
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

            if (setting.IsOutright)
            {
                return HtmlOutrightParser<T>(dataList, setting, dtoName);
            }

            if (setting.IsParlayPopUp)
            {
                return HtmlParlayParser<T>(dataList, setting, dtoName);
            }

            if (!Validate.Instance.ValidateNormal(dtoName, setting.SportType))
            {
                setting.SportType = SportType.Soccer;
            }

            var builder = GetBuilder(dtoName);

            return Helper.RemoveWhiteSpace(builder.HtmlBuilder(dataList, setting));
        }

        private static string HtmlParlayParser<T>(IList<T> dataList, ResultSetting setting, string dtoName)
        {
            if (!Validate.Instance.ValidateParlay(dtoName))
            {
                throw new ObjectNullException(string.Format(CoreResourceConfig.SPORTTYPEARGUEMENTMES, dtoName));
            }

            var builder = GetBuilder(DTOType.SPORTSBOOK);
            return Helper.RemoveWhiteSpace(builder.PopUpParalyBuilder(dataList, setting));
        }

        private static string HtmlOutrightParser<T>(IList<T> dataList, ResultSetting setting, string dtoName)
        {
            var builder = CreateOutrightBuilder(dtoName);
            return Helper.RemoveWhiteSpace(builder.OutrightHtmlBuilder(dataList, setting));
        }

        private static IBuilder CreateOutrightBuilder(string dtoName)
        {
            if (!Validate.Instance.ValidateOutright(dtoName))
            {
                throw new ObjectNullException(string.Format(CoreResourceConfig.ARGUEMENTMESOUTRIGHT, dtoName));
            }

            return GetBuilder(DTOType.SPORTSBOOK);
        }

        private static MemoryStream ExcelOutrightParser<T>(IList<T> dataList, ResultSetting setting, string dtoName)
        {
            var builder = CreateOutrightBuilder(dtoName);
            return builder.OutrightExcelBuilder<T>(dataList, setting);
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
            else
            {
                if (!Validate.Instance.ValidateResource(setting.LanguageCode))
                {
                    throw new ObjectUnSupportedException(string.Format(CoreResourceConfig.LANGUAGEARGUEMENTMES, setting.LanguageCode));
                }
            }

            if (string.IsNullOrEmpty(setting.ExcelTemplatePath))
            {
                setting.ExcelTemplatePath = AppDomain.CurrentDomain.BaseDirectory + CoreResourceConfig.EXCELTEMPLATEPATH;
            }
        }

        private static IBuilder GetBuilder(string productType)
        {
            try
            {
                var pluginName = string.Format(PluginPattern, productType);

                var plugins = GetResultPlugins();

                var plugin = plugins.FirstOrDefault(i => i.FullName.IndexOf(pluginName, StringComparison.OrdinalIgnoreCase) > -1);

                if (plugin == null)
                {
                    throw new ObjectUnSupportedException(productType);
                }

                var builderName = string.Format(BuilderNamespacePattern, productType);

                var typeBuilder = plugin.GetType(builderName);

                if (typeBuilder == null)
                {
                    throw new ObjectNullException(productType);
                }

                return (IBuilder)Activator.CreateInstance(typeBuilder);
            }
            catch (Exception exception)
            {
                throw new ResultCoreException(exception.Message, exception.InnerException);
            }
        }

        private static IList<Assembly> GetResultPlugins()
        {
            ObjectCache cache = MemoryCache.Default;

            var assemblies = (List<Assembly>)cache[CacheKey];

            if (assemblies == null || !assemblies.Any())
            {
                try
                {
                    assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
                }
                catch
                {
                    assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(i => !i.IsDynamic).ToList();
                }

                CacheItemPolicy policy = new CacheItemPolicy();
                policy.Priority = CacheItemPriority.NotRemovable;

                assemblies = assemblies.Where(x => x.FullName.StartsWith("Fanex.Result.Core", StringComparison.InvariantCulture)).ToList();

                cache.Set(CacheKey, assemblies, policy);
            }

            return assemblies;
        }
    }
}