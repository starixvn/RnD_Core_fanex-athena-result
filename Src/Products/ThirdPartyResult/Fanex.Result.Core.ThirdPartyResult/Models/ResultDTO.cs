namespace Fanex.Result.Core.ThirdParty.Models
{
    public class ResultRequest
    {
        public ResultRequest()
        {
            GMT = "0";
            Language = "en-US";
        }

        public ResultRequest(int custId, string tranId3rd, string gameId3rd, int currencyId, string siteId, string language = "en-US")
            : this()
        {
            CustId = custId;
            TranId3rd = tranId3rd;
            GameId3rd = gameId3rd;
            Language = language;
            CurrencyId = currencyId;
            SiteId = siteId;
        }

        public string SiteId { get; }

        public int CustId { get; }

        public string TranId3rd { get; }

        public string GameId3rd { get; }

        public string Language { get; }

        public int CurrencyId { get; }

        public int BetTypeId { get; set; }

        public string EndTime { get; set; }

        public int OwTransId { get; set; }

        public string Platform { get; set; }

        public string IsDarkMode { get; set; }

        public string GMT { get; set; }

        public string Location { get; set; }

        public string Site { get; set; }

        public string TransDesc { get; set; }
    }
}
