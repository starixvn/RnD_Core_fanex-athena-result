// -----------------------------------------------------------------------
// <copyright file="LiveCasino.cs" company="Nexcel.vn">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.DTO
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class LiveCasino
    {
        public int LeagueId { get; set; }
     
        public int Bettype { get; set; }
     
        public int? FinalHomeScore { get; set; }
     
        public int? FinalAwayScore { get; set; }
     
        public DateTime KickOffTime { get; set; }
      
        public DateTime EventDate { get; set; }
      
        public string ShowTime { get; set; }

        public string EventStatus { get; set; }
     
        public string BetStatus { get; set; }
     
        public bool IsPending { get; set; }
     
        public bool IsHTCal { get; set; }
      
        public bool Calculate { get; set; }
    }
}
