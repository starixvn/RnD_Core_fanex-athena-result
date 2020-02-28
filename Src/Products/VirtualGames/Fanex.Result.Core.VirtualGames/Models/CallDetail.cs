namespace Fanex.Result.Core.VirtualGames.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class CallDetail
    {
        [DataMember]
        public string Seq { get; set; }

        [DataMember]
        public string SiteId { get; set; }

        [DataMember]
        public string GameId { get; set; }
    }
}