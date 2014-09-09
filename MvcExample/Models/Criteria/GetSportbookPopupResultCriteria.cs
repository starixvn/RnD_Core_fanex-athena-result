using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.Data.Repository;

namespace MvcExample.Models.Criteria
{
   public class GetSportbookPopupResultCriteria:CriteriaBase
    {
        public int LicenseeID { get; set; }

        public int UserID { get; set; }

        public int MatchID { get; set; }

        public string RaceNo { get; set; }

        public override string GetSettingKey()
        {
            return "Lrf_BetList_SelMatchResult";
        }

        public override bool IsValid()
        {
            return LicenseeID > 0 && MatchID > 0;
        }
    }
}
