using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.Data.Repository;

namespace MvcExample.Models.Criteria
{
   public class GetNumberGamePopupResultCriteria:CriteriaBase
    {
        public int MatchID { get; set; }

        public override string GetSettingKey()
        {
            return "Lrf_BetList_SelMatchResultNumberGame";
        }

        public override bool IsValid()
        {
            return MatchID > 0;
        }
    }
}
