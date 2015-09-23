// -----------------------------------------------------------------------
// <copyright file="Resources.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Reflection;
    using System.Xml.Linq;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.Base.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class Resources : IResource
    {
        private static readonly Lazy<IResource> LazyResourceService =
        new Lazy<IResource>(() => new Resources());

        private readonly ConcurrentDictionary<string, LangResources> _langs;

        private Resources()
        {
            this._langs = new ConcurrentDictionary<string, LangResources>();
        }

        public static IResource Instance
        {
            get
            {
                return LazyResourceService.Value;
            }
        }

        /// <summary>
        /// Get language from xml resource file.
        /// </summary>
        /// <param name="languageCode">Option with en-US, vi-VN.</param>
        /// <param name="languagePath">The path of resources.</param>
        /// <returns>Return language object.</returns>
        public LangResources GetLanguage(string languageCode, string languagePath)
        {
            try
            {
                if (!this._langs.Keys.Contains(languageCode))
                {
                    this._langs.TryAdd(languageCode, GetResources(languageCode, languagePath));
                }

                return this._langs[languageCode];
            }
            catch (Exception exception)
            {
                throw new ResultCoreException(exception.Message, exception.InnerException);
            }
        }

        /// <summary>
        /// Get language resource.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <param name="languagePath">The path of resources.</param>
        /// <returns>The language object.</returns>
        private static LangResources GetResources(string languageCode, string languagePath)
        {
            var lang = new LangResources();
            if (string.IsNullOrWhiteSpace(languageCode))
            {
                throw new ArgumentNullException("languageCode");
            }

            var realPath = Path.Combine(languagePath + languageCode + ".xml");

            using (var resourceStream = GetResourceStream(languageCode, realPath))
            {
                ParseResource(lang, resourceStream);
            }

            return lang;
        }

        private static Stream GetResourceStream(string languageCode, string realPath)
        {
            if (File.Exists(realPath))
            {
                return new FileStream(realPath, FileMode.Open, FileAccess.Read);
            }
            else
            {
                var resourceStream = Assembly.GetExecutingAssembly()
                 .GetManifestResourceStream("Fanex.Result.Core.Base.Resources." + languageCode + ".xml");

                return resourceStream ?? GetResourceStream(CoreResourceConfig.ENGLISHLANGCODE, realPath);
            }
        }

        /// <summary>
        /// Parse XML to  object.
        /// </summary>
        /// <param name="lang">The resource object.</param>
        /// <param name="stream">The data stream.</param>
        private static void ParseResource(LangResources lang, Stream stream)
        {
            if (stream != null)
            {
                var doc = XDocument.Load(stream);
                var items = doc.Descendants("item");

                foreach (var item in items)
                {
                    var field = lang.GetType().GetProperty(item.Attribute("key").Value);
                    if (field != null)
                    {
                        field.SetValue(lang, item.Value, null);
                    }
                }
            }
        }
    }
}