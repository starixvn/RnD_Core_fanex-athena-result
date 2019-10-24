namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Linq;
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
    using Utilities;

    public sealed class BasketBallViewModel : SportsbookBaseViewModel, IResultViewModel
    {
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

            var moreResultDetailItem = ResultDetailItems
                .Where(item => BasketBallResourceID.MoreResultResourceIDs.Contains(item.ResourceID));

            var moreResultDetailItemWithRegex = ResultDetailItems
                .Where(item => BasketBallResourceID.MoreResultResourceIDRegex.Any(id => Regex.IsMatch(item.ResourceID, id)))
                .OrderBy(item => item.ResourceID);

            MoreResultDetailItems = moreResultDetailItem.Concat(moreResultDetailItemWithRegex)
                .OrderBy(item => BasketBallResourceID.MoreResultResourceIDs.ToList()
                .FindIndex(id => id == item.ResourceID || Regex.IsMatch(item.ResourceID, id))).ToSafeList();
        }

        public string OTHome => GetOvertimeSet(true);

        public string OTAway => GetOvertimeSet(false);

        public string HTHomeScore => GetTotalScore(true, false);

        public string HTAwayScore => GetTotalScore(false, false);

        public string FinalHomeScore => GetTotalScore(true, true);

        public string FinalAwayScore => GetTotalScore(false, true);

        public override bool VisibleDetailedLink => base.VisibleDetailedLink && HasGameDetailData();

        public override bool VisibleGameDetail => HasGameDetailData();

        public SafeList<BasketBallGameDetailItem> ResultDetailItems { get; }

        public SafeList<BasketBallGameDetailItem> MoreResultDetailItems { get; }

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

        private string GetResultFromGameDetails(string resourceId, bool isHome)
        {
            var result = ResultDetailItems.FirstOrDefault(g => g.ResourceID == resourceId);

            return isHome ? result?.HomeValue : result?.AwayValue;
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

        private bool HasGameDetailData() => MoreResultDetailItems.Any();
    }
}