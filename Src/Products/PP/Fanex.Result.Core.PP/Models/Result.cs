namespace Fanex.Result.Core.PP.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class Result
    {
        [DataMember]
        public string Seq { get; set; }

#pragma warning disable S3996 // URI properties should not be strings

        [DataMember]
        public string Url { get; set; }

#pragma warning restore S3996 // URI properties should not be strings

        [DataMember]
        public string ErrorCode { get; set; }

        [DataMember]
        public string ErrorDescription { get; set; }
    }
}