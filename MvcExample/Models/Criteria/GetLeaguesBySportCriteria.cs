// --------------------------------------------------------------------------
// <copyright file="Lrf_Result_LeagueSelByDate.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Alex.Nguyen" date="2014.9.9">Created</change>
// </history>

namespace MvcExample.Models.Criteria
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Data.Repository;

    public class GetLeaguesBySportCriteria : CriteriaBase
    {
        public DateTime EventDate { get; set; }
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public short sporttype { get; set; }
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool isShowAll { get; set; }

        public override string GetSettingKey()
        {
            return "Lrf_Result_LeagueSelByDate";
        }

        public override bool IsValid()
        {
           return EventDate > DateTime.MinValue && sporttype > 0;
        }
    }
}
