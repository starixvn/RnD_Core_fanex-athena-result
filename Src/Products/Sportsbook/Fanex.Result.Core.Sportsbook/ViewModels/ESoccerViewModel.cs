namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using Base.Extension;
    using Base.ViewModels;
    using BaseDetail;
    using ESoccer;
    using DataDomain;
    using DTO;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;

    public class ESoccerViewModel : SportsbookBaseViewModel, IResultViewModel
    {
         public ESoccerViewModel(
            Sportsbook sportbook,
            ResultSetting setting)
            : this(sportbook, setting, null, null)
        {
        }

        public ESoccerViewModel(
            Sportsbook sportbook,
            ResultSetting setting,
            IResultDetail<ESoccerGameDetailItem> resultDetail = null)
            : this(sportbook, setting, resultDetail, null)
        {
        }

        public ESoccerViewModel(
          Sportsbook sportbook,
          ResultSetting setting,
          IResultDetail<ESoccerGameDetailItem> resultDetail,
          IRefDataHelper refDataHelper)
            : base(sportbook, setting, refDataHelper)
        {
            ResultDetailItems = (resultDetail ?? new ESoccerResultDetail(sportbook, setting, null, null))
                .GetDetailItems();
        }


        public override string EventStatus
        {
            get
            {
                if (IsEventRunningOrEnded())
                {
                    return LangResource.Running;
                }

                return IsEventRefunded() ? LangResource.Refund : LangResource.Completed;
            }
        }

        public override string KickOffTime => GetFormatedKickOffTime(SPORTBOOK.EventDate);

        public override int LeagueId => SPORTBOOK.LeagueId;

        public override string LeagueName
         => RefDataHelper.GetLeague3rdName(LeagueId, GetSupportedLanguage, LeagueId.ToString());

        public override string HomeTeam
        => RefDataHelper.GetTeam3rdName(SPORTBOOK.HomeId, GetSupportedLanguage, SPORTBOOK.HomeId.ToString());

        public override string AwayTeam 
        => RefDataHelper.GetTeam3rdName(SPORTBOOK.AwayId, GetSupportedLanguage, SPORTBOOK.AwayId.ToString());

        public string HTHomeScore => GetTotalScore(true, false);

        public string HTAwayScore => GetTotalScore(false, false);

        public string FinalHomeScore => GetTotalScore(true, true);

        public string FinalAwayScore => GetTotalScore(false, true);

        public SafeList<ESoccerGameDetailItem> ResultDetailItems { get; }

        private string GetTotalScore(bool isHome, bool isFinal)
        {
            var resourceId = isFinal ? BasketBallResourceID.FullTime : BasketBallResourceID.FirstHalf;
            return GetResultFromGameDetails(resourceId, isHome);
        }

        public virtual string GetResultFromGameDetails(string resourceId, bool isHome)
        {
            var result = ResultDetailItems.FirstOrDefault(g => g.ResourceID == resourceId);

            var score = isHome ? result?.HomeValue : result?.AwayValue;

            return string.IsNullOrEmpty(score) ? ConstantValue.Hyphen : score;
        }

        private bool IsEventRunningOrEnded()
        {
            return ConstantValue.Running.Equals(SPORTBOOK.EventStatus, StringComparison.OrdinalIgnoreCase) ||
            ConstantValue.Ended.Equals(SPORTBOOK.EventStatus, StringComparison.OrdinalIgnoreCase);
        }

        private bool IsEventRefunded()
        => ConstantValue.Refund.Equals(SPORTBOOK.BetStatus, StringComparison.OrdinalIgnoreCase);
    }
}