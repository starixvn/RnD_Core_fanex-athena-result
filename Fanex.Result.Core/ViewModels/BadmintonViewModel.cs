// -----------------------------------------------------------------------
// <copyright file="BadmintonViewModel.cs" company="Nexcel">
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
    public sealed class BadmintonViewModel : SportbookBaseViewModel
    {
        private int _totalPointHome;
        private int _totalPointAway;

        public BadmintonViewModel(Sportbook sportbook, string languageCode, LangResources resource)
            : base(sportbook, languageCode, resource)
        {
            this.GetHomeAwaySet(0, true);
        }

        public string TotalPointHome
        {
            get
            {
                return this._totalPointHome > 0 ? this._totalPointHome.ToString(CultureInfo.InvariantCulture) : string.Empty;
            }
        }

        public string TotalPointAway
        {
            get
            {
                return this._totalPointAway > 0 ? this._totalPointAway.ToString(CultureInfo.InvariantCulture) : string.Empty;
            }
        }

        public string TotalSetHome
        {
            get
            {
                return this.SPORTBOOK.FinalHomeScore.HasValue ? this.SPORTBOOK.FinalHomeScore.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
            }
        }

        public string TotalSetAWay
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
        public string GetHomeAwaySet(int index, bool isHome)
        {
            if (index > 4)
            {
                return StringResources.EMPTYSCORE;
            }

            if (this.HomeSet == null || this.AwaySet == null)
            {
                this._totalPointAway = 0;
                this._totalPointHome = 0;
                this.HomeSet = new[] { "-", "-", "-", "-", "-" };
                this.AwaySet = new[] { "-", "-", "-", "-", "-" };
                if (this.SPORTBOOK.BetStatus.IndexOf(",", StringComparison.Ordinal) >= 0)
                {
                    string[] betStatusList = this.SPORTBOOK.BetStatus.Split(',');
                    for (int j = 0; j < betStatusList.Length && j < 5; j++)
                    {
                        var values = betStatusList[j].Trim().Split('-');

                        this.HomeSet[j] = values[0].Trim();
                        this.AwaySet[j] = values[1].Trim();

                        this._totalPointAway += Convert.ToInt32(this.AwaySet[j]);
                        this._totalPointHome += Convert.ToInt32(this.HomeSet[j]);
                    }
                }
            }

            return isHome ? HomeSet[index] : AwaySet[index];
        }
    }
}