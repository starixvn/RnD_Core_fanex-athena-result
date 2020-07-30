namespace Fanex.Result.Core.SabaClub.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class Call
    {
        [DataMember]
        public string Seq { get; set; }

        [DataMember]
        public string SiteId { get; set; }

        [DataMember]
        public string OBCustId { get; set; }

        [DataMember]
        public string GameRoundId { get; set; }

        [DataMember]
        public string GameId { get; set; }

        [DataMember]
        public string Language { get; set; }
    }
}