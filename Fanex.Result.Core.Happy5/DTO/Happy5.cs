namespace Fanex.Result.Core.Happy5.DTO
{
    using System;
    using System.Collections.Generic;

    public class Happy5
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

        public bool IsPending { get; set; }

        public IList<int> Seq { get; set; } = new List<int>();

        public IList<int> Ball { get; set; } = new List<int>();
    }
}