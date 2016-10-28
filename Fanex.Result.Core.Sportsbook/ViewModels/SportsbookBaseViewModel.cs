namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Globalization;
    using Base;
    using Base.DataDomain;
    using Base.Resources;
    using Base.Utilities;
    using Base.ViewModels;
    using DTO;
    using RemotingRef;

    public class SportsbookBaseViewModel : BaseViewModel, IResultViewModel
    {
        public SportsbookBaseViewModel(Sportsbook sportbook, ResultSetting setting)
            : base(setting)
        {
            this.SPORTBOOK = sportbook;
            this.LanguageCode = setting.LanguageCode;
            this.Sport = setting.SportType;
            VisibleDetailedLink = setting.VisibleDetailedLink;
            HighlightWinnerTeam = setting.HighlightWinnerTeam;
            VisibleGoalRecord = setting.VisibleGoalRecord;
        }

        public string[] HomeSet { get; set; }

        public string[] AwaySet { get; set; }

        #region common properties

        public virtual bool VisibleDetailedLink { get; } = false;

        public override string KickOffTime
        {
            get
            {
                return this.GetFormatedKickOffTime(this.SPORTBOOK.KickOffTime);
            }
        }

        public override int LeagueId
        {
            get
            {
                return SPORTBOOK.LeagueId;
            }
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
                    this.VisibleInternalEventStatus);
            }
        }

        public new virtual bool VisibleMatchResult
        {
            get { return true; }
        }

        public virtual bool VisibleGoalRecord { get; } = false;

        public new virtual bool VisibleGameDetail
        {
            get { return false; }
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
                return this.SPORTBOOK.IsPending;
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

        protected Sportsbook SPORTBOOK { get; set; }

        protected bool HasResult
        {
            get
            {
                return !string.IsNullOrEmpty(this.SPORTBOOK.BetStatus) && this.SPORTBOOK.BetStatus.IndexOf(",", StringComparison.Ordinal) >= 0;
            }
        }

        #endregion common properties

        #region Utilities

        protected bool HighlightWinnerTeam { get; } = false;

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
            return ConstantValue.Hyphen;
        }

        protected virtual string GetWonTeam(bool isHome)
        {
            if (HighlightWinnerTeam && this.SPORTBOOK.FinalHomeScore.HasValue && this.SPORTBOOK.FinalAwayScore.HasValue)
            {
                var temp = this.SPORTBOOK.FinalHomeScore.Value - this.SPORTBOOK.FinalAwayScore.Value;

                return (isHome && temp > 0) || (!isHome && temp < 0) ? ConstantValue.WinFormat : string.Empty;
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
                + (isNeutral ? LangResource.IsNeutral : string.Empty);
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

        protected virtual string GetScore(int? score)
        {
            return score.HasValue ? score.Value.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen;
        }

        #endregion Utilities
    }
}