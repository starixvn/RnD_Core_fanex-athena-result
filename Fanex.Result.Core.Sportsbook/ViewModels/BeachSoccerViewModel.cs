namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using Base;
    using Base.ViewModels;
    using DTO;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Resources;
    using Utilities;

    public sealed class BeachSoccerViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public BeachSoccerViewModel(Sportsbook sportbook, ResultSetting setting)
            : this(sportbook, setting, null)
        {
        }

        public BeachSoccerViewModel(Sportsbook sportbook, ResultSetting setting, ISportsbookRefDataHelper refDataHelper)
            : base(sportbook, setting, refDataHelper)
        {
        }

        public string FinalAwayScore => GetScore(SPORTBOOK.FinalAwayScore);

        public string FinalHomeScore => GetScore(SPORTBOOK.FinalHomeScore);

        protected override string GetScore(int? score)
        {
            var betStatus = SPORTBOOK.BetStatus.TrimEnd(';');
            var isRefund = betStatus.Equals(ConstantValue.Refund, StringComparison.InvariantCultureIgnoreCase);
            var isRefundFullTime = betStatus.Equals(ConstantValue.RefundFT, StringComparison.InvariantCultureIgnoreCase);

            if (isRefund || isRefundFullTime)
            {
                return LangResource.Refund;
            }

            return base.GetScore(score);
        }
    }
}