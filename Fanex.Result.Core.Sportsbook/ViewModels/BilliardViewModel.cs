namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System.Globalization;
    using Base;
    using Base.DataDomain;
    using Base.ViewModels;
    using DTO;

    public sealed class BilliardViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public BilliardViewModel(Sportsbook sportbook, ResultSetting setting)
            : base(sportbook, setting)
        {
        }

        public string FinalAwayScore
        {
            get
            {
                return GetScore(SPORTBOOK.FinalAwayScore);
            }
        }

        public string FinalHomeScore
        {
            get
            {
                return GetScore(SPORTBOOK.FinalHomeScore);
            }
        }
    }
}