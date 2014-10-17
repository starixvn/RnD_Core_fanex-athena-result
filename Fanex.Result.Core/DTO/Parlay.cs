// -----------------------------------------------------------------------
// <copyright file="Parlay.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class Parlay
    {
        public int SportId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets bettype.
        /// Used for tennis.
        /// </summary>
        public List<int> BetTypes { get; set; }

        /// <summary>
        /// Gets or sets bet id.
        /// Used for tennis.
        /// Example:80212 (Set:02 , Game:12).
        /// </summary>
        public List<string> BetIDs { get; set; }

        public int LeagueId { get; set; }

        public IList<Sportsbook> Sportbook { get; set; }
    }
}