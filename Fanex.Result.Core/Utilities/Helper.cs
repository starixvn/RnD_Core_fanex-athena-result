// -----------------------------------------------------------------------
// <copyright file="Helper.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Utilities
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Fanex.Result.Core.DataDomain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal static class Helper
    {
        private const string ResourceIDSuffix = "RS";
        private static readonly CultureInfo Cult = new CultureInfo("en-US");
        private static Regex setXGameYRegex = new Regex("^80[1-5]{1}\\d{2}$");

        private static Regex[] soccerRangeResourceIDRegex = new Regex[5]
        {
            new Regex(GameResourceID.NextGoalTime),
            new Regex(GameResourceID.NextGoal3rd),
            new Regex(GameResourceID.GoalScorer),
            new Regex(GameResourceID.NextGoal1H),
            new Regex(GameResourceID.ExtraTimeNextGoal),
        };

        private static Regex[] setXRGameYResourceID = new Regex[5]
        {
            new Regex("^01\\d{2}$"),
            new Regex("^02\\d{2}$"),
            new Regex("^03\\d{2}$"),
            new Regex("^04\\d{2}$"),
            new Regex("^05\\d{2}$")
        };

        public static string FullDateFormat(object obj, string formatPattern)
        {
            if (Convert.IsDBNull(obj) || obj == null)
            {
                return string.Empty;
            }

            return string.Format(Cult, formatPattern, obj);
        }

        public static string RemoveWhiteSpace(string html)
        {
            if (!string.IsNullOrEmpty(html))
            {
                return Regex.Replace(html, "\\s*(<[^>]+>)\\s*", "$1", RegexOptions.Singleline);
            }

            return html;
        }

        public static string DecFormat(decimal value, byte decDigits)
        {
            return value.ToString("N" + decDigits);
        }

        public static string FormatTwoDigit(int number)
        {
            return number < 10 ? ConstantValue.Zero + number : number.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToRtfString(string html)
        {
            return html.Replace("<br />", "\n").Replace("<br/>", "\n").Replace("<b>", string.Empty).Replace("</b>", string.Empty);
        }

        public static bool IsIntegerNumber(string inputvalue)
        {
            if (string.IsNullOrEmpty(inputvalue))
            {
                return false;
            }

            Regex isnumber = new Regex("[^0-9]");
            return !isnumber.IsMatch(inputvalue);
        }

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

        /// <summary>
        /// Get language text by key.
        /// </summary>
        /// <param name="keyName">The key name.</param>
        /// <param name="lang">The language objects.</param>
        /// <returns>The language value.</returns>
        public static string GetResourceLang(string keyName, LangResources lang)
        {
            if (string.IsNullOrEmpty(keyName))
            {
                return string.Empty;
            }

            var resourceNum = string.Empty;

            var realKeyName = new StringBuilder();
            var isSpecialResourceId = soccerRangeResourceIDRegex.Any(i => i.IsMatch(keyName));
            if (IsIntegerNumber(keyName) || isSpecialResourceId)
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

            var result = lang.GetType().GetProperty(realKeyName.ToString()).GetValue(lang, null).ToString();
            if (!string.IsNullOrEmpty(resourceNum))
            {
                return string.Format(result, resourceNum);
            }

            return result;
        }
        
    }
}