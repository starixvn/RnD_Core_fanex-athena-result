namespace Fanex.Result.Core.Sportsbook.Exceptions
{
    using System;

    [Serializable]
    public class SportsbookException : Exception
    {
        public SportsbookException(string message)
            : base(message)
        {
        }

        public override string Message
        {
            get
            {
                return $"Sportsbook Result Exception. Exception: {base.Message}";
            }
        }
    }
}