namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Globalization;
    using DTO;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Sportsbook.Utilities;

    /// <summary>
    /// NewOtherViewModel, based on OtherViewModel
    /// Intend to update score value based on event status
    /// </summary>
    public sealed class KabaddiViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public KabaddiViewModel(
         Sportsbook sportbook,
         ResultSetting setting,
         IRefDataHelper refDataHelper = null)
         : base(sportbook, setting, refDataHelper)
        {
            sportbook.MatchCode = RefDataHelper.GetBetRadarMatchCodeFormat(sportbook.MatchCode, GetSupportedLanguage);
        }

        public string HTHomeScore => GetScore(SPORTBOOK.HtHomeScore, isFinalScore: false);

        public string HTAwayScore => GetScore(SPORTBOOK.HtAwayScore, isFinalScore: false);

        public string FinalAwayScore => GetScore(SPORTBOOK.FinalAwayScore);

        public string FinalHomeScore => GetScore(SPORTBOOK.FinalHomeScore);

        private string GetScore(int? score, bool isFinalScore = true)
        {
            var betStatus = SPORTBOOK.BetStatus.TrimEnd(';');
            var isRefund = betStatus.Equals(ConstantValue.Refund, StringComparison.InvariantCultureIgnoreCase);
            var isRefundFullTime = betStatus.Equals(ConstantValue.RefundFT, StringComparison.InvariantCultureIgnoreCase);

            if (isRefund || (isRefundFullTime && isFinalScore))
            {
                return LangResource.Refund;
            }

            return score.HasValue ?
                score.Value.ToString(CultureInfo.InvariantCulture) :
                ConstantValue.Hyphen;
        }
    }
}