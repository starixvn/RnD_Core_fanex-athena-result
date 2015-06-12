// -----------------------------------------------------------------------
// <copyright file="SoccerDetailViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Sportsbook.ViewModels.SoccerInfo
{
    using System.Collections.Generic;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.Base.DataDomain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SoccerDetailViewModel
    {
        public LangResources Resource { get; set; }

        public string Name { get; set; }

        public IList<SoccerGameDetailItem> DetailItems { get; set; }
    }
}