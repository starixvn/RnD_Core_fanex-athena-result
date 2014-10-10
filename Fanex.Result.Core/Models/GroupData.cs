// -----------------------------------------------------------------------
// <copyright file="GroupData.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System.Collections.Generic;
    using Fanex.Result.Core.ViewModels;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GroupData
    {
        public int LeagueId { get; set; }

        public IList<IResultViewModel> Data { get; set; }
    }
}