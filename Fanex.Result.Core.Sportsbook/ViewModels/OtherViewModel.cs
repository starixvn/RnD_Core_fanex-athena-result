namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using Base;
    using Base.ViewModels;
    using DTO;
    using Resources;
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

        private static string GetMatchInfo(string matchCode)
            => " (" + string.Format(
                matchCode,
                SportsbookResx.Season,
                SportsbookResx.NationsCup,
                SportsbookResx.MatchDay,
                SportsbookResx.MatchDay1GroupAB,
                SportsbookResx.MatchDay1GroupCD,
                SportsbookResx.MatchDay1GroupEF,
                SportsbookResx.MatchDay2GroupAB,
                SportsbookResx.MatchDay2GroupCD,
                SportsbookResx.MatchDay2GroupEF,
                SportsbookResx.MatchDay3GroupAB,
                SportsbookResx.MatchDay3GroupCD,
                SportsbookResx.MatchDay3GroupEF,
                SportsbookResx.Round161,
                SportsbookResx.Round162,
                SportsbookResx.Quarterfinal,
                SportsbookResx.Semifinal1,
                SportsbookResx.Semifinal2,
                SportsbookResx.Final) + ")";
    }
}