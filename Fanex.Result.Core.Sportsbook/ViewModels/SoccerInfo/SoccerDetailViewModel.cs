// -----------------------------------------------------------------------
// <copyright file="SoccerDetailViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Sportsbook.ViewModels.SoccerInfo
{
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SoccerDetailViewModel
    {
        public string Name { get; set; }

        public IList<SoccerGameDetailItem> DetailItems { get; set; }
    }
}