namespace Fanex.Result.Core.ESport.DTO
{
    using System;
    using System.Collections.Generic;

    public sealed class ESport
    {
        public List<Map> Maps { get; set; } = new List<Map>();

        public int LeagueId { get; set; }

        public int MatchId { get; set; }

        public string EventStatus { get; set; }

        public int SettlementStatusID { get; set; }

        public string BetStatus { get; set; }

        public int HomeId { get; set; }

        public int AwayId { get; set; }

        public int FinalHomeScore { get; set; }

        public int FinalAwayScore { get; set; }

        public DateTime KickOffTime { get; set; }

        public bool IsPending { get; set; }
    }
}