namespace Fanex.Result.Core.VirtualGames.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class Call
    {
        [DataMember]
        public string Seq { get; set; }        

        [DataMember]
        public string SiteId { get; set; }
    }
}