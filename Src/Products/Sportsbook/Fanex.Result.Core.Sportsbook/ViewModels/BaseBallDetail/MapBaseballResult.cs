namespace Fanex.Result.Core.Baseball.DTO
{
    using Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail;

    public class MapBaseballResult : GameDetailItem
    {
        public int BetTypeId { get; set; }

        public bool IsDisplaySingleTeam { get; set; }

    }
}