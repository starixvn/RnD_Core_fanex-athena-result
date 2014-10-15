// -----------------------------------------------------------------------
// <copyright file="HockeyViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels
{
    using System;
    using System.Globalization;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.DTO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class HockeyViewModel : SportbookBaseViewModel, IResultViewModel
    {
        public HockeyViewModel(Sportsbook sportbook, ResultSetting setting, LangResources resource)
            : base(sportbook, setting, resource)
        {
            this.GetHomeAwaySet(0, true);
        }

        public string OTHome
        {
            get
            {
                var temp = this.GetPHomeAway(3, true);
                return Convert.ToInt32(temp) > 0 ? temp : StringResources.HYPHEN;
            }
        }

        public string OTAway
        {
            get
            {
                var temp = this.GetPHomeAway(3, false);
                return Convert.ToInt32(temp) > 0 ? temp : StringResources.HYPHEN;
            }
        }

        public string FinalHomeScore
        {
            get
            {
                return this.SPORTBOOK.FinalHomeScore.HasValue
                    ? this.SPORTBOOK.FinalHomeScore.Value.ToString(CultureInfo.InvariantCulture)
                    : StringResources.HYPHEN;
            }
        }

        public string FinalAwayScore
        {
            get
            {
                return this.SPORTBOOK.FinalAwayScore.HasValue
                    ? this.SPORTBOOK.FinalAwayScore.Value.ToString(CultureInfo.InvariantCulture)
                    : StringResources.HYPHEN;
            }
        }

        public string GetHomeAwaySet(int index, bool isHome)
        {
            return GetPHomeAway(index, isHome);
        }

        /// <summary>
        /// Get home and away set.
        /// Only used for some sport type such as tennis, badminton....
        /// </summary>
        /// <param name="index">The index of home or away array.</param>
        /// <param name="isHome">True if you want to get value of home set.</param>
        /// <returns>The string value.</returns>
        private string GetPHomeAway(int index, bool isHome)
        {
            if (index > 3)
            {
                return StringResources.HYPHEN;
            }

            if (this.AwaySet == null || this.HomeSet == null)
            {
                HomeSet = new[] { StringResources.HYPHEN, StringResources.HYPHEN, StringResources.HYPHEN, StringResources.ZERO };
                AwaySet = new[] { StringResources.HYPHEN, StringResources.HYPHEN, StringResources.HYPHEN, StringResources.ZERO };
                if (this.SPORTBOOK.BetStatus.IndexOf(",", StringComparison.Ordinal) >= 0)
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