namespace Fanex.Result.Core.GlobalGaming.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Call
    {
        [DataMember]
        public string Seq { get; set; }

        [DataMember]
        public string ObCustId { get; set; }

        [DataMember]
        public string Language { get; set; }

        [DataMember]
        public string Style { get; set; } = "0";

        [DataMember]
        public string GameId { get; set; }

        [DataMember]
        public string SiteId { get; set; }
    }
}