// -----------------------------------------------------------------------
// <copyright file="GetMatchResultDetail3rdSelCriteria.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
namespace MvcExample.Models.Criteria
{
    using Fanex.Data.Repository;

    public class GetMatchResultDetail3rdSelCriteria : CriteriaBase
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