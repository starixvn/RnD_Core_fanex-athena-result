using Fanex.Data.Repository;

namespace MvcExample.Models.Criteria
{
    public class GetAllBetCasinoPopUpResultCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public int Bettype { get; set; }

        public long TransID { get; set; }

        public override string GetSettingKey()
        {
            return "All_BetList_MatchResultAllBetCasinoSel";
        }

        public override bool IsValid()
        {
            return TransID > 0;
        }
    }
}