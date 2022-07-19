namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Base;
    using Base.ViewModels;
    using DTO;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Sportsbook.DataDomain;
    using Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail;
    using Fanex.Result.Core.Volleyball.DTO;

    public class VolleyballViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private int _totalPointHome;
        private int _totalPointAway;

        public VolleyballViewModel(Sportsbook sportbook, ResultSetting setting)
            : this(sportbook, setting, null, null)
        {
        }

        public VolleyballViewModel(Sportsbook sportbook, 
            ResultSetting setting, 
            IResultDetail<MapVolleyballResult> resultDetail, 
            IRefDataHelper refDataHelper)
            : base(sportbook, setting, refDataHelper)
        {
            ResultDetailItems = (resultDetail ?? new MapVolleyball(sportbook, setting, null, null)).GetDetailItems();

            MoreResultDetailByMap = OrderAndGroupMapResult();

            IsPopupOpenFromBetList = setting.IsPopupOpenFromBetList;
        }

        private Dictionary<int, List<MapVolleyballResult>> OrderAndGroupMapResult()
        {
            var OrderbyMapXRoundYAndResultType = ResultDetailItems
                .Where(item => item.Visible)
                .OrderBy(mapResult => MapVolleyballResultMetadata.SupportBettype(mapResult.BetTypeId)
                                    ? MapVolleyballResultMetadata.GetMapByBettypeId(mapResult.BetTypeId).Order
                                    : 0);

            return OrderbyMapXRoundYAndResultType                
                .GroupBy(mapResult => mapResult.SetNumber)
                .OrderBy(group => group.Key)
                .ToDictionary(group => group.Key, group => group.ToList());
        }

        public string TotalPointHome => CreateTotalPointValue(isHome: true);

        public string TotalPointAway => CreateTotalPointValue(isHome: false);

        public string TotalGameHome => GetTotalSets(isHome: true);

        public string TotalGameAway => GetTotalSets(isHome: false);

        public bool IsPopupOpenFromBetList { get; }

        public override bool VisibleDetailedLink => base.VisibleDetailedLink && HasGameDetailData();

        public override bool VisibleGameDetail => HasGameDetailData();

        public SafeList<MapVolleyballResult> ResultDetailItems { get; }

        public Dictionary<int, List<MapVolleyballResult>> MoreResultDetailByMap { get; }

        public override string GetHomeAwaySet(int index, bool isHome)
        {
            const int maxIndex = 4;
            if (index > maxIndex)
            {
                return ConstantValue.Hyphen;
            }

            if (HomeSet == null || AwaySet == null)
            {
                _totalPointAway = 0;
                _totalPointHome = 0;
                HomeSet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen };
                AwaySet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen };

                GenerateHomeAwaySetValue();
            }

            return isHome ? HomeSet[index] : AwaySet[index];
        }

        private void GenerateHomeAwaySetValue()
        {
            if (IsNewData())
            {
                for (var j = 0; j < VolleyballResourceID.GameResourcesId.Length; j++)
                {
                    HomeSet[j] = GetResultFromGameDetails(VolleyballResourceID.GameResourcesId[j], isHome: true);
                    AwaySet[j] = GetResultFromGameDetails(VolleyballResourceID.GameResourcesId[j], isHome: false);

                    if (int.TryParse(AwaySet[j], out int resultAway) && int.TryParse(HomeSet[j], out int resultHome))
                    {
                        _totalPointAway += Convert.ToInt32(AwaySet[j]);
                        _totalPointHome += Convert.ToInt32(HomeSet[j]);
                    }
                }
            }
            else
            {
                GenerateHomeAwaySetValueWithOldData();
            }
        }

        private void GenerateHomeAwaySetValueWithOldData()
        {
            if (HasResult)
            {
                const int maxIndex = 4;
                var betStatusList = GetScoreInfoFromBetStatus.Split(',');
                for (var j = 0; j < betStatusList.Length && j <= maxIndex; j++)
                {
                    var values = betStatusList[j].Trim().Split('-');
                    if (values.Length > 1)
                    {
                        HomeSet[j] = string.IsNullOrEmpty(values[0].Trim()) ? "0" : values[0].Trim();
                        AwaySet[j] = string.IsNullOrEmpty(values[1].Trim()) ? "0" : values[1].Trim();

                        _totalPointAway += Convert.ToInt32(AwaySet[j]);
                        _totalPointHome += Convert.ToInt32(HomeSet[j]);
                    }
                }
            }
        }

        private string GetTotalSets(bool isHome)
        {
            if (IsNewData())
            {
                return GetResultFromGameDetails(VolleyballResourceID.TotalGame, isHome);
            }

            return isHome ? GetScore(SPORTBOOK.FinalHomeScore) : GetScore(SPORTBOOK.FinalAwayScore);
        }


        private string CreateTotalPointValue(bool isHome)
        {
            if (IsNewData())
            {
                var totalGames = GetTotalSets(isHome);
                if (totalGames.Equals(ConstantValue.Hyphen) || totalGames.Equals(ConstantValue.Refund))
                {
                    return totalGames;
                }
            }

            if (HomeSet == null || AwaySet == null)
            {
                GetHomeAwaySet(0, true);
            }

            var totalPoint = _totalPointHome + _totalPointAway;

            if (totalPoint > 0)
            {
                return isHome ? _totalPointHome.ToString(CultureInfo.InvariantCulture)
                    : _totalPointAway.ToString(CultureInfo.InvariantCulture);
            }

            return ConstantValue.Hyphen;
        }

        private string GetResultFromGameDetails(string resourceId, bool isHome)
        {
            var result = ResultDetailItems.FirstOrDefault(g => g.ResourceID == resourceId);
            if (result == null)
            {
                return ConstantValue.Hyphen;
            }

            return isHome ? result.HomeValue : result.AwayValue;
        }

        private bool IsNewData() => ResultDetailItems.Any();

        private bool HasGameDetailData() => MoreResultDetailByMap.Count > 0;
    }
}