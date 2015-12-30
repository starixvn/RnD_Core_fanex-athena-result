// -----------------------------------------------------------------------
// <copyright file="Outright.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Sportsbook.DTO
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
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