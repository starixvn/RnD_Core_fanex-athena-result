namespace Fanex.Result.Core.AllBetCasino
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web;
    using LiveCasinoResult;

    public class AllbetResultBuilder : IAllbetResultBuilder
    {
        private const string InvalidTransDescErrorMessage = "tranDesc is invalid!";

        private const string InvalidBetTypeErrorMessage = "betType is invalid!";

        private const string InvalidImagePathErrorMessage = "not found AllBetCasino image path configuration!";

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

        public virtual string GetTemplatePath
        {
            get
            {
                return ConfigurationManager.AppSettings["AllbetResultImagePath"];
            }
        }

        public string Render(string transDesc, string matchCode, string betType, string languageCode = "en-US")
        {
            if (string.IsNullOrWhiteSpace(transDesc))
            {
                throw new AllBetCasinoException(InvalidTransDescErrorMessage);
            }

            if (string.IsNullOrWhiteSpace(betType))
            {
                throw new AllBetCasinoException(InvalidBetTypeErrorMessage);
            }

            var tplPath = GetTemplatePath;
            if (string.IsNullOrWhiteSpace(tplPath))
            {
                throw new AllBetCasinoException(InvalidImagePathErrorMessage);
            }

            tplPath = CreateRelativePath(tplPath);

            try
            {
                ResultPop resultContext = new ResultPop(HttpContext.Current, null, tplPath);
                var langCode = supportedLanguage.ContainsKey(languageCode) ? supportedLanguage[languageCode] : DefaultLanguageCode;

                return resultContext.genResultPopHtml(transDesc, matchCode, langCode, betType);
            }
            catch (Exception exception)
            {
                throw new AllBetCasinoException(exception.Message, exception.InnerException);
            }
        }

        private string CreateRelativePath(string path)
        {
            path = HttpRuntime.AppDomainAppVirtualPath + path;

            return path.Replace("//", "/");
        }
    }
}