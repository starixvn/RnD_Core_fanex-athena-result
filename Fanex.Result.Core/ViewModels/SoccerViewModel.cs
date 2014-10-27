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
    using Fanex.Result.Core.ViewModels.SoccerInfo;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class SoccerViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private SoccerDetailInfo _goalInfo;

        public SoccerViewModel(Sportsbook soccer, ResultSetting setting, LangResources resource)
            : base(soccer, setting, resource)
        {
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

        public override bool VisibleDetailedLink
        {
            get { return base.VisibleDetailedLink && !string.IsNullOrEmpty(this.SPORTBOOK.GoalRecord); }
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
                return GetGoalInfo().VisibleFGHome;
            }
        }

        public bool IsShowFGAway
        {
            get
            {
                return GetGoalInfo().VisibleFGAway;
            }
        }

        public bool IsShowLGHome
        {
            get
            {
                return GetGoalInfo().VisibleLGHome;
            }
        }

        public bool IsShowLGAway
        {
            get
            {
                return GetGoalInfo().VisibleLGAway;
            }
        }

        public bool IsShowedFirstHalfFGHome
        {
            get
            {
                return GetGoalInfo().Visible1stFGHome;
            }
        }

        public bool IsShowedFirstHalfFGAway
        {
            get
            {
                return GetGoalInfo().Visible1stFGAway;
            }
        }

        public bool IsShowedFirstHalfLGHome
        {
            get
            {
                return GetGoalInfo().Visible1stLGHome;
            }
        }

        public bool IsShowedFirstHalfLGAway
        {
            get
            {
                return GetGoalInfo().Visible1stLGAway;
            }
        }

        public string HTHomeScore
        {
            get
            {
                return this.SPORTBOOK.HtHomeScore.HasValue ? this.SPORTBOOK.HtHomeScore.Value.ToString(CultureInfo.InvariantCulture) : StringResources.HYPHEN;
            }
        }

        public string FinalHomeScore
        {
            get
            {
                return this.SPORTBOOK.FinalHomeScore.HasValue ? this.SPORTBOOK.FinalHomeScore.Value.ToString(CultureInfo.InvariantCulture) : StringResources.HYPHEN;
            }
        }

        public string HTAwayScore
        {
            get
            {
                return this.SPORTBOOK.HtAwayScore.HasValue ? this.SPORTBOOK.HtAwayScore.Value.ToString(CultureInfo.InvariantCulture) : StringResources.HYPHEN;
            }
        }

        public string FinalAwayScore
        {
            get
            {
                return this.SPORTBOOK.FinalAwayScore.HasValue ? this.SPORTBOOK.FinalAwayScore.Value.ToString(CultureInfo.InvariantCulture) : StringResources.HYPHEN;
            }
        }

        /// <summary>
        /// Get goal information.
        /// </summary>
        /// <returns>The goal detail information.</returns>
        [SuppressMessage(
"StyleCop.CSharp.Nexcel.NexcelCustomRules",
"SP2101:MethodMustNotContainMoreLinesThan",
Justification = "Reviewed. Suppression is OK here.")]
        private SoccerDetailInfo GetGoalInfo()
        {
            if (this._goalInfo == null)
            {
                this._goalInfo = new SoccerDetailInfo();
                const string HomeGoal = "1";
                const string AwayGoal = "2";
                const string GoalFirstHalf = "1FL";
                const string GoalFullTime = "FL";
                const int FGIndex = 0;
                const int LGIndex = 1;
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
                var betStatus = this.SPORTBOOK.BetStatus != null ? this.SPORTBOOK.BetStatus.Trim() : string.Empty;
                if (betStatus.IndexOf(":", StringComparison.Ordinal) > -1)
                {
                    string[] tokenInfos = betStatus.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var token in tokenInfos)
                    {
                        if (token.IndexOf(GoalFirstHalf, StringComparison.Ordinal) > -1)
                        {
                            var firstGoalInfo = token.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            if (firstGoalInfo.Length == 2)
                            {
                                var firstGoalToken = firstGoalInfo[1].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                                this._goalInfo.Visible1stFGHome = firstGoalToken[FGIndex] == HomeGoal;
                                this._goalInfo.Visible1stFGAway = firstGoalToken[FGIndex] == AwayGoal;

                                this._goalInfo.Visible1stLGHome = firstGoalToken[LGIndex] == HomeGoal;
                                this._goalInfo.Visible1stLGAway = firstGoalToken[LGIndex] == AwayGoal;
                            }
                        }
                        else if (token.IndexOf(GoalFullTime, StringComparison.Ordinal) > -1)
                        {
                            var goalInfo = token.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            if (goalInfo.Length == 2)
                            {
                                var goalInfoToken = goalInfo[1].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                                this._goalInfo.VisibleFGHome = goalInfoToken[FGIndex] == HomeGoal;
                                this._goalInfo.VisibleFGAway = goalInfoToken[FGIndex] == AwayGoal;

                                this._goalInfo.VisibleLGHome = goalInfoToken[LGIndex] == HomeGoal;
                                this._goalInfo.VisibleLGAway = goalInfoToken[LGIndex] == AwayGoal;
                            }
                        }
                    }
                }
            }

            return this._goalInfo;
        }
    }
}