namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using Base;
    using Base.ViewModels;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;

    public class VSCockFightingViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public VSCockFightingViewModel(Sportsbook sportbook, ResultSetting setting)
            : this(sportbook, setting, null)
        {
        }

        public VSCockFightingViewModel(Sportsbook sportbook, ResultSetting setting, IRefDataHelper refDataHelper)
           : base(sportbook, setting, refDataHelper)
        {
        }

        public string Winner => SPORTBOOK.FinalHomeScore.HasValue && SPORTBOOK.FinalHomeScore.Value == 1 ?
        RefDataHelper.GetTeamName(SPORTBOOK.HomeId.ToString(), GetSupportedLanguage, SPORTBOOK.HomeId.ToString()) :
        RefDataHelper.GetTeamName(SPORTBOOK.AwayId.ToString(), GetSupportedLanguage, SPORTBOOK.AwayId.ToString());

        public string MatchInfo => $" ({RefDataHelper.GetVirtualSportMatchCodeFormat(SPORTBOOK.MatchCode, "EGVS_MatchCode", GetSupportedLanguage)})";
    }
}