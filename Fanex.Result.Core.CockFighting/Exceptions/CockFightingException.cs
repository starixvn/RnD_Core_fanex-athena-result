// -----------------------------------------------------------------------
// <copyright file="CockFightException.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.CockFighting.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CockFightingException : Exception
    {
        public CockFightingException()
        {
        }

        public CockFightingException(string message)
            : base(message)
        {
        }

        public CockFightingException(string mesasge, Exception innerException)
            : base(mesasge, innerException)
        {
        }

        protected CockFightingException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message
        {
            get
            {
                return
                    string.Format("Cock Fighting Result API Exception. Exception: {0}", base.Message);
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}