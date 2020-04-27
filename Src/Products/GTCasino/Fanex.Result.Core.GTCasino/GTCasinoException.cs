namespace Fanex.Result.Core.GTCasino
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class GTCasinoException : Exception
    {
        public GTCasinoException()
        {
        }

        public GTCasinoException(string message)
            : base(message)
        {
        }

        public GTCasinoException(string mesasge, Exception innerException)
            : base(mesasge, innerException)
        {
        }

        protected GTCasinoException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message => string.Format("GTCasino Result API Exception. Exception: {0}", base.Message);

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}