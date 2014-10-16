// -----------------------------------------------------------------------
// <copyright file="SetValue.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels.TennisDetail
{
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SetValue
    {
        public int SetId { get; set; }

        public bool IsRefund { get; set; }

        public IList<GameValue> Games { get; set; }
    }
}