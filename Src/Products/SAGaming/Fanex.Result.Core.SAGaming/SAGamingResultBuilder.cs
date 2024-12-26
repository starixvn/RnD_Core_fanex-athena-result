namespace Fanex.Result.Core.SAGaming
{
    using Fanex.Result.Core.Base;

    public class SAGamingResultBuilder : ISAGamingResultBuilder
    {
        private const string InvalidImagePathErrorMessage = "not found SAGaming image path configuration!";

        private IResultPopup _resultPopup;
        private readonly string imagePath;

        public SAGamingResultBuilder(string imagePath)
        {
            this.imagePath = imagePath;
        }

        internal SAGamingResultBuilder(IResultPopup resultPopup, string imagePath) : this(imagePath)
        {
            _resultPopup = resultPopup;
        }

        public string Render(string transDesc, string gameId3rd, string betType, string languageCode = "en-US")
        {
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                throw new SAGamingException(InvalidImagePathErrorMessage);
            }

            _resultPopup = _resultPopup ?? new ResultPop(imagePath, ResultCoreManager.RefDataHelper);

            return _resultPopup.genResultPopHtml(transDesc, gameId3rd, languageCode, betType);
        }
    }
}