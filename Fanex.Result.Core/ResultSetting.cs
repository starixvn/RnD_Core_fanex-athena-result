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
        /// Default path ="App_Data/Fanex.Result"
        /// Must be physical path .
        /// </summary>
        /// <value>String value.</value>
        public string ExcelTemplatePath { get; set; }

        /// <summary>
        /// Gets or sets Resource Path.
        /// Default path ="App_Data/Fanex.Result.Resource"
        /// Must be physical path .
        /// </summary>
        /// <value>String value.</value>
        public string ResourcePath { get; set; }

        public string ExcelTitle { get; set; }
    }
}