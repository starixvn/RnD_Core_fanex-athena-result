namespace MvcExample.Models.Criteria
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Data.Repository;

    public class GetRacingResultCriteria : CriteriaBase
    {
        public DateTime FromDate { get; set; }

        public int LeagueID { get; set; }

        public short Sporttype { get; set; }

        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
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