namespace Fanex.Result.Core.WM
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class WMException : Exception
    {
        public WMException()
        {
        }

        public WMException(string message)
            : base(message)
        {
        }

        public WMException(string mesasge, Exception innerException)
            : base(mesasge, innerException)
        {
        }

        protected WMException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message => string.Format("WM Result API Exception. Exception: {0}", base.Message);

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}