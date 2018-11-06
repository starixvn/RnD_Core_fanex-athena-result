namespace Fanex.Result.Core.Sportsbook.Utilities
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Base.Resources;
    using Base.Utilities;
    using DataDomain;
    using Exceptions;

    internal static class SportsbookHelper
    {
        private const string ResourceIDSuffix = "RS";
        private static Regex setXGameYRegex = new Regex("^80[1-5]{1}\\d{2}$");

        private static Regex[] soccerRangeResourceIDRegex = new[] {
            new Regex(GameResourceID.NextGoalTime),
            new Regex(GameResourceID.NextGoal3rd),
            new Regex(GameResourceID.GoalScorer),
            new Regex(GameResourceID.NextGoal1H),
            new Regex(GameResourceID.ExtraTimeNextGoal),
        };

        private static Regex[] setXRGameYResourceID = new[] {
            new Regex("^01\\d{2}$"),
            new Regex("^02\\d{2}$"),
            new Regex("^03\\d{2}$"),
            new Regex("^04\\d{2}$"),
            new Regex("^05\\d{2}$")
        };

        public static bool IsSetXGameYBetID(string betID)
        {
            if (string.IsNullOrEmpty(betID))
            {
                return false;
            }

            return setXGameYRegex.IsMatch(betID);
        }

        public static bool IsTennisGameResourceID(int sexIndex, string resourceId)
        {
            return setXRGameYResourceID[sexIndex - 1].IsMatch(resourceId);
        }

        /// <summary>
        /// Validate game resource ID.
        /// </summary>
        /// <param name="pattern">The REGEX pattern.</param>
        /// <param name="resourceId">The resource ID.</param>
        /// <returns>True if it's ID is valid.</returns>
        public static bool IsValidResourceId(string pattern, string resourceId)
        {
            if (string.IsNullOrEmpty(pattern) || string.IsNullOrEmpty(resourceId))
            {
                return false;
            }

            if (pattern.Contains("^"))
            {
                var reg = new Regex(pattern);
                return reg.IsMatch(resourceId);
            }

            return pattern == resourceId;
        }

        public static bool IsFastMarketsResourceId(string resourceId)
        {
            return GameResources.FastMarkets.Contains(resourceId);
        }

        /// <summary>
        /// Get language text by key.
        /// </summary>
        /// <param name="keyName">The key name.</param>
        /// <returns>The language value.</returns>
        public static string GetResourceLang(string keyName)
        {
            if (string.IsNullOrEmpty(keyName))
            {
                return string.Empty;
            }

            var resourceNum = string.Empty;

            var realKeyName = new StringBuilder();
            var isSpecialResourceId = soccerRangeResourceIDRegex.Any(i => i.IsMatch(keyName));
            if (Helper.IsIntegerNumber(keyName) || isSpecialResourceId)
            {
                realKeyName.Append(ResourceIDSuffix);
                if (isSpecialResourceId)
                {
                    realKeyName.Append(keyName.Substring(0, 3));

                    resourceNum = keyName.Substring(3, 2);
                }
                else
                {
                    realKeyName.Append(keyName);
                }
            }
            else
            {
                realKeyName.Append(keyName);
            }

            var result = LangResource.ResourceManager.GetString(realKeyName.ToString());

            if (!string.IsNullOrEmpty(resourceNum))
            {
                return string.Format(result, resourceNum);
            }

            return result;
        }

        public static string CreateFastMarketsTimeRange(string resourceId, string startMinute)
        {
            int start = 0;
            var isValidStartTime = !string.IsNullOrEmpty(startMinute) && Helper.IsIntegerNumber(startMinute);

            if (isValidStartTime)
            {
                start = Convert.ToInt32(startMinute);
                isValidStartTime = start >= 0 && start <= 89;
            }

            if (!isValidStartTime)
            {
                throw new SportsbookException("Start time for Fast Markets must be integer from 0 to 89!");
            }

            var end =
                resourceId == GameResourceID.NextFiveMinutes ||
                resourceId == GameResourceID.FirstFiveMinutes ||
                resourceId == GameResourceID.WhichCombinationFirstFiveMinutes
                ? start + 4
                : start;

            return IsFastMarketsResourceId(resourceId)
                ? $"{Helper.FormatTwoDigit(start)}:00-{Helper.FormatTwoDigit(end)}:59"
                : string.Empty;
        }
    }
}