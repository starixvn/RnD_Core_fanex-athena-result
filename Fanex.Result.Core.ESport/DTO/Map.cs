namespace Fanex.Result.Core.ESport.DTO
{
    using System.Collections.Generic;

    public class Map
    {
        public int MapId { get; set; }

        public int SettlementStatusId { get; set; }

        public int HomeScore { get; set; }

        public int AwayScore { get; set; }

        public string ResourceId { get; set; }

        public string HomeScoreText => BuildScoreText(HomeScore);

        public string AwayScoreText => BuildScoreText(AwayScore);

        private string BuildScoreText(int score)
        {
            switch(SettlementStatusId)
            {
                case 0:
                    return "_";

                case 1:
                    return "R";

                default:
                    return score.ToString();
            }
        }

        public IList<MapResult> MapResults { get; set; } = new List<MapResult>();
    }
}