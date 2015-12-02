// -----------------------------------------------------------------------
// <copyright file="RazorBinderModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Base.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RazorBinderModel
    {
        private List<GroupData> _data = new List<GroupData>();

        public List<GroupData> Data
        {
            get { return _data; }
        }

        public bool IsPopUp { get; set; }

        public bool VisiblePopUpTitle { get; set; }

        public SportType SportType { get; set; }

        public bool VisibleMatchCodeIDColumn { get; set; }

        public bool VisibleDetailedLink { get; set; }

        public bool VisibleFreeHeader { get; set; }

        public bool IsDetail { get; set; }

        public bool VisibleMainResult
        {
            get
            {
                if (!this.IsDetail && this.IsPopUp && this._data.Count == 1)
                {
                    return this._data[0].Data.Any(i => i.VisibleMatchResult);
                }

                return true;
            }
        }

        public bool VisibleDetailResult
        {
            get
            {
                return this.IsDetail || CheckVisibleDetailResult;
            }
        }

        private bool CheckVisibleDetailResult
        {
            get
            {
                if (this.IsPopUp && this._data.Count == 1)
                {
                    return this._data[0].Data.Any(i => i.VisibleGameDetail);
                }

                return false;
            }
        }
    }
}