namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using Base.Extension;
    using Base.ViewModels;
    using BasketBallDetail;
    using DataDomain;
    using DTO;

    public sealed class BasketBallViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public BasketBallViewModel(
            Sportsbook sportbook,
            ResultSetting setting)
            : base(sportbook, setting)
        {
        }

        public string OTHome => GetOvertimeSet(true);

        public string OTAway => GetOvertimeSet(false);

        public string HTHomeScore => GetTotalScore(true, false);

        public string HTAwayScore => GetTotalScore(false, false);

        public string FinalHomeScore => GetTotalScore(true, true);

        public string FinalAwayScore => GetTotalScore(false, true);

        public override bool VisibleDetailedLink => base.VisibleDetailedLink && HasGameDetailData();

        public override bool VisibleGameDetail => HasGameDetailData();

        public SafeList<BasketBallGameDetailItem> ResultDetailItems
           => new BasketBallResultDetail(SPORTBOOK, Setting, null, null).GetDetailItems();

        public SafeList<BasketBallGameDetailItem> MoreResultDetailItems
            => ResultDetailItems
                .Where(item => BasketBalResourceID.MoreResultResourceIDs.Contains(item.ResourceID))
                .OrderBy(item => BasketBalResourceID.MoreResultResourceIDs.ToList().FindIndex(id => id == item.ResourceID))
                .ToSafeList();

        public override string GetHomeAwaySet(int index, bool isHome)
        {
            if (index > 4)
            {
                return ConstantValue.Hyphen;
            }

            var result = GetResultFromGameDetails($"Q{index + 1}", isHome);

            return string.IsNullOrEmpty(result) ? GetResultFromBetStatus(index, isHome) : result;
        }

        private string GetOvertimeSet(bool isHome)
        {
            var result = GetResultFromGameDetails(BasketBalResourceID.OverTime, isHome);

            result = string.IsNullOrEmpty(result) ? GetResultFromBetStatus(4, true) : result;

            return Convert.ToInt32(result) > 0 ? result : ConstantValue.Hyphen;
        }

        private string GetTotalScore(bool isHome, bool isFinal)
        {
            var resourceId = isFinal ? BasketBalResourceID.FullTime : BasketBalResourceID.FirstHalf;
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
            if (this.HomeSet == null || this.AwaySet == null)
            {
                this.HomeSet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Zero };
                this.AwaySet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Zero };

                if (this.SPORTBOOK.EventStatus != ConstantValue.RefundFT && this.HasResult)
                {
                    string[] betStatusList = this.GetCoreInfoFromBetStatus.Split(',');
                    for (int j = 0; j < betStatusList.Length && j < 5; j++)
                    {
                        var values = betStatusList[j].Trim().Split('-');
                        this.HomeSet[j] = values[0].Trim();
                        this.AwaySet[j] = values[1].Trim();
                    }
                }
            }

            return isHome ? this.HomeSet[index] : this.AwaySet[index];
        }

        private bool HasGameDetailData()
            => ResultDetailItems.Any(item =>
                BasketBalResourceID.MoreResultResourceIDs.Contains(item.ResourceID) &&
                !string.IsNullOrEmpty(item.ResourceID));
    }
}