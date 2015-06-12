// -----------------------------------------------------------------------
// <copyright file="NumberGame.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.NumberGame.DTO
{
    using System;
    using System.Collections.Generic;
    using Fanex.Result.Core.Base.DTO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class NumberGame:IProductResult
    {
        public NumberGame()
        {
            this.Seg = new List<int>();
            this.Ball = new List<int>();
            this.EventStatus = string.Empty;
            this.MatchCode = string.Empty;
        }

        public int LeagueId { get; set; }

        public int MatchId { get; set; }

        public string MatchCode { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime KickOffTime { get; set; }

        public string EventStatus { get; set; }

        public bool IsPending { get; set; }

        public IList<int> Seg { get; set; }

        public IList<int> Ball { get; set; }
    }
}