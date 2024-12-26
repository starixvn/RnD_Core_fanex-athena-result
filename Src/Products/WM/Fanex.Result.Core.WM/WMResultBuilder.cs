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
        private readonly string imagePath;

        private IResultPopup _resultPopup;

        public WMResultBuilder(string imagePath)
        {
            this.imagePath = imagePath;
        }

        internal WMResultBuilder(IResultPopup resultPopup, string imagePath) : this(imagePath)
        {
            _resultPopup = resultPopup;
        }

        public string Render(string transDesc, string gameId3rd, int betType, string languageCode = "en-US")
        {
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                throw new WMException(InvalidImagePathErrorMessage);
            }

            _resultPopup = _resultPopup ?? new ResultPop(imagePath, ResultCoreManager.RefDataHelper);

            return _resultPopup.genResultPopHtml(transDesc, gameId3rd, languageCode, betType);
        }
    }
}