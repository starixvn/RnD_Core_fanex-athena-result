// -----------------------------------------------------------------------
// <copyright file="DataDomain.cs" company="Nexcel">
// Data domain
// </copyright>
// <author>alex.nguyen@necel.vn</author>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.DTO
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Data domain class.
    /// </summary>
    public static class DataDomain
    {
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Reviewed.")]
        public const string SPORTSBOOK = "Sportsbook";

        public const string LIVECASINO = "LiveCasino";
        public const string NUMBERGAME = "NumberGame";
        public const string RACING = "Racing";
        public const string OUTRIGHT = "OutRight";
        public const string PARLAY = "Parlay";
    }
}