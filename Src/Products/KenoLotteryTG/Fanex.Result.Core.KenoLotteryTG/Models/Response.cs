namespace Fanex.Result.Core.KenoLotteryTG.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class Response
    {
        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public int ErrorCode { get; set; }

        [DataMember]
        public string ErrorMsg { get; set; }
    }
}