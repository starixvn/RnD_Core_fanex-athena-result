namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using Base;
    using Base.ViewModels;
    using DTO;

    public sealed class OtherViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public OtherViewModel(Sportsbook sportbook, ResultSetting setting)
            : base(sportbook, setting)
        {
        }

        public string HTHomeScore => GetScore(SPORTBOOK.HtHomeScore);

        public string HTAwayScore => GetScore(SPORTBOOK.HtAwayScore);

        public string FinalAwayScore => GetScore(SPORTBOOK.FinalAwayScore);

        public string FinalHomeScore => GetScore(SPORTBOOK.FinalHomeScore);
    }
}