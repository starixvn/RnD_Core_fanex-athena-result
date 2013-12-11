// -----------------------------------------------------------------------
// <copyright file="Parlay.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class Parlay
    {
        public int SportId
        {
            get;
            set;
        }
        
        public int LeagueId { get; set; }
       
        public IList<Sportbook> Sportbook { get; set; }
    }
}
