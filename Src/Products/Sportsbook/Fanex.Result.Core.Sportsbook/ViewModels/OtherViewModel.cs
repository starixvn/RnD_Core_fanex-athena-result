namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using Base;
    using Base.ViewModels;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using System.Collections.Generic;

    public sealed class OtherViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public OtherViewModel(
            Sportsbook sportbook,
            ResultSetting setting,
            IRefDataHelper refDataHelper = null)
            : base(sportbook, setting, refDataHelper)
        {
            sportbook.MatchCode = SportTypeOfVirutalSport3.Contains(Setting.SportType) ?
                GetMatchInfoOfVS3(sportbook.MatchCode) :
                RefDataHelper.GetBetRadarMatchCodeFormat(sportbook.MatchCode, GetSupportedLanguage);
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

            return virtualSoccer.Contains(sportType) || SportTypeOfVirutalSport3.Contains(sportType);
        }

        private static readonly List<SportType> SportTypeOfVirutalSport3 = new List<SportType> { SportType.VirtualFootballCup, SportType.VirtualFootballLeague };

        private string GetMatchInfoOfVS3(string matchCode)
        {
            var str1 = matchCode;
            var chArray = new[] { '{' };
            foreach (string str2 in str1.Split(chArray))
            {
                var length = str2.IndexOf('}');
                if (length != -1)
                {
                    var ResourceId = str2.Substring(0, length);
                    var refIdAndResourceId = RefDataHelper.GetResourceDataByRefIdAndResourceId("LVS_MatchCode", ResourceId, GetSupportedLanguage, string.Empty);
                    matchCode = matchCode.Replace("{" + ResourceId + "}", refIdAndResourceId);
                }
            }

            return matchCode;
        }
    }
}