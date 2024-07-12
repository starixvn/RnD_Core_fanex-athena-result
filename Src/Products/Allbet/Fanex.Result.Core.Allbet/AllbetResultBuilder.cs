namespace Fanex.Result.Core.AllBetCasino
{
    using System;    
    using Fanex.Result.Core.Base;

    public class AllbetResultBuilder : IAllbetResultBuilder
    {
        private readonly AllbetConfigs configuration;

        private const string InvalidTransDescErrorMessage = "tranDesc is invalid!";
        private const string InvalidBetTypeErrorMessage = "betType is invalid!";
        private const string InvalidImagePathErrorMessage = "not found AllBetCasino image path configuration!";        

        private IResultPopup _resultPopup;       

        public AllbetResultBuilder(AllbetConfigs configuration)
        {            
            this.configuration = configuration;
        }

        public virtual string GetTemplatePath => configuration.ImagePath;

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

            try
            {
                //tplPath = CreateTplPath(tplPath, configuration.UseOriginalPath);
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
                throw new AllBetCasinoException(message, exception.InnerException);
            }
        }
/*
        private static string CreateTplPath(string path, bool useOriginalPath = false)
        {
            var tplPath = useOriginalPath ? path : HttpRuntime.AppDomainAppVirtualPath + path;

            return tplPath.Replace("//", "/");
        }*/
    }
}