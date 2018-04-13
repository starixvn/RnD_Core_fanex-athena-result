namespace Fanex.Result.Core.PP.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class Request
    {
        [DataMember]
        public string TimeStamp { get; set; }

        [DataMember]
        public Auth Auth { get; set; }

        [DataMember]
        public Call Call { get; set; }
    }
}