// -----------------------------------------------------------------------
// <copyright file="HockeyViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Globalization;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.ViewModels;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class HockeyViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public HockeyViewModel(DTO.Sportsbook sportbook, ResultSetting setting, LangResources resource)
            : base(sportbook, setting, resource)
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
                return this.SPORTBOOK.FinalHomeScore.HasValue
                    ? this.SPORTBOOK.FinalHomeScore.Value.ToString(CultureInfo.InvariantCulture)
                    : ConstantValue.Hyphen;
            }
        }

        public string FinalAwayScore
        {
            get
            {
                return this.SPORTBOOK.FinalAwayScore.HasValue
                    ? this.SPORTBOOK.FinalAwayScore.Value.ToString(CultureInfo.InvariantCulture)
                    : ConstantValue.Hyphen;
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