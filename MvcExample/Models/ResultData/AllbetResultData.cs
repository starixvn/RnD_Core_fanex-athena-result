namespace MvcExample.Models.ResultData
{
    using Fanex.Result.Core.AllBetCasino;
    using ResultServices;

    public class AllbetResultData
    {
        public string GetPopupResult(int betType, string languageCode)
        {
            var allbetResultService = new AllbetResultBuilder();
            var transDesc = new CasinoResultService().GetAllBetTransDesc(betType);
            var matchCode = "123456";

            return allbetResultService.Render(transDesc, matchCode, betType.ToString(), languageCode);
        }
    }
}