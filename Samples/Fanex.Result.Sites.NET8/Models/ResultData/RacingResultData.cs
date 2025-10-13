using System;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Result.Sites.NET8.Models.ResultData;

public class RacingResultData : BaseResultData
{
    public override List<object> GetResult()
    {
        // Placeholder implementation
        return new List<object>();
    }

    public IEnumerable<object> GetNormalResult(short sportId, DateTime date, int leagueId)
    {
        // Placeholder implementation
        return new List<object>();
    }
}
