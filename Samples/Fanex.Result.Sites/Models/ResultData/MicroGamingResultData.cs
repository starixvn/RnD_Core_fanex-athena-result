namespace MvcExample.Models.ResultData
{
    using Fanex.Result.Core.MicroGaming;
    using Fanex.Result.Core.MicroGaming.DTO;

    public class MicroGamingResultData
    {
        public string GetPopupResult()
        {
            return ParserService.Instance.Parser(new MicroGaming
            {
                CustId = 18543926,
                SiteName = "spondemo",
                GameId = 74,
            });
        }
    }
}