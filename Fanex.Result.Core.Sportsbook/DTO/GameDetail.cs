// -----------------------------------------------------------------------
// <copyright file="GameDetail.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Sportsbook.DTO
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class GameDetail
    {
        private string _resrouceId = string.Empty;
        private string _homeValue = string.Empty;
        private string _awayValue = string.Empty;

        public int MatchId { get; set; }

        public string ResourceId
        {
            get { return this._resrouceId; }
            set { this._resrouceId = value; }
        }

        public string HomeValue
        {
            get { return this._homeValue; }
            set { this._homeValue = value; }
        }

        public string AwayValue
        {
            get { return this._awayValue; }
            set { this._awayValue = value; }
        }

        public int ResultStatusID { get; set; }

        public int SettlementStatusID { get; set; }

        public string OtherInfo { get; set; }
    }
}