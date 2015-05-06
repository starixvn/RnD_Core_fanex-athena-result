using System;

namespace MvcExample.Models.Criteria
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Data.Repository;

    public class GetGolDeluxePopUpResultCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public int Bettype { get; set; }

        public long TransID { get; set; }

        public override string GetSettingKey()
        {
            return "All_BetList_MatchResultGDSel";
        }

        public override bool IsValid()
        {
            return TransID > 0;
        }
    }

}
