// -----------------------------------------------------------------------
// <copyright file="SportbookBaseViewModel.cs" company="Nexcel">
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
    public class SportbookBaseViewModel : BaseViewModel, IResultViewModel
    {
        private string[] _homeSet;

        private string[] _awaySet;

        private Sportsbook _sportbook;

        public SportbookBaseViewModel(Sportsbook sportbook, ResultSetting setting, LangResources resource)
            : base(setting, resource)
        {
            this.SPORTBOOK = sportbook;
            this.LanguageCode = setting.LanguageCode;
            this.Sport = setting.SportType;
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
                return this.GetSportbookHomeTeam(
                    this.SPORTBOOK.HomeId,
                    this.SPORTBOOK.IsNeutral,
                    this.SPORTBOOK.FinalHomeScore,
                    this.SPORTBOOK.FinalAwayScore);
            }
        }

        public string AwayTeam
        {
            get
            {
                return this.GetSportbookAwayTeam(
                    this.SPORTBOOK.AwayId,
                    this.SPORTBOOK.FinalHomeScore,
                    this.SPORTBOOK.FinalAwayScore);
            }
        }

        protected Sportsbook SPORTBOOK
        {
            get { return this._sportbook; }
            set { this._sportbook = value; }
        }

        #endregion common properties

        #region Utilities

        protected string GetCoreInfoFromBetStatus
        {
            get
            {
                var realStatus = this.SPORTBOOK.BetStatus;
                var refundIndex = this.SPORTBOOK.BetStatus.IndexOf(";", StringComparison.Ordinal);
                if (refundIndex >= 0)
                {
                    var startCoreIndex = refundIndex + 1;
                    if (this.SPORTBOOK.BetStatus.Length > startCoreIndex)
                    {
                        realStatus = this.SPORTBOOK.BetStatus.Substring(startCoreIndex);
                    }
                }

                return realStatus;
            }
        }

        /// <summary>
        /// Get sport-book home team.
        /// </summary>
        /// <param name="homeId">The home id.</param>
        /// <param name="isNeutral">Is neutral.</param>
        /// <param name="finalHomeScore">The final home score.</param>
        /// <param name="finalAwayScore">The final away score.</param>
        /// <returns>The home team.</returns>
        private string GetSportbookHomeTeam(int homeId, bool isNeutral, int? finalHomeScore, int? finalAwayScore)
        {
            var homeTeam = RefData.GetTeamName(homeId.ToString(CultureInfo.InvariantCulture), this.GetSupportedLanguage, string.Empty)
                + (isNeutral ? Lang.IsNeutral : string.Empty);
            var homeScore = finalHomeScore.HasValue ? finalHomeScore.Value : 0;
            var awayScore = finalAwayScore.HasValue ? finalAwayScore.Value : 0;
            if (homeScore > awayScore)
            {
                return string.Format(StringResources.BOLDTEAMPATTERN, homeTeam);
            }

            return homeTeam;
        }

        /// <summary>
        /// Get sport-book away team.
        /// </summary>
        /// <param name="awayId">The away id.</param>
        /// <param name="finalHomeScore">The final home score.</param>
        /// <param name="finalAwayScore">The final away score.</param>
        /// <returns>The away team.</returns>
        private string GetSportbookAwayTeam(int awayId, int? finalHomeScore, int? finalAwayScore)
        {
            var awayTeam = RefData.GetTeamName(awayId.ToString(CultureInfo.InvariantCulture), this.GetSupportedLanguage, string.Empty);

            var homeScore = finalHomeScore.HasValue ? finalHomeScore.Value : 0;
            var awayScore = finalAwayScore.HasValue ? finalAwayScore.Value : 0;
            if (homeScore < awayScore)
            {
                return string.Format(StringResources.BOLDTEAMPATTERN, awayTeam);
            }

            return awayTeam;
        }

        #endregion Utilities
    }
}