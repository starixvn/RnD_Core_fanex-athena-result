namespace MvcExample.Models.Criteria
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Data.Repository;

    public class GetOutRightResult : CriteriaBase
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
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