namespace Fanex.Result.Core.WCasino
{
    using System;

    public class WCasinoException : Exception
    {
        public WCasinoException()
        {
        }

        public WCasinoException(string message)
            : base(message)
        {
        }

        public WCasinoException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
} 