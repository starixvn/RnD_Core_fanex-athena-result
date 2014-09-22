
namespace MvcExample.Models.Criteria
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Data.Repository;

   public class GetLiveCasinoResultCriteria:CriteriaBase
    {
        public DateTime FromDate { get; set; }

        public int LeagueID { get; set; }
       [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public byte sporttype { get; set; }
       [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool isShowAll { get; set; }

        public override string GetSettingKey()
        {
            return "Lrf_Result_MatchSelByLeague";
        }

        public override bool IsValid()
        {
            return FromDate > DateTime.MinValue && LeagueID > 0 && sporttype > 0;
        }
    }
}
