namespace Fanex.Result.Core.RemotingRef
{
    using System.Globalization;
    using Fanex.RemotingRef;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Utilities;

    public class RefDataHelper : IRefDataHelper
    {
        private readonly IRemotingRefService remotingRefService;

        public RefDataHelper(IRemotingRefService remotingRefService = null)
        {
            this.remotingRefService = remotingRefService ?? new RemotingRefService();
        }

        protected string LanguageCode { get; set; } = ConstantValue.EnglishLanguageCode;

        public string GetBetTypeNameById(string betTypeId, string lang, string resourceId)
            => remotingRefService.GetBetTypeName(betTypeId, lang, string.Empty, resourceId);

        public string GetTeamName(string teamId, string lang, string defaultValue)
         => remotingRefService.GetTeamName(teamId, lang, defaultValue);

        public string GetTeamName(int teamId, string lang, string defaultValue)
            => remotingRefService.GetTeamName(teamId.ToString(CultureInfo.InvariantCulture), lang, defaultValue);

        public string GetLeagueName(int leagueId, string lang, string defaultValue)
            => remotingRefService.GetLeagueName(leagueId.ToString(CultureInfo.InvariantCulture), lang, defaultValue);

        public string GetSportName(int sportType, string lang, string defaultValue)
            => remotingRefService.GetSportName(sportType.ToString(CultureInfo.InvariantCulture), lang, defaultValue);

        public string GetBetTypeNameById(string betTypeId, string lang)
            => remotingRefService.GetBetTypeName(betTypeId, lang);

        public string GetResourceDataByRefIdAndResourceId(string refId, string resourceId, string lang, string defaultValue)
            => remotingRefService.GetResourceDataByRefIdAndResourceId(refId, resourceId, lang, defaultValue);

        public string GetFastMarket225Result(string resultId, string lang)
            => remotingRefService.GetFastMarkets225Result(resultId, lang);

        public string GetFastMarketResult(string resultId, string lang)
            => remotingRefService.GetFastMarketsResult(resultId, lang);

        public string GetBetRadarMatchCodeFormat(string matchCode, string lang)
            => remotingRefService.GetBetRadarMatchCodeFormat(matchCode, lang);

        public string GetHorseTeamName(string codeId, string lang, string defaultValue)
            => remotingRefService.GetHorseTeamName(codeId, lang, defaultValue);

        public string GetRouletteBetItem(string itemId, string languageCode, string defaultValue)
            => remotingRefService.GetRouletteBetItem(itemId, languageCode, defaultValue);

        public string GetCasinoBetItem(string itemId, string languageCode, string defaultValue)
            => remotingRefService.GetCasinoBetItem(itemId, languageCode, defaultValue);

        public string GetBetBuilderSelectionName(int selectionType, string languageCode, string defaultValue)
        {
            var names = remotingRefService.GetBetBuilderSelectionNames(new int[] { selectionType }, languageCode);

            return names != null && names.Length == 1 ? names[0] : defaultValue;
        }
    }
}