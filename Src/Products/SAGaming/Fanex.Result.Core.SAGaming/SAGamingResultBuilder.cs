namespace Fanex.Result.Core.SAGaming
{
    using System;
    using System.Configuration;
    using System.Web;
    using Fanex.Result.Core.Base;
    using LiveCasinoResultForSA;

    public class SAGamingResultBuilder : ISAGamingResultBuilder
    {
        private const string InvalidTransDescErrorMessage = "tranDesc is invalid!";
        private const string InvalidBetTypeErrorMessage = "betType is invalid!";
        private const string InvalidImagePathErrorMessage = "not found SAGaming image path configuration!";
        private readonly bool useOriginalPath;       

        private IResultPopup _resultPopup;

        public SAGamingResultBuilder()
        {
            useOriginalPath = Convert.ToBoolean(ConfigurationManager.AppSettings["SAGamingUseOriginalPath"] ?? "false");
        }

        internal SAGamingResultBuilder(IResultPopup resultPopup)
        {
            _resultPopup = resultPopup;
        }

        public virtual string GetTemplatePath => ConfigurationManager.AppSettings["SAGamingResultImagePath"];

        public string Render(string transDesc, string gameId3rd, string betType, string languageCode = "en-US")
        {
            if (string.IsNullOrWhiteSpace(transDesc))
            {
                throw new SAGamingException(InvalidTransDescErrorMessage);
            }

            if (string.IsNullOrWhiteSpace(betType))
            {
                throw new SAGamingException(InvalidBetTypeErrorMessage);
            }

            var tplPath = GetTemplatePath;

            if (string.IsNullOrWhiteSpace(tplPath))
            {
                throw new SAGamingException(InvalidImagePathErrorMessage);
            }

            try
            {
                tplPath = CreateTplPath(tplPath, useOriginalPath);
                _resultPopup = _resultPopup ?? new ResultPop(tplPath, ResultCoreManager.RefDataHelper);

                return _resultPopup.genResultPopHtml(transDesc, gameId3rd, languageCode, betType);
            }
            catch (Exception exception)
            {
                var message = string.Join("\r\n",
                    $"transDesc: {transDesc}",
                    $"gameId3rd: {gameId3rd}",
                    $"langCode: {languageCode}",
                    $"betType: {betType}",
                    $"Exception: {exception}",
                    $"GetTemplatePath: {GetTemplatePath}",
                    $"tplPath: {tplPath}");
                throw new SAGamingException(message, exception.InnerException);
            }
        }

        private static string CreateTplPath(string path, bool useOriginalPath = false)
        {
            var tplPath = useOriginalPath ? path : HttpRuntime.AppDomainAppVirtualPath + path;

            return tplPath.Replace("//", "/");
        }
    }
}