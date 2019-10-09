namespace Fanex.Result.Core.CardClub.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class Auth
    {
        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public int SiteId { get; set; }
    }
}