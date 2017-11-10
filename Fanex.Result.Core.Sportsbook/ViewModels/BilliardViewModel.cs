namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using Base;
    using Base.ViewModels;
    using DTO;
    using Utilities;

    public sealed class BilliardViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public BilliardViewModel(Sportsbook sportbook, ResultSetting setting)
            : this(sportbook, setting, null)
        {
        }

        public BilliardViewModel(Sportsbook sportbook, ResultSetting setting, ISportsbookRefDataHelper refDataHelper)
            : base(sportbook, setting, refDataHelper)
        {
        }

        public string FinalAwayScore => GetScore(SPORTBOOK.FinalAwayScore);

        public string FinalHomeScore => GetScore(SPORTBOOK.FinalHomeScore);
    }
}