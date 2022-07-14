namespace Fanex.Result.Core.Volleyball.DTO
{
    using Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail;

    public class MapVolleyballResult : GameDetailItem
    {
        public int BetTypeId { get; set; }

        public int SetNumber { get; set; }

        public bool IsDisplaySingleTeam { get; set; }

        public bool Visible { get; set; }

        public int Point { get; set; }
    }
}