namespace Fanex.Result.Core.VirtualGames.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class Response
    {
        [DataMember]
        public string TimeStamp { get; set; }

        [DataMember]
        public Result Result { get; set; }
    }
}