using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.Data.Repository;

namespace MvcExample.Models.Criteria
{
    public class GetOutRightResult:CriteriaBase
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool isShowAll { get; set; }

        public override string GetSettingKey()
        {
            return "Lrf_Result_Outright";
        }

        public override bool IsValid()
        {
            return FromDate > DateTime.MinValue && FromDate <= ToDate && ToDate > DateTime.MinValue;
        }
    }
}
