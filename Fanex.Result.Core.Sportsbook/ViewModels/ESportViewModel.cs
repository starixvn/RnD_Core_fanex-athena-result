namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using Base.Extension;
    using Base.ViewModels;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.ESport.DTO;
    using Fanex.Result.Core.Sportsbook.DataDomain;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail;

    public sealed class ESportViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public const int MaxNumberOfMaps = 9;

        public ESportViewModel(
            Sportsbook sportbook,
            ResultSetting setting)
            : this(sportbook, setting, null, null)
        {
        }

        public ESportViewModel(
          Sportsbook sportbook,
          ResultSetting setting,
          IResultDetail<MapResult> resultDetail,
          IRefDataHelper refDataHelper)
            : base(sportbook, setting, refDataHelper)
        {
            ResultDetailItems = (resultDetail ?? new Map(sportbook, setting, null, null)).GetDetailItems();

            MoreResultDetailByMap = ResultDetailItems
                .OrderBy(mapResult => MapResultMetadata.ResultTypeOrder.Keys.Contains(mapResult.BetTypeId)
                                    ? MapResultMetadata.ResultTypeOrder[mapResult.BetTypeId].Order
                                    : 0)
                .GroupBy(mapResult => mapResult.MapId)
                .ToDictionary(group => group.Key, group => group.ToList());

        }

        public string FinalHomeScore => GetTotalScore(true);

        public string FinalAwayScore => GetTotalScore(false);

        public override bool VisibleDetailedLink => base.VisibleDetailedLink && HasGameDetailData();

        public override bool VisibleGameDetail => HasGameDetailData();

        public SafeList<MapResult> ResultDetailItems { get; }

        public Dictionary<int, List<MapResult>> MoreResultDetailByMap { get; }

        public override string GetHomeAwaySet(int index, bool isHome)
        {
            if (index > MaxNumberOfMaps)
            {
                return ConstantValue.Hyphen;
            }

            var result = GetResultFromGameDetails($"M{index + 1}", isHome);

            return string.IsNullOrEmpty(result) 
                ? GetResultFromBetStatus(index, isHome) 
                : result;
        }

        private string GetResultFromGameDetails(string resourceId, bool isHome)
        {
            var result = ResultDetailItems.FirstOrDefault(g => g.ResourceID == resourceId);

            return isHome 
                ? result?.HomeValue 
                : result?.AwayValue;
        }

        private string GetTotalScore(bool isHome)
        {
            var result = GetResultFromGameDetails(ESportResourceID.FinalScore, isHome);

            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }

            return GetScore(isHome ? SPORTBOOK.FinalHomeScore : SPORTBOOK.FinalAwayScore);
        }

        private string GetResultFromBetStatus(int index, bool isHome)
        {
            if (HomeSet == null || AwaySet == null)
            {
                HomeSet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen };
                AwaySet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen };

                if (SPORTBOOK.EventStatus != ConstantValue.RefundFT)
                {
                    GenerateHomeAwaySetValueFromBetStatus(MaxNumberOfMaps);
                }
            }

            return isHome ? HomeSet[index] : AwaySet[index];
        }

        private bool HasGameDetailData() => MoreResultDetailByMap.Any();
    }
}