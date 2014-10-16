// -----------------------------------------------------------------------
// <copyright file="RazorBinderModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System.Collections.Generic;
    using Fanex.Result.Core.DataDomain;

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

        /// <summary>
        /// Gets or sets bettype.
        /// Used for tennis POPUP.
        /// </summary>
        public int Bettype { get; set; }

        /// <summary>
        /// Gets or sets bet id.
        /// Used for tennis POPUP.
        /// Example:80212 (Set:02 , Game:12).
        /// </summary>
        public string BetID { get; set; }

        public bool IsDetail { get; set; }
    }
}