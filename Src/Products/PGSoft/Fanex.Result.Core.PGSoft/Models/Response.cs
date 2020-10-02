namespace Fanex.Result.Core.PGSoft.Models
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