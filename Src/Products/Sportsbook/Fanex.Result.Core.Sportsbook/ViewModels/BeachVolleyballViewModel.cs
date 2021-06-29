namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using Base.ViewModels;
    using DTO;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Sportsbook.DataDomain;
    using Fanex.Result.Core.Sportsbook.ViewModels.Badminton;
    using Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail;

    public sealed class BeachVolleyballViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private int _totalPointHome;
        private int _totalPointAway;

        public BeachVolleyballViewModel(Sportsbook sportbook, ResultSetting setting)
            : this(sportbook, setting, null, null)
        {
        }

        public BeachVolleyballViewModel(Sportsbook sportbook, ResultSetting setting, IResultDetail<GameDetailItem> resultDetail, IRefDataHelper refDataHelper)
           : base(sportbook, setting, refDataHelper)
        {
            ResultItems = (resultDetail ?? new BeachVolleyballResultDetail(sportbook, setting, refDataHelper, null))
                .GetDetailItems();
        }

        public SafeList<GameDetailItem> ResultItems { get; }

        public string TotalPointHome => CreateTotalPointValue(isHome: true);

        public string TotalPointAway => CreateTotalPointValue(isHome: false);

        public string TotalSetsHome => GetTotalSets(isHome: true);

        public string TotalSetsAway => GetTotalSets(isHome: false);

        public override string GetHomeAwaySet(int setIndex, bool isHome)
        {
            const int maxIndex = 2;
            if (setIndex > maxIndex)
            {
                return ConstantValue.Hyphen;
            }

            if (HomeSet == null || AwaySet == null)
            {
                _totalPointAway = 0;
                _totalPointHome = 0;
                HomeSet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen };
                AwaySet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen };

                GenerateHomeAwaySetValue();
            }

            return isHome ? HomeSet[setIndex] : AwaySet[setIndex];
        }

        private void GenerateHomeAwaySetValue()
        {
            for (var j = 0; j < BeachVolleyballResourceID.GameResourcesId.Length; j++)
            {
                HomeSet[j] = GetResultFromGameDetails(BeachVolleyballResourceID.GameResourcesId[j], isHome: true);
                AwaySet[j] = GetResultFromGameDetails(BeachVolleyballResourceID.GameResourcesId[j], isHome: false);

                if (int.TryParse(AwaySet[j], out int resultAway) && int.TryParse(HomeSet[j], out int resultHome))
                {
                    _totalPointAway += Convert.ToInt32(AwaySet[j]);
                    _totalPointHome += Convert.ToInt32(HomeSet[j]);
                }
            }
        }

        private string GetTotalSets(bool isHome)
            => GetResultFromGameDetails(BeachVolleyballResourceID.TotalSets, isHome);

        private string CreateTotalPointValue(bool isHome)
        {
            var totalSets = GetTotalSets(isHome);
            if (totalSets.Equals(ConstantValue.Hyphen) || totalSets.Equals(ConstantValue.Refund))
            {
                return totalSets;
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
            var result = ResultItems.FirstOrDefault(g => g.ResourceID == resourceId);
            if (result == null)
            {
                return ConstantValue.Hyphen;
            }

            var value = isHome ? result.HomeValue : result.AwayValue;

            return string.IsNullOrEmpty(value) ? ConstantValue.Hyphen : value;
        }
    }
}