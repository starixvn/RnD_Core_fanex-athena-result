namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System.Collections.Generic;
    using Base;
    using Base.ViewModels;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using Utilities;

    public sealed class OtherViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public OtherViewModel(
            Sportsbook sportbook,
            ResultSetting setting,
            IRefDataHelper refDataHelper = null)
            : base(sportbook, setting, refDataHelper)
        {
            sportbook.MatchCode = RefDataHelper.GetBetRadarMatchCodeFormat(sportbook.MatchCode, GetSupportedLanguage);
        }

        public string HTHomeScore => GetScore(SPORTBOOK.HtHomeScore);

        public string HTAwayScore => GetScore(SPORTBOOK.HtAwayScore);

        public string FinalAwayScore => GetScore(SPORTBOOK.FinalAwayScore);

        public string FinalHomeScore => GetScore(SPORTBOOK.FinalHomeScore);

        public string MatchInfo => (IsVirtualSports(Setting.SportType) ? $" ({SPORTBOOK.MatchCode})" : string.Empty);

        private static bool IsVirtualSports(SportType sportType)
        {
            var virtualSoccer = new List<SportType>
            {
                SportType.VirtualSoccerLeague,
                SportType.VirtualSoccerNation,
                SportType.VirtualSoccerWorldCup,
                SportType.VirtualSoccerAsianCup,
                SportType.VirtualSoccerEnglishPremier,
                SportType.VirtualSoccerChampionsCup,
                SportType.VirtualSoccerEuroCup
            };

            return virtualSoccer.Contains(sportType);
        }
    }
}