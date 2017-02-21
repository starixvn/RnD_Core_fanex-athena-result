namespace Fanex.Result.Core.Sportsbook.ViewModels.SoccerInfo
{
    using System.Collections.Generic;

    public class SoccerDetailViewModel
    {
        public string Name { get; set; }

        public IList<SoccerGameDetailItem> DetailItems { get; set; }
    }
}