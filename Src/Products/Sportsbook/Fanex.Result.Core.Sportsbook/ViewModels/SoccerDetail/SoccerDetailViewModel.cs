namespace Fanex.Result.Core.Sportsbook.ViewModels.SoccerDetail
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Result.Core.Sportsbook.DataDomain;

    public class SoccerDetailViewModel
    {
        public string Name { get; set; }

        public IList<SoccerGameDetailItem> DetailItems { get; set; }

        public string GetAwayResourceId(string homeResourceId)
        {
            var homeAwayMappers = new Dictionary<string, string>
            {
                {GameResourceID.HomePenaltyShootoutRound1.Substring(0,3), GameResourceID.AwayPenaltyShootoutRound1.Substring(0,3) }
            };

            var homeAwayMapper = homeAwayMappers.FirstOrDefault(i => homeResourceId.StartsWith(i.Key));
            if (!homeAwayMapper.Equals(new KeyValuePair<string, string>()))
            {
                var awayResourceId = homeResourceId.Replace(homeAwayMapper.Key, homeAwayMapper.Value);
                var awayItem = DetailItems.FirstOrDefault(i => i.ResourceID == awayResourceId);

                return awayItem != null ? awayItem.HomeValue : string.Empty;
            }

            return string.Empty;
        }
    }
}