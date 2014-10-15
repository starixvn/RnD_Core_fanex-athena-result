// -----------------------------------------------------------------------
// <copyright file="BasketBallViewModel.cs" company="Nexcel">
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
    public sealed class BasketBallViewModel : SportbookBaseViewModel, IResultViewModel
    {
        public BasketBallViewModel(Sportsbook sportbook, ResultSetting setting, LangResources resource)
            : base(sportbook, setting, resource)
        {
            this.GetQHomeAway(0, true);
        }

        public string QTHome
        {
            get
            {
                var temp = this.GetQHomeAway(4, true);
                return Convert.ToInt32(temp) > 0 ? temp : StringResources.HYPHEN;
            }
        }

        public string QTAway
        {
            get
            {
                var temp = this.GetQHomeAway(4, false);
                return Convert.ToInt32(temp) > 0 ? temp : StringResources.HYPHEN;
            }
        }

        public string HTHomeScore
        {
            get
            {
                return this.SPORTBOOK.HtHomeScore.HasValue ? this.SPORTBOOK.HtHomeScore.Value.ToString(CultureInfo.InvariantCulture) : StringResources.HYPHEN;
            }
        }

        public string HTAwayScore
        {
            get
            {
                return this.SPORTBOOK.HtAwayScore.HasValue ? this.SPORTBOOK.HtAwayScore.Value.ToString(CultureInfo.InvariantCulture) : StringResources.HYPHEN;
            }
        }

        public string FinalHomeScore
        {
            get
            {
                return this.SPORTBOOK.FinalHomeScore.HasValue ? this.SPORTBOOK.FinalHomeScore.Value.ToString(CultureInfo.InvariantCulture) : StringResources.HYPHEN;
            }
        }

        public string FinalAwayScore
        {
            get
            {
                return this.SPORTBOOK.FinalAwayScore.HasValue ? this.SPORTBOOK.FinalAwayScore.Value.ToString(CultureInfo.InvariantCulture) : StringResources.HYPHEN;
            }
        }

        /// <summary>
        /// Get home and away set.
        /// Only used for some sport type such as tennis, badminton....
        /// </summary>
        /// <param name="index">The index of home or away array.</param>
        /// <param name="isHome">True if you want to get value of home set.</param>
        /// <returns>The string value.</returns>
        public string GetQHomeAway(int index, bool isHome)
        {
            if (index > 4)
            {
                return StringResources.HYPHEN;
            }

            if (this.HomeSet == null || this.AwaySet == null)
            {
                this.HomeSet = new[] { StringResources.HYPHEN, StringResources.HYPHEN, StringResources.HYPHEN, StringResources.HYPHEN, StringResources.ZERO };
                this.AwaySet = new[] { StringResources.HYPHEN, StringResources.HYPHEN, StringResources.HYPHEN, StringResources.HYPHEN, StringResources.ZERO };
                if (this.SPORTBOOK.EventStatus != StringResources.REFUNDFT)
                {
                    if (this.HasResult)
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
            }

            return isHome ? this.HomeSet[index] : this.AwaySet[index];
        }
    }
}