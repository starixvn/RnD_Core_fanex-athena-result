using System;
using Fanex.Data.Repository;

namespace MvcExample.Models.Criteria
{
    public class GetRacingResultCriteria : CriteriaBase
    {
        public DateTime FromDate { get; set; }

        public int LeagueID { get; set; }

        public short Sporttype { get; set; }

        public bool isShowAll { get; set; }

        public override string GetSettingKey()
        {
            return "Lrf_Result_RaceSelByMeeting";
        }

        public override bool IsValid()
        {
            return FromDate > DateTime.MinValue && LeagueID > 0 && Sporttype > 0;
        }
    }
}