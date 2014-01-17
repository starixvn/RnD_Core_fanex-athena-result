// -----------------------------------------------------------------------
// <copyright file="OutRight.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.DTO
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class OutRight
    {
        public int SportType { get; set; }

        public int LeagueId { get; set; }

        public int TeamId { get; set; }

        public DateTime EventDate { get; set; }

        public string Status { get; set; }
    }
}