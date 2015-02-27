// -----------------------------------------------------------------------
// <copyright file="SportsbookBaseViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels
{
    using System;
    using System.Globalization;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Utilities;
    using RemotingRef;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SportsbookBaseViewModel : BaseViewModel, IResultViewModel
    {
        private string[] _homeSet;

        private string[] _awaySet;

        private Sportsbook _sportbook;

        private bool _visibleDetailedLink = false;

        private bool _highlightWinnerTeam = false;

        public SportsbookBaseViewModel(Sportsbook sportbook, ResultSetting setting, LangResources resource)
            : base(setting, resource)
        {
            this.SPORTBOOK = sportbook;
            this.LanguageCode = setting.LanguageCode;
            this.Sport = setting.SportType;
            this._visibleDetailedLink = setting.VisibleDetailedLink;
            this._highlightWinnerTeam = setting.HighlightWinnerTeam;
        }

        public string[] HomeSet
        {
            get { return this._homeSet; }
            set { this._homeSet = value; }
        }

        public string[] AwaySet
        {
            get { return this._awaySet; }
            set { this._awaySet = value; }
        }

        #region common properties

        public virtual bool VisibleDetailedLink
        {
            get { return this._visibleDetailedLink; }
        }

        public override string KickOffTime
        {
            get
            {
                return this.GetFormatedKickOffTime(this.SPORTBOOK.KickOffTime);
            }
        }

        public override int LeagueId
        {
            get { return this.SPORTBOOK.LeagueId; }
        }

        public override string LeagueName
        {
            get
            {
                return this.GetLeagueNameByLanguage(this.SPORTBOOK.LeagueId);
            }
        }

        public override string EventStatus
        {
            get
            {
                return MapEventStatus.MapEventStatusString(
                    this.SPORTBOOK.EventStatus,
                    this.SPORTBOOK.IsPending,
                    this.SPORTBOOK.IsHTCal,
                    this.SPORTBOOK.Calculate,
                    this.Lang);
            }
        }

        #region onlyForSettlementSite

        public string MatchCode
        {
            get
            {
                return this.SPORTBOOK.MatchCode;
            }
        }

        public string MatchId
        {
            get
            {
                return this.SPORTBOOK.MatchId.ToString(CultureInfo.InvariantCulture);
            }
        }

        #endregion onlyForSettlementSite

        public string HomeTeam
        {
            get
            {
                return this.GetSportbookHomeTeam(this.SPORTBOOK.HomeId, this.SPORTBOOK.IsNeutral);
            }
        }

        public string AwayTeam
        {
            get
            {
                return this.GetSportbookAwayTeam(this.SPORTBOOK.AwayId);
            }
        }

        public override bool IsPending
        {
            get
            {
                return this._sportbook.IsPending;
            }
        }

        public virtual string HomeWin
        {
            get
            {
                return GetWonTeam(true);
            }
        }

        public virtual string AwayWin
        {
            get
            {
                return GetWonTeam(false);
            }
        }

        protected Sportsbook SPORTBOOK
        {
            get { return this._sportbook; }
            set { this._sportbook = value; }
        }

        protected bool HasResult
        {
            get
            {
                return !string.IsNullOrEmpty(this.SPORTBOOK.BetStatus) && this.SPORTBOOK.BetStatus.IndexOf(",", StringComparison.Ordinal) >= 0;
            }
        }

        #endregion common properties

        #region Utilities

        protected bool HighlightWinnerTeam
        {
            get { return this._highlightWinnerTeam; }
        }

        protected string GetCoreInfoFromBetStatus
        {
            get
            {
                if (!string.IsNullOrEmpty(this.SPORTBOOK.BetStatus))
                {
                    var refundIndex = this.SPORTBOOK.BetStatus.IndexOf(";", StringComparison.Ordinal);
                    if (refundIndex >= 0)
                    {
                        var startCoreIndex = refundIndex + 1;
                        if (this.SPORTBOOK.BetStatus.Length > startCoreIndex)
                        {
                            return this.SPORTBOOK.BetStatus.Substring(startCoreIndex);
                        }
                    }
                }

                return this.SPORTBOOK.BetStatus != null ? this.SPORTBOOK.BetStatus.Trim() : string.Empty;
            }
        }

        public virtual string GetHomeAwaySet(int index, bool isHome)
        {
            return StringResources.HYPHEN;
        }

        protected virtual string GetWonTeam(bool isHome)
        {
            if (this._highlightWinnerTeam && this.SPORTBOOK.FinalHomeScore.HasValue && this.SPORTBOOK.FinalAwayScore.HasValue)
            {
                var temp = this.SPORTBOOK.FinalHomeScore.Value - this.SPORTBOOK.FinalAwayScore.Value;

                return (isHome && temp > 0) || (!isHome && temp < 0) ? ResourceConfig.WINFORMAT : string.Empty;
            }

            return string.Empty;
        }

        /// <summary>
        /// Get sport-book home team.
        /// </summary>
        /// <param name="homeId">The home id.</param>
        /// <param name="isNeutral">Is neutral.</param>
        /// <returns>The home team.</returns>
        private string GetSportbookHomeTeam(int homeId, bool isNeutral)
        {
            return RefData.GetTeamName(homeId.ToString(CultureInfo.InvariantCulture), this.GetSupportedLanguage, string.Empty)
                + (isNeutral ? Lang.IsNeutral : string.Empty);
        }

        /// <summary>
        /// Get sport-book away team.
        /// </summary>
        /// <param name="awayId">The away id.</param>
        /// <returns>The away team.</returns>
        private string GetSportbookAwayTeam(int awayId)
        {
            return RefData.GetTeamName(awayId.ToString(CultureInfo.InvariantCulture), this.GetSupportedLanguage, string.Empty);
        }

        #endregion Utilities
    }
}