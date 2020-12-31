namespace Fanex.Result.Core.LinkedGames.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class Response
    {
        [DataMember]
        public string TimeStamp { get; set; }

        [DataMember]
        public string Seq { get; set; }

        [DataMember]
        public string VendorId { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string ErrorCode { get; set; }

        [DataMember]
        public string ErrorMsg { get; set; }
    }
}