// -----------------------------------------------------------------------
// <copyright file="ConstantValueDefinition.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.DataDomain
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal static class ConstantValueDefinition
    {
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Reviewed.")]
        public const int NUMBERGAMEOFTENNISSET = 13;

        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Reviewed.")]
        public const int SETXGAMEYRESOURCEIDLENGTH = 4;

        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Reviewed.")]
        public const string EnglishLanguageCode = "en-US";

        public const string BackgroundPending = "cl-bg-pending";
        public const string BackgroundPosponed = "cl-bg-postponed";

        public const string Running = "running";
        public const string Completed = "completed";

        public const string Closed = "closed";
        public const string Postponed = "postponed";

        public const string InternalString = "internal";

        public const string Refund = "refund";

        public const string Refundft = "refundft";

        public const string Refundht = "refundht";
        public const string Won = "won";
    }
}