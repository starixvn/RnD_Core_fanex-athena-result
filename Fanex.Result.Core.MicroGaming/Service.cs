// -----------------------------------------------------------------------
// <copyright file="Service.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.MicroGaming
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Xml.Serialization;
    using Fanex.Result.Core.MicroGaming.ViewModels;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class Service
    {
        private const string IFRAMERESULT = "<div class=\"iframe-result\"><iframe src=\"{0}\" ></iframe></div>";
        private const string NORESULT = "<div class=\"iframe-result\">{0}</div>";

        public static string Parser(DTO.MicroGaming param)
        {
            try
            {
                var result = GetViewResult(GetXmlResult(param));
                return result.ReturnCode == 0 ? string.Format(IFRAMERESULT, result.ReturnMessage) :
                    string.Format(NORESULT, param.NoInformationMessage);
            }
            catch (Exception exception)
            {
                throw new MicroGaming.Exceptions.MicroGamingException(exception.Message, exception.InnerException);
            }
        }

        private static ResultViewModel GetViewResult(string xmlResult)
        {
            var reader = new XmlSerializer(typeof(ResultViewModel));

            using (var stringReader = new StringReader(xmlResult))
            {
                return (ResultViewModel)reader.Deserialize(stringReader);
            }
        }

        private static string GetXmlResult(DTO.MicroGaming param)
        {
            var nameValues = new[]
            {
                new KeyValuePair<string, string>("SiteName", param.SiteName),
                new KeyValuePair<string, string>("Deposit_CustID", param.DepositCustId.ToString()),
                new KeyValuePair<string, string>("GameID", param.GameId.ToString()),
            };
            var content = new FormUrlEncodedContent(nameValues);

            using (var client = new HttpClient())
            {
                return client
                    .PostAsync(param.APIUrl, content)
                    .Result
                    .Content
                    .ReadAsStringAsync()
                    .Result;
            }
        }
    }
}