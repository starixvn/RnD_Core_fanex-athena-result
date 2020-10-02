namespace Fanex.Result.Core.WM
{
    using System;
    using System.Configuration;
    using System.Web;
    using Fanex.Result.Core.Base;

    public interface IWMResultBuilder
    {
        string Render(string transDesc, string gameId3rd, int betType, string languageCode = "en-US");
    }

    public class WMResultBuilder : IWMResultBuilder
    {
        private const string InvalidImagePathErrorMessage = "not found WM image path configuration!";
        private readonly bool useOriginalPath;

        private IResultPopup _resultPopup;

        public WMResultBuilder()
        {
            useOriginalPath = Convert.ToBoolean(ConfigurationManager.AppSettings["WMUseOriginalPath"] ?? "false");
        }

        internal WMResultBuilder(IResultPopup resultPopup)
        {
            _resultPopup = resultPopup;
        }

        public virtual string GetTemplatePath => ConfigurationManager.AppSettings["WMResultImagePath"];

        public string Render(string transDesc, string gameId3rd, int betType, string languageCode = "en-US")
        {            
            var tplPath = GetTemplatePath;

            if (string.IsNullOrWhiteSpace(tplPath))
            {
                throw new WMException(InvalidImagePathErrorMessage);
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