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
#pragma warning disable S3996 // URI properties should not be strings
        public string ResultUrl { get; set; }

#pragma warning restore S3996 // URI properties should not be strings

        [DataMember]
        public int ErrorCode { get; set; }

        [DataMember]
        public string ErrorDescription { get; set; }
    }
}