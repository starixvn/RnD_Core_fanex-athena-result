// -----------------------------------------------------------------------
// <copyright file="IResultViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Base.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IResultViewModel
    {
        int LeagueId { get; }

        string LeagueName { get; }

        string SportName { get; }

        bool VisibleMatchResult { get; }

        bool VisibleGameDetail { get; }
    }
}
