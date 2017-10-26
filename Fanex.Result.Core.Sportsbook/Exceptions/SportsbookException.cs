namespace Fanex.Result.Core.Sportsbook.Exceptions
{
    using System;

    [Serializable]
#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    public class SportsbookException : Exception
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
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