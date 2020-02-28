namespace Fanex.Result.Core.VirtualGames.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class RequestDetail
    {
        [DataMember]
        public string TimeStamp { get; set; }

        [DataMember]
        public Auth Auth { get; set; }

        [DataMember]
        public CallDetail Call { get; set; }
    }
}