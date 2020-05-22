namespace Fanex.Result.Core.GTCasino
{
    using System;
    using System.Configuration;
    using System.Web;
    using Fanex.Result.Core.Base;

    public interface IGTCasinoResultBuilder
    {
        string Render(string transDesc, string gameId3rd, string betType, string languageCode = "en-US");
    }

    public class GTCasinoResultBuilder : IGTCasinoResultBuilder
    {
        private const string InvalidImagePathErrorMessage = "not found GTCasino image path configuration!";
        private readonly bool useOriginalPath;

        private IResultPopup _resultPopup;

        public GTCasinoResultBuilder()
        {
            useOriginalPath = Convert.ToBoolean(ConfigurationManager.AppSettings["GTCasinoUseOriginalPath"] ?? "false");
        }

        internal GTCasinoResultBuilder(IResultPopup resultPopup)
        {
            _resultPopup = resultPopup;
        }

        public virtual string GetTemplatePath => ConfigurationManager.AppSettings["GTCasinoResultImagePath"];

        public string Render(string transDesc, string gameId3rd, string betType, string languageCode = "en-US")
        {
            var tplPath = GetTemplatePath;

            if (string.IsNullOrWhiteSpace(tplPath))
            {
                throw new GTCasinoException(InvalidImagePathErrorMessage);
            }

            tplPath = CreateTplPath(tplPath, useOriginalPath);
            _resultPopup = _resultPopup ?? new ResultPop(tplPath, ResultCoreManager.RefDataHelper);

            return _resultPopup.genResultPopHtml(transDesc, gameId3rd, languageCode, betType);
        }

        private static string CreateTplPath(string path, bool useOriginalPath = false)
        {
            var tplPath = useOriginalPath ? path : HttpRuntime.AppDomainAppVirtualPath + path;

            return tplPath.Replace("//", "/");
        }
    }
}