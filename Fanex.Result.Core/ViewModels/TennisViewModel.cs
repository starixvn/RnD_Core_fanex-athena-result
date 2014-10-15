// -----------------------------------------------------------------------
// <copyright file="TennisViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.DTO;

    /// <summary>
    /// Tennis view model class.
    /// </summary>
    public sealed class TennisViewModel : SportbookBaseViewModel, IResultViewModel
    {
        private int _totalGameHome;
        private int _totalGameAway;

        public TennisViewModel(Sportsbook sportbook, ResultSetting setting, LangResources resources)
            : base(sportbook, setting, resources)
        {
            this.GetHomeAwaySet(0, true);
        }

        public string TotalGameHome
        {
            get { return this._totalGameHome > 0 ? this._totalGameHome.ToString(CultureInfo.InvariantCulture) : StringResources.HYPHEN; }
        }

        public string TotalGameAway
        {
            get { return this._totalGameAway > 0 ? this._totalGameAway.ToString(CultureInfo.InvariantCulture) : StringResources.HYPHEN; }
        }

        public string TotalSetHome
        {
            get
            {
                return this.SPORTBOOK.FinalHomeScore.HasValue ?
                    this.SPORTBOOK.FinalHomeScore.Value.ToString(CultureInfo.InvariantCulture)
                    : string.Empty;
            }
        }

        public string TotalSetAWay
        {
            get
            {
                return this.SPORTBOOK.FinalAwayScore.HasValue
                    ? this.SPORTBOOK.FinalAwayScore.Value.ToString(CultureInfo.InvariantCulture)
                    : string.Empty;
            }
        }

        #region match results detail

        public string DetailName
        {
            get { return Lang.Set + " " + this.SPORTBOOK.RefNo + " " + Lang.Score; }
        }

        public IList<TennisDetailViewModel> SetDetail
        {
            get
            {
                if (!string.IsNullOrEmpty(this.SPORTBOOK.ResultValue) && this.SPORTBOOK.ResultValue.IndexOf("*", StringComparison.Ordinal) >= 0)
                {
                    string[] betStatusList = this.SPORTBOOK.ResultValue.Replace("*", string.Empty).Split(' ');
                    var items = new List<TennisDetailViewModel>();
                    for (int j = 1; j < betStatusList.Length; j++)
                    {
                        items.Add(new TennisDetailViewModel
                        {
                            SetId = j,
                            SetScore1 = betStatusList[j].Trim().Split('-')[0].Trim(),
                            SetScore2 = betStatusList[j].Trim().Split('-')[1].Trim()
                        });
                    }

                    return items;
                }

                return null;
            }
        }

        #endregion match results detail

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
                return StringResources.HYPHEN;
            }

            if (this.HomeSet == null || this.AwaySet == null)
            {
                this._totalGameAway = 0;
                this._totalGameHome = 0;
                this.HomeSet = new[] { "-", "-", "-", "-", "-" };
                this.AwaySet = new[] { "-", "-", "-", "-", "-" };
                if (this.SPORTBOOK.BetStatus.IndexOf(",", StringComparison.Ordinal) >= 0)
                {
                    string[] betStatusList = this.GetCoreInfoFromBetStatus.Split(',');
                    for (int j = 0; j < betStatusList.Length && j < 5; j++)
                    {
                        var values = betStatusList[j].Trim().Split('-');
                        this.HomeSet[j] = values[0].Trim();
                        this.AwaySet[j] = values[1].Trim();
                        this._totalGameAway += Convert.ToInt32(this.AwaySet[j]);
                        this._totalGameHome += Convert.ToInt32(this.HomeSet[j]);
                    }
                }
            }

            return isHome ? HomeSet[index] : AwaySet[index];
        }
    }
}