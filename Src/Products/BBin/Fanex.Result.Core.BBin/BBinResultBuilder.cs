namespace Fanex.Result.Core.BBin
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web;
    using Fanex.Result.Core.Base;

    public interface IBBinResultBuilder
    {
        string Render(string transDesc, string gameId3rd, string languageCode = "en-US");
    }

    public class BBinResultBuilder : IBBinResultBuilder
    {
        private const string InvalidImagePathErrorMessage = "not found BBin image path configuration!";
        private readonly bool useOriginalPath;

        private IResultPopup _resultPopup;

        public BBinResultBuilder()
        {
            useOriginalPath = Convert.ToBoolean(ConfigurationManager.AppSettings["BBinUseOriginalPath"] ?? "false");
        }

        internal BBinResultBuilder(IResultPopup resultPopup)
        {
            _resultPopup = resultPopup;
        }

        public virtual string GetTemplatePath => ConfigurationManager.AppSettings["BBinResultImagePath"];

        public string Render(string transDesc, string gameId3rd, string languageCode = "en-US")
        {
            var tplPath = GetTemplatePath;

            if (string.IsNullOrWhiteSpace(tplPath))
            {
                throw new BBinException(InvalidImagePathErrorMessage);
            }

            tplPath = CreateTplPath(tplPath, useOriginalPath);
            _resultPopup = _resultPopup ?? new ResultPop(tplPath, ResultCoreManager.RefDataHelper);
            var gameId = GetGameId(transDesc);

            return _resultPopup.genResultPopHtml(transDesc, gameId3rd, languageCode, gameId);
        }

        private static string CreateTplPath(string path, bool useOriginalPath = false)
        {
            var tplPath = useOriginalPath ? path : HttpRuntime.AppDomainAppVirtualPath + path;

            return tplPath.Replace("//", "/");
        }

        private static string GetGameId(string transDesc)
        {
            const string GameNameKey = "GameName";
            var parsedTransDesc = ParseTransDesc(transDesc);

            return parsedTransDesc.ContainsKey(GameNameKey)
                ? parsedTransDesc[GameNameKey]
                : string.Empty;
        }

        private static IDictionary<string, string> ParseTransDesc(string transDesc)
        {
            const int numberOfExpressionsOfEachProperty = 2;

            var transDescItems = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var splittedTransDescParts = Split(transDesc, '*');

            foreach (var part in splittedTransDescParts)
            {
                var keyValuePair = Split(part, '=');

                if (keyValuePair.Length == numberOfExpressionsOfEachProperty)
                {
                    transDescItems.Add(keyValuePair[0], keyValuePair[1]);
                }
            }

            return transDescItems;
        }

        private static string[] Split(string value, char separator)
            => value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
    }
}