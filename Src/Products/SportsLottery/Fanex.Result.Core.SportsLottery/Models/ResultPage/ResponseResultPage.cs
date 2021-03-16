namespace Fanex.Result.Core.SportsLottery.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class ResponseResultPage
    {
        [DataMember]
        public string TimeStamp { get; set; }

        [DataMember]
        public Result Result { get; set; }
    }

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