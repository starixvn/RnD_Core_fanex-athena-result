namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using Base;
    using Base.DataDomain;
    using Base.ViewModels;
    using DTO;

    public sealed class HockeyViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public HockeyViewModel(Sportsbook sportbook, ResultSetting setting)
            : base(sportbook, setting)
        {
        }

        public string OTHome
        {
            get
            {
                var temp = this.GetHomeAwaySet(3, true);
                return Convert.ToInt32(temp) > 0 ? temp : ConstantValue.Hyphen;
            }
        }

        public string OTAway
        {
            get
            {
                var temp = this.GetHomeAwaySet(3, false);
                return Convert.ToInt32(temp) > 0 ? temp : ConstantValue.Hyphen;
            }
        }

        public string FinalHomeScore
        {
            get
            {
                return GetScore(SPORTBOOK.FinalHomeScore);
            }
        }

        public string FinalAwayScore
        {
            get
            {
                return GetScore(SPORTBOOK.FinalAwayScore);
            }
        }

        /// <summary>
        /// Get home and away set.
        /// Only used for some sport type such as tennis, badminton....
        /// </summary>
        /// <param name="index">The index of home or away array.</param>
        /// <param name="isHome">True if you want to get value of home set.</param>
        /// <returns>The string value.</returns>
        public override string GetHomeAwaySet(int index, bool isHome)
        {
            if (index > 3)
            {
                return ConstantValue.Hyphen;
            }

            if (this.AwaySet == null || this.HomeSet == null)
            {
                HomeSet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Zero };
                AwaySet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Zero };
                if (this.HasResult)
                {
                    string[] betStatusList = this.GetCoreInfoFromBetStatus.Split(',');
                    for (int j = 0; j < betStatusList.Length && j < 4; j++)
                    {
                        var values = betStatusList[j].Trim().Split('-');
                        HomeSet[j] = values[0].Trim();
                        AwaySet[j] = values[1].Trim();
                    }
                }
            }

            return isHome ? HomeSet[index] : AwaySet[index];
        }
    }
}