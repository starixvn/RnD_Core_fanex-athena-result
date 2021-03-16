namespace Fanex.Result.Core.SportsLottery.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class RequestResultPage
    {
        [DataMember]
        public string TimeStamp { get; set; }

        [DataMember]
        public string SiteId { get; set; }

        [DataMember]
        public string Seq { get; set; }

        [DataMember]
        public string Language { get; set; }

    }
}