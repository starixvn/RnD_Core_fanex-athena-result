namespace Fanex.Result.Core.Base.Utilities
{
    using System;
    using System.Collections;
    using System.Linq;
    using DataDomain;
    using Resources;

    public static class MapEventStatus
    {
        private const string InternalStatus = "internal";

        public static string MapEventStatusString(string eventstatus, bool isPending, bool visibleInternalEventStatus)
        {
            var isInternalMatch = IsInternal(eventstatus);

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

        public static string MapEventStatusString(byte settlementStatus, string eventStatus, string betStatus, bool visibleInternalEventStatus)
        {
            var bits = GetBitArray(settlementStatus);
            var isPending = bits[0] == 1;
            var isUndecided = bits[1] == 1;
            var isUnderSettlement = bits[2] == 1;
            var isCompleted = IsCompleted(eventStatus);
            var refundOrPendingValue = GetRefundOrPending(betStatus);

            if (isPending)
            {
                return LangResource.ResultPending;
            }

            if (isUndecided || isUnderSettlement || isCompleted)
            {
                if (isCompleted)
                {
                    return string.IsNullOrEmpty(refundOrPendingValue)
                        ? LangResource.Completed
                        : refundOrPendingValue;
                }

                return LangResource.UnderSettlement;
            }

            if (IsInternal(eventStatus) && visibleInternalEventStatus)
            {
                return LangResource.Internal;
            }

            return GetLogResource(eventStatus);
        }

        private static string GetRefundOrPending(string betStatus)
        {
            if (string.IsNullOrWhiteSpace(betStatus))
            {
                return betStatus;
            }

            var abandonValue = GetAbandon(betStatus);
            if (!string.IsNullOrEmpty(abandonValue))
            {
                return abandonValue;
            }

            var refundValue = GetRefund(betStatus);
            if (!string.IsNullOrEmpty(refundValue))
            {
                return refundValue;
            }

            return string.Empty;
        }

        private static bool IsCompleted(string eventStatus)
            => ConstantValue.Completed.Equals(eventStatus, StringComparison.OrdinalIgnoreCase);

        private static bool IsInternal(string eventStatus)
            => InternalStatus.Equals(eventStatus, StringComparison.OrdinalIgnoreCase);

        private static int[] GetBitArray(byte number)
            => new BitArray(new[] { number })
                .Cast<bool>().Select(bit => bit ? 1 : 0)
                .ToArray();

        private static string GetRefund(string betStatus)
        {
            if (betStatus.IndexOf(ConstantValue.RefundFT, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return LangResource.Refundft;
            }

            if (betStatus.IndexOf(ConstantValue.RefundHT, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return LangResource.Refundht;
            }

            if (betStatus.IndexOf(ConstantValue.Refund, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return LangResource.Refund;
            }

            return string.Empty;
        }

        private static string GetAbandon(string betStatus)
        {
            if (betStatus.IndexOf(ConstantValue.Abandoned1H, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return LangResource.Abandoned1h;
            }

            if (betStatus.IndexOf(ConstantValue.Abandoned2H, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return LangResource.Abandoned2h;
            }

            return string.Empty;
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

            char[] a = s.ToLowerInvariant().ToCharArray();
            a[0] = char.ToUpper(a[0]);

            return new string(a);
        }
    }
}