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
        private const string Pending = "pending";

        public static string MapEventStatusString(string eventstatus, bool isPending)
        {
            if (isPending)
            {
                return LangResource.Pending;
            }

            var isInternalMatch = IsInternal(eventstatus);

            return isInternalMatch ? LangResource.Running : GetLogResource(eventstatus);
        }

        public static string MapEventStatusString(byte settlementStatus, string eventStatus, string betStatus)
        {
            var bits = GetBitArray(settlementStatus);
            var isPending = bits[0] == 1;
            var isUnderSettlement = bits[2] == 1;
            var isSystemSettlement = bits[3] == 1;
            var isCompletedEventStatus = IsCompleted(eventStatus);
            var refundOrAbandonedStatus = GetRefundOrAbandonedStatus(betStatus);

            if (isPending)
            {
                return LangResource.ResultPending;
            }

            if (isCompletedEventStatus)
            {
                return string.IsNullOrEmpty(refundOrAbandonedStatus)
                    ? LangResource.Completed
                    : refundOrAbandonedStatus;
            }            

            if (isUnderSettlement || isSystemSettlement)
            {
                return LangResource.UnderSettlement;
            }

            // In case not completed event status and not under settlement status
            if (IsRefundHT(betStatus))
            {
                return LangResource.Refundht;
            }

            if (IsInternal(eventStatus))
            {
                return LangResource.Running;
            }

            return GetLogResource(eventStatus);
        }

        private static string GetRefundOrAbandonedStatus(string betStatus)
        {
            if (string.IsNullOrWhiteSpace(betStatus))
            {
                return betStatus;
            }

            var abandonValue = GetAbandoned(betStatus);

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

        public static bool IsPending(string eventStatus)
            => Pending.Equals(eventStatus, StringComparison.OrdinalIgnoreCase);

        public static bool IsCompleted(string eventStatus)
            => ConstantValue.Completed.Equals(eventStatus, StringComparison.OrdinalIgnoreCase);

        private static bool IsInternal(string eventStatus)
            => InternalStatus.Equals(eventStatus, StringComparison.OrdinalIgnoreCase);

        private static bool IsRefundHT(string betStatus)
           => !string.IsNullOrEmpty(betStatus) && betStatus.IndexOf(ConstantValue.RefundHT, StringComparison.OrdinalIgnoreCase) >= 0;

        private static int[] GetBitArray(byte number)
            => new BitArray(new[] { number })
                .Cast<bool>().Select(bit => bit ? 1 : 0)
                .ToArray();

        public static string GetRefund(string betStatus)
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

        private static string GetAbandoned(string betStatus)
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