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
        private string _betStatus = string.Empty;
        private string _goalRecord = string.Empty;
        private string _matchCode = string.Empty;
        private string _refNo = string.Empty;
        private string _color = string.Empty;
        private string _eventStatus = string.Empty;
        private List<GameDetail> _gameDetails = new List<GameDetail>();

        public string BetStatus
        {
            get { return this._betStatus; }
            set { this._betStatus = value; }
        }

        public int LeagueId { get; set; }

        public int MatchId { get; set; }

        public string Color
        {
            get { return this._color; }
            set { this._color = value; }
        }

        public string MatchCode
        {
            get { return this._matchCode; }
            set { this._matchCode = value; }
        }

        public DateTime EventDate { get; set; }

        public DateTime KickOffTime { get; set; }

        public int HomeId { get; set; }

        public int AwayId { get; set; }

        public int? HtHomeScore { get; set; }

        public int? HtAwayScore { get; set; }

        public int? FinalHomeScore { get; set; }

        public int? FinalAwayScore { get; set; }

        public string EventStatus
        {
            get { return this._eventStatus; }
            set { this._eventStatus = value; }
        }

        public bool IsPending { get; set; }

        public bool IsNeutral { get; set; }

        public string GoalRecord
        {
            get { return this._goalRecord; }
            set { this._goalRecord = value; }
        }

        public bool Calculate { get; set; }

        public bool IsHTCal { get; set; }

        /// <summary>
        /// Gets or sets RefNo.It's used for popUp Result.
        /// </summary>
        /// <value>Get from RefNo column.</value>
        public string RefNo
        {
            get { return this._refNo; }
            set { this._refNo = value; }
        }

        /// <summary>
        /// Gets or sets game detail values.
        /// Used for tennis sport.
        /// </summary>
        /// <value>The game detail value.</value>
        public List<GameDetail> GameDetails
        {
            get { return this._gameDetails; }
            set {
                if (value != null)
                {
                    this._gameDetails = value;
                }
            }
        }
    }
}