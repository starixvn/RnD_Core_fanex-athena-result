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
    using Utilities;

    public class SportsbookBaseViewModel : BaseViewModel, IResultViewModel
    {
        public SportsbookBaseViewModel(
            Sportsbook sportbook,
            ResultSetting setting,
            IRefDataHelper refDataHelper = null)
            : base(setting, refDataHelper)
        {
            SPORTBOOK = sportbook;
            LanguageCode = setting.LanguageCode;
            Sport = setting.SportType;
            VisibleDetailedLink = setting.VisibleDetailedLink;
            HighlightWinnerTeam = setting.HighlightWinnerTeam;
            VisibleGoalRecord = setting.VisibleGoalRecord;
            Setting = setting;
        }

        public string[] HomeSet { get; set; }

        public string[] AwaySet { get; set; }

        #region common properties

        public virtual bool VisibleDetailedLink { get; }

        public override string KickOffTime => GetFormatedKickOffTime(SPORTBOOK.KickOffTime);

        public override int LeagueId => SPORTBOOK.LeagueId;

        public override string LeagueName
        => Setting.SupportHideSabaText ?
        RefDataHelper.GetLeagueName(SPORTBOOK.LeagueId, SPORTBOOK.LeagueGroupId, GetSupportedLanguage, string.Empty) :
        RefDataHelper.GetLeagueName(SPORTBOOK.LeagueId, GetSupportedLanguage, string.Empty);

        public override string EventStatus
            => MapEventStatus.MapEventStatusString(
                    SPORTBOOK.SettlementStatus,
                    SPORTBOOK.EventStatus,
                    SPORTBOOK.BetStatus);

        public new virtual bool VisibleMatchResult => true;

        public virtual bool VisibleGoalRecord { get; }

        public new virtual bool VisibleGameDetail => false;

        #region onlyForSettlementSite

        public string MatchCode => SPORTBOOK.MatchCode;

        public string MatchId => SPORTBOOK.MatchId.ToString(CultureInfo.InvariantCulture);

        #endregion onlyForSettlementSite

        public virtual string HomeTeam => GetSportbookHomeTeam(SPORTBOOK.HomeId, SPORTBOOK.IsNeutral);

        public string AwayTeam => GetSportbookAwayTeam(SPORTBOOK.AwayId);

        public virtual string HomeWin => GetWonTeam(true);

        public virtual string AwayWin => GetWonTeam(false);

        protected Sportsbook SPORTBOOK { get; set; }

        protected ResultSetting Setting { get; }

        protected bool HasResult
            => !string.IsNullOrEmpty(SPORTBOOK.BetStatus) && SPORTBOOK.BetStatus.IndexOf(",", StringComparison.Ordinal) >= 0;

        #endregion common properties

        #region Utilities

        protected bool HighlightWinnerTeam { get; }

        protected string GetScoreInfoFromBetStatus
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

        protected internal virtual string GetWonTeam(bool isHome)
        {
            if (HighlightWinnerTeam && SPORTBOOK.FinalHomeScore.HasValue && SPORTBOOK.FinalAwayScore.HasValue)
            {
                var score = SPORTBOOK.FinalHomeScore.Value - SPORTBOOK.FinalAwayScore.Value;
                var homeWin = isHome && score > 0;
                var awayWin = !isHome && score < 0;

                return homeWin || awayWin ? ConstantValue.WinFormat : string.Empty;
            }

            return string.Empty;
        }

        /// <summary>
        /// Get sport-book home team.
        /// </summary>
        /// <param name="homeId">The home id.</param>
        /// <param name="isNeutral">Is neutral.</param>
        /// <returns>The home team.</returns>
        protected string GetSportbookHomeTeam(int homeId, bool isNeutral)
            => base.RefDataHelper.GetTeamName(homeId, GetSupportedLanguage, string.Empty)
                + (isNeutral ? LangResource.IsNeutral : string.Empty);

        /// <summary>
        /// Get sport-book away team.
        /// </summary>
        /// <param name="awayId">The away id.</param>
        /// <returns>The away team.</returns>
        protected string GetSportbookAwayTeam(int awayId)
            => base.RefDataHelper.GetTeamName(awayId, GetSupportedLanguage, string.Empty);

        protected virtual string GetScore(int? score)
            => score.HasValue ? score.Value.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen;

        protected virtual void GenerateHomeAwaySetValueFromBetStatus(int maxSetNumber)
        {
            if (HasResult)
            {
                string[] betStatusList = GetScoreInfoFromBetStatus.Split(',');

                for (int j = 0; j < betStatusList.Length && j < maxSetNumber; j++)
                {
                    var values = betStatusList[j].Trim().Split('-');

                    if (values.Length > 1)
                    {
                        HomeSet[j] = string.IsNullOrEmpty(values[0].Trim()) ? ConstantValue.Hyphen : values[0].Trim();
                        AwaySet[j] = string.IsNullOrEmpty(values[1].Trim()) ? ConstantValue.Hyphen : values[1].Trim();
                    }
                }
            }
        }

        #endregion Utilities
    }
}