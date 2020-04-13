namespace Fanex.Result.Core.GlobalGaming.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class Auth
    {
        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}