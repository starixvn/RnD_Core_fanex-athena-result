namespace Fanex.Result.Core.Base.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;
    using DataDomain;
    using Fanex.Result.Core.Base.Exception;

    public static class Helper
    {
        private static readonly CultureInfo Cult = new CultureInfo("en-US");
        private static readonly Regex NumberPattern = new Regex("[0-9]");

        public static string FullDateFormat(object dateValue, string formatPattern)
        {
            if (Convert.IsDBNull(dateValue) || dateValue == null)
            {
                return string.Empty;
            }

            return string.Format(Cult, formatPattern, dateValue);
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

            return NumberPattern.IsMatch(inputvalue);
        }

        public static void CheckArgument(string arg, string argName)
        {
            if (string.IsNullOrEmpty(arg))
            {
                throw new ResultCoreException($"{argName} can not be null or emtpy");
            }
        }

        public static void CheckNotZero(int arg, string argName)
        {
            if (arg == 0)
            {
                throw new ResultCoreException($"{argName} can not be zero");
            }
        }

        public static string DictionaryToString(Dictionary<string, string> dictionary)
        {
            var dictionaryString = new StringBuilder();
            foreach (KeyValuePair<string, string> keyValues in dictionary)
            {
                dictionaryString.Append($"{keyValues.Key}={keyValues.Value}&");
            }

            return dictionaryString.ToString();
        }
    }
}