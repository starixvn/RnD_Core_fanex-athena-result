// --------------------------------------------------------------------------
// <copyright file="Lrf_Result_LeagueSelByDate.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Alex.Nguyen" date="2014.9.9">Created</change>
// </history>

namespace MvcExample
{
    using System;

    public class League
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public DateTime? EventDate { get; set; }

        public byte? SportId { get; set; }
    }
}
