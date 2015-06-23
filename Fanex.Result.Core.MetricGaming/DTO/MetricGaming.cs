// -----------------------------------------------------------------------
// <copyright file="MetricGaming.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.MetricGaming.DTO
{
    using Fanex.Result.Core.Base.DTO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MetricGaming : IProductResult
    {
        public int LeagueId { get; set; }

        public int MatchId { get; set; }

        public int GameId { get; set; }

        public string BetTypeTime { get; set; }

        public int SettlementStatusId { get; set; }

        public string GameResult { get; set; }

        public string OtherInfo { get; set; }

        public string HomeId { get; set; }

        public string AwayId { get; set; }
    }
}