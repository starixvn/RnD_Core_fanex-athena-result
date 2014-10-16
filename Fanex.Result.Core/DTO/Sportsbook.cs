// -----------------------------------------------------------------------
// <copyright file="Sportsbook.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class Sportsbook
    {
        public Sportsbook()
        {
            this.BetStatus = string.Empty;
            this.GoalRecord = string.Empty;
            this.MatchCode = string.Empty;
            this.RefNo = string.Empty;
            this.GameDetail = new List<GameDetail>();
        }

        public string BetStatus { get; set; }

        public int LeagueId { get; set; }

        public int MatchId { get; set; }

        public string Color { get; set; }

        public string MatchCode { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime KickOffTime { get; set; }

        public int HomeId { get; set; }

        public int AwayId { get; set; }

        public int? HtHomeScore { get; set; }

        public int? HtAwayScore { get; set; }

        public int? FinalHomeScore { get; set; }

        public int? FinalAwayScore { get; set; }

        public string EventStatus { get; set; }

        public bool IsPending { get; set; }

        public bool IsNeutral { get; set; }

        public string GoalRecord { get; set; }

        public bool Calculate { get; set; }

        public bool IsHTCal { get; set; }

        /// <summary>
        /// Gets or sets RefNo.It's used for popUp Result.
        /// </summary>
        /// <value>Get from RefNo column.</value>
        public string RefNo { get; set; }

        /// <summary>
        /// Gets or sets game detail values.
        /// Used for tennis sport.
        /// </summary>
        public IList<GameDetail> GameDetail { get; set; }
    }
}