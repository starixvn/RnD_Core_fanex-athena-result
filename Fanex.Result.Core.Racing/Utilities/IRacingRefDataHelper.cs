namespace Fanex.Result.Core.Racing.Utilities
{
    using Base.Utilities;

    public interface IRacingRefDataHelper : IRefDataHelper
    {
        string GetHorseTeamName(string codeId, string lang, string defaultValue);
    }
}