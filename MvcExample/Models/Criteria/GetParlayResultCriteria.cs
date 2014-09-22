

namespace MvcExample.Models.Criteria
{
    using Fanex.Data.Repository;
    using System.Diagnostics.CodeAnalysis;
    public class GetParlayResultCriteria:CriteriaBase
    {
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public long refno { get; set; }

        public override string GetSettingKey()
        {
            return "Lrf_BetList_SelMatchResultParlay";
        }

        public override bool IsValid()
        {
            return refno > 0;
        }
    }
}
