// -----------------------------------------------------------------------
// <copyright file="RazorBinderModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System.Collections.Generic;
    using Fanex.Result.Core.DTO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RazorBinderModel
    {
        private List<object> _data = new List<object>();

        public LangResources Resource { get; set; }

        public List<object> Data
        {
            get
            {
                return _data;
            }
        }

        public bool Settlement { get; set; }

        public bool IsPopUp { get; set; }

        public bool IsParlay { get; set; }

        public bool IsShowPopUpTitle { get; set; }

        public SportType SportType { get; set; }
    }
}