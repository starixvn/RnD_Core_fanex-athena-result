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
        private const string InvalidImagePathErrorMessage = "not found W Casino image path configuration!";
        private readonly string imagePath;

        private IResultPopup _resultPopup;

        public WCasinoResultBuilder(string imagePath)
        {
            this.imagePath = imagePath;
        }

        internal WCasinoResultBuilder(IResultPopup resultPopup, string imagePath) : this(imagePath)
        {
            _resultPopup = resultPopup;
        }

        public string Render(string transDesc, string gameId3rd, int betType, string languageCode = "en-US")
        {
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                throw new WCasinoException(InvalidImagePathErrorMessage);
            }

            _resultPopup = _resultPopup ?? new ResultPop(imagePath, ResultCoreManager.RefDataHelper);

            return _resultPopup.genResultPopHtml(transDesc, gameId3rd, languageCode, betType);
        }
    }
}