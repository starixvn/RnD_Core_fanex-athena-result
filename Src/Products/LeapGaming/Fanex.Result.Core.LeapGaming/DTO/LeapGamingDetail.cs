namespace Fanex.Result.Core.LeapGaming.DTO
{
    using System;
    using System.Collections.Generic;

    public class LeapGamingDetail
    {
        public LeapGamingDetail(string transdesc)
        {
            var items = ParseTransDesc(transdesc);

            MatchDate = items.ContainsKey("md") ? items["md"] : string.Empty;
            MatchId = items.ContainsKey("mid") ? items["mid"] : string.Empty;
            Season = items.ContainsKey("se") ? items["se"] : string.Empty;

            match = items.ContainsKey("m") ? Split(items["m"], ':') : new[] { string.Empty, string.Empty };
            haftScore = items.ContainsKey("ht") ? Split(items["ht"], '-') : new[] { string.Empty, string.Empty };
            finalScore = items.ContainsKey("ft") ? Split(items["ft"], '-') : new[] { string.Empty, string.Empty };
        }

        private readonly string[] match;
        private readonly string[] haftScore;
        private readonly string[] finalScore;

        public string MatchDate { get; }

        public string MatchId { get; }

        public string Season { get; }

        public string HomeTeam => match.Length > 0 ? match[0].Trim() : string.Empty;

        public string AwayTeam => match.Length > 1 ? match[1].Trim() : string.Empty;

        public string HomeHTScore => haftScore.Length > 0 ? haftScore[0] : string.Empty;

        public string HomeFinalScore => finalScore.Length > 0 ? finalScore[0] : string.Empty;

        public string AwayHTScore => haftScore.Length > 1 ? haftScore[1] : string.Empty;

        public string AwayFinalScore => finalScore.Length > 1 ? finalScore[1] : string.Empty;

        private static IDictionary<string, string> ParseTransDesc(string transDesc)
        {
            const int numberOfExpressionsOfEachProperty = 2;

            var transDescItems = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var splittedTransDescParts = Split(transDesc, ';');

            foreach (var part in splittedTransDescParts)
            {
                var keyValuePair = Split(part, '=');

                if (keyValuePair.Length == numberOfExpressionsOfEachProperty)
                {
                    transDescItems.Add(keyValuePair[0].Trim(), keyValuePair[1].Trim());
                }
            }

            return transDescItems;
        }

        private static string[] Split(string value, char separator)
            => value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
    }
}