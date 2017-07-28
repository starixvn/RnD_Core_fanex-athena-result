namespace Fanex.Result.Core.Base.Utilities
{
    public interface IRefDataHelper
    {
        string GetBetTypeNameById(string betTypeId, string lang, string resourceId);

        string GetTeamName(int homeId, string lang, string defaultValue);

        string GetLeagueNameByLanguage(int leagueId, string lang, string defaultValue);
    }
}