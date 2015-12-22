// -----------------------------------------------------------------------
// <copyright file="GroupData.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Base.ViewModels
{
    using System.Collections.Generic;
    using Fanex.Result.Core.Base.Extension;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GroupData
    {
        private IList<IResultViewModel> _data = new SafeList<IResultViewModel>();

        public int LeagueId { get; set; }

        public IList<IResultViewModel> Data
        {
            get
            {
                return this._data;
            }

            set
            {
                if (value != null)
                {
                    this._data = value;
                }
            }
        }
    }
}