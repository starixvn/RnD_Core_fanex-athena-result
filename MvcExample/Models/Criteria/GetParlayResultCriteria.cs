using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.Data.Repository;

namespace MvcExample.Models.Criteria
{
    public class GetParlayResultCriteria:CriteriaBase
    {
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
