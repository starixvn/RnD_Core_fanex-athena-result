namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Globalization;
    using Base;
    using Base.DataDomain;
    using Base.ViewModels;
    using DTO;
    using Utilities;

    public sealed class BadmintonViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private int _totalPointHome;
        private int _totalPointAway;

        public BadmintonViewModel(Sportsbook sportbook, ResultSetting setting)
            : this(sportbook, setting, null)
        {
        }

        public BadmintonViewModel(Sportsbook sportbook, ResultSetting setting, ISportsbookRefDataHelper refDataHelper)
           : base(sportbook, setting, refDataHelper)
        {
        }

        public string TotalPointHome => CreateTotalPointValue(true);

        public string TotalPointAway => CreateTotalPointValue(false);

        public string TotalSetHome => GetScore(SPORTBOOK.FinalHomeScore);

        public string TotalSetAWay => GetScore(SPORTBOOK.FinalAwayScore);

        /// <summary>
        /// Get home and away set.
        /// Only used for some sport type such as tennis, badminton....
        /// </summary>
        /// <param name="index">The index of home or away array.</param>
        /// <param name="isHome">True if you want to get value of home set.</param>
        /// <returns>The string value.</returns>
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

                GenerateHomeAwaySetValueFromBetStatus(5);
            }

            return isHome ? HomeSet[index] : AwaySet[index];
        }

        protected override void GenerateHomeAwaySetValueFromBetStatus(int maxSetNumber)
        {
            if (HasResult)
            {
                string[] betStatusList = GetScoreInfoFromBetStatus.Split(',');

                for (int j = 0; j < betStatusList.Length && j < 5; j++)
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

        /// <summary>
        /// Create total point value.
        /// </summary>
        /// <param name="isHome">If true, return home value.</param>
        /// <returns>The total point value.</returns>
        private string CreateTotalPointValue(bool isHome)
        {
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
    }
}