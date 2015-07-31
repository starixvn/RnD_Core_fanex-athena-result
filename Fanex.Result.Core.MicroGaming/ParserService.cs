// -----------------------------------------------------------------------
// <copyright file="ParserService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.MicroGaming
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Net.Http;
    using System.Xml.Serialization;
    using Fanex.Result.Core.MicroGaming.Exceptions;
    using Fanex.Result.Core.MicroGaming.ViewModels;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ParserService : IParserService
    {
        private const string IFRAMERESULT = "<div class=\"iframe-rs-wrap\"><iframe class=\"iframe-rs\" src=\"{0}\" ></iframe></div>";
        private const string NORESULT = "<div class=\"no-info-rs\">{0}</div>";
        /*singleton*/
        private static readonly object SynceObject = new object();
        private static IParserService instance;

        private ParserService()
        {
        }

        public static IParserService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SynceObject)
                    {
                        if (instance == null)
                        {
                            instance = new ParserService();
                        }
                    }
                }

                return instance;
            }
        }

        public string Parser(DTO.MicroGaming param)
        {
            try
            {
                var result = GetViewResult(GetXmlResult(param));

                if (result.ReturnCode != 0)
                {
                    throw new MicroGamingException("Get an Error from API result:" + result.ReturnMessage);
                }

                return string.Format(IFRAMERESULT, result.Url);
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
                new KeyValuePair<string, string>("CustID", param.CustId.ToString()),
                new KeyValuePair<string, string>("GameID", param.GameId.ToString()),
            };
            var content = new FormUrlEncodedContent(nameValues);
            var api = ConfigurationManager.AppSettings["MicroGamingResultAPI"];
            using (var client = new HttpClient())
            {
                return client
                    .PostAsync(api, content)
                    .Result
                    .Content
                    .ReadAsStringAsync()
                    .Result;
            }
        }
    }
}