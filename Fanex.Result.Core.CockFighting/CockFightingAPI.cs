// -----------------------------------------------------------------------
// <copyright file="CockFightingAPI.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.CockFighting
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Cache;
    using System.Text.RegularExpressions;
    using System.Web.Configuration;
    using Fanex.Result.Core.CockFighting.Exceptions;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class CockFightingAPI : ICockFightingAPI
    {
        private readonly Config _config;

        public CockFightingAPI()
        {
            this._config = GetConfig();
        }

        public string GetResultToken(string customerId, string languageCode = "en")
        {
            ValidateConfig();
            if (string.IsNullOrEmpty(customerId))
            {
                throw new CockFightingException("Invalid customer ID");
            }

            if (string.IsNullOrEmpty(languageCode))
            {
                throw new CockFightingException("Invalid language code");
            }

            try
            {
                var authorizedAPI = string.Format(_config.AuthorizedAPI, _config.SiteId, languageCode, DateTime.Now.ToString("yyyyMMdd"), customerId,_config.Vendor,_config.VendorKey);

                using (var client = new WebClient())
                {
                    client.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);
                    client.UseDefaultCredentials = true;
                    var result = client.DownloadString(new Uri(authorizedAPI));
                    if (!IsValidResultToken(result))
                    {
                        throw new CockFightingException("Cock Fighting API Exception - The raw message:" + result + "\n Url=" + authorizedAPI);
                    }

                    return result.Replace("\"", string.Empty);
                }
            }
            catch (Exception exception)
            {
                throw new CockFightingException(exception.Message, exception);
            }
        }

        public string ExtendToken(string customerId)
        {
            ValidateConfig();

            if (string.IsNullOrEmpty(customerId))
            {
                throw new CockFightingException("Invalid customer ID");
            }

            try
            {
                var extendedAPI = string.Format(_config.ExtendedAPI, _config.Vendor, _config.VendorKey, customerId);

                WebRequest request = WebRequest.Create(extendedAPI);

                request.Credentials = CredentialCache.DefaultCredentials;
                request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (var dataStream = response.GetResponseStream())
                    {
                        using (var streamReader = new StreamReader(dataStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new CockFightingException(exception.Message, exception);
            }
        }

        private static bool IsValidResultToken(string resultToken)
        {
            var regex = new Regex(@"(http|https):\/\/[^\/]+\/subCF\/\?action=[^&]*&siteID=[^&]*&lang=[^&]*&date=[^&]*&authorization=[^&]*&timezone=[^&]*");
            return regex.IsMatch(resultToken);
        }

        private static Config GetConfig()
        {
            var setting = WebConfigurationManager.AppSettings;
            return new Config
            {
                AuthorizedAPI = setting["CockFightingAuthorizedAPI"],
                ExtendedAPI = setting["CockFightingExtendedAPI"],
                SiteId = setting["CockFightingSiteId"],
                Vendor = setting["CockFightingVendor"],
                VendorKey = setting["CockFightingVendorKey"]
            };
        }

        private void ValidateConfig()
        {
            if (string.IsNullOrEmpty(_config.AuthorizedAPI))
            {
                throw new CockFightingException("Wrong Config!Not found CockFightingAuthorizedAPI key !");
            }

            if (string.IsNullOrEmpty(_config.ExtendedAPI))
            {
                throw new CockFightingException("Wrong Config! Not found CockFightingExtendedAPI key !");
            }

            if (string.IsNullOrEmpty(_config.SiteId))
            {
                throw new CockFightingException("Wrong Config! Not found CockFightingSiteId key !");
            }

            if (string.IsNullOrEmpty(_config.Vendor))
            {
                throw new CockFightingException("Wrong Config! Not found CockFightingVendor key !");
            }

            if (string.IsNullOrEmpty(_config.VendorKey))
            {
                throw new CockFightingException("Wrong Config! Not found CockFightingVendorKey key!");
            }
        }
    }
}