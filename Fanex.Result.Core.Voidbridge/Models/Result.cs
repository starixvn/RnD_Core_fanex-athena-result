namespace Fanex.Result.Core.Voidbridge.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class Result
    {
        [DataMember]
        public string Seq { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string ErrorCode { get; set; }

        [DataMember]
        public string ErrorDescription { get; set; }
    }
}