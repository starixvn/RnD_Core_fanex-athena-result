// -----------------------------------------------------------------------
// <copyright file="aaa.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace MvcExample.Models.Criteria
{
    using Fanex.Data.Repository;
    using System;

    public class GetSportByDateCriteria : CriteriaBase
    {
        public DateTime EventDate { get; set; }

        public int LicenseeID { get; set; }

        public override string GetSettingKey()
        {
            return "Lrf_Result_SportSelByDate";
        }

        public override bool IsValid()
        {
            return EventDate > DateTime.MinValue;
        }
    }
}