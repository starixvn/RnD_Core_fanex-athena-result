namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Base;
    using Base.DataDomain;
    using Base.Extension;
    using Base.ViewModels;
    using BaseDetail;
    using BasketBallDetail;
    using DataDomain;
    using DTO;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Sportsbook.Constants;
    using Fanex.Result.Core.Sportsbook.Models;
    using Fanex.Result.Core.Sportsbook.Resources;
    using Fanex.Result.Core.Sportsbook.ViewModels.SoccerDetail;
    using RazorEngine;
    using RazorEngine.Templating;

    public class BasketBallViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private const string TemplateNameSpace = "SoccerDetail.";
        private readonly BetBuilderResultViewModel betBuilderViewModel;

        public BasketBallViewModel(
            Sportsbook sportbook,
            ResultSetting setting,
            IResultDetail<BasketBallGameDetailItem> resultDetail = null)
            : this(sportbook, setting, resultDetail, null)
        {
        }

        public BasketBallViewModel(
          Sportsbook sportbook,
          ResultSetting setting,
          IResultDetail<BasketBallGameDetailItem> resultDetail,
          IRefDataHelper refDataHelper)
            : base(sportbook, setting, refDataHelper)
        {
            ResultDetailItems = (resultDetail ?? new BasketBallResultDetail(sportbook, setting, null, null))
                .GetDetailItems();

            PlayerProMoreResultItems = ResultDetailItems
                .Where(item => BasketBallResourceID.PlayerProsMoreResultResourceIDs.Any(i => Regex.IsMatch(item.ResourceID, i)))
                .ToSafeList();

            var moreResultDetailItem = ResultDetailItems
                .Where(item => BasketBallResourceID.MoreResultResourceIDs.Contains(item.ResourceID));

            var moreResultDetailItemWithRegex = ResultDetailItems
                .Where(item => BasketBallResourceID.MoreResultResourceIDRegex.Any(id => Regex.IsMatch(item.ResourceID, id)))
                .OrderBy(item => item.ResourceID);

            MoreResultDetailItems = moreResultDetailItem.Concat(moreResultDetailItemWithRegex)
                .OrderBy(item => BasketBallResourceID.MoreResultResourceIDs.ToList()
                .FindIndex(id => id == item.ResourceID || Regex.IsMatch(item.ResourceID, id))).ToSafeList();

            betBuilderViewModel = new BetBuilderResultViewModel(sportbook, setting, refDataHelper);

            IsShowLiveScore = DetermineShowingLiveScore();
        }


        public override string EventStatus
        {
            get
            {
                var abandonedQuater = SPORTBOOK.GameDetails
                    .OrderBy(i => BasketBallResourceID.AbandonedResourceIDs.ToList()
                    .FindIndex(id => id == i.ResourceId))
                    .FirstOrDefault(i => i.SettlementStatusID == EventStatusID.RefundWithSettleDetermined);
                var bits = MapEventStatus.GetBitArray(SPORTBOOK.SettlementStatus);
                var isAbandoned = bits[0] == 0;
                if (abandonedQuater != null && isAbandoned)
                {
                    var key = $"Abandoned{abandonedQuater.ResourceId}";
                    var text = SportsbookResx.ResourceManager.GetString(key);
                    return string.IsNullOrWhiteSpace(text) ? key : text;
                }

                return MapEventStatus.MapEventStatusString(
                    SPORTBOOK.SettlementStatus,
                    SPORTBOOK.EventStatus,
                    SPORTBOOK.BetStatus);
            }
        }

        public bool IsShowLiveScore { get; protected set; }

        public string OTHome => GetOvertimeSet(true);

        public string OTAway => GetOvertimeSet(false);

        public string HTHomeScore => GetTotalScore(true, false);

        public string HTAwayScore => GetTotalScore(false, false);

        public string FinalHomeScore => GetTotalScore(true, true);

        public string FinalAwayScore => GetTotalScore(false, true);

        public string SecondHaftHomeScore => GetSecondHaftScore(isHome: true);

        public string SecondHaftAwayScore => GetSecondHaftScore(isHome: false);

        public override bool VisibleDetailedLink => base.VisibleDetailedLink && HasGameDetailData();

        public override bool VisibleGameDetail => HasGameDetailData();

        public SafeList<BasketBallGameDetailItem> ResultDetailItems { get; }

        public SafeList<BasketBallGameDetailItem> MoreResultDetailItems { get; }
        
        public SafeList<BasketBallGameDetailItem> PlayerProMoreResultItems { get; }

        public override string GetHomeAwaySet(int index, bool isHome)
        {
            const int maxIndex = 4;

            if (index > maxIndex)
            {
                return ConstantValue.Hyphen;
            }

            var result = GetResultFromGameDetails($"Q{index + 1}", isHome);

            return string.IsNullOrEmpty(result) ? GetResultFromBetStatus(index, isHome) : result;
        }

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

        private string GetOvertimeSet(bool isHome)
        {
            var result = GetResultFromGameDetails(BasketBallResourceID.OverTime, isHome);

            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }

            result = GetResultFromBetStatus(4, isHome);

            return (!string.IsNullOrEmpty(result) && result.All(char.IsDigit) && Convert.ToInt32(result) > 0)
                ? result : ConstantValue.Hyphen;
        }

        public string LiveHomeScore
            => SPORTBOOK.LiveHomeScore.HasValue && IsShowLiveScore?
            SPORTBOOK.LiveHomeScore.Value.ToString(CultureInfo.InvariantCulture) :
            ConstantValue.Hyphen;

        public string LiveAwayScore
            => SPORTBOOK.LiveAwayScore.HasValue && IsShowLiveScore ?
            SPORTBOOK.LiveAwayScore.Value.ToString(CultureInfo.InvariantCulture) :
            ConstantValue.Hyphen;

        private string GetTotalScore(bool isHome, bool isFinal)
        {
            var resourceId = isFinal ? BasketBallResourceID.FullTime : BasketBallResourceID.FirstHalf;
            var result = GetResultFromGameDetails(resourceId, isHome);

            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }

            return isFinal ? GetScore(isHome ? SPORTBOOK.FinalHomeScore : SPORTBOOK.FinalAwayScore) :
                GetScore(isHome ? SPORTBOOK.HtHomeScore : SPORTBOOK.HtAwayScore);
        }

        private string GetSecondHaftScore(bool isHome)
        {
            var cutoffdate = new DateTime(2023, 7, 26);

            return SPORTBOOK.EventDate < cutoffdate ?
                GetSecondHaftScoreByOldData(isHome) :
                GetResultFromGameDetails(BasketBallResourceID.SecondHalf, isHome);
        }


        private string GetSecondHaftScoreByOldData(bool isHome)
        {
            if (!int.TryParse(HTHomeScore, out int HHome) ||
                !int.TryParse(HTAwayScore, out int HAway))
            {
                return HTHomeScore;
            }

            if (!int.TryParse(FinalHomeScore, out int FHome) ||
                !int.TryParse(FinalAwayScore, out int FAway))
            {
                return FinalHomeScore;
            }

            return isHome ? (FHome - HHome).ToString() : (FAway - HAway).ToString();
        }

        public virtual string GetResultFromGameDetails(string resourceId, bool isHome)
        {
            var result = ResultDetailItems.FirstOrDefault(g => g.ResourceID == resourceId);

            var score = isHome ? result?.HomeValue : result?.AwayValue;

            return string.IsNullOrEmpty(score) ? ConstantValue.Hyphen : score;
        }

        private string GetResultFromBetStatus(int index, bool isHome)
        {
            if (HomeSet == null || AwaySet == null)
            {
                HomeSet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Zero };
                AwaySet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Zero };

                if (EventStatus != LangResource.Refundft)
                {
                    GenerateHomeAwaySetValueFromBetStatus(5);
                }
            }

            return isHome ? HomeSet[index] : AwaySet[index];
        }

        private bool HasGameDetailData() => MoreResultDetailItems.Any() || PlayerProMoreResultItems.Any();

        private bool DetermineShowingLiveScore()
        {
            var betTypeSupport = BetTypeConstants.BetTypeBasketballShowLiveScore.ContainsKey(SPORTBOOK.Bettype);
            if (betTypeSupport && Setting.IsPopUp)
            {
                if (!SPORTBOOK.GameDetails.Any())
                {
                    return true;
                }

                var resourceID = BetTypeConstants.BetTypeBasketballShowLiveScore[SPORTBOOK.Bettype];
                var betId = SPORTBOOK.BetID.Length == 3 ? SPORTBOOK.BetID.Substring(SPORTBOOK.BetID.Length - 1) : SPORTBOOK.BetID;
                var id = resourceID == "QX" ? resourceID.Replace("X", betId) : resourceID;
                var gameDetail = SPORTBOOK.GameDetails.FirstOrDefault(i => i.ResourceId == id);

                return gameDetail != null && gameDetail.SettlementStatusID == EventStatusID.RunningSettlementStatusID;
            }

            return false;
        }
    }
}