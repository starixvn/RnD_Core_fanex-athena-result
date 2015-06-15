// -----------------------------------------------------------------------
// <copyright file="BadmintonViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Globalization;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Base.DataDomain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class BadmintonViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private int _totalPointHome;
        private int _totalPointAway;

        public BadmintonViewModel(DTO.Sportsbook sportbook, ResultSetting setting, LangResources resource)
            : base(sportbook, setting, resource)
        {
        }

        public string TotalPointHome
        {
            get
            {
                return CreateTotalPointValue(true);
            }
        }

        public string TotalPointAway
        {
            get
            {
                return CreateTotalPointValue(false);
            }
        }

        public string TotalSetHome
        {
            get
            {
                return this.SPORTBOOK.FinalHomeScore.HasValue ? this.SPORTBOOK.FinalHomeScore.Value.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen;
            }
        }

        public string TotalSetAWay
        {
            get
            {
                return this.SPORTBOOK.FinalAwayScore.HasValue ? this.SPORTBOOK.FinalAwayScore.Value.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen;
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
            if (index > 4)
            {
                return ConstantValue.Hyphen;
            }

            if (this.HomeSet == null || this.AwaySet == null)
            {
                this._totalPointAway = 0;
                this._totalPointHome = 0;
                this.HomeSet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen };
                this.AwaySet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen };
                if (this.HasResult)
                {
                    string[] betStatusList = this.GetCoreInfoFromBetStatus.Split(',');
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

        /// <summary>
        /// Create total point value.
        /// </summary>
        /// <param name="isHome">If true, return home value.</param>
        /// <returns>The total point value.</returns>
        private string CreateTotalPointValue(bool isHome)
        {
            if (this.HomeSet == null || this.AwaySet == null)
            {
                this.GetHomeAwaySet(0, true);
            }

            var totalPoint = this._totalPointHome + this._totalPointAway;
            if (totalPoint > 0)
            {
                return isHome ? this._totalPointHome.ToString(CultureInfo.InvariantCulture) : this._totalPointAway.ToString(CultureInfo.InvariantCulture);
            }

            return ConstantValue.Hyphen;
        }
    }
}