// -----------------------------------------------------------------------
// <copyright file="MapEventStatus.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Result.Core.DataDomain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal static class MapEventStatus
    {
        // Define list key for eventstatus
        private static readonly Dictionary<string, int> Event = new Dictionary<string, int>
       {
        { "running", Convert.ToInt32("0000000", 2) },
        { "completed", Convert.ToInt32("0000001", 2) },
        { "closed", Convert.ToInt32("0000010", 2) },
        { "postponed", Convert.ToInt32("0000011", 2) },
        { "refund", Convert.ToInt32("0000100", 2) },
        { "refund ft", Convert.ToInt32("0000101", 2) },
        { "refund ht", Convert.ToInt32("0000110", 2) },
        { "won", Convert.ToInt32("0000111", 2) },
        { "internal", Convert.ToInt32("0001000", 2) }
        };

        /*
         * Define list key for Result: isPending(1 bit) isHTCal(1 bit) calculate(1 bit) enventStatus(4 bit)
         */

        private static readonly Dictionary<int, string> Result = new Dictionary<int, string>
       {
                { Convert.ToInt32("0000010", 2), "closed" },
                { Convert.ToInt32("0100010", 2), "closed" },
                { Convert.ToInt32("0000000", 2), "running" },
                { Convert.ToInt32("0100000", 2), "running" },
                { Convert.ToInt32("1000000", 2), "pending" },
                { Convert.ToInt32("1010010", 2), "pending" },
                { Convert.ToInt32("1100010", 2), "pending" },
                { Convert.ToInt32("1100000", 2), "pending" },
                { Convert.ToInt32("1010000", 2), "pending" },
                { Convert.ToInt32("0110001", 2), "completed" },
                { Convert.ToInt32("1000010", 2), "pending" },
                { Convert.ToInt32("0000100", 2), "refund" },
                { Convert.ToInt32("0000101", 2), "refundft" },
                { Convert.ToInt32("0010101", 2), "refundft" },
                { Convert.ToInt32("0010110", 2), "refundht" },
                { Convert.ToInt32("0000110", 2), "refundht" },
                { Convert.ToInt32("0010111", 2), "won" },
                { Convert.ToInt32("0000111", 2), "won" },
                { Convert.ToInt32("0010011", 2), "postponed" },
                { Convert.ToInt32("0000011", 2), "postponed" },
                { Convert.ToInt32("0011000", 2), "internal" },
                { Convert.ToInt32("0001000", 2), "nternal" },
};

        /// <summary>
        /// Render status.
        /// </summary>
        /// <param name="eventstatus">The event status.</param>
        /// <param name="isPending">Is pending.</param>
        /// <param name="isHTcal">Is HT calculation.</param>
        /// <param name="calculate">Is calculation.</param>
        /// <param name="lang">The resource language.</param>
        /// <returns>The status string.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Microsoft.StyleCop.CSharp.NamingRules",
    "SA1305:FieldNamesMustNotUseHungarianNotation",
    Justification = "Using Win32 naming for consistency.")]
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Reviewed.")]
        public static string MapEventStatusString(string eventstatus, bool isPending, bool isHTcal, bool calculate, LangResources lang)
        {
            var pending = Convert.ToInt32(isPending);
            var htCal = Convert.ToInt32(isHTcal);
            var cal = Convert.ToInt32(calculate);

            pending <<= 6; // Shift right 6 bit for pending status
            htCal <<= 5; // Shift right 5 bit for haft time calculate status
            cal <<= 4; // Shift right 4 bit for calculate status

            var resultKey = pending | htCal | cal;

            if (Event.ContainsKey(eventstatus.ToLowerInvariant()))
            {
                resultKey |= Event[eventstatus.ToLowerInvariant()]; // Or with event status
                if (Result.ContainsKey(resultKey))
                {
                    return GetLogResource(Result[resultKey], lang);
                }

                /* Get status from resource lang, if have.*/
                var str = eventstatus.Replace(" ", string.Empty);
                return GetLogResource(str.ToLower(), lang);
            }

            return UppercaseFirst(eventstatus);
        }

        /// <summary>
        /// Get status from resource language.
        /// </summary>
        /// <param name="key">The status key.</param>
        /// <param name="lang">The language resource.</param>
        /// <returns>The status string.</returns>
        private static string GetLogResource(string key, LangResources lang)
        {
            switch (key.ToLower())
            {
                case "running":
                    return lang.Running;
                case "completed":
                    return lang.Completed;
                case "closed":
                    return lang.Closed;
                case "postponed":

                    return lang.PostPoned;
                case "internal":
                    return lang.Internal;
                case "refund":
                    return lang.Refund;
                case "refundft":
                    return lang.Refundft;
                case "refundht":
                    return lang.Refundht;
                case "won":
                    return lang.Won;
                default:
                    return UppercaseFirst(key);
            }
        }

        private static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}