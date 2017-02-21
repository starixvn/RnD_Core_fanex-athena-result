namespace Fanex.Result.Core.Sportsbook.Utilities
{
    using Base.Utilities;
    using RemotingRef;

    public class SportsbookRefDataHelper : RefDataHelper, ISportsbookRefDataHelper
    {
        public string GetFastMarketResult(string resultId, string lang)
        {
            return RefData.GetFastMarketsResult(resultId, lang);
        }
    }
}