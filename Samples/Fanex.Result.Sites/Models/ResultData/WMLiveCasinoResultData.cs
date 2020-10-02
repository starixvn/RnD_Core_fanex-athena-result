namespace MvcExample.Models.ResultData
{
    using Fanex.Result.Core.WM;
    using ResultServices;

    public class WMLiveCasinoResultData
    {
        public string GetPopupResult(int betType, string languageCode)
        {
            var allbetResultService = new WMResultBuilder();
            var transDesc = CasinoResultService.GetWMTransDesc(betType);
            var matchCode = "123456";

            return allbetResultService.Render(transDesc, matchCode, betType, languageCode);
        }
    }
}