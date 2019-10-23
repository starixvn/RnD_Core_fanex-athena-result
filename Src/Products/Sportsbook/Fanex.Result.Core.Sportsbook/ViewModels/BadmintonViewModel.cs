namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Base;
    using Base.DataDomain;
    using Base.ViewModels;
    using DTO;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Sportsbook.DataDomain;
    using Fanex.Result.Core.Sportsbook.ViewModels.Badminton;
    using Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail;

    public sealed class BadmintonViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private int _totalPointHome;
        private int _totalPointAway;

        public BadmintonViewModel(Sportsbook sportbook, ResultSetting setting, IResultDetail<BadmintonlGameDetailItem> resultDetail = null)
            : this(sportbook, setting, resultDetail, null)
        {
        }

        public BadmintonViewModel(Sportsbook sportbook, ResultSetting setting, IResultDetail<BadmintonlGameDetailItem> resultDetail, IRefDataHelper refDataHelper)
           : base(sportbook, setting, refDataHelper)
        {
            ResultItems = (resultDetail ?? new BadmintonResultDetail(sportbook, setting, refDataHelper, null))
                .GetDetailItems();

            ResultDetailItems = ResultItems
                .Where(item => BadmintonResourceID.MoreResultResourceIDRegex.Any(id => Regex.IsMatch(item.ResourceID, id)))
                .OrderBy(item => item.GameNumber)
                .ThenBy(item => BadmintonResourceID.MoreResultResourceIDRegex.ToList().FindIndex(id => id == item.ResourceID || Regex.IsMatch(item.ResourceID, id)))
                .ToSafeList();
        }

        public SafeList<BadmintonlGameDetailItem> ResultItems { get; }

        public SafeList<BadmintonlGameDetailItem> ResultDetailItems { get; }

        public override bool VisibleDetailedLink => base.VisibleDetailedLink && HasGameDetailData();

        public override bool VisibleGameDetail => HasGameDetailData();

        public string TotalPointHome => CreateTotalPointValue(isHome: true);

        public string TotalPointAway => CreateTotalPointValue(isHome: false);

        public string TotalGameHome => GetTotalGames(isHome: true);

        public string TotalGameAway => GetTotalGames(isHome: false);

        public override string GetHomeAwaySet(int index, bool isHome)
        {
            if (index > 4)
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
            if (ResultItems.Any())
            {
                for (var j = 0; j < BadmintonResourceID.GameResourcesId.Length; j++)
                {
                    HomeSet[j] = GetResultFromGameDetails(BadmintonResourceID.GameResourcesId[j], isHome: true);
                    AwaySet[j] = GetResultFromGameDetails(BadmintonResourceID.GameResourcesId[j], isHome: false);

                    if (int.TryParse(AwaySet[j], out int resultAway) && int.TryParse(HomeSet[j], out int resultHome))
                    {
                        _totalPointAway += Convert.ToInt32(AwaySet[j]);
                        _totalPointHome += Convert.ToInt32(HomeSet[j]);
                    }
                }
            }
        }

        private string GetTotalGames(bool isHome)
            => GetResultFromGameDetails(BadmintonResourceID.TotalGame, isHome);

        private string CreateTotalPointValue(bool isHome)
        {
            var totalGames = GetTotalGames(isHome);
            if (totalGames.Equals(ConstantValue.Hyphen) || totalGames.Equals(ConstantValue.Refund))
            {
                return totalGames;
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

            return isHome ? result.HomeValue : result.AwayValue;
        }

        private bool HasGameDetailData() => ResultDetailItems.Any();
    }
}