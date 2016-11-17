namespace Fanex.Result.Core.GlobalGaming.Exceptions
{
    using System;

    [Serializable]
    public class GlobalGamingException : Exception
    {
        public GlobalGamingException(string message)
            : base(message)
        {
        }

        public override string Message
        {
            get
            {
                return $"Global Gaming Result API Exception. Exception: {base.Message}";
            }
        }
    }
}