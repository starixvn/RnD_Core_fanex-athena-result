namespace MvcExample.Models.GlobalGaming
{
    public class GlobalGamingResponse
    {
        public string TimeStamp { get; set; }

        public Result Result { get; set; }
    }

    public class Result
    {
        public string Seq { get; set; }

        public string ObCustId { get; set; }

        public string GameId { get; set; }

        public string ResultUrl { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorDescription { get; set; }
    }
}