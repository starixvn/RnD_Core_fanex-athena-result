namespace Fanex.Result.Core.CardClub.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class Call
    {
        [DataMember]
        public string Seq { get; set; }

        [DataMember]
        public string OBCustId { get; set; }

        [DataMember]
        public string GameTypeId { get; set; }

        [DataMember]
        public string TransId3rd { get; set; }

        [DataMember]
        public string Type { get; set; }
    }
}