namespace Fanex.Result.Core.Base.Utilities
{
    public interface IRefDataHelper
    {
        string GetBetTypeNameById(string betTypeId, string lang);

        string GetBetTypeNameById(string betTypeId, string lang, string resourceId);

        string GetTeamName(string teamId, string lang, string defaultValue);

        string GetTeamName(int teamId, string lang, string defaultValue);

        string GetLeagueName(int leagueId, string lang, string defaultValue);

        string GetSportName(int sportType, string lang, string defaultValue);

        string GetResourceDataByRefIdAndResourceId(string refId, string resourceId, string lang, string defaultValue);

        string GetFastMarketResult(string resultId, string lang);

        string GetFastMarket225Result(string resultId, string lang);

        string GetBetRadarMatchCodeFormat(string matchCode, string lang);

        string GetHorseTeamName(string codeId, string lang, string defaultValue);
    }
}