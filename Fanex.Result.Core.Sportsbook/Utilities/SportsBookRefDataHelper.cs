namespace Fanex.Result.Core.Sportsbook.Utilities
{
    using Base.Utilities;
    using RemotingRef;

    public class SportsbookRefDataHelper : RefDataHelper, ISportsbookRefDataHelper
    {
        public string GetFastMarket225Result(string resultId, string lang)
            => RefData.GetFastMarkets225Result(resultId, lang);

        public string GetFastMarketResult(string resultId, string lang)
            => RefData.GetFastMarketsResult(resultId, lang);
    }
}