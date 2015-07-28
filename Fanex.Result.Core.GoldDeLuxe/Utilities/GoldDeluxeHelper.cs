// -----------------------------------------------------------------------
// <copyright file="GoldDeluxeHelper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.GoldDeluxe.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Result.Core.Base.Utilities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal static class GoldDeluxeHelper
    {
        private const string RoutletResultTemplate = "<span class=\"{0}\">{1}</span>";
        private const string BacaratResultTemplate = "<li><span class=\"{0}\"><span class=\"icon\">{1}</span><span class=\"card\">{2}</span></span></li>";
        private const string BankerKey = "B";
        private const string PlayerKey = "P";
        private const string Aces = "A";

        private static readonly string[] RoutletRedResult = new string[]
        {
            "1", "3", "5", "7", "9", "12", "14", "16", "18",
            "19", "21", "23", "25", "27", "30", "32", "34", "36"
        };

        private static readonly IDictionary<string, string> CardType = new Dictionary<string, string>()
        {
        { "club", "&clubs;" },
        { "spade", "&spades;" },
        { "diamond", "&diams;" },
        { "heart", "&hearts;" }
        };

        public static string GetBacaratResult(string result, string bankerText, string playerText)
        {
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

        public static string GetRoutletResult(string result)
        {
            var color = result == "0" ? "green" : RoutletRedResult.Contains(result) ?
                "red" : "black";

            return string.Format(RoutletResultTemplate, color, result);
        }

        private static string GetHtmlResult(string choiceName, string choiceKey, List<string> choiceResult, int choicePoint)
        {
            var htmlResult = new StringBuilder("<ul class=\"" + choiceName.ToLower() + "\">");
            htmlResult.Append(string.Format(BacaratResultTemplate, choiceKey, string.Empty, choiceName + "&nbsp;(" + choicePoint + ")&nbsp;"));
            KeyValuePair<string, string> currentCard = new KeyValuePair<string, string>("space", "&nbsp;");
            foreach (var item in choiceResult.Skip(1))
            {
                if (CardType.Keys.Any(i => i.Equals(item, StringComparison.OrdinalIgnoreCase)))
                {
                    currentCard = CardType.SingleOrDefault(i => i.Key.Equals(item, StringComparison.OrdinalIgnoreCase));
                    continue;
                }

                htmlResult.Append(string.Format(BacaratResultTemplate, currentCard.Key, currentCard.Value, item));
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

            bankerPoint = TrimPoint(bankerPoint);

            for (var i = playerIndex; i <= lastplayerIndex; i++)
            {
                var value = resultTokens[i];
                playerResult.Add(value);
                playerPoint += GetPoint(value);
            }

            playerPoint = TrimPoint(playerPoint);
        }

        private static int TrimPoint(int point)
        {
            if (point > 9)
            {
                var lastDigit = point.ToString().Last();
                return int.Parse(lastDigit.ToString());
            }

            return point;
        }

        private static int GetPoint(string value)
        {
            return value.Equals(Aces, StringComparison.OrdinalIgnoreCase) ? 1 :
                Helper.IsIntegerNumber(value) && value != "10" ? int.Parse(value) : 0;
        }
    }
}