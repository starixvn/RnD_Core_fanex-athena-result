using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.Data.Repository;

namespace MvcExample.Models.Criteria
{
    public class GetAGCasinoPopUpResultCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public int Bettype { get; set; }

        public long TransID { get; set; }

        public override string GetSettingKey()
        {
            return "All_BetList_MatchResultAGSel";
        }

        public override bool IsValid()
        {
            return TransID > 0;
        }
    }
}
