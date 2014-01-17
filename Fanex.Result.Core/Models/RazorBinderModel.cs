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
        public LangResources Resource { get; set; }

        public IList<object> Data { get; set; }

        public bool Settlement { get; set; }

        public bool IsPopUp { get; set; }

        public bool IsParlay { get; set; }
    }
}