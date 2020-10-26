namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using Base;
    using Base.ViewModels;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using Utilities;

    public class VSTennisViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public VSTennisViewModel(Sportsbook sportbook, ResultSetting setting)
            : this(sportbook, setting, null)
        {
        }

        public VSTennisViewModel(Sportsbook sportbook, ResultSetting setting, IRefDataHelper refDataHelper)
           : base(sportbook, setting, refDataHelper)
        {
        }

        public string HomePoint => GetScore(SPORTBOOK.FinalHomeScore);

        public string AwayPoint => GetScore(SPORTBOOK.FinalAwayScore);

        public string TotalPoint => GetScore(SPORTBOOK.FinalHomeScore + SPORTBOOK.FinalAwayScore);

        public string CorrectScore => RefDataHelper.GetResourceDataByRefIdAndResourceId("BetChoice", $"bet1235_{SPORTBOOK.BetStatus}", GetSupportedLanguage, string.Empty);
    }
}