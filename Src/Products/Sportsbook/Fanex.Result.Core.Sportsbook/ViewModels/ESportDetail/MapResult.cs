namespace Fanex.Result.Core.ESport.DTO
{
    using Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail;

    public class MapResult : GameDetailItem
    {
        public int BetTypeId { get; set; }

        public int MapId { get; set; }

        public bool IsDisplaySingleTeam { get; set; }

        public bool IsDisplayBothTeam { get; set; }

        public bool Visible { get; set; }

        public int RoundId { get; set; }
    }
}