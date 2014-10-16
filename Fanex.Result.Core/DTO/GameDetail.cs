// -----------------------------------------------------------------------
// <copyright file="GameDetail.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.DTO
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class GameDetail
    {
        public int MatchId { get; set; }

        public string ResourceId { get; set; }

        public string HomeValue { get; set; }

        public string AwayValue { get; set; }

        public int ResultStatusID { get; set; }

        public int SettlementStatusID { get; set; }
    }
}