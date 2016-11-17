namespace Fanex.Result.Core.Base.Utilities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using DataDomain;
    using Resources;

    public static class MapEventStatus
    {
        private static readonly string InternalStatus = "internal";

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

        public static string GetLogResource(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return ConstantValue.Hyphen;
            }

            var upperCaseKey = UppercaseFirst(key);

            var keyStatus = upperCaseKey.Replace(" ", string.Empty);

            var item = LangResource.ResourceManager.GetString(keyStatus);

            return string.IsNullOrEmpty(item) ? upperCaseKey : item;
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