namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using Base;
    using Base.ViewModels;
    using DTO;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;

    public sealed class BandyViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public BandyViewModel(Sportsbook sportbook, ResultSetting setting)
            : this(sportbook, setting, null)
        {
        }

        public BandyViewModel(Sportsbook sportbook, ResultSetting setting, IRefDataHelper refDataHelper)
            : base(sportbook, setting, refDataHelper)
        {
        }

        public string HTHomeScore => CalculateScore(SPORTBOOK.HtHomeScore, isFinalScore: false);

        public string HTAwayScore => CalculateScore(SPORTBOOK.HtAwayScore, isFinalScore: false);

        public string FinalAwayScore => CalculateScore(SPORTBOOK.FinalAwayScore, isFinalScore: true);

        public string FinalHomeScore => CalculateScore(SPORTBOOK.FinalHomeScore, isFinalScore: true);

        private string CalculateScore(int? score, bool isFinalScore)
        {
            if (EventStatus.Equals(LangResource.Refund, StringComparison.OrdinalIgnoreCase)
                && (!SPORTBOOK.IsHTCal || (SPORTBOOK.IsHTCal && isFinalScore)))
            {
                return LangResource.Refund;
            }

            if (EventStatus.Equals(LangResource.Refundht, StringComparison.OrdinalIgnoreCase) && !isFinalScore)
            {
                return LangResource.Refund;
            }

            if (ScoreIsNotCalculated(isFinalScore))
            {
                return ConstantValue.Hyphen;
            }

            return GetScore(score);
        }

        private bool ScoreIsNotCalculated(bool isFinalScore)
        {
            if (!isFinalScore && !SPORTBOOK.IsHTCal)
            {
                return true;
            }

            if (isFinalScore && !SPORTBOOK.Calculate)
            {
                return true;
            }

            return false;
        }
    }
}