namespace Fanex.Result.Core.NumberGame.DTO
{
    using System;
    using System.Collections.Generic;

    public sealed class NumberGame
    {
        public NumberGame()
        {
            Seg = new List<int>();
            Ball = new List<int>();
            EventStatus = string.Empty;
            MatchCode = string.Empty;
        }

        public int LeagueId { get; set; }

        public int MatchId { get; set; }

        public string MatchCode { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime KickOffTime { get; set; }

        public string EventStatus { get; set; }

        public IList<int> Seg { get; set; }

        public IList<int> Ball { get; set; }

        public byte SettlementStatus { get; set; }

        public string BetStatus { get; set; } = string.Empty;
    }
}