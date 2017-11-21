namespace Fanex.Result.Core.Racing.Utilities
{
    using Base.Utilities;
    using RemotingRef;

    public class RacingRefDataHelper : RefDataHelper, IRacingRefDataHelper
    {
        public string GetHorseTeamName(string codeId, string lang, string defaultValue)
            => RefData.GetHorseTeamName(codeId, lang, defaultValue);
    }
}