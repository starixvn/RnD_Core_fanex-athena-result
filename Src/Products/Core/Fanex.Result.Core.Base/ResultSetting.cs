namespace Fanex.Result.Core.Base
{
    public class ResultSetting
    {
        public string LanguageCode { get; set; }

        public bool IsPopUp { get; set; }

        public bool IsParlayPopUp { get; set; }

        public bool IsOutright { get; set; }

        public bool VisiblePopUpTitle { get; set; }

        public SportType SportType { get; set; }

        /// <summary>
        /// Excel source path.
        /// Default physical path ="APP_DATA/FANEX.RESUST"
        /// </summary>
        public string ExcelTemplatePath { get; set; }

        public bool HighlightWinnerTeam { get; set; }

        public string ExcelTitle { get; set; }

        public bool VisibleMatchCodeIDColumn { get; set; }

        public bool VisibleDetailedLink { get; set; }

        public bool VisibleGoalRecord { get; set; }

        public string DateTimeFormat { get; set; } = "{0:MM/dd/yyyy<br/>hh:mm:ss tt}";

        public bool VisibleFreeHeader { get; set; }

        public bool EnableCheckScoreCalculated { get; set; }

        public bool IsPopupOpenFromBetList { get; set; }
    }
}