namespace Fanex.Result.Core.RemotingRefV1
{
    using System.Globalization;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Utilities;
    using RemotingRef;

    public class RefDataHelperV1 : IRefDataHelper
    {
        protected string LanguageCode { get; set; } = ConstantValue.EnglishLanguageCode;

        public string GetBetTypeNameById(string betTypeId, string lang, string resourceId)
            => RefData.GetBetTypeName(betTypeId, lang, string.Empty, resourceId);

        public string GetTeamName(string teamId, string lang, string defaultValue)
         => RefData.GetTeamName(teamId, lang, defaultValue);

        public string GetTeamName(int teamId, string lang, string defaultValue)
            => RefData.GetTeamName(teamId.ToString(CultureInfo.InvariantCulture), lang, defaultValue);

        public string GetLeagueName(int leagueId, string lang, string defaultValue)
            => RefData.GetLeagueName(leagueId.ToString(CultureInfo.InvariantCulture), lang, defaultValue);

        public string GetSportName(int sportType, string lang, string defaultValue)
            => RefData.GetSportName(sportType.ToString(CultureInfo.InvariantCulture), lang, defaultValue);

        public string GetBetTypeNameById(string betTypeId, string lang)
            => RefData.GetBetTypeName(betTypeId, lang);

        public string GetResourceDataByRefIdAndResourceId(string refId, string resourceId, string lang, string defaultValue)
            => RefData.GetResourceDataByRefIdAndResourceId(refId, resourceId, lang, defaultValue);

        public string GetFastMarket225Result(string resultId, string lang)
            => RefData.GetFastMarkets225Result(resultId, lang);

        public string GetFastMarketResult(string resultId, string lang)
            => RefData.GetFastMarketsResult(resultId, lang);

        public string GetBetRadarMatchCodeFormat(string matchCode, string lang)
            => RefData.GetBetRadarMatchCodeFormat(matchCode, lang);

        public string GetHorseTeamName(string codeId, string lang, string defaultValue)
            => RefData.GetHorseTeamName(codeId, lang, defaultValue);
    }
}