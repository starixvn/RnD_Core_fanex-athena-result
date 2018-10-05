namespace Fanex.Result.Core.Lucky3.DTO
{
    using System;
    using System.Collections.Generic;

    public class Lucky3
    {
        public int MatchId { get; set; }

        public int LeagueId { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime KickOffTime { get; set; }

        public string MatchCode { get; set; } = string.Empty;

        public int LiveHomeScore { get; set; }

        public int LiveAwayScore { get; set; }

        public int FinalHomeScore { get; set; }

        public string EventStatus { get; set; } = string.Empty;

        public IList<int> Seq { get; set; } = new List<int>();

        public IList<int> Ball { get; set; } = new List<int>();

        public byte SettlementStatus { get; set; }

        public string BetStatus { get; set; } = string.Empty;
    }
}