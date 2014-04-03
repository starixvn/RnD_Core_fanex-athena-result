// -----------------------------------------------------------------------
// <copyright file="KenoApi.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System;
    using System.Net;
    using System.Web.Script.Serialization;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Exception;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class KenoApi : IKenoApi
    {
        /// <summary>
        /// Get authorized information.
        /// </summary>
        /// <param name="setting">The keno setting.</param>
        /// <param name="userId">The userId.</param>
        /// <returns>The keno authorized information.</returns>
        public KenoAuthorization Authorize(KenoResultSetting setting, string userId)
        {
            try
            {
                var authorizedUrl = string.Format(setting.AuthorizedUrl, setting.Vendor, setting.VenderKey, userId);
                var data = this.DownLoadData(authorizedUrl);
                if (string.IsNullOrEmpty(data))
                {
                    throw new ResultCoreException("Couldn't get authorized data from:" + authorizedUrl);
                }

                var serialize = new JavaScriptSerializer();
                return serialize.Deserialize<KenoAuthorization>(data);
            }
            catch (WebException exception)
            {
                throw new ResultCoreException(exception.Message, exception);
            }
            catch (ArgumentNullException exception)
            {
                throw new ResultCoreException(exception.Message, exception);
            }
            catch (ArgumentException exception)
            {
                throw new ResultCoreException(exception.Message, exception);
            }
            catch (InvalidOperationException exception)
            {
                throw new ResultCoreException(exception.Message, exception);
            }
            catch (FormatException exception)
            {
                throw new ResultCoreException(exception.Message, exception);
            }
        }

        /// <summary>
        /// Extend the expiration time for authorized token.
        /// </summary>
        /// <param name="setting">The keno setting.</param>
        /// <param name="userId">The user ID.</param>
        /// <returns>The extended result.</returns>
        public KenoExtendedToken ExtendToken(KenoResultSetting setting, string userId)
        {
            try
            {
                var extendedUrl = string.Format(setting.ExtendTokenUrl, setting.Vendor, setting.VenderKey, userId);
                var data = this.DownLoadData(extendedUrl);
                if (string.IsNullOrEmpty(data))
                {
                    throw new ResultCoreException("Couldn't get authorized data from:" + extendedUrl);
                }

                var serialize = new JavaScriptSerializer();
                return serialize.Deserialize<KenoExtendedToken>(data);
            }
            catch (WebException exception)
            {
                throw new ResultCoreException(exception.Message, exception);
            }
            catch (ArgumentNullException exception)
            {
                throw new ResultCoreException(exception.Message, exception);
            }
            catch (ArgumentException exception)
            {
                throw new ResultCoreException(exception.Message, exception);
            }
            catch (InvalidOperationException exception)
            {
                throw new ResultCoreException(exception.Message, exception);
            }
            catch (FormatException exception)
            {
                throw new ResultCoreException(exception.Message, exception);
            }
        }

        private string DownLoadData(string url)
        {
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                return client.DownloadString(url);
            }
        }
    }
}