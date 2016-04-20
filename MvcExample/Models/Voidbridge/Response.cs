namespace MvcExample.Models.Voidbridge
{
    public class Response
    {
        public string TimeStamp { get; set; }

        public Result Result { get; set; }
    }

    public class Result
    {
        public string Seq { get; set; }

        public string Url { get; set; }

        public string ErrorCode { get; set; }

        public string ErrorDescription { get; set; }
    }
}