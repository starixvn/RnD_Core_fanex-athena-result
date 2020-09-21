namespace Fanex.Result.Core.Joker.Models
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
        public string GamePlayId { get; set; }

        [DataMember]
        public string VendorId { get; set; }

        [DataMember]
        public string Language { get; set; }

        [DataMember]
        public string GameCode { get; set; }
    }
}