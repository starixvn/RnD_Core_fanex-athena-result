namespace Fanex.Result.Core.Casino.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class RequestResultPage
    {
        [DataMember]
        public string TransId3rd { get; set; }

        [DataMember]
        public string SiteId { get; set; }

        [DataMember]
        public string ObCustId { get; set; }

        [DataMember]
        public string DarkMode { get; set; }

        [DataMember]
        public string Language { get; set; }

        [DataMember]
        public string Platform { get; set; }
    }
}