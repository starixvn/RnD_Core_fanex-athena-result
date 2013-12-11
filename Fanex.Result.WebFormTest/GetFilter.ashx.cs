// --------------------------------------------------------------------------
// <copyright file="GetFilter.ashx.cs" company="Nexcel Solutions Vietnam">
// Copyright (coffee) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="" date="">Created</change>
// </history>
namespace Fanex.Result.WebFormTest
{
    using System;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web;
    using System.Web.Script.Serialization;
    using Fanex.Result.DataTest;

    /// <summary>
    /// Summary description.
    /// </summary>
    public class GetFilter : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Process request.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        [SuppressMessage(
          "StyleCop.CSharp.Nexcel.NexcelCustomRules",
          "SP2101:MethodMustNotContainMoreLinesThan",
          Justification = "Reviewed. Suppression is OK here.")]
        public void ProcessRequest(HttpContext context)
        {
            var model = new ResultModel();
            JavaScriptSerializer serrialize = new JavaScriptSerializer();
            context.Response.ContentType = "application/json";

            var dateTime = DateTime.Parse(context.Request.QueryString["datetime"]);
            var sportId = int.Parse(context.Request.QueryString["sportid"]);
            if (dateTime != null)
            {
                if (sportId == 0)
                {
                    var sport = model.Lrf_Result_SportSelByDate(dateTime);
                    var sportList = sport.AsEnumerable().Select(i => new
                    {
                        sporttype = i["sporttype"],
                        sportname = i["sportname"]
                    }).ToList();

                    if (sportList.Count > 0)
                    {
                        var firstSport = int.Parse(sportList[0].sporttype.ToString());
                        if (firstSport > 0)
                        {
                            var league = model.Lrf_Result_LeagueSelByDate(dateTime, firstSport, true, "en-US");
                            var leagueList = league.AsEnumerable().Select(i => new
                            {
                                leaguename = i["leaguename"],
                                leagueId = i["leagueid"],
                                eventdate = i["eventdate"],
                                sporttype = i["sporttype"]
                            }).ToList();
                            context.Response.Write(serrialize.Serialize(new
                            {
                                Sport = sportList,
                                League = leagueList,
                            }));
                        }
                        else
                        {
                            context.Response.Write(serrialize.Serialize(
                                new
                                {
                                    Sport = sportList,
                                    League = string.Empty,
                                }));
                        }
                    }
                }
                else
                {
                    var league = model.Lrf_Result_LeagueSelByDate(dateTime, sportId, true, "en-US");
                    var leagueList = league.AsEnumerable().Select(i => new
                    {
                        leaguename = i["leaguename"],
                        leagueId = i["leagueid"],
                        eventdate = i["eventdate"],
                        sporttype = i["sporttype"]
                    }).ToList();
                    context.Response.Write(serrialize.Serialize(new
                    {
                        League = leagueList,
                    }));
                }
            }
            else
            {
                context.Response.Write(serrialize.Serialize(new
                {
                    Sport = string.Empty,
                    League = string.Empty,
                }));
            }
        }
    }
}