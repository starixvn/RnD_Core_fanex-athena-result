namespace Fanex.Result.Core.SAGaming
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class SAGamingException : Exception
    {
        public SAGamingException()
        {
        }

        public SAGamingException(string message)
            : base(message)
        {
        }

        public SAGamingException(string mesasge, Exception innerException)
            : base(mesasge, innerException)
        {
        }

        protected SAGamingException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message => string.Format("SAGaming Result API Exception. Exception: {0}", base.Message);

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}