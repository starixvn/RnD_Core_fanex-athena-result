namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Base;
    using Base.DataDomain;
    using Base.Extension;
    using Base.Resources;
    using Base.ViewModels;
    using DataDomain;
    using DTO;
    using Models;
    using RazorEngine;
    using RazorEngine.Templating;
    using RemotingRef;
    using SoccerInfo;
    using Utilities;

    public sealed class SoccerViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private const string TemplateNameSpace = "SoccerDetail.";
        private const string FastMarketsTemplate = "FastMarkets";
        private SoccerDetailInfo _goalInfo;
        private bool? _hasValidGameData;
        private IList<SoccerGameDetailItem> _detailedItemModels;

        public SoccerViewModel(Sportsbook soccer, ResultSetting setting)
            : base(soccer, setting)
        {
            IsShowLiveScore = DetermineShowingLiveScore(setting.IsParlayPopUp, setting.IsPopUp);
        }

        #region Settlement Site

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
                return base.VisibleDetailedLink && (HasValidGameData || this.VisibleGoalRecord);
            }
        }

        #endregion Settlement Site

        public override bool VisibleGameDetail
        {
            get { return HasValidGameData; }
        }

        public bool IsShowFGHome
        {
            get { return GetGoalInfo().VisibleFGHome; }
        }

        public bool IsShowFGAway
        {
            get { return GetGoalInfo().VisibleFGAway; }
        }

        public bool IsShowLGHome
        {
            get { return GetGoalInfo().VisibleLGHome; }
        }

        public bool IsShowLGAway
        {
            get { return GetGoalInfo().VisibleLGAway; }
        }

        public bool IsShowedFirstHalfFGHome
        {
            get { return GetGoalInfo().Visible1stFGHome; }
        }

        public bool IsShowedFirstHalfFGAway
        {
            get { return GetGoalInfo().Visible1stFGAway; }
        }

        public bool IsShowedFirstHalfLGHome
        {
            get { return GetGoalInfo().Visible1stLGHome; }
        }

        public bool IsShowedFirstHalfLGAway
        {
            get { return GetGoalInfo().Visible1stLGAway; }
        }

        public string HTHomeScore
        {
            get { return CalculateScore(SPORTBOOK.HtHomeScore, false); }
        }

        public string FinalHomeScore
        {
            get { return CalculateScore(SPORTBOOK.FinalHomeScore, true); }
        }

        public string HTAwayScore
        {
            get { return CalculateScore(SPORTBOOK.HtAwayScore, false); }
        }

        public string FinalAwayScore
        {
            get { return CalculateScore(SPORTBOOK.FinalAwayScore, true); }
        }

        public string LiveHomeScore
        {
            get { return SPORTBOOK.LiveHomeScore.HasValue ? SPORTBOOK.LiveHomeScore.Value.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen; }
        }

        public string LiveAwayScore
        {
            get { return SPORTBOOK.LiveAwayScore.HasValue ? SPORTBOOK.LiveAwayScore.Value.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen; }
        }

        public bool IsShowLiveScore { get; }

        public string GoalRecord
        {
            get
            {
                return string.IsNullOrEmpty(this.SPORTBOOK.GoalRecord) ? string.Empty : Regex.Replace(this.SPORTBOOK.GoalRecord.ToUpper(), "[^HA]", string.Empty);
            }
        }

        public override bool VisibleGoalRecord
        {
            get
            {
                return base.VisibleGoalRecord && !string.IsNullOrEmpty(this.GoalRecord);
            }
        }

        public string DetailedResult
        {
            get
            {
                if (_detailedItemModels == null)
                {
                    GetDetailedResult();
                }

                var temp = new StringBuilder();
                var detailedReulstByGroup = _detailedItemModels.GroupBy(i => i.GroupCode).ToList();
                foreach (var item in detailedReulstByGroup)
                {
                    var templateName = SportsbookHelper.IsFastMarketsResourceId(item.Key)
                        ? TemplateNameSpace + FastMarketsTemplate
                        : TemplateNameSpace + item.Key;

                    var templateByGroup = Template.Instance.GetRazorTemplate(templateName);
                    if (!string.IsNullOrEmpty(templateByGroup))
                    {
                        var binderModel = new SoccerDetailViewModel
                        {
                            Name = item.FirstOrDefault().GroupName,
                            DetailItems = item.ToSafeList()
                        };
                        var result = new StringBuilder();
                        using (var writer = new StringWriter(result))
                        {
                            Engine.Razor.RunCompile(templateByGroup, templateName, writer, typeof(SoccerDetailViewModel), binderModel, null);
                        }

                        temp.Append(result.ToString());
                    }
                }

                return temp.ToString();
            }
        }

        #region Utilities

        private bool HasValidGameData
        {
            get
            {
                if (!_hasValidGameData.HasValue)
                {
                    _hasValidGameData = SPORTBOOK.GameDetails.Any(i => !string.IsNullOrEmpty(i.ResourceId)
                    && (ValidateBetRadarResourceId(i.ResourceId) || SportsbookHelper.IsFastMarketsResourceId(i.ResourceId)));
                }

                return _hasValidGameData.Value;
            }
        }

        private bool DetermineShowingLiveScore(bool isParlayPopup, bool isPopUp)
        {
            if (!isParlayPopup && isPopUp)
            {
                switch (SPORTBOOK.Bettype)
                {
                    case 3:
                        return SPORTBOOK.FinalHomeScore.HasValue ? false : true;

                    case 8:
                        return SPORTBOOK.HtHomeScore.HasValue ? false : true;
                }
            }

            return false;
        }

        private string CalculateScore(int? score, bool isFinalScore)
        {
            var refundType = isFinalScore ? ConstantValue.RefundFT : ConstantValue.RefundHT;

            if (SPORTBOOK.EventStatus.Equals(ConstantValue.Refund, StringComparison.OrdinalIgnoreCase)
                || SPORTBOOK.EventStatus.Equals(refundType, StringComparison.OrdinalIgnoreCase))
            {
                return LangResource.Refund;
            }

            return GetScore(score);
        }

        private bool ValidateBetRadarResourceId(string resourceId)
        {
            return GameResources.BetRadar.Values.Any(pattern => ValidateResourceIdByPattern(pattern, resourceId));
        }

        private bool ValidateResourceIdByPattern(List<string> patterns, string resourceId)
        {
            return patterns.Any(i => SportsbookHelper.IsValidResourceId(i, resourceId));
        }

        /// <summary>
        /// Get detailed game results.
        /// </summary>
        private void GetDetailedResult()
        {
            _detailedItemModels = new SafeList<SoccerGameDetailItem>();
            GetBetRadarDetailedResult();
            GetFastMarketsDetailedResult();
        }

        private void GetBetRadarDetailedResult()
        {
            foreach (var group in GameResources.BetRadar)
            {
                foreach (var game in group.Value)
                {
                    var detailedGameRecords = SPORTBOOK.GameDetails
                        .Where(i => SportsbookHelper.IsValidResourceId(game, i.ResourceId)).ToSafeList();

                    foreach (GameDetail detailedItem in detailedGameRecords)
                    {
                        if (detailedItem.ResourceId != GameResourceID.GoalSequence || IsShowGoalSequence(detailedItem.SettlementStatusID))
                        {
                            _detailedItemModels.Add(MapBetRadarDetailedGameValue(group.Key, detailedItem));
                        }
                    }
                }
            }
        }

        private void GetFastMarketsDetailedResult()
        {
            var gameDetails = SPORTBOOK.GameDetails.Where(i => SportsbookHelper.IsFastMarketsResourceId(i.ResourceId));
            var detailItemModels = gameDetails.Select(x => MapFastMarketsDetailedGameValue(x));

            _detailedItemModels = _detailedItemModels.Concat(detailItemModels).ToSafeList();
        }

        private bool IsShowGoalSequence(int settlementStatusID)
        {
            return settlementStatusID == EventStatusID.CompletedSettlementStatusID && !string.IsNullOrEmpty(GoalRecord);
        }

        /// <summary>
        /// Map game values.
        /// </summary>
        /// <param name="groupCode">Group game code.</param>
        /// <param name="detailedItem">The raw detailed value.</param>
        /// <returns>The detail view model.</returns>
        private SoccerGameDetailItem MapBetRadarDetailedGameValue(string groupCode, GameDetail detailedItem)
        {
            var item = new SoccerGameDetailItem
            {
                GroupCode = groupCode,
                ResourceID = detailedItem.ResourceId,
                GroupName = SportsbookHelper.GetResourceLang(groupCode),
                ResourceName = SportsbookHelper.GetResourceLang(detailedItem.ResourceId)
            };

            return MapMoreDetailedGameValue(item, detailedItem);
        }

        private SoccerGameDetailItem MapFastMarketsDetailedGameValue(GameDetail detailedItem)
        {
            var item = new SoccerGameDetailItem
            {
                GroupCode = detailedItem.ResourceId,
                ResourceID = detailedItem.ResourceId,
                GroupName = SportsbookHelper.GetFastMarketsResourceLang(detailedItem.ResourceId, GetSupportedLanguage),
                ResourceName = SportsbookHelper.CreateFastMarketsTimeRange(detailedItem.ResourceId, SPORTBOOK.BetID)
            };

            return MapMoreDetailedGameValue(item, detailedItem);
        }

        private SoccerGameDetailItem MapMoreDetailedGameValue(SoccerGameDetailItem soccerItem, GameDetail gameDetail)
        {
            switch (gameDetail.SettlementStatusID)
            {
                case EventStatusID.CompletedSettlementStatusID:
                case EventStatusID.RunningSettlementStatusID:
                    MapResourceGameValueByPattern(soccerItem, gameDetail);
                    break;

                case EventStatusID.RefundStettlementStatusID:
                    soccerItem.HomeValue = soccerItem.AwayValue = LangResource.Refund;
                    break;

                case EventStatusID.RefundHTSettlementStatusID:
                    soccerItem.HomeValue = soccerItem.AwayValue = LangResource.Refundht;

                    break;

                case EventStatusID.RefundFTSettlementStatusID:
                    soccerItem.HomeValue = soccerItem.AwayValue = LangResource.Refundft;

                    break;
            }

            return soccerItem;
        }

        /// <summary>
        /// Map resource code to appropriate value.
        /// </summary>
        /// <param name="item">The view model Item.</param>
        /// <param name="detailedValue">The raw game value.</param>
        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        private void MapResourceGameValueByPattern(SoccerGameDetailItem item, GameDetail detailedValue)
        {
            switch (item.ResourceID)
            {
                /*Group: Goals.*/
                case GameResourceID.FirstTenMinutes1X2:
                    item.HomeValue = item.AwayValue = GetResourceGameValueByPattern1X2(detailedValue.HomeValue, LangResource.Draw);
                    break;

                case GameResourceID.FirstGoalMethod:
                    item.HomeValue = item.AwayValue = GetFirstGoalMethodResult(detailedValue.HomeValue);
                    break;

                case GameResourceID.GoalSequence:
                    item.HomeValue = item.AwayValue = string.Join(".", GoalRecord.ToArray());
                    break;
                /*Group: Goal Time*/
                case GameResourceID.FirstGoalTime10Min:
                case GameResourceID.FirstGoalTime15Min:
                    item.HomeValue = item.AwayValue = GetResourceGameValueByPatternGoalTime(detailedValue.HomeValue);
                    break;
                /*Group :Goal Scorer*/
                case GameResourceID.FirstGoalScorer:
                    item.HomeValue = item.AwayValue = string.IsNullOrEmpty(detailedValue.OtherInfo) ?
                        ConstantValue.Hyphen : detailedValue.OtherInfo;
                    break;
                /*Group :Corner*/
                case GameResourceID.FirstHalfCorners:
                case GameResourceID.FinalCorners:
                    item.HomeValue = string.IsNullOrEmpty(detailedValue.HomeValue) ? ConstantValue.Hyphen : detailedValue.HomeValue;
                    item.AwayValue = string.IsNullOrEmpty(detailedValue.AwayValue) ? ConstantValue.Hyphen : detailedValue.AwayValue;
                    break;
                /*Group :First/Last Corner*/
                case GameResourceID.First1HCorner:
                case GameResourceID.Last1HCorner:
                case GameResourceID.FirstCorner:
                case GameResourceID.LastCorner:
                    item.HomeValue = item.AwayValue =
                    GetResourceGameValueByPattern1X2(detailedValue.HomeValue, LangResource.None);
                    break;
                /*Group :Player sent Off*/
                case GameResourceID.PlayerSentOff:
                case GameResourceID.PlayserSentOff1H:
                    item.HomeValue = item.AwayValue = GetTextValueOfYesNoChoice(detailedValue.HomeValue);
                    break;

                /*Group :Home/Away Player sent Off*/
                case GameResourceID.HomePlayserSentOff:
                case GameResourceID.HomePlayserSentOff1H:
                    item.HomeValue = item.AwayValue = GetTextValueOfYesNoChoice(detailedValue.HomeValue);
                    break;

                case GameResourceID.AwayPlayserSentOff:
                case GameResourceID.AwayPlayserSentOff1H:
                    item.AwayValue = item.HomeValue = GetTextValueOfYesNoChoice(detailedValue.AwayValue);
                    break;

                /*Group: Special*/
                case GameResourceID.ExtraTimeYesNo:
                case GameResourceID.ExtraTimeAndGoal:
                case GameResourceID.PenaltyShootout:
                    item.HomeValue = item.AwayValue = GetTextValueOfYesNoChoice(detailedValue.HomeValue);
                    break;

                case GameResourceID.WhoAdvancesToNextRound:
                    item.HomeValue = item.AwayValue =
                    GetResourceGameValueByPattern1X2(detailedValue.HomeValue, string.Empty);
                    break;

                case GameResourceID.MatchDecidedMethod:
                    item.HomeValue = item.AwayValue = GetResourceGameValueByPattern175(detailedValue.HomeValue);
                    break;

                /* Group: Fast Markets */
                case GameResourceID.NextOneMinutes:
                case GameResourceID.NextFiveMinutes:
                case GameResourceID.FirstOneMinutes:
                case GameResourceID.FirstFiveMinutes:
                case GameResourceID.NextOneMinuteSetPiece:
                    item.HomeValue = item.AwayValue = GetFastMarketSituation(detailedValue, Convert.ToInt32(SPORTBOOK.BetID));
                    break;

                default:
                    if (SportsbookHelper.IsValidResourceId(GameResourceID.NextGoalTime, item.ResourceID))
                    {
                        /*Group : Goal Time*/
                        item.HomeValue = item.AwayValue = GetResourceGameValueByPatternGoalTime(detailedValue.HomeValue);
                    }
                    else if (SportsbookHelper.IsValidResourceId(GameResourceID.GoalScorer, item.ResourceID))
                    {
                        /*Group :Goal Scorer*/
                        item.HomeValue = item.AwayValue = detailedValue.OtherInfo;
                    }
                    else if (SportsbookHelper.IsValidResourceId(GameResourceID.NextGoal1H, item.ResourceID)
                       || SportsbookHelper.IsValidResourceId(GameResourceID.NextGoal3rd, item.ResourceID)
                   || SportsbookHelper.IsValidResourceId(GameResourceID.ExtraTimeNextGoal, item.ResourceID))
                    {
                        /*Group :Goal Record*/
                        item.HomeValue = item.AwayValue = GetResourceGameValueByPattern1X2(detailedValue.HomeValue, LangResource.None);
                    }

                    break;
            }
        }

        /// <summary>
        /// Get game value by pattern goal time.
        /// </summary>
        /// <param name="homeValue">The home value.</param>
        /// <returns>The game value.</returns>
        private string GetResourceGameValueByPatternGoalTime(string homeValue)
        {
            if (string.IsNullOrEmpty(homeValue))
            {
                return ConstantValue.Hyphen;
            }

            if (homeValue.Equals(ConstantValue.None, StringComparison.OrdinalIgnoreCase))
            {
                return LangResource.None;
            }
            else
            {
                return homeValue + " " + LangResource.Min;
            }
        }

        /// <summary>
        /// Map resource value for ID 175.
        /// </summary>
        /// <param name="homeValue">The raw game value.</param>
        /// <returns>The game value.</returns>
        private string GetResourceGameValueByPattern175(string homeValue)
        {
            if (!string.IsNullOrEmpty(homeValue))
            {
                switch (homeValue.ToUpper())
                {
                    case "HR":
                        return this.HomeTeam + "/" + LangResource.RegularTime;

                    case "HE":
                        return this.HomeTeam + "/" + LangResource.ExtraTime;

                    case "HP":
                        return this.HomeTeam + "/" + LangResource.Penalty;

                    case "AR":
                        return this.AwayTeam + "/" + LangResource.RegularTime;

                    case "AE":
                        return this.AwayTeam + "/" + LangResource.ExtraTime;

                    case "AP":
                        return this.AwayTeam + "/" + LangResource.Penalty;
                }
            }

            return ConstantValue.Hyphen;
        }

        private string GetFastMarketSituation(GameDetail detailedItem, int startMinute)
        {
            var results = startMinute < 45
                ? System.Text.Encoding.Default.GetBytes(detailedItem.HomeValue)
                : System.Text.Encoding.Default.GetBytes(detailedItem.AwayValue);

            var result = results[startMinute % 45];
            var situation = RefData.GetFastMarketsResult(result.ToString(), GetSupportedLanguage);

            return situation;
        }

        private string GetTextValueOfYesNoChoice(string choice)
        {
            if (string.IsNullOrEmpty(choice))
            {
                return ConstantValue.Hyphen;
            }

            return choice.Equals(ConstantValue.Yes, StringComparison.OrdinalIgnoreCase) ? LangResource.Yes : LangResource.No;
        }

        /// <summary>
        /// Map resource value by pattern 1X2.
        /// </summary>
        /// <param name="homeValue">The raw game value.</param>
        /// <param name="noneValue">This value is used in case "None" or no have value.</param>
        /// <returns>The game value.</returns>
        private string GetResourceGameValueByPattern1X2(string homeValue, string noneValue)
        {
            if (!string.IsNullOrEmpty(homeValue))
            {
                switch (homeValue.ToUpper())
                {
                    case "1":
                    case "H":
                        return this.HomeTeam;

                    case "2":
                    case "A":
                        return this.AwayTeam;

                    case "X":
                    case "N":
                        return noneValue;
                }
            }

            return ConstantValue.Hyphen;
        }

        private string GetFirstGoalMethodResult(string homeValue)
        {
            switch (homeValue.ToUpper())
            {
                case "S":
                    return LangResource.Shot;

                case "H":
                    return LangResource.Header;

                case "P":
                    return LangResource.Penalty;

                case "FK":
                    return LangResource.FreeKick;

                case "OG":
                    return LangResource.OwnGoal;

                case "NG":
                    return LangResource.NoGoal;
            }

            return string.Empty;
        }

        /// <summary>
        /// Get goal information.
        /// </summary>
        /// <returns>The goal detail information.</returns>
        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules",
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