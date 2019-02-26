namespace Fanex.Result.Core.Sportsbook.ViewModels.SoccerDetail
{
    using Fanex.Result.Core.Sportsbook.DTO;
    using System.Collections.Generic;
    using System.Linq;

    public class BetBuilderResutlViewModel
    {
        private readonly IEnumerable<BetBuilderDetail> detail;
        public BetBuilderResutlViewModel(Sportsbook sportsbook)
        {
            detail = sportsbook.BetBuilderDetails;
        }

        public IEnumerable<BetBuilderDetail> Detail => detail.Where(i => !IsTotalRow(i));

        public BetBuilderDetail Total => detail.FirstOrDefault(i => IsTotalRow(i));

        private static bool IsTotalRow(BetBuilderDetail row) => row != null && row.SelectionType == 0;
    }
}
