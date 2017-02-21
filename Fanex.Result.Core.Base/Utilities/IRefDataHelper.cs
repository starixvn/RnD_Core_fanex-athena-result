namespace Fanex.Result.Core.Base.Utilities
{
    public interface IRefDataHelper
    {
        string GetBetTypeNameById(string bettypeId, string lang, string resourceId);

        string GetTeamName(int homeId, string lang, string defaultValue);
    }
}