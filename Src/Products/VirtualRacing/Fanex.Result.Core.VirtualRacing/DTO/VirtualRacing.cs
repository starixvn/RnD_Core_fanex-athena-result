namespace Fanex.Result.Core.VirtualRacing.DTO
{
    using System;
    using System.Collections.Generic;

    public sealed class VirtualRacing
    {
        public VirtualRacing()
        {
            Details = new List<VirtualRacingDetail>();
        }

        public DateTime EventTime { get; set; }

        public int EventId { get; set; }

        public int LeagueId { get; set; }

        public int VendorMatchId { get; set; }

        public string Distance { get; set; }
        
        public string EventStatus { get; set; }

        public string BetStatus { get; set; }

        public List<VirtualRacingDetail> Details { get; set; }

    }
}