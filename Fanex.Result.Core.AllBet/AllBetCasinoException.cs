using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Fanex.Result.Core.AllBetCasino
{
    public class AllBetCasinoException : System.Exception
    {
        public AllBetCasinoException()
        {
        }

        public AllBetCasinoException(string message)
            : base(message)
        {
        }

        public AllBetCasinoException(string mesasge, Exception innerException)
            : base(mesasge, innerException)
        {
        }

        protected AllBetCasinoException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message
        {
            get
            {
                return string.Format("AllBetCasino Result API Exception. Exception: {0}", base.Message);
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
