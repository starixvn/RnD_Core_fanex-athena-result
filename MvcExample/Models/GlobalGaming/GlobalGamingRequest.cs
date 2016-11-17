namespace MvcExample.Models.GlobalGaming
{
    public class GlobalGamingRequest
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

        public string ObCustId { get; set; }

        public string Language { get; set; }

        public string Style { get; set; }

        public string GameId { get; set; }

        public string BackUrl { get; set; }
    }
}