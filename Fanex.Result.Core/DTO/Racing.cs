// -----------------------------------------------------------------------
// <copyright file="Racing.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.DTO
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class Racing
    {
        public DateTime KickOffTime { get; set; }

        public int Place { get; set; }

        public int LeagueId { get; set; }

        /// <summary>
        /// Gets or sets the race name or race number.
        /// </summary>
        /// <value>Race name or race number column.</value>
        public int RaceName { get; set; }

        public string EngineNo { get; set; }

        public decimal PlaceOdds { get; set; }

        public decimal WinOdds { get; set; }

        public string EngineId { get; set; }

        public bool IsSCR { get; set; }

        public bool IsFavor { get; set; }

        /// <summary>
        /// Gets or sets status field for virtual horse racing.
        /// </summary>
        /// <value>Get from IsFavor column.</value>
        public string Favourite { get; set; }

        public int RowCount { get; set; }

        public string SCROfMatch { get; set; }

        public string TFOfMatch { get; set; }
    }
}