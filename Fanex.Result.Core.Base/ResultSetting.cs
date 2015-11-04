// -----------------------------------------------------------------------
// <copyright file="ResultSetting.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Base
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ResultSetting
    {
        private string _dateTimeFormat = "{0:MM/dd/yyyy<br/>hh:mm:ss tt}";

        public string LanguageCode { get; set; }

        public bool IsPopUp { get; set; }

        public bool IsParlayPopUp { get; set; }

        public bool IsOutRight { get; set; }

        public bool VisiblePopUpTitle { get; set; }

        public SportType SportType { get; set; }

        /// <summary>
        /// Gets or sets Excel source path.
        /// Default path ="APP_DATA/FANEX.RESUST"
        /// Must be physical path .
        /// </summary>
        /// <value>String value.</value>
        public string ExcelTemplatePath { get; set; }

        public bool HighlightWinnerTeam { get; set; }

        public string ExcelTitle { get; set; }

        public bool VisibleMatchCodeIDColumn { get; set; }

        public bool VisibleDetailedLink { get; set; }

        public bool VisibleGoalRecord { get; set; }

        /// <summary>
        /// Gets or sets Kick Of Time format.
        /// Default is {0:MM/DD/YYYY<BR/>HH:MM:SS TT}.
        /// </summary>
        /// <value>The Date Time Format.</value>
        public string DateTimeFormat
        {
            get { return _dateTimeFormat; }
            set { _dateTimeFormat = value; }
        }

        public bool VisibleFreeHeader { get; set; }

        public bool VisibleInternalEventStatus { get; set; }
    }
}