namespace Fanex.Result.Core.Base.Utilities
{
    using System.Globalization;
    using DataDomain;
    using RemotingRef;

    public class RefDataHelper : IRefDataHelper
    {
        protected string LanguageCode { get; set; } = ConstantValue.EnglishLanguageCode;

        public string GetBetTypeNameById(string betTypeId, string lang, string resourceId)
        {
            return RefData.GetBetTypeName(betTypeId, lang, string.Empty, resourceId);
        }

        public string GetTeamName(int homeId, string lang, string defaultValue)
        {
            return RefData.GetTeamName(homeId.ToString(CultureInfo.InvariantCulture), lang, defaultValue);
        }

        public string GetLeagueNameByLanguage(int leagueId, string lang, string defaultValue)
        {
            return RefData.GetLeagueName(leagueId.ToString(CultureInfo.InvariantCulture), lang, defaultValue);
        }
    }
}