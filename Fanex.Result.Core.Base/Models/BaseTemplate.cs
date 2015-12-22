// -----------------------------------------------------------------------
// <copyright file="BaseTemplate.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Base.Models
{
    using System;
    using System.IO;
    using System.Runtime.Caching;
    using Fanex.Result.Core.Base.Exception;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class BaseTemplate
    {
        protected const string VIEWDOMAINPATTERN = "Fanex.Result.Core.{0}.Views.{1}.cshtml";

        private const string CACHEPATTERN = "Fanex.Result.Core.{0}.Views.{1}";

        protected abstract string GetProductName { get; }

        /// <summary>
        /// Get razor template.
        /// </summary>
        /// <param name="templateName">The template name.</param>
        /// <returns>The html string.</returns>
        public virtual string GetRazorTemplate(string templateName)
        {
            if (string.IsNullOrEmpty(templateName) || string.IsNullOrWhiteSpace(templateName))
            {
                throw new ArgumentNullException("templateName");
            }

            var cache = MemoryCache.Default;
            var templateCacheName = string.Format(CACHEPATTERN, GetProductName, templateName);
            var item = cache[templateCacheName] as string;

            if (string.IsNullOrEmpty(item))
            {
                item = GetTemplateFromPlugin(templateName);
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.Priority = CacheItemPriority.NotRemovable;
                cache.Set(templateCacheName, item, policy);
            }

            return item;
        }

        private string GetTemplateFromPlugin(string templateName)
        {
            try
            {
                var assembly = this.GetType().Assembly;
                using (var stream = assembly
                 .GetManifestResourceStream(string.Format(VIEWDOMAINPATTERN, GetProductName, templateName)))
                {
                    if (stream == null)
                    {
                        throw new ArgumentNullException(string.Format("The {0} was not found", templateName));
                    }

                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new ResultCoreException(exception.Message, exception.InnerException);
            }
        }
    }
}