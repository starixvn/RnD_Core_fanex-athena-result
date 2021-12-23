namespace Fanex.Result.Core.ThirdParty.Models
{
    public class ResultRequest
    {
        public ResultRequest()
        {
            GMT = "0";
            Language = "en-US";
        }

        public ResultRequest(int custId, string tranId3rd, string gameId3rd, int currencyId, string language = "en-US")
            : this()
        {
            CustId = custId;
            TranId3rd = tranId3rd;
            GameId3rd = gameId3rd;
            Language = language;
            CurrencyId = currencyId;
        }

        public string SiteId { get; set; }

        public int CustId { get; }

        public string TranId3rd { get; }

        public string GameId3rd { get; }

        public string Language { get; }

        public int CurrencyId { get; }

        public int BetTypeId { get; set; }

        public string EndTime { get; set; }

        public string OwTransId { get; set; }

        public string Platform { get; set; }

        public bool IsDarkMode { get; set; }

        public string GMT { get; set; }
    }
}
