// -----------------------------------------------------------------------
// <copyright file="ResultSetting.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ResultSetting
    {
        public string LanguageCode { get; set; }

        public bool IsPopUp { get; set; }

        public bool IsParlayPopUp { get; set; }

        public bool IsOutRight { get; set; }

        public bool IsShowPopUpTitle { get; set; }

        public SportType SportType { get; set; }

        /// <summary>
        /// Gets or sets Excel source Path.
        /// Must be physical path .
        /// </summary>
        /// <value>String value.</value>
        public string ExcelTemplatePath { get; set; }

        public string ExcelTitle { get; set; }
    }
}