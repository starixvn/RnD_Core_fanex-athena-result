namespace Fanex.Result.Core.Sportsbook.Utilities
{
    using System.Globalization;
    using Base.Utilities;
    using RemotingRef;

    public class SportsbookRefDataHelper : RefDataHelper, ISportsbookRefDataHelper
    {
        public string GetFastMarketResult(string resultId, string lang)
            => RefData.GetFastMarketsResult(resultId, lang);
    }
}