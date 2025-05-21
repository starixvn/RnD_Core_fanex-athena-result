namespace Fanex.Result.Core.WCasino
{
    using System;
    using System.Configuration;
    using System.Web;
    using Fanex.Result.Core.Base;

    public interface IWCasinoResultBuilder
    {
        string Render(string transDesc, string gameId3rd, int betType, string languageCode = "en-US");
    }

    public class WCasinoResultBuilder : IWCasinoResultBuilder
    {
        private const string InvalidImagePathErrorMessage = "not found WCasino image path configuration!";
        private readonly bool useOriginalPath;

        private IResultPopup _resultPopup;

        public WCasinoResultBuilder()
        {
            useOriginalPath = Convert.ToBoolean(ConfigurationManager.AppSettings["WMUseOriginalPath"] ?? "false");
        }

        internal WCasinoResultBuilder(IResultPopup resultPopup)
        {
            _resultPopup = resultPopup;
        }

        public virtual string GetTemplatePath => ConfigurationManager.AppSettings["WMResultImagePath"];

        public string Render(string transDesc, string gameId3rd, int betType, string languageCode = "en-US")
        {            
            var tplPath = GetTemplatePath;

            if (string.IsNullOrWhiteSpace(tplPath))
            {
                throw new WCasinoException(InvalidImagePathErrorMessage);
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