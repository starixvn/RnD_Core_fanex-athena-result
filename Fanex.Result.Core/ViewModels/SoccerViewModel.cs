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
    using System.Text;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Models;
    using Fanex.Result.Core.Utilities;
    using Fanex.Result.Core.ViewModels.SoccerInfo;
    using RazorEngine;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class SoccerViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private const string TemplateNameSpace = "SoccerDetail.";
        private SoccerDetailInfo _goalInfo;

        private IList<SoccerGameDetailItem> _detailedItemModels = null;

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
                return this.SPORTBOOK.HtHomeScore.HasValue ? this.SPORTBOOK.HtHomeScore.Value.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen;
            }
        }

        public string FinalHomeScore
        {
            get
            {
                return this.SPORTBOOK.FinalHomeScore.HasValue ? this.SPORTBOOK.FinalHomeScore.Value.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen;
            }
        }

        public string HTAwayScore
        {
            get
            {
                return this.SPORTBOOK.HtAwayScore.HasValue ? this.SPORTBOOK.HtAwayScore.Value.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen;
            }
        }

        public string FinalAwayScore
        {
            get
            {
                return this.SPORTBOOK.FinalAwayScore.HasValue ? this.SPORTBOOK.FinalAwayScore.Value.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen;
            }
        }

        public string DetailedResult
        {
            get
            {
                if (this._detailedItemModels == null)
                {
                    this.GetDetailedResult();
                }

                var temp = new StringBuilder();
                var templateOperator = Template.Instance;
                var detailedReulstByGroup = this._detailedItemModels.GroupBy(i => i.GroupCode);
                foreach (var item in detailedReulstByGroup)
                {
                    var templateName = TemplateNameSpace + item.Key;
                    var templateByGroup = templateOperator.GetRazorTemplate(templateName);
                    if (!string.IsNullOrEmpty(templateByGroup))
                    {
                        Razor.GetTemplate(templateByGroup, templateName);
                        var binderModel = new SoccerDetailViewModel
                        {
                            Name = item.FirstOrDefault().GroupName,
                            Resource = this.Lang,
                            DetailItems = item.ToList()
                        };

                        temp.Append(Razor.Run(templateName, binderModel));
                    }
                }

                return temp.ToString();
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
            var validResources = GameResources.BetRada.Values;
            foreach (var pattern in validResources)
            {
                if (ValidateResourceIdByPattern(pattern, resourceId))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ValidateResourceIdByPattern(List<string> patterns, string resourceId)
        {
            return patterns.Any(i => Helper.IsValidResourceId(i, resourceId));
        }

        /// <summary>
        /// Get detailed game results.
        /// </summary>
        private void GetDetailedResult()
        {
            this._detailedItemModels = new List<SoccerGameDetailItem>();
            foreach (var group in GameResources.BetRada)
            {
                var detailedGameRecords = this.SPORTBOOK.GameDetails
                    .Where(i => ValidateResourceIdByPattern(group.Value, i.ResourceId));
                foreach (var detailedItem in detailedGameRecords)
                {
                    this._detailedItemModels.Add(MapDetailedGameValue(group.Key, detailedItem));
                }
            }
        }

        /// <summary>
        /// Map game values.
        /// </summary>
        /// <param name="groupCode">Group game code.</param>
        /// <param name="detailedItem">The raw detailed value.</param>
        /// <returns>The detail view model.</returns>
        private SoccerGameDetailItem MapDetailedGameValue(string groupCode, GameDetail detailedItem)
        {
            var item = new SoccerGameDetailItem
            {
                GroupCode = groupCode,
                ResourceID = detailedItem.ResourceId,
                GroupName = Helper.GetResourceLang(groupCode, this.Lang),
                ResourceName = Helper.GetResourceLang(detailedItem.ResourceId, this.Lang),
            };

            switch (detailedItem.SettlementStatusID)
            {
                case EventStatusID.CompletedSettlementStatusID:
                    MapResourceGameValueByPattern(item, detailedItem);
                    break;

                case EventStatusID.RefundStettlementStatusID:
                    item.HomeValue = item.AwayValue = this.Lang.Refund;
                    break;

                case EventStatusID.RefundHTSettlementStatusID:
                    item.HomeValue = item.AwayValue = this.Lang.Refundht;

                    break;

                case EventStatusID.RefundFTSettlementStatusID:
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
        /// <param name="detailedValue">The raw game value.</param>
          [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2101:MethodMustNotContainMoreLinesThan",
    Justification = "Reviewed. Suppression is OK here.")]
        private void MapResourceGameValueByPattern(SoccerGameDetailItem item, GameDetail detailedValue)
        {
            switch (item.ResourceID)
            {
                /*Group: Goals.*/
                case GameResourceID.FirstTenMinutes1X2:
                    MapResourceGameValueByPattern1X2(item, detailedValue, Lang.Draw);
                    break;
                /*Group: Goal Time*/
                case GameResourceID.FirstGoalTime10Min:
                case GameResourceID.FirstGoalTime15Min:
                    MapResourceGameValueByPatternGoalTime(item, detailedValue);
                    break;
                /*Group :Goal Scorer*/
                case GameResourceID.FirstGoalScorer:
                    item.HomeValue = item.AwayValue = detailedValue.OtherInfo;
                    break;
                /*Group :Corner*/
                case GameResourceID.FirstHalfCorners:
                case GameResourceID.FinalCorners:
                    item.HomeValue = detailedValue.HomeValue;
                    item.AwayValue = detailedValue.AwayValue;
                    break;
                /*Group :First/Last Corner*/
                case GameResourceID.First1HCorner:
                case GameResourceID.Last1HCorner:
                case GameResourceID.FirstCorner:
                case GameResourceID.LastCorner:
                    MapResourceGameValueByPattern1X2(item, detailedValue, Lang.None);
                    break;
                /*Group :Player sent Off*/
                case GameResourceID.PlayerSentOff:
                case GameResourceID.PlayserSentOff1H:
                    item.HomeValue = item.AwayValue = GetTextValueOfYesNoChoice(detailedValue.HomeValue);

                    break;
                /*Group :Home/Away Player sent Off*/
                case GameResourceID.HomePlayserSentOff:
                case GameResourceID.HomePlayserSentOff1H:
                    item.HomeValue = GetTextValueOfYesNoChoice(detailedValue.HomeValue);
                    break;

                case GameResourceID.AwayPlayserSentOff:
                case GameResourceID.AwayPlayserSentOff1H:
                    item.AwayValue = GetTextValueOfYesNoChoice(detailedValue.AwayValue);
                    break;
                /*Group: Special*/
                case GameResourceID.ExtraTimeYesNo:
                case GameResourceID.PenaltyShootout:
                case GameResourceID.WhoAdvancesToNextRound:
                    MapResourceGameValueByPattern1X2(item, detailedValue, string.Empty);
                    break;

                case GameResourceID.MatchDecidedMethod:
                    MapResourceGameValueByPattern175(item, detailedValue);
                    break;

                default:
                    if (Helper.IsValidResourceId(GameResourceID.NextGoalTime, item.ResourceID))
                    {
                        /*Group : Goal Time*/
                        MapResourceGameValueByPatternGoalTime(item, detailedValue);
                    }
                    else if (Helper.IsValidResourceId(GameResourceID.GoalScorer, item.ResourceID))
                    {
                        /*Group :Goal Scorer*/
                        item.HomeValue = item.AwayValue = detailedValue.OtherInfo;
                    }
                    else if (Helper.IsValidResourceId(GameResourceID.NextGoal1H, item.ResourceID)
                       || Helper.IsValidResourceId(GameResourceID.NextGoal3rd, item.ResourceID)
                   || Helper.IsValidResourceId(GameResourceID.ExtraTimeNextGoal, item.ResourceID))
                    {
                        /*Group :Goal Record*/

                        MapResourceGameValueByPattern1X2(item, detailedValue, Lang.None);
                    }

                    break;
            }
        }

        private void MapResourceGameValueByPatternGoalTime(SoccerGameDetailItem item, GameDetail detailedValue)
        {
            if (detailedValue.HomeValue.Equals(ConstantValue.None, StringComparison.OrdinalIgnoreCase))
            {
                item.HomeValue = item.AwayValue = Lang.None;
            }
            else
            {
                item.HomeValue = item.AwayValue = detailedValue.HomeValue + " " + Lang.Min;
            }
        }

        /// <summary>
        /// Map resource value for ID 175.
        /// </summary>
        /// <param name="item">The current game detail item.</param>
        /// <param name="detailedValue">The raw game value.</param>
        private void MapResourceGameValueByPattern175(SoccerGameDetailItem item, GameDetail detailedValue)
        {
            switch (detailedValue.HomeValue)
            {
                case "HR":
                    item.HomeValue = item.AwayValue = this.HomeTeam + "/" + Lang.RegularTime;
                    break;

                case "HE":
                    item.HomeValue = item.AwayValue = this.HomeTeam + "/" + Lang.ExtraTime;
                    break;

                case "HP":
                    item.HomeValue = item.AwayValue = this.HomeTeam + "/" + Lang.Penalty;
                    break;

                case "AR":
                    item.HomeValue = item.AwayValue = this.AwayTeam + "/" + Lang.RegularTime;
                    break;

                case "AE":
                    item.HomeValue = item.AwayValue = this.AwayTeam + "/" + Lang.ExtraTime;
                    break;

                case "AP":
                    item.HomeValue = item.AwayValue = this.AwayTeam + "/" + Lang.Penalty;

                    break;
            }
        }

        private string GetTextValueOfYesNoChoice(string choice)
        {
            return choice.Equals(ConstantValue.Yes, StringComparison.OrdinalIgnoreCase)
                        ? Lang.Yes : Lang.No;
        }

        /// <summary>
        /// Map resource value by pattern 1X2.
        /// </summary>
        /// <param name="item">The current game detail item.</param>
        /// <param name="detailedValue">The raw game value.</param>
        /// <param name="noneValue">This value is used incase "None" or no have value.</param>
        private void MapResourceGameValueByPattern1X2(SoccerGameDetailItem item, GameDetail detailedValue, string noneValue)
        {
            switch (detailedValue.HomeValue)
            {
                case "1":
                case "H":
                    item.HomeValue = item.AwayValue = this.HomeTeam;
                    break;

                case "2":
                case "A":
                    item.HomeValue = item.AwayValue = this.AwayTeam;
                    break;

                case "X":
                case "N":
                    item.HomeValue = item.AwayValue = noneValue;
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