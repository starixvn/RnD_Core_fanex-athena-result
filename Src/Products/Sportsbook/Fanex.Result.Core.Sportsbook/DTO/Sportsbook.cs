namespace Fanex.Result.Core.Sportsbook.DTO
{
    using System;
    using System.Collections.Generic;

    public sealed class Sportsbook
    {
        private List<GameDetail> _gameDetails = new List<GameDetail>();

        private List<BetBuilderDetail> betBuilderDetails = new List<BetBuilderDetail>();

        public string BetStatus { get; set; } = string.Empty;

        public int LeagueId { get; set; }

        public int LeagueGroupId { get; set; }

        public int MatchId { get; set; }

        public string Color { get; set; } = string.Empty;

        public string MatchCode { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public DateTime KickOffTime { get; set; }

        public int HomeId { get; set; }

        public int AwayId { get; set; }

        public int? HtHomeScore { get; set; }

        public int? HtAwayScore { get; set; }

        public int? FinalHomeScore { get; set; }

        public int? FinalAwayScore { get; set; }

        public int? LiveHomeScore { get; set; }

        public int? LiveAwayScore { get; set; }

        public string EventStatus { get; set; } = string.Empty;

        public bool IsNeutral { get; set; }

        public string GoalRecord { get; set; } = string.Empty;

        public bool Calculate { get; set; }

        public bool IsHTCal { get; set; }

        /// <summary>
        /// Gets or sets RefNo.It's used for popUp Result.
        /// </summary>
        /// <value>Get from RefNo column.</value>
        public string RefNo { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets game detail values.
        /// Used for tennis sport.
        /// </summary>
        /// <value>The game detail value.</value>
        public List<GameDetail> GameDetails
        {
            get
            {
                return _gameDetails;
            }

            set
            {
                if (value != null)
                {
                    _gameDetails = value;
                }
            }
        }

        public List<BetBuilderDetail> BetBuilderDetails
        {
            get => betBuilderDetails;

            set
            {
                betBuilderDetails = value ?? new List<BetBuilderDetail>();
            }
        }

        /// <summary>
        /// Gets or sets bet type.
        /// Used for tennis in parlay result.
        /// </summary>
        /// <value>The bet type ID list.</value>
        public int Bettype { get; set; }

        /// <summary>
        /// Gets or sets betId.
        /// Used for tennis in parlay result. Example:80212 (Set:02 , Game:12).
        /// Used for soccer in Fast Markets (start time): hdp1 from bettrans
        /// </summary>
        /// <value>The bet ID list.</value>
        public string BetID { get; set; }

        public int Handicap2 { get; set; }

        public byte SettlementStatus { get; set; }

        public bool NoHTScore { get; set; }

        public byte MainType { get; set; }

        public int  SubType { get; set; }
    }
}