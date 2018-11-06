namespace MvcExample.Models.ResultData
{
    using System.Collections.Generic;
    using Criteria;
    using Fanex.Result.Core.MetricGaming.DTO;
    using ResultServices;

    public class MetricGamingResultData
    {
        private readonly MetricGamingResultService resultService;

        public MetricGamingResultData()
        {
            resultService = new MetricGamingResultService();
        }

        public IEnumerable<MetricGaming> GetPopupResult(int betId)
        {
            return resultService.GetResult(new GetMatchResultDetail3rdSelCriteria
            {
                BetId = betId
            });
        }
    }
}