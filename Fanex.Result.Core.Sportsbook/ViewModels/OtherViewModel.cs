namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using Base;
    using Base.ViewModels;
    using DTO;
    using Utilities;

    public sealed class OtherViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public OtherViewModel(
            Sportsbook sportbook,
            ResultSetting setting,
            ISportsbookRefDataHelper refDataHelper = null)
            : base(sportbook, setting, refDataHelper)
        {
        }

        public string HTHomeScore => GetScore(SPORTBOOK.HtHomeScore);

        public string HTAwayScore => GetScore(SPORTBOOK.HtAwayScore);

        public string FinalAwayScore => GetScore(SPORTBOOK.FinalAwayScore);

        public string FinalHomeScore => GetScore(SPORTBOOK.FinalHomeScore);

        public string MatchInfo => (IsVirtualSports(Setting.SportType) ? GetMatchInfo(SPORTBOOK.MatchCode) : string.Empty);

        private static bool IsVirtualSports(SportType sportType)
            => sportType == SportType.VirtualSoccerLeague || sportType == SportType.VirtualSoccerNation;

        private string GetMatchInfo(string matchCode)
        => $" ({SportsbookRefDataHelper.GetBetRadarMatchCodeFormat(matchCode, GetSupportedLanguage)})";
    }
}