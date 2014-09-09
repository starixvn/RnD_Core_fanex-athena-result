namespace MvcExample.Models.Criteria
{
    using System;
    using Fanex.Data.Repository;

    public class GetSportbookResultCriteria : CriteriaBase
    {
        public DateTime FromDate { get; set; }

        public int LeagueID { get; set; }

        public byte sporttype { get; set; }

        public bool isShowAll { get; set; }

        public override string GetSettingKey()
        {
            return "Lrf_Result_MatchSelByLeague";
        }

        public override bool IsValid()
        {
            return FromDate > DateTime.MinValue && sporttype
                > 0 && LeagueID > 0;
        }
    }
}