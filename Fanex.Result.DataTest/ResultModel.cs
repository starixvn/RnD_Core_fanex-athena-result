// --------------------------------------------------------------------------
// <copyright file="ResultModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (coffee) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Alex.Nguyeen" date="2013/09/30">Created</change>
// </history>
namespace Fanex.Result.DataTest
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Result model class.
    /// </summary>
    public class ResultModel
    {
        private const string Connection = @"packet size=4096;
            Data Source=10.18.200.215;Initial Catalog=bodb02;integrated security=false; User Id=bodbLicenseeNet;Password=%bodbLicenseeNetLogin0112";

        /// <summary>
        /// Get sports.
        /// </summary>
        /// <param name="date">The date parameters.</param>
        /// <returns>The data table.</returns>
        public DataTable Lrf_Result_SportSelByDate(DateTime date)
        {
            var dataTable = new DataTable("SportSelByDate");
            using (var sql = new SqlConnection(Connection))
            {
                using (var sqlCmd = new SqlCommand("Lrf_Result_SportSelByDate", sql))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@eventdate", date);
                    sqlCmd.Parameters.AddWithValue("@licenseeid", 1);
                    sql.Open();
                    using (var sqlAdapter = new SqlDataAdapter(sqlCmd))
                    {
                        sqlAdapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        /// <summary>
        /// Get leagues.
        /// </summary>
        /// <param name="date">The date parameters.</param>
        /// <param name="sportId">The sport id.</param>
        /// <param name="isShowAll">Show all leagues.</param>
        /// <param name="language">The language code.</param>
        /// <returns>The data table.</returns>
        public DataTable Lrf_Result_LeagueSelByDate(DateTime date, int sportId, bool isShowAll, string language)
        {
            var dataTable = new DataTable("LeagueSelByDate");
            using (var sql = new SqlConnection(Connection))
            {
                using (var sqlCmd = new SqlCommand("Lrf_Result_LeagueSelByDate", sql))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@eventdate", date);
                    sqlCmd.Parameters.AddWithValue("@sporttype", sportId);
                    sqlCmd.Parameters.AddWithValue("@isshowall", isShowAll);

                    sql.Open();
                    using (var sqlAdapter = new SqlDataAdapter(sqlCmd))
                    {
                        sqlAdapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        /// <summary>
        /// Get match result.
        /// </summary>
        /// <param name="date">The date parameter.</param>
        /// <param name="sportId">The sport id.</param>
        /// <param name="leaugeId">The league id.</param>
        /// <param name="isShowAll">Show all result.</param>
        /// <param name="language">The language code.</param>
        /// <returns>The data table.</returns>
        public DataTable Lrf_Result_MatchSelByLeague(DateTime date, int sportId, int leaugeId, bool isShowAll, string language)
        {
            var dataTable = new DataTable("MatchSelByLeague");
            using (var sql = new SqlConnection(Connection))
            {
                using (var sqlCmd = new SqlCommand("Lrf_Result_MatchSelByLeague", sql))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@fromdate", date);
                    sqlCmd.Parameters.AddWithValue("@sporttype", sportId);
                    sqlCmd.Parameters.AddWithValue("@leagueid", leaugeId);
                    sqlCmd.Parameters.AddWithValue("@isshowall", isShowAll);
                    sql.Open();
                    using (var sqlAdapter = new SqlDataAdapter(sqlCmd))
                    {
                        sqlAdapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        /// <summary>
        /// Get racing result.
        /// </summary>
        /// <param name="date">The data parameters.</param>
        /// <param name="sportType">The sport id.</param>
        /// <param name="leagueId">The league id.</param>
        /// <param name="isShowAll">Show all result.</param>
        /// <returns>The data table.</returns>
        public DataTable Lrf_Result_RaceSelByMeeting(DateTime date, int sportType, int leagueId, bool isShowAll)
        {
            var dataTable = new DataTable("RaceSelByMeeting");
            using (var sql = new SqlConnection(Connection))
            {
                using (var sqlCmd = new SqlCommand("Lrf_Result_RaceSelByMeeting", sql))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@fromdate", date);
                    sqlCmd.Parameters.AddWithValue("@leagueid", leagueId);
                    sqlCmd.Parameters.AddWithValue("@isshowall", isShowAll);
                    sqlCmd.Parameters.AddWithValue("@sporttype", sportType);
                    sql.Open();
                    using (var sqlAdapter = new SqlDataAdapter(sqlCmd))
                    {
                        sqlAdapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        /// <summary>
        /// Get number game result.
        /// </summary>
        /// <param name="date">The date parameters.</param>
        /// <param name="leaugeId">The league id.</param>
        /// <param name="isShowAll">Show all result.</param>
        /// <param name="language">The language code.</param>
        /// <returns>The data table.</returns>
        public DataTable Lrf_Result_MatchSelNumberGameByLeague(DateTime date, int leaugeId, bool isShowAll, string language)
        {
            var dataTable = new DataTable("MatchSelNumberGameByLeague");
            using (var sql = new SqlConnection(Connection))
            {
                using (var sqlCmd = new SqlCommand("Lrf_Result_MatchSelNumberGameByLeague", sql))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@fromdate", date);
                    sqlCmd.Parameters.AddWithValue("@leagueid", leaugeId);
                    sqlCmd.Parameters.AddWithValue("@isshowall", isShowAll);
                    sql.Open();
                    using (var sqlAdapter = new SqlDataAdapter(sqlCmd))
                    {
                        sqlAdapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        /// <summary>
        /// Get number game result.
        /// </summary>
        /// <param name="matchId">The match id.</param>
        /// <returns>The data table.</returns>
        public DataTable Lrf_BetList_SelMatchResultNumberGame(int matchId)
        {
            var dataTable = new DataTable("SelMatchResultNumberGame");
            using (var sql = new SqlConnection(Connection))
            {
                using (var sqlCmd = new SqlCommand("Lrf_BetList_SelMatchResultNumberGame", sql))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@matchId", matchId);

                    sql.Open();
                    using (var sqlAdapter = new SqlDataAdapter(sqlCmd))
                    {
                        sqlAdapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        /// <summary>
        /// Get parlay result.
        /// </summary>
        /// <param name="refno">The referent number.</param>
        /// <returns>The data table.</returns>
        public DataTable Lrf_BetList_SelMatchResultParlay(long refno)
        {
            var dataTable = new DataTable("SelMatchResultParlay");
            using (var sql = new SqlConnection(Connection))
            {
                using (var sqlCmd = new SqlCommand("Lrf_BetList_SelMatchResultParlay", sql))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@refno", refno);

                    sql.Open();
                    using (var sqlAdapter = new SqlDataAdapter(sqlCmd))
                    {
                        sqlAdapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        /// <summary>
        /// Get results.
        /// </summary>
        /// <param name="matchId">The match id.</param>
        /// <param name="raceno">The racing number.</param>
        /// <returns>The data table.</returns>
        public DataTable Lrf_BetList_SelMatchResult(int matchId, string raceno)
        {
            var dataTable = new DataTable("SelMatchResult");
            using (var sql = new SqlConnection(Connection))
            {
                using (var sqlCmd = new SqlCommand("Lrf_BetList_SelMatchResult", sql))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@matchId", matchId);
                    sqlCmd.Parameters.AddWithValue("@raceno", string.IsNullOrEmpty(raceno) ? null : raceno);
                    sqlCmd.Parameters.AddWithValue("@licenseeid", 1);
                    sqlCmd.Parameters.AddWithValue("@userid", 5);
                    sql.Open();
                    using (var sqlAdapter = new SqlDataAdapter(sqlCmd))
                    {
                        sqlAdapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        /// <summary>
        /// The outright result.
        /// </summary>
        /// <param name="dateFrom">The start date.</param>
        /// <param name="dateTo">The end date.</param>
        /// <param name="isShowAll">Show all result.</param>
        /// <returns>The data table.</returns>
        public DataTable Lrf_Result_Outright(DateTime dateFrom, DateTime dateTo, bool isShowAll)
        {
            var dataTable = new DataTable("Result_Outright");
            using (var sql = new SqlConnection(Connection))
            {
                using (var sqlCmd = new SqlCommand("Lrf_Result_Outright", sql))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@fromdate", dateFrom);
                    sqlCmd.Parameters.AddWithValue("@todate", dateTo);
                    sqlCmd.Parameters.AddWithValue("@isshowall", isShowAll);
                    sql.Open();
                    using (var sqlAdapter = new SqlDataAdapter(sqlCmd))
                    {
                        sqlAdapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }
    }
}