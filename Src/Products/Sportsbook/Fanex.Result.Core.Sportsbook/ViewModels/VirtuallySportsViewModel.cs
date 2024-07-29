namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using Base;
    using Base.ViewModels;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Base.Resources;
    using System;

    public class VirtuallySportsViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public VirtuallySportsViewModel(Sportsbook sportbook, ResultSetting setting)
            : this(sportbook, setting, null)
        {
        }

        public VirtuallySportsViewModel(Sportsbook sportbook, ResultSetting setting, IRefDataHelper refDataHelper)
           : base(sportbook, setting, refDataHelper)
        {
        }

        public override string LeagueName
         => RefDataHelper.GetLeague3rdName(SPORTBOOK.LeagueId, GetSupportedLanguage, string.Empty);

        public override string HomeTeam
        => RefDataHelper.GetTeam3rdName(SPORTBOOK.HomeId, GetSupportedLanguage, SPORTBOOK.HomeId.ToString());
        public override string AwayTeam
       => RefDataHelper.GetTeam3rdName(SPORTBOOK.AwayId, GetSupportedLanguage, SPORTBOOK.AwayId.ToString());

        public string HTHomeScore => CalculateScore(SPORTBOOK.HtHomeScore, false);

        public string FinalHomeScore => CalculateScore(SPORTBOOK.FinalHomeScore, true);

        public string HTAwayScore => CalculateScore(SPORTBOOK.HtAwayScore, false);

        public string FinalAwayScore => CalculateScore(SPORTBOOK.FinalAwayScore, true);

        public string MatchInfo => $" ({RefDataHelper.GetVirtualSportMatchCodeFormat(SPORTBOOK.MatchCode, "VYVS_MatchCode", GetSupportedLanguage)})";


        public override string EventStatus
        {
            get
            {
                var isCompletedEventStatus = MapEventStatus.IsCompleted(SPORTBOOK.EventStatus);
                var isRefundStatus = SPORTBOOK.BetStatus.IndexOf("Refund", StringComparison.OrdinalIgnoreCase) >= 0;
                if (isCompletedEventStatus)
                {
                    return isRefundStatus ? LangResource.Refund : LangResource.Completed;
                }

                return LangResource.Running;
            }
        }

        private string CalculateScore(int? score, bool isFinalScore)
        {
            if (EventStatus.Equals(LangResource.Refund, StringComparison.OrdinalIgnoreCase))
            {
                if (isFinalScore || (!isFinalScore && SPORTBOOK.BetStatus.Equals("Refund")))
                {
                    return LangResource.Refund;
                }
            }

            return GetScore(score);
        }
    }
}