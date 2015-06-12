// -----------------------------------------------------------------------
// <copyright file="Helper.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Base.Utilities
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Fanex.Result.Core.Base.DataDomain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class Helper
    {
        private static readonly CultureInfo Cult = new CultureInfo("en-US");

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

    }
}