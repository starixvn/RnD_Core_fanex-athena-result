using System;

namespace Fanex.Result.Sites.NET8.Models.ResultServices;

public static class BaseResultService
{
    public static BalanceUp GetBalanceUpDay()
    {
        // Placeholder implementation
        return new BalanceUp { MoveBetsDay = DateTime.Now };
    }
}

public class BalanceUp
{
    public DateTime MoveBetsDay { get; set; }
}
