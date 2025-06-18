namespace Fanex.Result.Core.WCasino
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class WCasinoException : Exception
    {
        public WCasinoException()
        {
        }

        public WCasinoException(string message)
            : base(message)
        {
        }

        public WCasinoException(string mesasge, Exception innerException)
            : base(mesasge, innerException)
        {
        }

        protected WCasinoException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message => string.Format("W Casino Result API Exception. Exception: {0}", base.Message);

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}