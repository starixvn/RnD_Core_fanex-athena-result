namespace Fanex.Result.Core.Sportsbook.DTO
{
    public sealed class GameDetail
    {
        public int MatchId { get; set; }

        public string ResourceId { get; set; } = string.Empty;

        public string HomeValue { get; set; } = string.Empty;

        public string AwayValue { get; set; } = string.Empty;

        public int ResultStatusID { get; set; }

        public int SettlementStatusID { get; set; }

        public string OtherInfo { get; set; }

        public int BettypeId { get; set; }
    }
}