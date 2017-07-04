namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using Base;
    using Base.ViewModels;
    using DTO;

    public sealed class BilliardViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public BilliardViewModel(Sportsbook sportbook, ResultSetting setting)
            : base(sportbook, setting)
        {
        }

        public string FinalAwayScore => GetScore(SPORTBOOK.FinalAwayScore);

        public string FinalHomeScore => GetScore(SPORTBOOK.FinalHomeScore);
    }
}