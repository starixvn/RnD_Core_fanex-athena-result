namespace Fanex.Result.Core.Sportsbook.DTO
{
    using System;

    public sealed class Outright
    {
        public Outright()
        {
            this.Status = string.Empty;
        }

        public int SportType { get; set; }

        public int LeagueId { get; set; }

        public int TeamId { get; set; }

        public DateTime EventDate { get; set; }

        public string Status { get; set; }
    }
}