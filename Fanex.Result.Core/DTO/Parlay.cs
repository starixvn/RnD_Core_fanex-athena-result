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
        private IList<int> _bettypes = new List<int>();
        private IList<string> _betIDS = new List<string>();
        private IList<Sportsbook> _sportsBookd = new List<Sportsbook>();

        public int SportId { get; set; }

        /// <summary>
        /// Gets or sets bet type.
        /// Used for tennis.
        /// </summary>
        /// <value>The bet type ID list.</value>
        public IList<int> BetTypes
        {
            get { return this._bettypes; }
            set { this._bettypes = value; }
        }

        /// <summary>
        /// Gets or sets bet id.
        /// Used for tennis.
        /// Example:80212 (Set:02 , Game:12).
        /// </summary>
        /// <value>The bet ID list.</value>
        public IList<string> BetIDs
        {
            get { return this._betIDS; }
            set { this._betIDS = value; }
        }

        public int LeagueId { get; set; }

        public IList<Sportsbook> Sportbook
        {
            get { return this._sportsBookd; }
            set { this._sportsBookd = value; }
        }
    }
}