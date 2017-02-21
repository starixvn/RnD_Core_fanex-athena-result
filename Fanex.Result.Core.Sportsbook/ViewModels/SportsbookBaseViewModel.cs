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
            Setting = setting;
        }

        public string[] HomeSet { get; set; }

        public string[] AwaySet { get; set; }

        #region common properties

        public virtual bool VisibleDetailedLink { get; } = false;

        public override string KickOffTime => GetFormatedKickOffTime(SPORTBOOK.KickOffTime);

        public override int LeagueId => SPORTBOOK.LeagueId;

        public override string LeagueName => GetLeagueNameByLanguage(SPORTBOOK.LeagueId);

        public override string EventStatus
            => MapEventStatus.MapEventStatusString(
                    SPORTBOOK.EventStatus,
                    SPORTBOOK.IsPending,
                    VisibleInternalEventStatus);

        public new virtual bool VisibleMatchResult => true;

        public virtual bool VisibleGoalRecord { get; } = false;

        public new virtual bool VisibleGameDetail => false;

        #region onlyForSettlementSite

        public string MatchCode => SPORTBOOK.MatchCode;

        public string MatchId => SPORTBOOK.MatchId.ToString(CultureInfo.InvariantCulture);

        #endregion onlyForSettlementSite

        public string HomeTeam => GetSportbookHomeTeam(SPORTBOOK.HomeId, SPORTBOOK.IsNeutral);

        public string AwayTeam => GetSportbookAwayTeam(SPORTBOOK.AwayId);

        public override bool IsPending => SPORTBOOK.IsPending;

        public virtual string HomeWin => GetWonTeam(true);

        public virtual string AwayWin => GetWonTeam(false);

        protected Sportsbook SPORTBOOK { get; set; }

        protected ResultSetting Setting { get; }

        protected bool HasResult
            => !string.IsNullOrEmpty(SPORTBOOK.BetStatus) && SPORTBOOK.BetStatus.IndexOf(",", StringComparison.Ordinal) >= 0;

        #endregion common properties

        #region Utilities

        protected bool HighlightWinnerTeam { get; } = false;

        protected string GetCoreInfoFromBetStatus
        {
            get
            {
                if (!string.IsNullOrEmpty(SPORTBOOK.BetStatus))
                {
                    var refundIndex = SPORTBOOK.BetStatus.IndexOf(";", StringComparison.Ordinal);
                    if (refundIndex >= 0)
                    {
                        var startCoreIndex = refundIndex + 1;
                        if (SPORTBOOK.BetStatus.Length > startCoreIndex)
                        {
                            return SPORTBOOK.BetStatus.Substring(startCoreIndex);
                        }
                    }
                }

                return SPORTBOOK.BetStatus != null ? SPORTBOOK.BetStatus.Trim() : string.Empty;
            }
        }

        public virtual string GetHomeAwaySet(int index, bool isHome) => ConstantValue.Hyphen;

        protected virtual string GetWonTeam(bool isHome)
        {
            if (HighlightWinnerTeam && SPORTBOOK.FinalHomeScore.HasValue && SPORTBOOK.FinalAwayScore.HasValue)
            {
                var temp = SPORTBOOK.FinalHomeScore.Value - SPORTBOOK.FinalAwayScore.Value;

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
            => RefData.GetTeamName(
                homeId.ToString(CultureInfo.InvariantCulture),
                GetSupportedLanguage,
                string.Empty) + (isNeutral ? LangResource.IsNeutral : string.Empty);

        /// <summary>
        /// Get sport-book away team.
        /// </summary>
        /// <param name="awayId">The away id.</param>
        /// <returns>The away team.</returns>
        private string GetSportbookAwayTeam(int awayId)
            => RefData.GetTeamName(awayId.ToString(CultureInfo.InvariantCulture), GetSupportedLanguage, string.Empty);

        protected virtual string GetScore(int? score)
            => score.HasValue ? score.Value.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen;

        #endregion Utilities
    }
}