namespace MvcExample.Models.ResultData
{
    using Fanex.Result.Core.GTCasino;
    using ResultServices;

    public class GTCasinoResultData
    {
        public string GetPopupResult(int betType, string languageCode)
        {
            var allbetResultService = new GTCasinoResultBuilder();
            var transDesc = CasinoResultService.GetGTCasinoTransDesc(betType);
            var matchCode = "123456";

            return allbetResultService.Render(transDesc, matchCode, betType.ToString(), languageCode);
        }
    }
}