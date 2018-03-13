namespace Fanex.Result.Core.Sportsbook.Utilities
{
    using Base.Utilities;

    public interface ISportsbookRefDataHelper : IRefDataHelper
    {
        string GetFastMarketResult(string resultId, string lang);

        string GetFastMarket225Result(string resultId, string lang);

        string GetBetRadarMatchCodeFormat(string matchCode, string lang);
    }
}