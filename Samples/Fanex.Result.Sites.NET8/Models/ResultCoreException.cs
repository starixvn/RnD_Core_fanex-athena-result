using System;

namespace Fanex.Result.Sites.NET8.Models;

public class ResultCoreException : Exception
{
    public ResultCoreException() : base()
    {
    }

    public ResultCoreException(string message) : base(message)
    {
    }

    public ResultCoreException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
