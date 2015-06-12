// -----------------------------------------------------------------------
// <copyright file="ConstantValue.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Base.DataDomain
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ConstantValue
    {
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Reviewed.")]
        public const int NUMBERGAMEOFTENNISSET = 13;

        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Reviewed.")]
        public const int SETXGAMEYRESOURCEIDLENGTH = 4;

        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Reviewed.")]
        public const string EnglishLanguageCode = "en-US";

        public const string BackgroundPending = "cl-bg-pending";
        public const string BackgroundPosponed = "cl-bg-postponed";

        public const string RunningStatus = "running";
        public const string CompletedStatus = "completed";

        public const string ClosedStatus = "closed";
        public const string PostponedStatus = "postponed";

        public const string InternalString = "internal";

        public const string RefundStatus = "refund";

        public const string RefundftStatus = "refundft";

        public const string RefundhtStatus = "refundht";
        public const string Won = "won";

        public const string Hyphen = "-";

        public const string RefundFT = "Refund FT";

        public const string Refund = "Refund";

        public const string Void = "Void";
        public const string PenaltyLabel = "PS:";
        public const string Zero = "0";
        public const string WinFormat = "win";

        public const string None = "None";

        public const string Yes = "Y";
    }
}