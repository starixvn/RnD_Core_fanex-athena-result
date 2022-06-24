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

            MoreResultDetailByMap = OrderAndGroupMapResult();

            IsPopupOpenFromBetList = setting.IsPopupOpenFromBetList;
        }

        private Dictionary<int, List<MapResult>> OrderAndGroupMapResult()
        {
            var orderByRound = ResultDetailItems
                .Where(item => item.MapId > 0 && item.Visible)
                .OrderBy(mapResult => MapResultMetadata.ResultTypeOrder.Keys.Contains(mapResult.BetTypeId)
                                        && !MapResultMetadata.ResultTypeOrder[mapResult.BetTypeId].MapXRoundYType
                                    ? mapResult.RoundId
                                    : 0);

            var OrderbyMapXRoundYAndResultType = orderByRound
                .OrderBy(mapResult => MapResultMetadata.ResultTypeOrder[mapResult.BetTypeId].MapXRoundYType
                                    ? mapResult.RoundId
                                    : 0)
                .ThenBy(mapResult => MapResultMetadata.ResultTypeOrder.Keys.Contains(mapResult.BetTypeId)
                                    ? MapResultMetadata.ResultTypeOrder[mapResult.BetTypeId].Order
                                    : 0);

            return OrderbyMapXRoundYAndResultType
                .OrderBy(mapResult => MapResultMetadata.ResultTypeOrder[mapResult.BetTypeId].IsLastResult
                                ? 1
                                : 0)
                .GroupBy(mapResult => mapResult.MapId)
                .OrderBy(group => group.Key)
                .ToDictionary(group => group.Key, group => group.ToList());
        }

        public string FinalHomeScore => GetTotalScore(true);

        public string FinalAwayScore => GetTotalScore(false);

        public bool IsPopupOpenFromBetList { get; }

        public override bool VisibleDetailedLink => base.VisibleDetailedLink && HasGameDetailData();

        public override bool VisibleGameDetail => HasGameDetailData();

        public SafeList<MapResult> ResultDetailItems { get; }

        public Dictionary<int, List<MapResult>> MoreResultDetailByMap { get; }

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

        private bool HasGameDetailData() => MoreResultDetailByMap.Count > 0;
    }
}