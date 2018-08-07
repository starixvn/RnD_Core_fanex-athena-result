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

        private IResultPopup _resultPopup;

        public AllbetResultBuilder()
        {
        }

        internal AllbetResultBuilder(IResultPopup resultPopup)
        {
            _resultPopup = resultPopup;
        }

        public virtual string GetTemplatePath => ConfigurationManager.AppSettings["AllbetResultImagePath"];

        public string Render(string transDesc, string gameId3rd, string betType, string languageCode = "en-US")
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
                _resultPopup = _resultPopup ?? new ResultPop(HttpContext.Current, null, tplPath);
                var langCode = supportedLanguage.ContainsKey(languageCode) ? supportedLanguage[languageCode] : DefaultLanguageCode;

                return _resultPopup.genResultPopHtml(transDesc, gameId3rd, langCode, betType);
            }
            catch (Exception exception)
            {
                throw new AllBetCasinoException(exception.Message, exception.InnerException);
            }
        }

        private static string CreateRelativePath(string path)
        {
            var relativePath = HttpRuntime.AppDomainAppVirtualPath + path;

            return relativePath.Replace("//", "/");
        }
    }
}