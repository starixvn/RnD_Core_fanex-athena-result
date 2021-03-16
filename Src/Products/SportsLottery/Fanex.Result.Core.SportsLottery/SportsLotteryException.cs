using System;
using System.Runtime.Serialization;

namespace Fanex.Result.Core.SportsLottery
{
    [Serializable]
    public class SportsLotteryException : Exception
    {
        public SportsLotteryException(string message)
            : base(message)
        {
        }

        protected SportsLotteryException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message
            => $"Result API Exception. Exception: {base.Message}";
    }
}
