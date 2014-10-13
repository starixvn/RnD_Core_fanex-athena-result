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
        public LangResources Resource { get; set; }

        public List<GroupData> Data = new List<GroupData>();

        public bool IsPopUp { get; set; }

        public bool IsParlay { get; set; }

        public bool VisiblePopUpTitle { get; set; }

        public SportType SportType { get; set; }

        public bool VisibleMatchCodeIDColumn { get; set; }

        public bool VisibleDetailedLink { get; set; }

        public bool VisibleFreeHeader { get; set; }
    }
}