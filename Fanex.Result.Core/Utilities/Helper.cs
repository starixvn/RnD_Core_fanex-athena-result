// -----------------------------------------------------------------------
// <copyright file="Helper.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Utilities
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal static class Helper
    {
        private static readonly CultureInfo Cult = new CultureInfo("en-US");

        public static string FullDateFormat2Ln(object obj)
        {
            if (Convert.IsDBNull(obj) || obj == null)
            {
                return string.Empty;
            }

            return string.Format(Cult, "{0:MM/dd/yyyy<br/>hh:mm:ss tt}", obj);
        }

        public static string FullDateFormat(object obj)
        {
            if (Convert.IsDBNull(obj) || obj == null)
            {
                return string.Empty;
            }

            return string.Format(Cult, "{0:MM/dd/yyyy hh:mm:ss tt}", obj);
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
    }
}