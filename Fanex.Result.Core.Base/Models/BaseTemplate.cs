namespace Fanex.Result.Core.Base.Models
{
    using System;
    using System.IO;
    using System.Runtime.Caching;
    using Exception;

    public abstract class BaseTemplate
    {
        protected const string ViewDomainPattern = "Fanex.Result.Core.{0}.Views.{1}.cshtml";

        private const string CachePattern = "Fanex.Result.Core.{0}.Views.{1}";

        protected abstract string GetProductName { get; }

        public virtual string GetRazorTemplate(string templateName)
        {
            if (string.IsNullOrEmpty(templateName) || string.IsNullOrWhiteSpace(templateName))
            {
                throw new ArgumentNullException("templateName");
            }

            var cache = MemoryCache.Default;
            var templateCacheName = string.Format(CachePattern, GetProductName, templateName);
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
                var assembly = GetType().Assembly;
                using (var stream = assembly
                 .GetManifestResourceStream(string.Format(ViewDomainPattern, GetProductName, templateName)))
                {
                    if (stream == null)
                    {
                        throw new ArgumentNullException(string.Format("The {0} was not found", templateName));
                    }

                    var reader = new StreamReader(stream);

                    return reader.ReadToEnd();
                }
            }
            catch (Exception exception)
            {
                throw new ResultCoreException(exception.Message, exception.InnerException);
            }
        }
    }
}