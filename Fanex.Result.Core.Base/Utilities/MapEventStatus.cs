// -----------------------------------------------------------------------
// <copyright file="MapEventStatus.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Base.Utilities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Resources;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class MapEventStatus
    {
        private static readonly string InternalStatus = "internal";

        /// <summary>
        /// Render status.
        /// </summary>
        /// <param name="eventstatus">The event status.</param>
        /// <param name="isPending">Is pending.</param>
        /// <param name="isHTcal">Is HT calculation.</param>
        /// <param name="calculate">Is calculation.</param>
        /// <returns>The status string.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Microsoft.StyleCop.CSharp.NamingRules",
    "SA1305:FieldNamesMustNotUseHungarianNotation",
    Justification = "Using Win32 naming for consistency.")]
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Reviewed.")]
        public static string MapEventStatusString(string eventstatus, bool isPending, bool visibleInternalEventStatus)
        {
            var isInternalMatch = InternalStatus.Equals(eventstatus, StringComparison.OrdinalIgnoreCase);
            if (isInternalMatch && visibleInternalEventStatus)
            {
                return LangResource.Internal;
            }

            if (!isPending)
            {
                return isInternalMatch ? LangResource.Running : GetLogResource(eventstatus);
            }

            return LangResource.Pending;
        }

        /// <summary>
        /// Get status from resource language.
        /// </summary>
        /// <param name="key">The status key.</param>
        /// <returns>The status string.</returns>
        public static string GetLogResource(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return ConstantValue.Hyphen;
            }

            var upCaseKey = UppercaseFirst(key);

            var keyStatus = upCaseKey.Replace(" ", string.Empty);

            var item = LangResource.ResourceManager.GetString(keyStatus);

            return string.IsNullOrEmpty(item) ? upCaseKey : item;

        }

        private static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            char[] a = s.ToLower().ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}