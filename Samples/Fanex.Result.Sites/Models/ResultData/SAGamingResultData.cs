namespace MvcExample.Models.ResultData
{
    using Fanex.Result.Core.SAGaming;
    using ResultServices;

    public class SAGamingResultData
    {
        public string GetPopupResult(int betType, string languageCode)
        {
            var allbetResultService = new SAGamingResultBuilder();
            var transDesc = CasinoResultService.GetSAGamingTransDesc(betType);
            var matchCode = "123456";

            return allbetResultService.Render(transDesc, matchCode, betType.ToString(), languageCode);
        }
    }
}