namespace Fanex.Result.Core.GlobalGaming.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class Result
    {
        [DataMember]
        public string Seq { get; set; }

        [DataMember]
        public string ObCustId { get; set; }

        [DataMember]
        public string GameId { get; set; }

        [DataMember]
        public string ResultUrl { get; set; }

        [DataMember]
        public int ErrorCode { get; set; }

        [DataMember]
        public string ErrorDescription { get; set; }
    }
}