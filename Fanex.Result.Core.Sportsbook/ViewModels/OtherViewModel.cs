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

        public string HTHomeScore
        {
            get
            {
                return GetScore(SPORTBOOK.HtHomeScore);
            }
        }

        public string HTAwayScore
        {
            get
            {
                return GetScore(SPORTBOOK.HtAwayScore);
            }
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