namespace MvcExample.Models.ResultData
{
    using Fanex.Result.Core.BBin;
    using ResultServices;

    public class BBinResultData
    {
        public string GetPopupResult(int betType, string languageCode)
        {
            var service = new BBinResultBuilder();
            var transDesc = CasinoResultService.GetBBinTransDesc(betType);
            var matchCode = "123456";

            return service.Render(transDesc, matchCode, languageCode);
        }
    }
}