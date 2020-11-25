namespace MvcExample.Models.ResultData
{
    using System.Collections.Generic;
    using Fanex.Result.Core.LeapGaming.DTO;

    public class LeapGamingResultData
    {
        public List<LeapGaming> GetPopupResult(string transDesc)
            => new List<LeapGaming> { new LeapGaming { TransDesc = transDesc } };
    } 
}