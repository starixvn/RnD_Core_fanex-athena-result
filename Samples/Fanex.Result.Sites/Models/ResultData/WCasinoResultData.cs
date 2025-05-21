using Fanex.Result.Core.WCasino;
using MvcExample.Models.ResultServices;

namespace MvcExample.Models.ResultData
{
	public class WCasinoResultData
	{
        public string GetPopupResult(int betType, string languageCode)
        {
            var allbetResultService = new WCasinoResultBuilder("/Content/Fanex.Result/WM/images/");
            var transDesc = CasinoResultService.GetWCasinoTransDesc(betType);
            var matchCode = "123456";

            return allbetResultService.Render(transDesc, matchCode, betType, languageCode);
        }
    }
}