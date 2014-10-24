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
        private IList<Sportsbook> _sportsBook = new List<Sportsbook>();

        public int SportId { get; set; }

        public int LeagueId { get; set; }

        public IList<Sportsbook> Sportbook
        {
            get
            {
                return this._sportsBook;
            }

            set
            {
                if (value != null)
                {
                    this._sportsBook = value;
                }
            }
        }
    }
}