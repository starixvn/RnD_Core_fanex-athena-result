namespace Fanex.Result.Core.AllBetCasino
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web;
    using LiveCasinoResult;

    public class BuilderService : IBuilderService
    {
        private const string InvalidTransIdErrorMessage = "The TransId is invalid!";

        private const string InvalidConfigErrorMessage = "Not found AllBetCasino Configuration";

        private const string DefaultLanguageCode = "en";

        private static Dictionary<string, string> supportedLanguage = new Dictionary<string, string>
        {
            { "en-US", "en" },
            { "zh-TW", "ch" },
            { "zh-CN", "cs" },
            { "zh-Hans", "zhcn" },
            { "ja-JP", "jp" },
            { "th-TH", "th" },
            { "ko-KR", "ko" },
            { "vi-VN", "vn" }
        };

        public virtual string GetConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Fanex.Result.Core.3rdLiveCasino"].ConnectionString;
            }
        }

        public virtual string GetTemplatePath
        {
            get
            {
                return ConfigurationManager.AppSettings["Fanex.Result.Core.3rdLiveCasinoTplPath"];
            }
        }
      
        public string Render(string transID, string languageCode = "en-US")
        {
            if (string.IsNullOrWhiteSpace(transID) || transID.Contains(" "))
            {
                throw new AllBetCasinoException(InvalidTransIdErrorMessage);
            }

            var conn = GetConnectionString;
            var tplPath = GetTemplatePath;
            if (string.IsNullOrEmpty(conn) || string.IsNullOrEmpty(tplPath))
            {
                throw new AllBetCasinoException(InvalidConfigErrorMessage);
            }

            try
            {
                ResultPop resultContext = new ResultPop(HttpContext.Current, conn, tplPath);
                var langCode = supportedLanguage.ContainsKey(languageCode) ? supportedLanguage[languageCode] : DefaultLanguageCode;
                return resultContext.genResultPopHtmlbyID(transID, languageCode);
            }
            catch (Exception exception)
            {
                throw new AllBetCasinoException(exception.Message, exception.InnerException);
            }
        }
    }
}
