// -----------------------------------------------------------------------
// <copyright file="RazorBinderModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System.Collections.Generic;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.ViewModels;

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

        public LangResources Resource { get; set; }

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
                if (this.IsDetail || !IsPopUp)
                {
                    return true;
                }

                if (SportType == SportType.Tennis && this._data.Count == 1)
                {
                    foreach (TennisViewModel item in this._data[0].Data)
                    {
                        if (item.VisibleMatchResult)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    return true;
                }

                return false;
            }
        }

        public bool VisibleDetailResult
        {
            get
            {
                var visible = false;
                if (SportType == SportType.Tennis && this.IsPopUp && this._data.Count == 1)
                {
                    foreach (TennisViewModel item in this._data[0].Data)
                    {
                        if (item.VisibleGameDetail)
                        {
                            visible = true;
                            break;
                        }
                    }
                }

                return this.IsDetail || visible;
            }
        }
    }
}