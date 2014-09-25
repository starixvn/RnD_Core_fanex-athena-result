// -----------------------------------------------------------------------
// <copyright file="BasketBallViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels
{
    using System;
    using System.Globalization;
    using Fanex.Result.Core.DTO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class BasketBallViewModel : SportbookBaseViewModel
    {
        public BasketBallViewModel(Sportbook sportbook, SportType sport, string languageCode, LangResources resource)
            : base(sportbook, sport, languageCode, resource)
        {
            this.GetQHomeAway(0, true);
        }

        public string QTHome
        {
            get
            {
                var temp = this.GetQHomeAway(4, true);
                return Convert.ToInt32(temp) > 0 ? temp : string.Empty;
            }
        }

        public string QTAway
        {
            get
            {
                var temp = this.GetQHomeAway(4, false);
                return Convert.ToInt32(temp) > 0 ? temp : string.Empty;
            }
        }

        public string HTHomeScore
        {
            get
            {
                return this.SPORTBOOK.HtHomeScore.HasValue ? this.SPORTBOOK.HtHomeScore.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
            }
        }

        public string HTAwayScore
        {
            get
            {
                return this.SPORTBOOK.HtAwayScore.HasValue ? this.SPORTBOOK.HtAwayScore.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
            }
        }

        public string FinalHomeScore
        {
            get
            {
                return this.SPORTBOOK.FinalHomeScore.HasValue ? this.SPORTBOOK.FinalHomeScore.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
            }
        }

        public string FinalAwayScore
        {
            get
            {
                return this.SPORTBOOK.FinalAwayScore.HasValue ? this.SPORTBOOK.FinalAwayScore.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
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
                return StringResources.EMPTYSCORE;
            }

            if (this.HomeSet == null || this.AwaySet == null)
            {
                this.HomeSet = new[] { "-", "-", "-", "-", "0" };
                this.AwaySet = new[] { "-", "-", "-", "-", "0" };
                if (this.SPORTBOOK.EventStatus != StringResources.REFUNDFT)
                {
                    if (this.SPORTBOOK.BetStatus.IndexOf(",", StringComparison.Ordinal) >= 0)
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