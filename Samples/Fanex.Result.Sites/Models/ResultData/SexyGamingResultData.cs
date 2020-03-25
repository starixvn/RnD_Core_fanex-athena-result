namespace MvcExample.Models.ResultData
{
    using Fanex.Result.Core.SexyGaming;
    using ResultServices;

    public class SexyGamingResultData
    {
        public string GetPopupResult(int betType, string languageCode)
        {
            var service = new SexyGamingResultBuilder();
            var transDesc = CasinoResultService.GetSexyGamingTransDesc(betType);
            var matchCode = "123456";

            return service.Render(transDesc, matchCode, betType.ToString(), languageCode);
        }
    }
}