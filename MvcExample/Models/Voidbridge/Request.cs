namespace MvcExample.Models.Voidbridge
{
    public class Request
    {
        public string TimeStamp { get; set; }

        public Auth Auth { get; set; }

        public Call Call { get; set; }
    }

    public class Auth
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string SiteId { get; set; }
    }

    public class Call
    {
        public string Seq { get; set; }

        public string OBCustId { get; set; }

        public string GamePlayId { get; set; }

        public string VendorId { get; set; }
    }
}