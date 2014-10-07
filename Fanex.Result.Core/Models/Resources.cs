// -----------------------------------------------------------------------
// <copyright file="Resources.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Fanex.Result.Core.DTO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class Resources : IResource
    {
        private static readonly string[] LangCodes = { "en-US", "ja-JP", "ko-KR", "vi-VN", "zh-CN", "zh-TW" };
        private static readonly object SynceObject = new object();
        private static IResource instance;
        private readonly IDictionary<string, LangResources> _langs;

        private Resources()
        {
            this._langs = new Dictionary<string, LangResources>();
        }

        public static IResource Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SynceObject)
                    {
                        if (instance == null)
                        {
                            instance = new Resources();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Get language from xml resource file.
        /// </summary>
        /// <param name="languageCode">Option with en-US, vi-VN.</param>
        /// <returns>Return language object.</returns>
        public LangResources GetLanguage(string languageCode, string languagePath)
        {
            if (!LangCodes.Contains(languageCode))
            {
                return new LangResources();
            }

            if (!this._langs.Keys.Contains(languageCode))
            {
                this._langs.Add(languageCode, GetResources(languageCode, languagePath));
            }

            return this._langs[languageCode];
        }

        /// <summary>
        /// Get language resource.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns>The language object.</returns>
        private static LangResources GetResources(string languageCode, string languagePath)
        {
            var lang = new LangResources();
            if (string.IsNullOrEmpty(languageCode) || string.IsNullOrWhiteSpace(languageCode))
            {
                throw new ArgumentNullException("languageCode");
            }
            var realPath = languagePath + languageCode + ".xml";
            using (var stream = new FileStream(realPath, FileMode.Open, FileAccess.Read))
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

                return lang;
            }
        }
    }
}