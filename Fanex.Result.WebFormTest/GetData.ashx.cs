// --------------------------------------------------------------------------
// <copyright file="GetData.ashx.cs" company="Nexcel Solutions Vietnam">
// Copyright (coffee) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="" date="">Created</change>
// </history>

namespace Fanex.Result.WebFormTest
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Script.Serialization;
    using Fanex.Result.Core;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.DataTest;

    /// <summary>
    /// Summary description.
    /// </summary>
    public class GetData : IHttpHandler
    {
        private ResultModel _model = new ResultModel();
        private JavaScriptSerializer _serrialize = new JavaScriptSerializer();

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
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            var dateTime = DateTime.Parse(context.Request.QueryString["datetime"]);
            var tdatetime = context.Request.QueryString["tdatetime"] != null ? DateTime.Parse(context.Request.QueryString["tdatetime"]) : DateTime.Now;
            var sportId = int.Parse(context.Request.QueryString["sportid"]);
            var leagueId = int.Parse(context.Request.QueryString["leagueId"]);
            var viewBy = context.Request.QueryString["by"];
            var isOutRight = context.Request.QueryString["isoutright"] != null && context.Request.QueryString["isoutright"] == "1";
            if (dateTime != null)
            {
                if (isOutRight)
                {
                    GetOutRightData(context, dateTime, tdatetime, viewBy);
                }
                else if (sportId > 0)
                {
                    GetDataBySport(context, dateTime, sportId, leagueId, viewBy);
                }
            }
        }

        /// <summary>
        /// Get outright data.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="fdatetime">The start date.</param>
        /// <param name="tdatetime">The end date.</param>
        /// <param name="viewBy">The view by.</param>
        private void GetOutRightData(HttpContext context, DateTime fdatetime, DateTime tdatetime, string viewBy)
        {
            string htmlResult = string.Empty;
            string jsonResult = string.Empty;
            MemoryStream excelResult = null;
            var data = _model.Lrf_Result_Outright(fdatetime, tdatetime, true);
            var outRightData = data.AsEnumerable().Select(i => new Fanex.Result.Core.DTO.OutRight
            {
                EventDate = DateTime.Parse(i["eventdate"].ToString()),
                SportType = int.Parse(i["sporttype"].ToString()),
                LeagueId = int.Parse(i["leagueid"].ToString()),
                TeamId = int.Parse(i["teamid"].ToString()),
                Status = i["status"].ToString()
            }).ToList();

            htmlResult = ResultEngine.HtmlParser<Fanex.Result.Core.DTO.OutRight>(
                    outRightData,
                    new ResultSetting
                    {
                        IsOutRight = true,
                        IsPopUp = false
                    });
            excelResult = ResultEngine.ExcelParser<Fanex.Result.Core.DTO.OutRight>(
                outRightData,
              new ResultSetting
              {
                  IsPopUp = false,
                  IsOutRight = true,
                  ExcelTitle = "Fanex.Result.Demo",
                  ExcelTemplatePath = context.Server.MapPath(@"~/App_Data/Fanex.Result/")
              });
            jsonResult = ResultEngine.JsonParser<Fanex.Result.Core.DTO.OutRight>(
                 outRightData,
                 new ResultSetting
                 {
                     IsOutRight = true,
                     IsPopUp = false
                 });
            ResponseResult(context, viewBy, htmlResult, jsonResult, excelResult);
        }

        /// <summary>
        /// Get data by sport.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="date">The start date.</param>
        /// <param name="sportId">The sport id.</param>
        /// <param name="leagueId">The league id.</param>
        /// <param name="viewBy">The view by.</param>
        [SuppressMessage(
          "StyleCop.CSharp.Nexcel.NexcelCustomRules",
          "SP2101:MethodMustNotContainMoreLinesThan",
          Justification = "Reviewed. Suppression is OK here.")]
        private void GetDataBySport(HttpContext context, DateTime date, int sportId, int leagueId, string viewBy)
        {
            var sportType = (SportType)sportId;
            string htmlResult = string.Empty;
            string jsonResult = string.Empty;
            MemoryStream excelResult = null;
            switch (sportType)
            {
                case SportType.HorseRacing:
                case SportType.Greyhound:
                case SportType.Harness:
                case SportType.VirtualHorseRacing:
                case SportType.VirtualGreyhounds:
                case SportType.VirtualMotorRacing:
                    var dataRC = _model.Lrf_Result_RaceSelByMeeting(date, sportId, leagueId, true);
                    var dataListRC = dataRC.AsEnumerable().Select(i => new Racing
                    {
                        LeagueId = int.Parse(i["meetingid"].ToString()),
                        EngineId = i["HorseId"].ToString(),
                        EngineNo = i["HorseNo"].ToString(),
                        IsFavor = i["IsFavor"] != DBNull.Value && i["IsFavor"].ToString().Equals("1") ? true : false,
                        IsSCR = i["IsSCR"] != DBNull.Value && i["IsSCR"].ToString().Equals("1") ? true : false,
                        Place = int.Parse(i["place"].ToString()),
                        PlaceOdds = decimal.Parse(i["placeodds"].ToString()),
                        WinOdds = decimal.Parse(i["odds"].ToString()),
                        RaceName = i["racename"] != DBNull.Value ? int.Parse(i["racename"].ToString()) : 0,
                        KickOffTime = DateTime.Parse(i["kickofftime"].ToString()),
                        Favourite = i["IsFavor"].ToString(),
                        RowCount = int.Parse(i["rowcount"].ToString()),
                    }).ToList();
                    htmlResult = viewBy == "Table" ? ResultEngine.HtmlParser<Racing>(
                        dataListRC,
                        new ResultSetting { SportType = sportType, IsPopUp = false }) : string.Empty;
                    excelResult = viewBy == "Excel" ? ResultEngine.ExcelParser<Racing>(
                        dataListRC,
                      new ResultSetting
                      {
                          SportType = sportType,
                          IsPopUp = false,
                          ExcelTitle = "Fanex.Result.Demo",
                          ExcelTemplatePath = context.Server.MapPath(@"~/App_Data/Fanex.Result/")
                      }) : null;
                    jsonResult = viewBy == "Json" ? ResultEngine.JsonParser<Racing>(
                        dataListRC,
                        new ResultSetting
                        {
                            SportType = sportType,
                            IsPopUp = false
                        }) : string.Empty;
                    break;

                case SportType.LiveCasino:
                    var dataLC = _model.Lrf_Result_MatchSelByLeague(date, sportId, leagueId, true, "en-US");
                    var dataListLC = dataLC.AsEnumerable().Select(i => new LiveCasino
                    {
                        ShowTime = i["showtime"].ToString(),
                        LeagueId = int.Parse(i["leagueid"].ToString()),

                        EventStatus = i["eventstatus"].ToString(),
                        FinalAwayScore = i["finalawayscore"] != DBNull.Value ? int.Parse(i["finalawayscore"].ToString()) : -1,

                        FinalHomeScore = i["finalhomescore"] != DBNull.Value ? int.Parse(i["finalhomescore"].ToString()) : -1,

                        EventDate = DateTime.Parse(i["eventdate"].ToString()),

                        IsPending = bool.Parse(i["IsPending"].ToString()),
                        KickOffTime = DateTime.Parse(i["kickofftime"].ToString()),

                        IsHTCal = Convert.ToBoolean(i["IsHTCal"]),
                        Calculate = Convert.ToBoolean(i["Calculate"]),
                        BetStatus = i["betstatus"] != DBNull.Value ? i["betstatus"].ToString() : string.Empty,
                        Bettype = int.Parse(i["homeid2"].ToString())
                    }).ToList();
                    htmlResult = viewBy == "Table" ? ResultEngine.HtmlParser<LiveCasino>(
                        dataListLC,
                        new ResultSetting
                        {
                            SportType = sportType,
                            IsPopUp = false
                        }) : string.Empty;
                    excelResult = viewBy == "Excel" ? ResultEngine.ExcelParser<LiveCasino>(
                        dataListLC,
                       new ResultSetting
                       {
                           SportType = sportType,
                           IsPopUp = false,
                           ExcelTitle = "Fanex.Result.Demo",
                           ExcelTemplatePath = context.Server.MapPath(@"~/App_Data/Fanex.Result/")
                       }) : null;
                    jsonResult = viewBy == "Json" ? ResultEngine.JsonParser<LiveCasino>(
                        dataListLC,
                        new ResultSetting
                        {
                            SportType = sportType,
                            IsPopUp = false
                        }) : string.Empty;
                    break;

                case SportType.NumberGame:
                    var dataNG = _model.Lrf_Result_MatchSelNumberGameByLeague(date, leagueId, true, "en-US");
                    var matchs = dataNG.AsEnumerable().OrderBy(i => i["leagueId"])
                        .OrderBy(i => i["matchid"]).OrderBy(i => i["seq"]);
                    var dataListNG = new List<NumberGame>();
                    foreach (var mtc in dataNG.AsEnumerable())
                    {
                        var matchId = int.Parse(mtc["matchid"].ToString());
                        var existMatch = dataListNG.SingleOrDefault(i => i.MatchId == matchId);
                        if (existMatch != null)
                        {
                            existMatch.Seg.Add(int.Parse(mtc["seq"].ToString()));
                            existMatch.Ball.Add(int.Parse(mtc["number"].ToString()));
                        }
                        else
                        {
                            dataListNG.Add(new NumberGame
                            {
                                LeagueId = int.Parse(mtc["leagueid"].ToString()),
                                MatchId = int.Parse(mtc["matchid"].ToString()),
                                EventStatus = mtc["eventstatus"].ToString(),
                                EventDate = DateTime.Parse(mtc["eventdate"].ToString()),
                                IsPending = mtc.Table.Columns.Contains("isrefund") && mtc["isrefund"].ToString() != "0",
                                KickOffTime = DateTime.Parse(mtc["kickofftime"].ToString()),
                                MatchCode = mtc["matchcode"].ToString(),
                                Seg = new List<int> { int.Parse(mtc["seq"].ToString()) },
                                Ball = new List<int> { int.Parse(mtc["number"].ToString()) }
                            });
                        }
                    }

                    htmlResult = viewBy == "Table" ? ResultEngine.HtmlParser<NumberGame>(
                        dataListNG,
                        new ResultSetting
                        {
                            SportType = sportType,
                            IsPopUp = false
                        }) : string.Empty;
                    jsonResult = ResultEngine.JsonParser<NumberGame>(
                   dataListNG,
                   new ResultSetting
                   {
                       SportType = sportType,
                       IsPopUp = false
                   });

                    excelResult = viewBy == "Excel" ? ResultEngine.ExcelParser<NumberGame>(
                        dataListNG,
                       new ResultSetting
                       {
                           SportType = sportType,
                           IsPopUp = false,
                           ExcelTitle = "Fanex.Result.Demo",
                           ExcelTemplatePath = context.Server.MapPath(@"~/App_Data/Fanex.Result/")
                       }) : null;
                    break;

                default:
                    var data = _model.Lrf_Result_MatchSelByLeague(date, sportId, leagueId, true, "en-US");
                    var dataList = data.AsEnumerable().Select(i => new Sportbook
                    {
                        AwayId = int.Parse(i["awayID"].ToString()),
                        HomeId = int.Parse(i["homeID"].ToString()),
                        BetStatus = i["betstatus"] != DBNull.Value ? i["betstatus"].ToString() : string.Empty,
                        LeagueId = int.Parse(i["leagueid"].ToString()),
                        MatchId = int.Parse(i["matchid"].ToString()),
                        EventStatus = i["eventstatus"].ToString(),
                        FinalAwayScore = i["finalawayscore"] != DBNull.Value ? int.Parse(i["finalawayscore"].ToString()) : -1,
                        HtAwayScore = i["htawayscore"] != DBNull.Value ? int.Parse(i["htawayscore"].ToString()) : -1,
                        Color = i["lcolor"] != DBNull.Value ? i["lcolor"].ToString() : string.Empty,
                        FinalHomeScore = i["finalhomescore"] != DBNull.Value ? int.Parse(i["finalhomescore"].ToString()) : -1,
                        HtHomeScore = i["finalhomescore"] != DBNull.Value ? int.Parse(i["hthomescore"].ToString()) : -1,
                        EventDate = DateTime.Parse(i["eventdate"].ToString()),
                        IsNeutral = bool.Parse(i["IsNeutral"].ToString()),
                        IsPending = bool.Parse(i["IsPending"].ToString()),
                        KickOffTime = DateTime.Parse(i["kickofftime"].ToString()),
                        MatchCode = i["matchcode"].ToString(),
                        GoalRecord = i["goalrecord"].ToString(),
                        ResultValue = "6-1 *30-40 40-30* *40-0 40-0* *40-0 40-0* *40-0",
                        IsHTCal = Convert.ToBoolean(i["IsHTCal"]),
                        Calculate = Convert.ToBoolean(i["Calculate"]),
                        RefNo = "12"
                    }).ToList();

                    htmlResult = viewBy == "Table" ? ResultEngine.HtmlParser<Sportbook>(
                        dataList,
                        new ResultSetting
                        {
                            SportType = sportType,
                            IsPopUp = false
                        }) : string.Empty;
                    jsonResult = ResultEngine.JsonParser<Sportbook>(
                 dataList,
                 new ResultSetting
                 {
                     SportType = sportType,
                     IsPopUp = false
                 });
                    excelResult = viewBy == "Excel" ? ResultEngine.ExcelParser<Sportbook>(
                        dataList,
                        new ResultSetting
                        {
                            SportType = sportType,
                            IsPopUp = false,
                            ExcelTitle = "Fanex.Result.Demo",
                            ExcelTemplatePath = context.Server.MapPath(@"~/App_Data/Fanex.Result/")
                        }) : null;

                    break;
            }

            ResponseResult(context, viewBy, htmlResult, jsonResult, excelResult);
        }

        /// <summary>
        /// Response result.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="viewBy">The view by.</param>
        /// <param name="htmlResult">The HTML result.</param>
        /// <param name="jsonResult">The JSON result.</param>
        /// <param name="excelResult">The excel result.</param>
        private void ResponseResult(HttpContext context, string viewBy, string htmlResult, string jsonResult, MemoryStream excelResult)
        {
            switch (viewBy)
            {
                case "Table":
                    context.Response.Write(htmlResult);
                    break;

                case "Json":

                    context.Response.Write(jsonResult);
                    break;

                case "Excel":
                    byte[] bfr = excelResult.ToArray();
                    context.Response.ContentType = "application/application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    context.Response.AppendHeader("content-disposition", "attachment; filename=ExcelFileName.xlsx");

                    context.Response.OutputStream.Write(bfr, 0, bfr.Length);
                    context.Response.Flush();
                    context.Response.Close();
                    break;

                default:
                    context.Response.Write(htmlResult);
                    break;
            }
        }
    }
}