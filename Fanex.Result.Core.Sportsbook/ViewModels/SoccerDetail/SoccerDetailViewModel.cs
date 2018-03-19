namespace Fanex.Result.Core.Sportsbook.ViewModels.SoccerDetail
{
    using System.Collections.Generic;

    public class SoccerDetailViewModel
    {
        public string Name { get; set; }

        public IList<SoccerGameDetailItem> DetailItems { get; set; }
    }
}