// --------------------------------------------------------------------------
// <copyright file="GetPopUpData.ashx.cs" company="Nexcel Solutions Vietnam">
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
    using System.Globalization;
    using System.Linq;
    using System.Web;
    using System.Web.Script.Serialization;
    using Fanex.Result.Core;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.DataTest;

    /// <summary>
    /// Get pop up data.
    /// </summary>
    public class GetPopUpData : IHttpHandler
    {
        private readonly ResultModel _model = new ResultModel();
        private string _htmlResult = string.Empty;
        private string _jsonResult = string.Empty;

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
        /// <param name="context">The context.</param>
        public void ProcessRequest(HttpContext context)
        {
            long refno;
            int matchid;
            int type;
            int sport;
            context.Response.ContentType = "text/plain";

            int.TryParse(context.Request.QueryString["type"], out type);
            long.TryParse(context.Request.QueryString["refno"], out refno);
            int.TryParse(context.Request.QueryString["matchid"], out matchid);
            var raceno = context.Request.QueryString["raceno"];
            int.TryParse(context.Request.QueryString["sport"], out sport);
            var viewBy = context.Request.QueryString["by"];

            switch (type)
            {
                case 0:
                    ProcessNumgerGame(context, matchid, viewBy);
                    break;

                case 1:
                    ProcessParlay(context, refno, viewBy);
                    break;

                default:
                    ProcessNormal(context, matchid, (SportType)sport, raceno, viewBy);
                    break;
            }
        }

        /// <summary>
        /// Process normal sport.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="matchid">The match id.</param>
        /// <param name="sportType">The sport id.</param>
        /// <param name="raceno">The referent number.</param>
        /// <param name="viewBy">The view by.</param>
        [SuppressMessage(
          "StyleCop.CSharp.Nexcel.NexcelCustomRules",
          "SP2101:MethodMustNotContainMoreLinesThan",
          Justification = "Reviewed. Suppression is OK here.")]
        private void ProcessNormal(HttpContext context, int matchid, SportType sportType, string raceno, string viewBy)
        {
            var data = _model.Lrf_BetList_SelMatchResult(matchid, raceno);
            if (data != null)
            {
                switch (sportType)
                {
                    case SportType.HorseRacing:
                    case SportType.Greyhound:
                    case SportType.Harness:
                    case SportType.VirtualHorseRacing:
                    case SportType.VirtualGreyhounds:
                    case SportType.VirtualMotorRacing:
                    case SportType.VirtualSpeedway:
                    case SportType.VirtualCycling:
                        var dataListRC =
                            data.AsEnumerable()
                                .Select(
                                    i =>
                                        new Racing
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
                                            SCROfMatch = i["SCR"].ToString(),
                                            TFOfMatch = i["TF"].ToString()
                                        })
                                .ToList();
                        _htmlResult = ResultEngine.HtmlParser<Racing>(
                            dataListRC,
                            new ResultSetting { SportType = sportType, IsPopUp = true });
                        _jsonResult = ResultEngine.JsonParser<Racing>(
                            dataListRC,
                            new ResultSetting { SportType = sportType, IsPopUp = true });
                        break;

                    case SportType.LiveCasino:

                        var dataListLC =
                            data.AsEnumerable()
                                .Select(
                                    i =>
                                        new LiveCasino
                                        {
                                            ShowTime = i["showtime"].ToString(),
                                            LeagueId = int.Parse(i["leagueid"].ToString()),
                                            EventStatus = i["eventstatus"].ToString(),
                                            FinalAwayScore =
                                                i["finalawayscore"] != DBNull.Value
                                                    ? int.Parse(i["finalawayscore"].ToString())
                                                    : -1,
                                            FinalHomeScore =
                                                i["finalhomescore"] != DBNull.Value
                                                    ? int.Parse(i["finalhomescore"].ToString())
                                                    : -1,
                                            EventDate = DateTime.Parse(i["eventdate"].ToString()),
                                            IsPending = bool.Parse(i["IsPending"].ToString()),
                                            KickOffTime = DateTime.Parse(i["kickofftime"].ToString()),
                                            IsHTCal = Convert.ToBoolean(i["IsHTCal"]),
                                            Calculate = Convert.ToBoolean(i["Calculate"]),
                                            BetStatus =
                                                i["betstatus"] != DBNull.Value
                                                    ? i["betstatus"].ToString()
                                                    : string.Empty,
                                            Bettype = int.Parse(i["homeid2"].ToString())
                                        })
                                .ToList();
                        _htmlResult = ResultEngine.HtmlParser(
                            dataListLC,
                            new ResultSetting { SportType = sportType, IsPopUp = true });
                        _jsonResult = ResultEngine.JsonParser(
                            dataListLC,
                            new ResultSetting { SportType = sportType, IsPopUp = true });
                        break;

                    case SportType.NumberGame:

                        var matchs =
                            data.AsEnumerable()
                                .OrderBy(i => i["leagueId"])
                                .OrderBy(i => i["matchid"])
                                .OrderBy(i => i["seq"]);
                        var dataListNG = new List<NumberGame>();
                        foreach (var mtc in data.AsEnumerable())
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
                                dataListNG.Add(
                                    new NumberGame
                                    {
                                        LeagueId = int.Parse(mtc["leagueid"].ToString()),
                                        MatchId = int.Parse(mtc["matchid"].ToString()),
                                        EventStatus = mtc["eventstatus"].ToString(),
                                        EventDate = DateTime.Parse(mtc["eventdate"].ToString()),
                                        IsPending = mtc["isrefund"].ToString() == "0" ? false : true,
                                        KickOffTime = DateTime.Parse(mtc["kickofftime"].ToString()),
                                        MatchCode = mtc["matchcode"].ToString(),
                                        Seg = new List<int> { int.Parse(mtc["seq"].ToString()) },
                                        Ball = new List<int> { int.Parse(mtc["number"].ToString()) }
                                    });
                            }
                        }

                        _htmlResult = ResultEngine.HtmlParser<NumberGame>(
                            dataListNG,
                            new ResultSetting { SportType = sportType, IsPopUp = true });
                        _jsonResult = ResultEngine.JsonParser<NumberGame>(
                            dataListNG,
                            new ResultSetting { SportType = sportType, IsPopUp = true });

                        break;

                    default:
                        var dataList =
                            data.AsEnumerable()
                                .Select(
                                    i =>
                                        new Sportbook
                                        {
                                            AwayId = int.Parse(i["awayID"].ToString()),
                                            HomeId = int.Parse(i["homeID"].ToString()),
                                            BetStatus =
                                                i["betstatus"] != DBNull.Value
                                                    ? i["betstatus"].ToString()
                                                    : string.Empty,
                                            LeagueId = int.Parse(i["leagueid"].ToString()),
                                            MatchId = int.Parse(i["matchid"].ToString()),
                                            EventStatus = i["eventstatus"].ToString(),
                                            FinalAwayScore =
                                                i["finalawayscore"] != DBNull.Value
                                                    ? int.Parse(i["finalawayscore"].ToString())
                                                    : -1,
                                            HtAwayScore =
                                                i["htawayscore"] != DBNull.Value
                                                    ? int.Parse(i["htawayscore"].ToString())
                                                    : -1,
                                            FinalHomeScore =
                                                i["finalhomescore"] != DBNull.Value
                                                    ? int.Parse(i["finalhomescore"].ToString())
                                                    : -1,
                                            HtHomeScore =
                                                i["finalhomescore"] != DBNull.Value
                                                    ? int.Parse(i["hthomescore"].ToString())
                                                    : -1,
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
                                        })
                                .ToList();

                        _htmlResult = ResultEngine.HtmlParser<Sportbook>(
                            dataList,
                            new ResultSetting { SportType = sportType, IsPopUp = true });
                        _jsonResult = ResultEngine.JsonParser<Sportbook>(
                            dataList,
                            new ResultSetting { SportType = sportType, IsPopUp = true });

                        break;
                }
            }

            ResponseResult(context, viewBy, _htmlResult, _jsonResult);
        }

        /// <summary>
        /// Response result.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="viewBy">The view by.</param>
        /// <param name="htmlResult">The HTML result.</param>
        /// <param name="jsonResult">The JSON result.</param>
        private void ResponseResult(HttpContext context, string viewBy, string htmlResult, string jsonResult)
        {
            switch (viewBy)
            {
                case "Table":
                    context.Response.Write(htmlResult);
                    break;

                case "Json":

                    context.Response.Write(jsonResult);
                    break;

                default:
                    context.Response.Write(htmlResult);
                    break;
            }
        }

        /// <summary>
        /// Process parlay.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="refno">The referent number.</param>
        /// <param name="viewBy">The view by.</param>
        [SuppressMessage(
          "StyleCop.CSharp.Nexcel.NexcelCustomRules",
          "SP2101:MethodMustNotContainMoreLinesThan",
          Justification = "Reviewed. Suppression is OK here.")]
        private void ProcessParlay(HttpContext context, long refno, string viewBy)
        {
            var data = _model.Lrf_BetList_SelMatchResultParlay(refno);
            if (data != null)
            {
                var dataList = data.AsEnumerable();
                var dataParlay = new List<Core.DTO.Parlay>();

                foreach (var row in dataList)
                {
                    var leagueId =
                    int.Parse(row["LeagueID"].ToString());
                    if (dataParlay.All(i => i.LeagueId != leagueId))
                    {
                        dataParlay.Add(
                            new Core.DTO.Parlay
                            {
                                LeagueId = leagueId,
                                SportId = int.Parse(row["sporttype"].ToString()),
                                Sportbook = dataList.Where(i => i["LeagueID"].ToString() == leagueId.ToString())
                                .Select(
                                    i =>
                                        new Sportbook
                                        {
                                            AwayId = int.Parse(i["awayID"].ToString()),
                                            HomeId = int.Parse(i["homeID"].ToString()),
                                            BetStatus =
                                                i["betstatus"] != DBNull.Value
                                                    ? i["betstatus"].ToString()
                                                    : string.Empty,
                                            LeagueId = leagueId,
                                            MatchId = int.Parse(i["matchid"].ToString()),
                                            EventStatus = i["eventstatus"].ToString(),
                                            FinalAwayScore =
                                                i["finalawayscore"] != DBNull.Value
                                                    ? int.Parse(i["finalawayscore"].ToString())
                                                    : -1,
                                            HtAwayScore =
                                                i["htawayscore"] != DBNull.Value
                                                    ? int.Parse(i["htawayscore"].ToString())
                                                    : -1,
                                            FinalHomeScore =
                                                i["finalhomescore"] != DBNull.Value
                                                    ? int.Parse(i["finalhomescore"].ToString())
                                                    : -1,
                                            HtHomeScore =
                                                i["finalhomescore"] != DBNull.Value
                                                    ? int.Parse(i["hthomescore"].ToString())
                                                    : -1,
                                            EventDate = DateTime.Parse(i["eventdate"].ToString()),
                                            IsNeutral = bool.Parse(i["IsNeutral"].ToString()),
                                            IsPending = bool.Parse(i["IsPending"].ToString()),
                                            KickOffTime = DateTime.Parse(i["kickofftime"].ToString()),
                                            MatchCode = i["matchcode"].ToString(),
                                            GoalRecord = i["goalrecord"].ToString(),
                                            ResultValue = "6-1 *30-40 40-30* *40-0 40-0* *40-0 40-0* *40-0",
                                            IsHTCal = Convert.ToBoolean(i["IsHTCal"]),
                                            Calculate = Convert.ToBoolean(i["Calculate"]),
                                            RefNo = refno.ToString(CultureInfo.InvariantCulture)
                                        })
                                .ToList()
                            });
                    }
                }

                _htmlResult = ResultEngine.HtmlParser<Fanex.Result.Core.DTO.Parlay>(
                  dataParlay,
                  new ResultSetting { IsPopUp = true, IsParlayPopUp = true });
                _jsonResult = ResultEngine.JsonParser<Fanex.Result.Core.DTO.Parlay>(
                   dataParlay,
                    new ResultSetting { IsPopUp = true, IsParlayPopUp = true });
                ResponseResult(context, viewBy, _htmlResult, _jsonResult);
            }
        }

        /// <summary>
        /// Process number game.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="matchid">The match id.</param>
        /// <param name="viewby">The view by.</param>
        private void ProcessNumgerGame(HttpContext context, int matchid, string viewby)
        {
            var data = _model.Lrf_BetList_SelMatchResultNumberGame(matchid);
            if (data != null)
            {
                data.AsEnumerable().OrderBy(i => i["leagueId"]).OrderBy(i => i["matchid"]).OrderBy(i => i["seq"]);
                var dataListNG = new List<NumberGame>();
                foreach (var mtc in data.AsEnumerable())
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
                        dataListNG.Add(
                            new NumberGame
                            {
                                LeagueId = int.Parse(mtc["leagueid"].ToString()),
                                MatchId = int.Parse(mtc["matchid"].ToString()),
                                EventStatus = mtc["eventstatus"].ToString(),
                                EventDate = DateTime.Parse(mtc["eventdate"].ToString()),
                                IsPending = !mtc.Table.Columns.Contains("isrefund") || mtc["isrefund"].ToString() != "0",
                                KickOffTime = DateTime.Parse(mtc["kickofftime"].ToString()),
                                MatchCode = mtc["matchcode"].ToString(),
                                Seg = new List<int> { int.Parse(mtc["seq"].ToString()) },
                                Ball = new List<int> { int.Parse(mtc["number"].ToString()) }
                            });
                    }
                }

                _htmlResult = ResultEngine.HtmlParser<NumberGame>(
                    dataListNG,
                    new ResultSetting { SportType = SportType.NumberGame, IsPopUp = true });
                _jsonResult = ResultEngine.JsonParser<NumberGame>(
                    dataListNG,
                    new ResultSetting { SportType = SportType.NumberGame, IsPopUp = true });
            }

            ResponseResult(context, viewby, _htmlResult, _jsonResult);
        }
    }
}