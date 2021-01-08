namespace Fanex.Result.Core.MaxGame.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class Response
    {
        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string ErrorCode { get; set; }

        [DataMember]
        public string ErrorMsg { get; set; }
    }
}