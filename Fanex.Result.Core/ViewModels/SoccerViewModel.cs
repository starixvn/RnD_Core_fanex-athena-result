// -----------------------------------------------------------------------
// <copyright file="SoccerViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Utilities;
    using Fanex.Result.Core.ViewModels.SoccerInfo;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class SoccerViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private SoccerDetailInfo _goalInfo;

        private IList<SoccerDetailViewModel> _detailedViewModels = null;

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

        public override bool VisibleDetailedLink
        {
            get
            {
                return base.VisibleDetailedLink &&
                 this.SPORTBOOK.GameDetails.Any(i => !string.IsNullOrEmpty(i.ResourceId) && i.SettlementStatusID > 0
                 && ValidateResourceId(i.ResourceId));
            }
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

        public IList<SoccerDetailViewModel> DetailedResult
        {
            get
            {
                if (this._detailedViewModels == null)
                {
                    this.GetDetailedResult();
                }

                return this._detailedViewModels;
            }
        }

        #region Utilities

        /// <summary>
        /// Validate resource ID.
        /// </summary>
        /// <param name="resourceId">The resource ID.</param>
        /// <returns>True if it's ID is valid.</returns>
        private static bool ValidateResourceId(string resourceId)
        {
            var validResources = GameResourceDefinition.BetRada.Values;
            foreach (var pattern in validResources)
            {
                if (ValidateResourceId(pattern, resourceId))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ValidateResourceId(List<string> resourceIdList, string resourceId)
        {
            return resourceIdList.Any(i => Helper.IsValidResourceId(i, resourceId));
        }

        /// <summary>
        /// Get detailed game results.
        /// </summary>
        private void GetDetailedResult()
        {
            this._detailedViewModels = new List<SoccerDetailViewModel>();
            foreach (var group in GameResourceDefinition.BetRada)
            {
                var detailedGameRecords = this.SPORTBOOK.GameDetails
                    .Where(i => ValidateResourceId(group.Value, i.ResourceId));
                foreach (var detailedItem in detailedGameRecords)
                {
                    this._detailedViewModels.Add(MapDetailedGameValue(group.Key, detailedItem));
                }
            }
        }

        /// <summary>
        /// Map game values.
        /// </summary>
        /// <param name="groupCode">Group game code.</param>
        /// <param name="detailedItem">The raw detailed value.</param>
        /// <returns>The detail view model.</returns>
        private SoccerDetailViewModel MapDetailedGameValue(string groupCode, GameDetail detailedItem)
        {
            var item = new SoccerDetailViewModel
            {
                GroupCode = groupCode,
                ResourceID = detailedItem.ResourceId,
                GroupName = Helper.GetResourceLang(groupCode, this.Lang),
                ResourceName = Helper.GetResourceLang(detailedItem.ResourceId, this.Lang)
            };

            switch (detailedItem.SettlementStatusID)
            {
                case EventStatusIDDefinition.CompletedSettlementStatusID:
                    MapResourceGameValue(item);
                    break;

                case EventStatusIDDefinition.RefundStettlementStatusID:
                    item.HomeValue = item.AwayValue = this.Lang.Refund;
                    break;

                case EventStatusIDDefinition.RefundHTSettlementStatusID:
                    item.HomeValue = item.AwayValue = this.Lang.Refundht;

                    break;

                case EventStatusIDDefinition.RefundFTSettlementStatusID:
                    item.HomeValue = item.AwayValue = this.Lang.Refundft;

                    break;

                default:
                    break;
            }

            return item;
        }

        /// <summary>
        /// Map resource code to appropriate value.
        /// </summary>
        /// <param name="item">The view model Item.</param>
        private void MapResourceGameValue(SoccerDetailViewModel item)
        {
            switch (item.ResourceID)
            {
                case "04":
                case "05":
                    break;

                default:
                    break;
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

        #endregion Utilities
    }
}