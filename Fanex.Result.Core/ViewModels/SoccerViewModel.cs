// -----------------------------------------------------------------------
// <copyright file="SoccerViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.DTO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class SoccerViewModel : SportbookBaseViewModel,IResultViewModel
    {
        private bool _visibleDetailedLink = false;

        public SoccerViewModel(Sportsbook soccer, ResultSetting setting, LangResources resource)
            : base(soccer, setting, resource)
        {
            this._visibleDetailedLink = setting.VisibleDetailedLink;
        }

        #region Setttlement Site

        public int HomeId
        {
            get { return this.SPORTBOOK.HomeId; }
        }

        public int AwayId
        {
            get { return this.SPORTBOOK.AwayId; }
        }

        public string GoalRecord
        {
            get
            {
                return string.IsNullOrEmpty(this.SPORTBOOK.GoalRecord) ? string.Empty : this.SPORTBOOK.GoalRecord.Replace(";", string.Empty);
            }
        }

        public bool VisibleDetailedLink
        {
            get { return this._visibleDetailedLink && !string.IsNullOrEmpty(this.SPORTBOOK.GoalRecord.Trim()); }
        }

        public string PenaltyShootoutResult
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.SPORTBOOK.BetStatus))
                {
                    var index = this.SPORTBOOK.BetStatus.IndexOf(StringResources.PENALTYLABEL, StringComparison.Ordinal);

                    return index > -1 ? this.SPORTBOOK.BetStatus.Substring(index + StringResources.PENALTYLABEL.Length, 1) : string.Empty;
                }

                return string.Empty;
            }
        }

        public string PenaltyShootoutTooltip
        {
            get { return this.PenaltyShootoutResult.Equals("Y", StringComparison.Ordinal) ? Lang.PenaltyShootoutYes : Lang.PenaltyShootoutNo; }
        }

        #endregion Setttlement Site

        public bool IsShowFGHome
        {
            get
            {
                return IsShowedFirstLastGoal(true, true, false);
            }
        }

        public bool IsShowFGAway
        {
            get
            {
                return IsShowedFirstLastGoal(false, true, false);
            }
        }

        public bool IsShowLGHome
        {
            get
            {
                return IsShowedFirstLastGoal(true, false, false);
            }
        }

        public bool IsShowLGAway
        {
            get
            {
                return IsShowedFirstLastGoal(false, false, false);
            }
        }

        public bool IsShowedFirstHalfFGHome
        {
            get
            {
                return IsShowedFirstLastGoal(true, true, true);
            }
        }

        public bool IsShowedFirstHalfFGAway
        {
            get
            {
                return IsShowedFirstLastGoal(false, true, true);
            }
        }

        public bool IsShowedFirstHalfLGHome
        {
            get
            {
                return IsShowedFirstLastGoal(true, false, true);
            }
        }

        public bool IsShowedFirstHalfLGAway
        {
            get
            {
                return IsShowedFirstLastGoal(false, false, true);
            }
        }

        public string HTHomeScore
        {
            get
            {
                if (this.SPORTBOOK.EventStatus == StringResources.REFUND
                    || this.SPORTBOOK.EventStatus == StringResources.REFUNDHT || this.SPORTBOOK.EventStatus == StringResources.Postponed)
                {
                    return StringResources.NA;
                }

                return this.SPORTBOOK.HtHomeScore.HasValue ? this.SPORTBOOK.HtHomeScore.Value.ToString(CultureInfo.InvariantCulture) : StringResources.HYPHEN;
            }
        }

        public string FinalHomeScore
        {
            get
            {
                if (this.SPORTBOOK.EventStatus == StringResources.REFUND || this.SPORTBOOK.EventStatus == StringResources.REFUNDFT
                    || this.SPORTBOOK.EventStatus == StringResources.Postponed)
                {
                    return StringResources.NA;
                }

                return this.SPORTBOOK.FinalHomeScore.HasValue ? this.SPORTBOOK.FinalHomeScore.Value.ToString(CultureInfo.InvariantCulture) : StringResources.HYPHEN;
            }
        }

        public string HTAwayScore
        {
            get
            {
                if (this.SPORTBOOK.EventStatus == StringResources.REFUND
                    || this.SPORTBOOK.EventStatus == StringResources.REFUNDHT || this.SPORTBOOK.EventStatus == StringResources.Postponed)
                {
                    return StringResources.NA;
                }

                return this.SPORTBOOK.HtAwayScore.HasValue ? this.SPORTBOOK.HtAwayScore.Value.ToString(CultureInfo.InvariantCulture) : StringResources.HYPHEN;
            }
        }

        public string FinalAwayScore
        {
            get
            {
                if (this.SPORTBOOK.EventStatus == StringResources.REFUND
                    || this.SPORTBOOK.EventStatus == StringResources.REFUNDFT || this.SPORTBOOK.EventStatus == StringResources.Postponed)
                {
                    return StringResources.NA;
                }

                return this.SPORTBOOK.FinalAwayScore.HasValue ? this.SPORTBOOK.FinalAwayScore.Value.ToString(CultureInfo.InvariantCulture) : StringResources.HYPHEN;
            }
        }

        /// <summary>
        /// Show or don't show goal ICON.
        /// </summary>
        /// <param name="isHome">Is home Team.</param>
        /// <param name="isFirstGoal">Is first goal.</param>
        /// <param name="isFirstHalf">Is first half bet type.</param>
        /// <returns>The True or False.</returns>
        [SuppressMessage(
"StyleCop.CSharp.Nexcel.NexcelCustomRules",
"SP2101:MethodMustNotContainMoreLinesThan",
Justification = "Reviewed. Suppression is OK here.")]
        private bool IsShowedFirstLastGoal(bool isHome, bool isFirstGoal, bool isFirstHalf)
        {
            /*
         // Possible formats:
         // Refund HT;1FL:1-2;FL:2-0
         // 1FL:1-2;FL:2-0
         // Refund HT;FL:2-0
         // Refund HT;1FL:1-2
         // FL:2-0
         // Refund HT
         // 1FL:1-2
         */
            string betStatus;
            var homeOrAway = isHome ? "1" : "2";
            var firstOrLast = isFirstGoal ? 0 : 1;
            var firsHalfOrFullTime = isFirstHalf ? "1FL" : "FL";
            switch (this.SPORTBOOK.EventStatus)
            {
                case StringResources.REFUND:
                case StringResources.REFUNDHT:
                case StringResources.REFUNDFT:
                    betStatus = this.SPORTBOOK.BetStatus.Trim();
                    if (!string.IsNullOrEmpty(betStatus) && betStatus.Contains(":"))
                    {
                        string[] refundArray = betStatus.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        if (refundArray.Length >= 2 && !string.IsNullOrEmpty(refundArray[1]))
                        {
                            var goadRecord = refundArray.Length == 2 ? refundArray[1] : isFirstHalf ? refundArray[1] : refundArray[2];
                            var fglgArray = goadRecord.Split(':');
                            if (goadRecord.Length <= 1 || string.Compare(fglgArray[0], firsHalfOrFullTime, StringComparison.OrdinalIgnoreCase) != 0)
                            {
                                return false;
                            }

                            var goal = fglgArray[1].Split('-')[firstOrLast];
                            if (goal == homeOrAway)
                            {
                                return true;
                            }
                        }
                    }

                    return false;

                default:
                    betStatus = this.SPORTBOOK.BetStatus.Trim();
                    if (!string.IsNullOrEmpty(betStatus) && betStatus.Contains(":"))
                    {
                        string[] refundArray = betStatus.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                        var fglgArray = refundArray.Length == 1 ?
                            betStatus.Split(':') : isFirstHalf ? refundArray[0].Split(':') : refundArray[1].Split(':');

                        if (fglgArray.Length <= 1 || string.Compare(fglgArray[0], firsHalfOrFullTime, StringComparison.OrdinalIgnoreCase) != 0)
                        {
                            return false;
                        }

                        var goal = fglgArray[1].Split('-')[firstOrLast];

                        if (goal == homeOrAway)
                        {
                            return true;
                        }
                    }

                    return false;
            }
        }
    }
}