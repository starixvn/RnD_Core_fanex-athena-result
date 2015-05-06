// -----------------------------------------------------------------------
// <copyright file="GoldDeluxeHelper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal static class GoldDeluxeHelper
    {
        private const string routletResult = "<span class=\"{0}\">{1}</span>";
        private const string bacaratResult = "<li><span class=\"{0}\"><i>{1}</i>{2}</span></li>";
        const string BankerKey = "B";
        const string PlayerKey = "P";
        const string Aces = "A";
        private static readonly string[] routletRedResult = new string[] {
            "1", "3", "5", "7", "9", "12", "14", "16", "18",
            "19", "21", "23", "25", "27", "30", "32", "34", "36" };
        private static readonly IDictionary<string, string> cardType = new Dictionary<string, string>() {
        
        {"club","&clubs;"}, {"spade","&spades;"},  {"diamond","&diams;"}, {"heart","&hearts;"}
        };
        public static string GetBacaratResult(string result, string bankerText, string playerText)
        {
            if (string.IsNullOrEmpty(result))
            {
                return string.Empty;
            }
            var bankerResult = new List<string>();
            var playerResult = new List<string>();
            var bankerPoint = 0;
            var playerPoint = 0;
            GetBankerAndPlayerResultValue(result, bankerResult, playerResult, ref bankerPoint, ref playerPoint);

            var htmlResult = new StringBuilder();
            htmlResult.Append(GetHtmlResult(bankerText, BankerKey, bankerResult, bankerPoint));
            htmlResult.Append(GetHtmlResult(playerText, PlayerKey, playerResult, playerPoint));

            return htmlResult.ToString();
        }

        private static string GetHtmlResult(string choiceName, string choiceKey, List<string> choiceResult, int choicePoint)
        {
            var htmlResult = new StringBuilder("<ul class=\"" + choiceName.ToLower() + "\">");
            htmlResult.Append(string.Format(bacaratResult, choiceKey, string.Empty, choiceName + "&nbsp;(" + choicePoint + ")&nbsp;"));
            KeyValuePair<string, string> currentCard = new KeyValuePair<string, string>("space", "&nbsp;");
            foreach (var item in choiceResult.Skip(1))
            {
                if (cardType.Keys.Any(i => i.Equals(item, StringComparison.OrdinalIgnoreCase)))
                {
                    currentCard = cardType.SingleOrDefault(i => i.Key.Equals(item, StringComparison.OrdinalIgnoreCase));
                    continue;
                }

                htmlResult.Append(string.Format(bacaratResult, currentCard.Key, currentCard.Value, item));

            }

            htmlResult.Append("</ul>");

            return htmlResult.ToString();
        }

        private static void GetBankerAndPlayerResultValue(string result, List<string> bankerResult, List<string> playerResult, ref int bankerPoint, ref int playerPoint)
        {
            var resultTokens = result.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var totalTokens = resultTokens.Count();
            var bankerIndex = Array.FindIndex(resultTokens, w => w.Equals(BankerKey, StringComparison.OrdinalIgnoreCase));
            var playerIndex = Array.FindIndex(resultTokens, w => w.Equals(PlayerKey, StringComparison.OrdinalIgnoreCase));
            var lastBankerIndex = bankerIndex > playerIndex ? totalTokens - 1 : playerIndex - 1;
            var lastplayerIndex = playerIndex > bankerIndex ? totalTokens - 1 : bankerIndex - 1;

            for (var i = bankerIndex; i <= lastBankerIndex; i++)
            {
                var value = resultTokens[i];
                bankerResult.Add(value);
                bankerPoint += GetPoint(value);
            }
            for (var i = playerIndex; i <= lastplayerIndex; i++)
            {
                var value = resultTokens[i];
                playerResult.Add(value);
                playerPoint += GetPoint(value);
            }
        }

        private static int GetPoint(string value)
        {
            return value.Equals(Aces, StringComparison.OrdinalIgnoreCase) ? 1 :
                Helper.IsIntegerNumber(value) && value != "10" ? int.Parse(value) : 0;
        }

        public static string GetRoutletResult(string result)
        {
            if (string.IsNullOrEmpty(result))
            {
                return string.Empty;
            }

            var color = result == "0" ? "green" : routletRedResult.Contains(result) ?
                "red" : "black";

            return string.Format(routletResult, color, result);
        }
    }
}