// -----------------------------------------------------------------------
// <copyright file="AGCasinoHelper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.AGCasino.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Fanex.Result.Core.Base.Utilities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal static class AGCasinoHelper
    {
        private const string RoutletResultTemplate = "<span class=\"black\">{0}</span>";
        private const string BacaratResultTemplate = "<li><span class=\"{0}\"><span class=\"icon\">{1}</span><span class=\"card\">{2}</span></span></li>";
        private const string DragonTigerResultTemplate = "<li><span class=\"{0}\"><span class=\"icon\">{1}</span><span class=\"card\">{2}</span></span></li>";
        private const string SicboResultTemplate = "<span class=\"black\">{0}</span>";
        private const string BankerKey = "B";
        private const string PlayerKey = "P";
        private const string Aces = "A";
        private const string Card10 = "10";
        private const string DragonKey = "D";
        private const string TigerKey = "T";
        private const string CSSBanker = "banker";
        private const string CSSPlayer = "player";
        private const string CSSDragon = "dragon";
        private const string CSSTiger = "tiger";
        private static readonly Regex NoneNumber = new Regex("[^0-9]");

        private static readonly IDictionary<string, string> CardType = new Dictionary<string, string>()
        {
        { "C", "&clubs;" },
        { "S", "&spades;" },
        { "D", "&diams;" },
        { "H", "&hearts;" }
        };

        private static readonly IDictionary<string, string> CSSClassByCard = new Dictionary<string, string>()
        {
        { "C", "club" },
        { "S", "spade" },
        { "D", "diamond" },
        { "H", "heart" }
        };

        internal static string GetBacaratResult(string result, string bankerText, string playerText)
        {
            var htmlResult = new StringBuilder();

            var resultTokens = result.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in resultTokens)
            {
                var cardTokens = item.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var choiceKey = cardTokens[0].Equals(BankerKey, StringComparison.OrdinalIgnoreCase) ? BankerKey : PlayerKey;
                var choiceName = cardTokens[0].Equals(BankerKey, StringComparison.OrdinalIgnoreCase) ? bankerText : playerText;
                var choicePoint = TrimPoint(cardTokens.Skip(1).Sum(x => GetPoint(x)));
                htmlResult.Append(GetBacaratHtmlResult(choiceName, choiceKey, cardTokens, choicePoint));
            }

            return htmlResult.ToString();
        }

        internal static string GetRoutletResult(string result)
        {
            return string.Format(RoutletResultTemplate, result);
        }

        internal static string GetDragonTigerResult(string gameResult, string daragonName, string tigerName)
        {
            var resultTokens = gameResult.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            var tempResult = new StringBuilder();

            foreach (var item in resultTokens)
            {
                var choiceTokens = item.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var choiceKey = choiceTokens[0].Equals(DragonKey, StringComparison.OrdinalIgnoreCase) ? DragonKey : TigerKey;
                var choiceName = choiceTokens[0].Equals(DragonKey, StringComparison.OrdinalIgnoreCase) ? daragonName : tigerName;
                var cssByChoiceKey = choiceKey == DragonKey ? CSSDragon : CSSTiger;

                tempResult.Append("<ul class=\"" + cssByChoiceKey + "\">");
                tempResult.Append(string.Format(DragonTigerResultTemplate, choiceKey, string.Empty, choiceName + "&nbsp;:"));

                var cardTokens = choiceTokens[1].ToCharArray();
                var cardKey = cardTokens[0].ToString();
                var cardValue = string.Join(string.Empty, cardTokens.Skip(1));
                tempResult.Append(string.Format(DragonTigerResultTemplate, CSSClassByCard[cardKey], CardType[cardKey], cardValue));

                tempResult.Append("</ul>");
            }

            return tempResult.ToString();
        }

        internal static string GetSicBoResult(string gameResult)
        {
            var tempResult = new StringBuilder();
            var resultTokens = gameResult.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in resultTokens)
            {
                tempResult.Append(string.Format(SicboResultTemplate, item));
            }

            return tempResult.ToString();
        }
        #region Bacarat Helper
        private static string GetBacaratHtmlResult(string choiceName, string choiceKey, string[] choiceResult, int choicePoint)
        {
            var htmlResult = new StringBuilder("<ul class=\"" + (choiceKey == BankerKey ? CSSBanker : CSSPlayer) + "\">");
            htmlResult.Append(string.Format(BacaratResultTemplate, choiceKey, string.Empty, choiceName + "&nbsp;(" + choicePoint + ")&nbsp;:"));
            KeyValuePair<string, string> currentCard = new KeyValuePair<string, string>("space", "&nbsp;");
            foreach (var item in choiceResult.Skip(1))
            {
                var cardTokens = item.ToCharArray();
                var cardKey = cardTokens[0].ToString();
                var cardValue = string.Join(string.Empty, cardTokens.Skip(1));
                htmlResult.Append(string.Format(BacaratResultTemplate, CSSClassByCard[cardKey], CardType[cardKey], cardValue));
            }

            htmlResult.Append("</ul>");

            return htmlResult.ToString();
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
            var valueWithoutNoneNumber = NoneNumber.Replace(value, string.Empty);

            return valueWithoutNoneNumber.Equals(Aces, StringComparison.OrdinalIgnoreCase) ? 1 :
                Helper.IsIntegerNumber(valueWithoutNoneNumber) && valueWithoutNoneNumber != Card10 ? int.Parse(valueWithoutNoneNumber) : 0;
        }
        #endregion
    }
}