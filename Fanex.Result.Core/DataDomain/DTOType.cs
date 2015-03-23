// -----------------------------------------------------------------------
// <copyright file="DTOType.cs" company="Nexcel">
// Data domain
// </copyright>
// <author>alex.nguyen@necel.vn</author>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.DataDomain
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Data domain class.
    /// </summary>
    internal static class DTOType
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