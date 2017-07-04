namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using Base;
    using Base.ViewModels;
    using DTO;

    public class VSTennisViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public VSTennisViewModel(Sportsbook sportbook, ResultSetting setting)
            : base(sportbook, setting)
        {
        }

        public string HomePoint => GetScore(SPORTBOOK.FinalHomeScore);

        public string AwayPoint => GetScore(SPORTBOOK.FinalAwayScore);
    }
}