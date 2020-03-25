namespace Fanex.Result.Core.SexyGaming
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class SexyGamingException : Exception
    {
        public SexyGamingException()
        {
        }

        public SexyGamingException(string message)
            : base(message)
        {
        }

        public SexyGamingException(string mesasge, Exception innerException)
            : base(mesasge, innerException)
        {
        }

        protected SexyGamingException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message => string.Format("SexyGaming Result API Exception. Exception: {0}", base.Message);

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}