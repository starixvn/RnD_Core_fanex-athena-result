using System.Collections.Generic;

namespace Fanex.Result.Sites.NET8.Models.ResultData;

public abstract class BaseResultData
{
    // Base implementation for result data classes
    public abstract List<object> GetResult();
}
