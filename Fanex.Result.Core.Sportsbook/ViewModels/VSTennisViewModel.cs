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

        public string HomePoint
        {
            get
            {
                return GetScore(SPORTBOOK.FinalHomeScore);
            }
        }

        public string AwayPoint
        {
            get
            {
                return GetScore(SPORTBOOK.FinalAwayScore);
            }
        }
    }
}