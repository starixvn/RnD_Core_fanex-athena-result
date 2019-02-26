namespace MvcExample.Models.Criteria
{
    using Fanex.Data.Repository;

#pragma warning disable S101 // Types should be named in PascalCase

    public class GetMatchResultDetail3rdSelCriteria : CriteriaBase
#pragma warning restore S101 // Types should be named in PascalCase
    {
        public int BetId { get; set; }

        public override string GetSettingKey()
        {
            return "All_BetList_MatchResultDetail3rdSel";
        }

        public override bool IsValid()
        {
            return BetId > 0;
        }
    }
}