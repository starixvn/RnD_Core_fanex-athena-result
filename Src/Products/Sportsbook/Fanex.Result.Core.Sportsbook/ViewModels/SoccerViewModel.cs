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
    using BaseDetail;
    using DataDomain;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Sportsbook.Constants;
    using Models;
    using RazorEngine;
    using RazorEngine.Templating;
    using SoccerDetail;
    using SoccerInfo;
    using Utilities;

    public sealed class SoccerViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private const string GameOnTopInDetailItem = "Sequence";
        private const string TemplateNameSpace = "SoccerDetail.";
        private const string FastMarketsTemplate = "FastMarkets";
        private const string PenaltyHandicapCode = "(h)";
        private const string PenaltyOverUnderCode = "(o)";
        private const string PenaltyHandicapName = "(HDP)";
        private const string PenaltyOverUnderName = "(O/U)";
        private SoccerDetailInfo _goalInfo;

        private bool? _hasValidGameData;
        private IList<SoccerGameDetailItem> _detailedItemModels;
        private readonly IResultDetail<SoccerGameDetailItem> _resultDetail;
        private readonly IResultDetail<SoccerGameDetailItem> _fastMarketResultDetail;
        private readonly BetBuilderResultViewModel betBuilderViewModel;

        public SoccerViewModel(Sportsbook soccer, ResultSetting setting)
            : this(soccer, setting, null, null, null)
        {
        }

        public SoccerViewModel(
            Sportsbook soccer,
            ResultSetting setting,
            IResultDetail<SoccerGameDetailItem> resultDetail,
            IResultDetail<SoccerGameDetailItem> fastMarketResultDetail,
            IRefDataHelper refDataHelper)
            : base(soccer, setting, refDataHelper)
        {
            PenaltyMatchDetail = ExistMatchCode(PenaltyHandicapCode) ?
                PenaltyHandicapName :
                ExistMatchCode(PenaltyOverUnderCode) ? PenaltyOverUnderName : string.Empty;
            _resultDetail = resultDetail ?? new SoccerResultDetail(soccer, setting);
            _fastMarketResultDetail = fastMarketResultDetail ?? new FastMarketResultDetail(soccer, setting);
            betBuilderViewModel = new BetBuilderResultViewModel(soccer, setting, refDataHelper);
            IsShowLiveScore = DetermineShowingLiveScore(setting.IsPopUp);
        }

        public bool IsShowLiveScore { get; }

        public string PenaltyMatchDetail { get; }

        public int HomeId => SPORTBOOK.HomeId;

        public int AwayId => SPORTBOOK.AwayId;

        public override bool VisibleDetailedLink
            => base.VisibleDetailedLink && (HasValidGameData || VisibleGoalRecord);

        public override bool VisibleGameDetail => HasValidGameData;

        public bool IsShowFGHome => GetGoalInfo().VisibleFGHome;

        public bool IsShowFGAway => GetGoalInfo().VisibleFGAway;

        public bool IsShowLGHome => GetGoalInfo().VisibleLGHome;

        public bool IsShowLGAway => GetGoalInfo().VisibleLGAway;

        public bool IsShowedFirstHalfFGHome => GetGoalInfo().Visible1stFGHome;

        public bool IsShowedFirstHalfFGAway => GetGoalInfo().Visible1stFGAway;

        public bool IsShowedFirstHalfLGHome => GetGoalInfo().Visible1stLGHome;

        public bool IsShowedFirstHalfLGAway => GetGoalInfo().Visible1stLGAway;

        public string HTHomeScore => CalculateHTScore(SPORTBOOK.HtHomeScore);

        public string FinalHomeScore => CalculateScore(SPORTBOOK.FinalHomeScore, true);

        public string HTAwayScore => CalculateHTScore(SPORTBOOK.HtAwayScore);

        public string FinalAwayScore => CalculateScore(SPORTBOOK.FinalAwayScore, true);

        public bool IsLastSeriesChildMatch => CheckLastSeriesChildMatch(SPORTBOOK.MainType, SPORTBOOK.SubType);

        public string HTLastSeriesChildMatchChoice => GetHTLastSeriesChildMatchChoice();

        public string FTLastSeriesChildMatchChoice => GetFTLastSeriesChildMatchChoice();

        public string LiveHomeScore
            => SPORTBOOK.LiveHomeScore.HasValue && IsShowLiveScore ?
            SPORTBOOK.LiveHomeScore.Value.ToString(CultureInfo.InvariantCulture) :
            ConstantValue.Hyphen;

        public string LiveAwayScore
            => SPORTBOOK.LiveAwayScore.HasValue && IsShowLiveScore ?
            SPORTBOOK.LiveAwayScore.Value.ToString(CultureInfo.InvariantCulture) :
            ConstantValue.Hyphen;

        public string GoalRecord
            => string.IsNullOrEmpty(SPORTBOOK.GoalRecord) ?
            string.Empty :
            Regex.Replace(SPORTBOOK.GoalRecord.ToUpperInvariant(), "[^HA]", string.Empty);

        public override bool VisibleGoalRecord => base.VisibleGoalRecord && !string.IsNullOrEmpty(GoalRecord);

        public bool VisibleBetBuilderDetail => Setting.IsPopupOpenFromBetList && betBuilderViewModel.Detail.Any();

        public string BetBuilderResult
        {
            get
            {
                var result = new StringBuilder();
                const string templateName = TemplateNameSpace + "BetBuilder";
                var templateContent = Template.Instance.GetRazorTemplate(templateName);

                if (!string.IsNullOrEmpty(templateContent))
                {
                    using (var writer = new StringWriter(result))
                    {
                        Engine.Razor.RunCompile(templateContent, templateName, writer, typeof(BetBuilderResultViewModel), betBuilderViewModel);
                    }
                }

                return result.ToString();
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
                var detailedResultsByGroup = _detailedItemModels.GroupBy(i => i.GroupCode).ToList();

                foreach (var item in detailedResultsByGroup)
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
                            Engine.Razor.RunCompile(templateByGroup, templateName, writer, typeof(SoccerDetailViewModel), binderModel);
                        }

                        temp.Append(result.ToString());
                    }
                }

                return temp.ToString();
            }
        }

        #region Utilities

        private bool EventStatusIsRefundHT => EventStatus.Equals(LangResource.Refundht, StringComparison.OrdinalIgnoreCase);

        private bool EventStatusIsRefundFT => EventStatus.Equals(LangResource.Refundft, StringComparison.OrdinalIgnoreCase);

        private bool EventStatusIsRefund
            => EventStatusIsRefundFT
            || EventStatus.Equals(LangResource.Refundht, StringComparison.OrdinalIgnoreCase)
            || EventStatus.Equals(LangResource.Refund, StringComparison.OrdinalIgnoreCase);

        private bool EventStatusIsAbandoned
            => EventStatus.Equals(LangResource.Abandoned1h, StringComparison.OrdinalIgnoreCase)
                || EventStatus.Equals(LangResource.Abandoned2h, StringComparison.OrdinalIgnoreCase);

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

        private bool DetermineShowingLiveScore(bool isPopUp)
        {
            if (isPopUp)
            {
                return ShowLiveScoreForFTBettype() || ShowLiveScoreForHTBettype() || ShowLiveScoreForFastMarket();
            }

            return false;
        }

        public bool ShowLiveScoreForFastMarket()
        {
            var timeMachineOverUnderTime = SPORTBOOK.GameDetails.Any(i => i.ResourceId == GameResourceID.TimeMachineOverUnderStatus);
            if (timeMachineOverUnderTime)
            {
                return !_fastMarketResultDetail.GetDetailItems().Any();
            }

            return false;
        }

        private bool ShowLiveScoreForFTBettype()
        {
            var isInstantSettlementBetTypesSupportFT = BetTypeConstants.IsInstantSettlementBetTypesSupportFT(SPORTBOOK.Bettype);
            if (isInstantSettlementBetTypesSupportFT)
            {
                if (EventStatusIsAbandoned
                    || EventStatusIsRefundFT
                    || EventStatus.Equals(LangResource.Refund, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                return !SPORTBOOK.Calculate;
            }

            return false;
        }

        private bool ShowLiveScoreForHTBettype()
        {
            var isRefundOrAbandonedEvent = EventStatusIsRefund || EventStatusIsAbandoned;
            var isInstantSettlementBetTypesSupportHT = BetTypeConstants.IsInstantSettlementBetTypesSupportHT(SPORTBOOK.Bettype);
            if (isInstantSettlementBetTypesSupportHT)
            {
                return isRefundOrAbandonedEvent ? false : !SPORTBOOK.IsHTCal;
            }

            return false;
        }

        private string CalculateHTScore(int? score)
            => SPORTBOOK.NoHTScore ? ConstantValue.Hyphen : CalculateScore(score, false);

        private string CalculateScore(int? score, bool isFinalScore)
        {
            var refundType = isFinalScore ? LangResource.Refundft : LangResource.Refundht;

            if (EventStatus.Equals(LangResource.Refund, StringComparison.OrdinalIgnoreCase)
                || EventStatus.Equals(refundType, StringComparison.OrdinalIgnoreCase))
            {
                return LangResource.Refund;
            }

            if (EventStatus.Equals(LangResource.Abandoned1h, StringComparison.OrdinalIgnoreCase) &&
                isFinalScore)
            {
                return ConstantValue.Hyphen;
            }

            if (ScoreIsNotCalculated(isFinalScore))
            {
                return ConstantValue.Hyphen;
            }

            return GetScore(score);
        }

        private static bool ValidateBetRadarResourceId(string resourceId)
            => GameResources.MoreResultGroup.Values.Any(pattern => ValidateResourceIdByPattern(pattern, resourceId)) ||
               GameResources.BetRadar.Values.Any(pattern => ValidateResourceIdByPattern(pattern, resourceId));

        private static bool ValidateResourceIdByPattern(List<string> patterns, string resourceId)
            => patterns.Any(i => SportsbookHelper.IsValidResourceId(i, resourceId));

        /// <summary>
        /// Get detailed game results.
        /// </summary>
        private void GetDetailedResult()
        {
            _detailedItemModels = _resultDetail.GetDetailItems();
            GetBetRadarDetailedResult();
            _detailedItemModels = _detailedItemModels.Concat(_fastMarketResultDetail.GetDetailItems()).ToSafeList();
            _detailedItemModels = _detailedItemModels
                .OrderBy(item => item.GroupCode != GameOnTopInDetailItem)
                .ThenBy(item => GameResources.BetRadar.Keys.ToList().FindIndex(k => k == item.GroupCode)).ToList();
        }

        private void GetBetRadarDetailedResult()
        {
            foreach (var group in GameResources.BetRadar)
            {
                foreach (var game in group.Value)
                {
                    GenerateGameDetailItems(group, game);
                }
            }
        }

        private void GenerateGameDetailItems(KeyValuePair<string, List<string>> group, string game)
        {
            var detailedGameRecords = SPORTBOOK.GameDetails
                .Where(i => SportsbookHelper.IsValidResourceId(game, i.ResourceId)).ToSafeList();

            foreach (GameDetail detailedItem in detailedGameRecords)
            {
                var detailedItemModel = MapBetRadarDetailedGameValue(group.Key, detailedItem);
                var isSequence = detailedItemModel.ResourceID == GameResourceID.FTSequence ||
                    detailedItemModel.ResourceID == GameResourceID.H1Sequence;

                if (!isSequence ||
                    (isSequence && !string.IsNullOrEmpty(detailedItemModel.HomeValue)))
                {
                    _detailedItemModels.Add(detailedItemModel);
                }
            }
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
                ResourceName = SportsbookHelper.GetResourceLang(detailedItem.ResourceId),
                SettlementStatusID = detailedItem.SettlementStatusID
            };

            return MapMoreDetailedGameValue(item, detailedItem);
        }

        private SoccerGameDetailItem MapMoreDetailedGameValue(SoccerGameDetailItem soccerItem, GameDetail gameDetail)
        {
            switch (gameDetail.SettlementStatusID)
            {
                case EventStatusID.CompletedSettlementStatusID:
                case EventStatusID.RunningSettlementStatusID:
                case EventStatusID.Abandoned1HSettlementStatusID:
                case EventStatusID.Abandoned2HSettlementStatusID:
                case EventStatusID.RefundWithSettleDetermined:
                    MapResourceGameValueByPattern(soccerItem, gameDetail);
                    break;

                case EventStatusID.RefundSettlementStatusID:
                    if (SportsbookHelper.IsValidResourceId(GameResourceID.HomePenaltyShootoutRoundX, gameDetail.ResourceId) ||
                        SportsbookHelper.IsValidResourceId(GameResourceID.AwayPenaltyShootoutRoundX, gameDetail.ResourceId))
                    {
                        MapResourceGameValueByPattern(soccerItem, gameDetail);
                    }
                    else
                    {
                        soccerItem.HomeValue = soccerItem.AwayValue = LangResource.Refund;
                    }

                    break;

                case EventStatusID.RefundHTSettlementStatusID:
                    soccerItem.HomeValue = soccerItem.AwayValue = LangResource.Refundht;
                    break;

                case EventStatusID.RefundFTSettlementStatusID:
                    soccerItem.HomeValue = soccerItem.AwayValue = LangResource.Refundft;
                    break;

                default:
                    soccerItem.HomeValue = soccerItem.AwayValue = ConstantValue.Hyphen;
                    break;
            }

            return soccerItem;
        }
        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
#pragma warning disable S1541 // Methods and properties should not be too complex
#pragma warning disable S3776 // Cognitive Complexity of methods should not be too high
        private void MapResourceGameValueByPattern(SoccerGameDetailItem item, GameDetail detailedValue)
#pragma warning restore S3776 // Cognitive Complexity of methods should not be too high
#pragma warning restore S1541 // Methods and properties should not be too complex
        {
            // TODO: Refactor later
            switch (item.ResourceID)
            {
                /*Group: Goals.*/
                case GameResourceID.FirstTenMinutes1X2:
                    item.HomeValue = item.AwayValue = GetResourceGameValueByPattern1X2(detailedValue.HomeValue, LangResource.Draw);
                    break;

                case GameResourceID.FirstGoalMethod:
                    item.HomeValue = item.AwayValue = ConvertHomeAwayToText.GetFirstGoalMethodResult(detailedValue.HomeValue);
                    break;

                case GameResourceID.FTSequence:
                case GameResourceID.H1Sequence:
                    item.HomeValue = item.AwayValue = string.Empty;
                    var sequence = GetSequenceFromGameDetails(detailedValue.HomeValue);
                    if (IsShowSequence(detailedValue.SettlementStatusID, sequence))
                    {
                        item.HomeValue = item.AwayValue = string.Join(".", sequence.ToArray());
                    }
                    break;

                /*Group: Goal Time*/
                case GameResourceID.FirstGoalTime10Min:
                case GameResourceID.FirstGoalTime15Min:
                    item.HomeValue = item.AwayValue = ConvertHomeAwayToText.GetResourceGameValueByPatternGoalTime(detailedValue.HomeValue);
                    break;
                /*Group :Goal Scorer*/
                case GameResourceID.FirstGoalScorer:
                    item.HomeValue = item.AwayValue = string.IsNullOrEmpty(detailedValue.OtherInfo) ?
                        ConstantValue.Hyphen : detailedValue.OtherInfo;
                    break;

                /*Group :Corner*/
                case GameResourceID.FirstHalfCorners:
                case GameResourceID.FinalCorners:
                case GameResourceID.PenaltyShootoutOddEvenFirst10:
                case GameResourceID.PenaltyShootoutExactTotalGoalFirst10:
                    item.HomeValue = string.IsNullOrEmpty(detailedValue.HomeValue) ? ConstantValue.Hyphen : detailedValue.HomeValue;
                    item.AwayValue = string.IsNullOrEmpty(detailedValue.AwayValue) ? ConstantValue.Hyphen : detailedValue.AwayValue;
                    break;

                /*Group :First/Last Corner*/
                case GameResourceID.First1HCorner:
                case GameResourceID.FirstCorner:
                case GameResourceID.FTFirstCorner:
                case GameResourceID.FirstHaftFirstCorner:
                case GameResourceID.SecondHaftFirstCorner:
                    item.HomeValue = item.AwayValue =
                        GetResourceGameValueByPattern1X2(detailedValue.HomeValue, LangResource.None);
                    break;

                case GameResourceID.Last1HCorner:
                case GameResourceID.LastCorner:
#pragma warning disable S3240 // The simplest possible condition syntax should be used
                    if (detailedValue.SettlementStatusID == EventStatusID.Abandoned1HSettlementStatusID ||
#pragma warning restore S3240 // The simplest possible condition syntax should be used
                        (detailedValue.SettlementStatusID == EventStatusID.Abandoned2HSettlementStatusID &&
                        item.ResourceID == GameResourceID.LastCorner))
                    {
                        item.HomeValue = item.AwayValue = LangResource.Refund;
                    }
                    else
                    {
                        item.HomeValue = item.AwayValue = GetResourceGameValueByPattern1X2(detailedValue.HomeValue, LangResource.None);
                    }

                    break;

                case GameResourceID.PlayerSentOff:
                case GameResourceID.PlayserSentOff1H:
                case GameResourceID.HomePlayerSentOff:
                case GameResourceID.HomePlayerSentOff1H:
                case GameResourceID.ExtraTimeYesNo:
                case GameResourceID.ExtraTimeAndGoal:
                case GameResourceID.PenaltyShootout:
                case GameResourceID.PenaltyShootoutToGoToSuddenDeath:
                    item.HomeValue = item.AwayValue = ConvertHomeAwayToText.GetTextValueOfYesNoChoice(detailedValue.HomeValue);
                    break;

                case GameResourceID.AwayPlayerSentOff:
                case GameResourceID.AwayPlayerSentOff1H:
                    item.AwayValue = item.HomeValue = ConvertHomeAwayToText.GetTextValueOfYesNoChoice(detailedValue.AwayValue);
                    break;

                case GameResourceID.WhoAdvancesToNextRound:
                    item.HomeValue = item.AwayValue =
                    GetResourceGameValueByPattern1X2(detailedValue.HomeValue, string.Empty);
                    break;

                case GameResourceID.MatchDecidedMethod:
                    item.HomeValue = item.AwayValue = GetResourceGameValueByPattern175(detailedValue.HomeValue);
                    break;
                case GameResourceID.TimeOfFirstCorner:
                case GameResourceID.TimeOfSecondHalfFirstCorner:
                    item.HomeValue = ConvertHomeAwayToText.GetResourceGameValueByTimeEvent(detailedValue);
                    break;
                case GameResourceID.HomePenaltyShootoutOverUnderFirst10:
                    item.HomeValue = item.AwayValue = detailedValue.HomeValue;
                    break;
                case GameResourceID.AwayPenaltyShootoutOverUnderFirst10:
                    item.HomeValue = item.AwayValue = detailedValue.HomeValue;
                    break;

                default:
                    MapResourceGameValueLikePattern(detailedValue, item);
                    break;
            }
        }

        private void MapResourceGameValueLikePattern(GameDetail detailedValue, SoccerGameDetailItem item)
        {
            if (SportsbookHelper.IsValidResourceId(GameResourceID.NextGoalTime, item.ResourceID))
            {
                item.HomeValue = item.AwayValue = ConvertHomeAwayToText.GetResourceGameValueByPatternGoalTime(detailedValue.HomeValue);
            }
            else if (SportsbookHelper.IsValidResourceId(GameResourceID.GoalScorer, item.ResourceID))
            {
                item.HomeValue = item.AwayValue = detailedValue.OtherInfo;
            }
            else if (SportsbookHelper.IsValidResourceId(GameResourceID.CornersRace, item.ResourceID) ||
                SportsbookHelper.IsValidResourceId(GameResourceID.FirstHaftCornerRace, item.ResourceID))
            {
                item.HomeValue = item.AwayValue = GetResourceGameValueByPattern1X2(detailedValue.HomeValue, LangResource.Neither);
            }
            else
            {
                if (SportsbookHelper.IsValidResourceId(GameResourceID.NextGoal1H, item.ResourceID) ||
                    SportsbookHelper.IsValidResourceId(GameResourceID.NextGoal3rd, item.ResourceID) ||
                    SportsbookHelper.IsValidResourceId(GameResourceID.ExtraTimeNextGoal, item.ResourceID))
                {
                    item.HomeValue = item.AwayValue = GetResourceGameValueByPattern1X2(detailedValue.HomeValue, LangResource.None);
                }

                if (SportsbookHelper.IsValidResourceId(GameResourceID.ExtraTimeNextGoalMethod, item.ResourceID))
                {
                    item.HomeValue = ConvertHomeAwayToText.GetFirstGoalMethodResult(detailedValue.HomeValue);
                }

                if (SportsbookHelper.IsValidResourceId(GameResourceID.PenaltyShootoutWoodworkRoundX, item.ResourceID))
                {
                    item.HomeValue = ConvertHomeAwayToText.GetResourceGameValueByWoodwork(detailedValue, isHome: true);
                    item.AwayValue = ConvertHomeAwayToText.GetResourceGameValueByWoodwork(detailedValue, isHome: false);
                }
                if (SportsbookHelper.IsValidResourceId(GameResourceID.PenaltyShootout1X2RoundX, item.ResourceID))
                {
                    item.HomeValue = item.AwayValue = ConvertHomeAwayToText.GetResourceGameValueBy1X2Determinded(detailedValue.HomeValue);
                }
                if (SportsbookHelper.IsValidResourceId(GameResourceID.PenaltyShootoutBothOneNeitherRoundX, item.ResourceID))
                {
                    item.HomeValue = item.AwayValue = ConvertHomeAwayToText.GetResourceGameValueByBothOneNeith(detailedValue.HomeValue);
                }
                if (SportsbookHelper.IsValidResourceId(GameResourceID.HomePenaltyShootoutRoundX, item.ResourceID) ||
                    SportsbookHelper.IsValidResourceId(GameResourceID.AwayPenaltyShootoutRoundX, item.ResourceID))
                {
                    item.HomeValue = item.AwayValue = ConvertHomeAwayToText.GetResourceGameValueByScoreMiss(detailedValue.HomeValue);
                }
            }
        }

        private static bool IsShowSequence(int settlementStatusID, string goalSequence)
            => (settlementStatusID == EventStatusID.CompletedSettlementStatusID ||
                settlementStatusID == EventStatusID.Abandoned1HSettlementStatusID ||
                settlementStatusID == EventStatusID.Abandoned2HSettlementStatusID) &&
                !string.IsNullOrEmpty(GetSequenceFromGameDetails(goalSequence));

        /// <summary>
        /// Map resource value for ID 175.
        /// </summary>
        /// <param name="homeValue">The raw game value.</param>
        /// <returns>The game value.</returns>
        private string GetResourceGameValueByPattern175(string homeValue)
        {
            if (!string.IsNullOrEmpty(homeValue))
            {
                switch (homeValue.ToUpperInvariant())
                {
                    case "HR":
                        return HomeTeam + "/" + LangResource.RegularTime;

                    case "HE":
                        return HomeTeam + "/" + LangResource.ExtraTime;

                    case "HP":
                        return HomeTeam + "/" + LangResource.Penalty;

                    case "AR":
                        return AwayTeam + "/" + LangResource.RegularTime;

                    case "AE":
                        return AwayTeam + "/" + LangResource.ExtraTime;

                    case "AP":
                        return AwayTeam + "/" + LangResource.Penalty;

                    default:
                        return ConstantValue.Hyphen;
                }
            }

            return ConstantValue.Hyphen;
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
                switch (homeValue.ToUpperInvariant())
                {
                    case "1":
                    case "H":
                        return HomeTeam;

                    case "2":
                    case "A":
                        return AwayTeam;

                    case "X":
                    case "N":
                        return noneValue;

                    default:
                        return ConstantValue.Hyphen;
                }
            }

            return ConstantValue.Hyphen;
        }


        /// <summary>
        /// Get goal information.
        /// </summary>
        /// <returns>The goal detail information.</returns>
        private SoccerDetailInfo GetGoalInfo()
        {
            if (_goalInfo == null)
            {
                _goalInfo = new SoccerDetailInfo();
                const string HomeGoal = "1";
                const string AwayGoal = "2";
                const string GoalFirstHalf = "1FL";
                const string GoalFullTime = "FL";
                const int FGIndex = 0;
                const int LGIndex = 1;

                var betStatus = SPORTBOOK.BetStatus != null ? SPORTBOOK.BetStatus.Trim() : string.Empty;

                if (betStatus.IndexOf(":", StringComparison.Ordinal) > -1)
                {
                    string[] tokenInfos = betStatus.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var token in tokenInfos)
                    {
                        GenerateGoalInfo(HomeGoal, AwayGoal, GoalFirstHalf, GoalFullTime, FGIndex, LGIndex, token);
                    }
                }
            }

            return _goalInfo;
        }

        private void GenerateGoalInfo(
            string homeGoal,
            string awayGoal,
            string goalFirstHalf,
            string goalFullTime,
            int firstGoalIndex,
            int lastGoalIndex,
            string token)
        {
            if (token.IndexOf(goalFirstHalf, StringComparison.Ordinal) > -1)
            {
                var firstGoalInfo = token.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (firstGoalInfo.Length == 2)
                {
                    var firstGoalToken = firstGoalInfo[1].Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    _goalInfo.Visible1stFGHome = firstGoalToken[firstGoalIndex] == homeGoal;
                    _goalInfo.Visible1stFGAway = firstGoalToken[firstGoalIndex] == awayGoal;

                    _goalInfo.Visible1stLGHome = firstGoalToken[lastGoalIndex] == homeGoal;
                    _goalInfo.Visible1stLGAway = firstGoalToken[lastGoalIndex] == awayGoal;
                }
            }
            else
            {
                if (token.IndexOf(goalFullTime, StringComparison.Ordinal) > -1)
                {
                    var goalInfo = token.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if (goalInfo.Length == 2)
                    {
                        var goalInfoToken = goalInfo[1].Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                        _goalInfo.VisibleFGHome = goalInfoToken[firstGoalIndex] == homeGoal;
                        _goalInfo.VisibleFGAway = goalInfoToken[firstGoalIndex] == awayGoal;
                        _goalInfo.VisibleLGHome = goalInfoToken[lastGoalIndex] == homeGoal;
                        _goalInfo.VisibleLGAway = goalInfoToken[lastGoalIndex] == awayGoal;
                    }
                }
            }
        }

        private static string GetSequenceFromGameDetails(string goalSequence)
        {
            return string.IsNullOrEmpty(goalSequence) ?
                string.Empty :
                Regex.Replace(goalSequence.ToUpperInvariant(), "[^HA]", string.Empty);
        }

        private bool ScoreIsNotCalculated(bool isFinalScore)
        {
            if (!EventStatusIsAbandoned)
            {
                if (!isFinalScore && !SPORTBOOK.IsHTCal && !EventStatusIsRefundFT)
                {
                    return true;
                }

                if (isFinalScore && !SPORTBOOK.Calculate)
                {
                    return true;
                }
            }

            return false;
        }

        private bool ExistMatchCode(string matchCode)
            => SPORTBOOK.MatchCode.Replace(" ", string.Empty).ToLowerInvariant().Contains(matchCode);

        private static bool CheckLastSeriesChildMatch(byte? mainType, int? subType)
        {
            var lastSeriesChildMatchId = string.Format(SoccerConstants.LastSeriesChildMatchIdFormat, mainType, subType);

            return SoccerConstants.LastSeriesChildMatchMapper.Contains(lastSeriesChildMatchId);
        }

        private string GetHTLastSeriesChildMatchChoice()
        {
            var htHomeScore = SPORTBOOK.HtHomeScore ?? default;
            var htAwayScore = SPORTBOOK.HtAwayScore ?? default;
            if(IsLastSeriesChildMatch &&
            !EventStatusIsRefundHT &&
            SPORTBOOK.IsHTCal &&
            htHomeScore != htAwayScore)
            {
                return htHomeScore > htAwayScore ? HomeTeam : AwayTeam;
            }

            return null;
        }

        private string GetFTLastSeriesChildMatchChoice()
        {
            var ftHomeScore = SPORTBOOK.FinalHomeScore ?? default;
            var ftAwayScore = SPORTBOOK.FinalAwayScore ?? default;
            if(IsLastSeriesChildMatch && !EventStatusIsRefundFT)
            {
                if(SPORTBOOK.Calculate && ftHomeScore != ftAwayScore)
                {
                    return ftHomeScore > ftAwayScore ? HomeTeam : AwayTeam;
                }
                
                if(!SPORTBOOK.Calculate && !string.IsNullOrEmpty(HTLastSeriesChildMatchChoice))
                {
                    return ConstantValue.Hyphen;
                }
            }

            return null;
        }

        #endregion Utilities
    }
}