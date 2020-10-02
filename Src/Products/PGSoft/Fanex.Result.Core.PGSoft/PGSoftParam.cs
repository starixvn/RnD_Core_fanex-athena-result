namespace Fanex.Result.Core.PGSoft
{
    public class PGSoftParam
    {
        public PGSoftParam()
        {
            Language = "en-US";
        }

        public string CustId { get; set; }

        public string GamePlayId { get; set; }

        public string GameId3rd { get; set; }

        public string Language { get; set; }

        public string SiteId { get; set; }
    }
}
