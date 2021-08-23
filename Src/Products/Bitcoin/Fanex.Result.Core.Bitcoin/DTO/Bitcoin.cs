namespace Fanex.Result.Core.Bitcoin.DTO
{
    using System;
    using System.Collections.Generic;

    public sealed class Bitcoin
    {
        public Bitcoin()
        {
            GameResults = new List<GameDetail>();
        }

        public int League3rdId { get; set; }

        public int Match3rdId { get; set; }

        public DateTime EventDate { get; set; }

        public string EventStatus { get; set; }

        public string BetStatus { get; set; }

        public IList<GameDetail> GameResults { get; set; }
    }
}