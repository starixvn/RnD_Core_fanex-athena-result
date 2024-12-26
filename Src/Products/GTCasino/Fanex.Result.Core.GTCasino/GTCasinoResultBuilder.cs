namespace Fanex.Result.Core.GTCasino
{
    using Fanex.Result.Core.Base;

    public interface IGTCasinoResultBuilder
    {
        string Render(string transDesc, string gameId3rd, string betType, string languageCode = "en-US");
    }

    public class GTCasinoResultBuilder : IGTCasinoResultBuilder
    {
        private const string InvalidImagePathErrorMessage = "not found GTCasino image path configuration!";
        private readonly string imagePath;

        private IResultPopup _resultPopup;

        public GTCasinoResultBuilder(string imagePath)
        {
            this.imagePath = imagePath;
        }

        internal GTCasinoResultBuilder(IResultPopup resultPopup, string imagePath): this(imagePath)
        {
            _resultPopup = resultPopup;
        }


        public string Render(string transDesc, string gameId3rd, string betType, string languageCode = "en-US")
        {
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                throw new GTCasinoException(InvalidImagePathErrorMessage);
            }
            
            _resultPopup = _resultPopup ?? new ResultPop(imagePath, ResultCoreManager.RefDataHelper);

            return _resultPopup.genResultPopHtml(transDesc, gameId3rd, languageCode, betType);
        }
    }
}