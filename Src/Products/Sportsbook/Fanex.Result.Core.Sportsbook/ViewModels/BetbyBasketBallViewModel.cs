namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Base;
    using Base.DataDomain;
    using Base.Extension;
    using Base.ViewModels;
    using BaseDetail;
    using BasketBallDetail;
    using DataDomain;
    using DTO;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;

    public class BetbyBasketballViewModel : SportsbookBaseViewModel, IResultViewModel
    {
         public BetbyBasketballViewModel(
            Sportsbook sportbook,
            ResultSetting setting)
            : this(sportbook, setting, null, null)
        {
        }

        public BetbyBasketballViewModel(
            Sportsbook sportbook,
            ResultSetting setting,
            IResultDetail<BasketBallGameDetailItem> resultDetail = null)
            : this(sportbook, setting, resultDetail, null)
        {
        }

        public BetbyBasketballViewModel(
          Sportsbook sportbook,
          ResultSetting setting,
          IResultDetail<BasketBallGameDetailItem> resultDetail,
          IRefDataHelper refDataHelper)
            : base(sportbook, setting, refDataHelper)
        {
            ResultDetailItems = (resultDetail ?? new BasketBallResultDetail(sportbook, setting, null, null))
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

        public string OTHome => GetOvertimeSet(true);

        public string OTAway => GetOvertimeSet(false);

        public string HTHomeScore => GetTotalScore(true, false);

        public string HTAwayScore => GetTotalScore(false, false);

        public string FinalHomeScore => GetTotalScore(true, true);

        public string FinalAwayScore => GetTotalScore(false, true);

        public string SecondHaftHomeScore => GetSecondHaftScore(isHome: true);

        public string SecondHaftAwayScore => GetSecondHaftScore(isHome: false);

        public SafeList<BasketBallGameDetailItem> ResultDetailItems { get; }

        public override string GetHomeAwaySet(int index, bool isHome)
        {
            const int maxIndex = 4;

            if (index > maxIndex)
            {
                return ConstantValue.Hyphen;
            }

            return GetResultFromGameDetails($"Q{index}", isHome);
        }

        private string GetOvertimeSet(bool isHome)
        => GetResultFromGameDetails(BasketBallResourceID.OverTime, isHome);

        private string GetTotalScore(bool isHome, bool isFinal)
        {
            var resourceId = isFinal ? BasketBallResourceID.FullTime : BasketBallResourceID.FirstHalf;
            return GetResultFromGameDetails(resourceId, isHome);
        }

        private string GetSecondHaftScore(bool isHome)
        => GetResultFromGameDetails(BasketBallResourceID.SecondHalf, isHome);

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