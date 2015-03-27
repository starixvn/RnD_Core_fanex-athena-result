// -----------------------------------------------------------------------
// <copyright file="ResultService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace MvcExample.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Fanex.Data;
    using Fanex.Result.Core.DTO;
    using MvcExample.Models.Criteria;
    using MvcExample.Models.DTO;

    /// <summary>
    /// The result service.
    /// </summary>
    public class ResultService : IResultService
    {
        public IEnumerable<Sport> GetSports(GetSportByDateCriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<Sport>(criteria.GetSpParams());
            }
        }

        public IEnumerable<League> GetLeagues(GetLeaguesBySportCriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<League>(criteria.GetSpParams());
            }
        }

        #region Normal-Report

        /// <summary>
        /// Get game detail.
        /// </summary>
        /// <param name="criteria">The match ID.</param>
        /// <returns>The SPORTSBOOK data.</returns>
        public Sportsbook GetGameDetail(GetGameDetailCriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    var result = new Sportsbook();
                    while (reader.Read())
                    {
                        result = BindSportbook(reader);
                    }

                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            result.GameDetails.Add(BindGameDetail(reader));
                        }
                    }

                    return result;
                }
            }
        }

        /// <summary>
        /// Get racing result.
        /// </summary>
        /// <param name="criteria">The criterion.</param>
        /// <returns>The racing data.</returns>
        public IEnumerable<Racing> GetRacingResult(GetRacingResultCriteria criteria)
        {
            var result = new List<Racing>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        result.Add(BindRacing(reader));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get number game result.
        /// </summary>
        /// <param name="criteria">The criterion.</param>
        /// <returns>The number game data.</returns>
        public IEnumerable<Fanex.Result.Core.DTO.NumberGame> GetNumberGameResult(GetNumberGameResultCriteria criteria)
        {
            var result = new List<NumberGame>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        BindNumberGame(result, reader);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get live casino result.
        /// </summary>
        /// <param name="criteria">The criterion.</param>
        /// <returns>The live casino data.</returns>
        public IEnumerable<Fanex.Result.Core.DTO.LiveCasino> GetLiveCasinoResult(GetLiveCasinoResultCriteria criteria)
        {
            var result = new List<LiveCasino>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        result.Add(BindLiveCasino(reader));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get SPORTBOOK result.
        /// </summary>
        /// <param name="criteria">The criterion.</param>
        /// <returns>The SPORTBOOK data.</returns>
        public IEnumerable<Fanex.Result.Core.DTO.Sportsbook> GetSportbookResult(GetSportbookResultCriteria criteria)
        {
            var results = new List<Sportsbook>();
            var gameDetails = new List<GameDetail>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        results.Add(BindSportbook(reader));
                    }

                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            gameDetails.Add(BindGameDetail(reader));
                        }
                    }
                }
            }

            foreach (var result in results)
            {
                result.GameDetails = gameDetails.Where(i => i.MatchId == result.MatchId).ToList();
            }

            return results;
        }

        #endregion Normal-Report

        #region PopUp

        /// <summary>
        /// Get racing POPUP result.
        /// </summary>
        /// <param name="criteria">The criterion.</param>
        /// <returns>The racing POPUP data.</returns>
        public IEnumerable<Fanex.Result.Core.DTO.Racing> GetRacingPopupResult(GetRacingPopupResultCriteria criteria)
        {
            var result = new List<Racing>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        result.Add(BindRacing(reader));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get number game POPUP result.
        /// </summary>
        /// <param name="criteria">The criterion.</param>
        /// <returns>The number game POPUP data.</returns>
        public IEnumerable<Fanex.Result.Core.DTO.NumberGame> GetNumberGamePopupResult(GetNumberGamePopupResultCriteria criteria)
        {
            var result = new List<NumberGame>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        BindNumberGame(result, reader);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get live casino POPUP result.
        /// </summary>
        /// <param name="criteria">The criterion.</param>
        /// <returns>The live casino POPUP data.</returns>
        public IEnumerable<Fanex.Result.Core.DTO.LiveCasino> GetLiveCasinoPopupResult(GetLiveCasinoPopupResultCriteria criteria)
        {
            var result = new List<LiveCasino>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        result.Add(BindLiveCasino(reader));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get SPORTBOOK POPUP result.
        /// </summary>
        /// <param name="criteria">The criterion.</param>
        /// <returns>The SPORTBOOK POPUP data.</returns>
        public IEnumerable<Sportsbook> GetSportbookPopupResult(GetSportbookPopupResultCriteria criteria)
        {
            var results = new List<Sportsbook>();
            var gameDetails = new List<GameDetail>();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        var item = BindSportbook(reader);
                        item.Bettype = criteria.Bettype;
                        item.BetID = criteria.BetId.ToString();
                        results.Add(item);
                    }

                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            gameDetails.Add(BindGameDetail(reader));
                        }
                    }
                }
            }

            if (gameDetails.Count > 0)
            {
                foreach (var result in results)
                {
                    result.GameDetails.AddRange(gameDetails.Where(i => i.MatchId == result.MatchId));
                }
            }

            return results;
        }

        #endregion PopUp

        /// <summary>
        /// Get Parlay result.
        /// </summary>
        /// <param name="criteria">The criterion.</param>
        /// <returns>The Parlay data.</returns>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2101:MethodMustNotContainMoreLinesThan",
     Justification = "Reviewed. Suppression is OK here.")]
        public IEnumerable<Fanex.Result.Core.DTO.Parlay> GetParlayResult(GetParlayResultCriteria criteria)
        {
            var results = new List<Parlay>();
            var gameDetails = new List<GameDetail>();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    var leagueId = 0;
                    var sportId = 0;
                    IList<Sportsbook> sportbooks = new List<Sportsbook>();
                    while (reader.Read())
                    {
                        var newLeagueId = int.Parse(reader["LeagueID"].ToString());
                        var newsportId = int.Parse(reader["sporttype"].ToString());

                        if (leagueId != newLeagueId && leagueId > 0)
                        {
                            results.Add(new Parlay
                            {
                                SportId = sportId,
                                LeagueId = leagueId,
                                Sportbook = sportbooks,
                            });
                            sportbooks = new List<Sportsbook>();
                        }

                        sportbooks.Add(BindSportbook(reader));

                        leagueId = newLeagueId;
                        sportId = newsportId;
                    }

                    /* add the latest row*/
                    results.Add(new Parlay
                    {
                        SportId = sportId,
                        LeagueId = leagueId,
                        Sportbook = sportbooks
                    });
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            gameDetails.Add(BindGameDetail(reader));
                        }
                    }

                    if (gameDetails.Count > 0)
                    {
                        foreach (var result in results)
                        {
                            foreach (var sb in result.Sportbook)
                            {
                                sb.GameDetails.AddRange(gameDetails.Where(i => i.MatchId == sb.MatchId));
                            }
                        }
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Get Out Right result.
        /// </summary>
        /// <param name="criteria">The criterion.</param>
        /// <returns>The Out Right data.</returns>
        public IEnumerable<Fanex.Result.Core.DTO.OutRight> GetOutRightResult(GetOutRightResult criteria)
        {
            var result = new List<OutRight>();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        result.Add(new OutRight
                        {
                            EventDate = DateTime.Parse(reader["eventdate"].ToString()),
                            SportType = int.Parse(reader["sporttype"].ToString()),
                            LeagueId = int.Parse(reader["leagueid"].ToString()),
                            TeamId = int.Parse(reader["teamid"].ToString()),
                            Status = reader["status"].ToString()
                        });
                    }
                }
            }

            return result;
        }

        #region Utilites

        /// <summary>
        /// Bind data from IDATAREADER to DTO.
        /// </summary>
        /// <param name="reader">The IDATAREADER.</param>
        /// <returns>The SPORTBOOK DTO.</returns>
        private static Sportsbook BindSportbook(System.Data.IDataReader reader)
        {
            var item = new Sportsbook
            {
                AwayId = int.Parse(reader["awayID"].ToString()),
                HomeId = int.Parse(reader["homeID"].ToString()),
                BetStatus =
                    reader["betstatus"] != DBNull.Value ? reader["betstatus"].ToString() : string.Empty,
                LeagueId = int.Parse(reader["leagueid"].ToString()),
                MatchId = int.Parse(reader["matchid"].ToString()),
                EventStatus = reader["eventstatus"].ToString(),
                FinalAwayScore =
                    reader["finalawayscore"] != DBNull.Value
                        ? int.Parse(reader["finalawayscore"].ToString())
                        : new int?(),
                HtAwayScore =
                    reader["htawayscore"] != DBNull.Value ? int.Parse(reader["htawayscore"].ToString()) : new int?(),
                Color = ColumnExisted(reader, "lcolor") && reader["lcolor"] != DBNull.Value ? reader["lcolor"].ToString() : string.Empty,
                FinalHomeScore =
                    reader["finalhomescore"] != DBNull.Value
                        ? int.Parse(reader["finalhomescore"].ToString())
                        : new int?(),
                HtHomeScore =
                    reader["hthomescore"] != DBNull.Value
                        ? int.Parse(reader["hthomescore"].ToString())
                        : new int?(),
                EventDate = DateTime.Parse(reader["eventdate"].ToString()),
                IsNeutral = bool.Parse(reader["IsNeutral"].ToString()),
                IsPending = bool.Parse(reader["IsPending"].ToString()),
                KickOffTime = DateTime.Parse(reader["kickofftime"].ToString()),
                MatchCode = reader["matchcode"].ToString(),
                GoalRecord = reader["goalrecord"].ToString(),
                IsHTCal = Convert.ToBoolean(reader["IsHTCal"]),
                Calculate = Convert.ToBoolean(reader["Calculate"]),
                RefNo = string.Empty,
                GameDetails = new List<GameDetail>(),
                Bettype = GetBetTypeFromToken(reader),
                BetID = GetBetIDFromToken(reader)
            };

            return item;
        }

        /// <summary>
        /// Bind data from IDATAREADER to DTO.
        /// </summary>
        /// <param name="reader">The IDATAREADER.</param>
        /// <returns>The live casino DTO.</returns>
        private static LiveCasino BindLiveCasino(System.Data.IDataReader reader)
        {
            return new LiveCasino
            {
                ShowTime = reader["showtime"].ToString(),
                LeagueId = int.Parse(reader["leagueid"].ToString()),
                EventStatus = reader["eventstatus"].ToString(),
                FinalAwayScore =
                    reader["finalawayscore"] != DBNull.Value ? int.Parse(reader["finalawayscore"].ToString()) : new int?(),
                FinalHomeScore =
                    reader["finalhomescore"] != DBNull.Value ? int.Parse(reader["finalhomescore"].ToString()) : new int?(),
                EventDate = DateTime.Parse(reader["eventdate"].ToString()),
                IsPending = bool.Parse(reader["IsPending"].ToString()),
                KickOffTime = DateTime.Parse(reader["kickofftime"].ToString()),
                IsHTCal = Convert.ToBoolean(reader["IsHTCal"]),
                Calculate = Convert.ToBoolean(reader["Calculate"]),
                BetStatus = reader["betstatus"] != DBNull.Value ? reader["betstatus"].ToString() : string.Empty,
                Bettype = int.Parse(reader["homeid2"].ToString())
            };
        }

        /// <summary>
        /// Bind data from IDATAREADER to DTO.
        /// </summary>
        /// <param name="reader">The IDATAREADER.</param>
        /// <returns>The Racing DTO.</returns>
        private static Racing BindRacing(System.Data.IDataReader reader)
        {
            return new Racing
            {
                LeagueId = int.Parse(reader["meetingid"].ToString()),
                EngineId = reader["HorseId"].ToString(),
                EngineNo = reader["HorseNo"].ToString(),
                IsFavor = reader["IsFavor"] != DBNull.Value && reader["IsFavor"].ToString().Equals("1") ? true : false,
                IsSCR = reader["IsSCR"] != DBNull.Value && reader["IsSCR"].ToString().Equals("1") ? true : false,
                Place = int.Parse(reader["place"].ToString()),
                PlaceOdds = decimal.Parse(reader["placeodds"].ToString()),
                WinOdds = decimal.Parse(reader["odds"].ToString()),
                RaceName = reader["racename"] != DBNull.Value ? int.Parse(reader["racename"].ToString()) : 0,
                KickOffTime = reader["kickofftime"] != DBNull.Value ? DateTime.Parse(reader["kickofftime"].ToString()) : DateTime.MinValue,
                Favourite = reader["IsFavor"] != DBNull.Value ? reader["IsFavor"].ToString() : string.Empty,
                RowCount = int.Parse(reader["rowcount"].ToString()),
                SCROfMatch = reader["SCR"].ToString(),
                TFOfMatch = reader["TF"].ToString()
            };
        }

        /// <summary>
        /// Bind data from IDATAREADER to DTO.
        /// </summary>
        /// <param name="result">The result list.</param>
        /// <param name="reader">The IDATAREADER.</param>
        private static void BindNumberGame(List<NumberGame> result, System.Data.IDataReader reader)
        {
            var matchId = int.Parse(reader["matchid"].ToString());
            var existMatch = result.SingleOrDefault(i => i.MatchId == matchId);
            if (existMatch != null)
            {
                existMatch.Seg.Add(int.Parse(reader["seq"].ToString()));
                existMatch.Ball.Add(int.Parse(reader["number"].ToString()));
            }
            else
            {
                result.Add(new NumberGame
                {
                    LeagueId = int.Parse(reader["leagueid"].ToString()),
                    MatchId = int.Parse(reader["matchid"].ToString()),
                    EventStatus = reader["eventstatus"].ToString(),
                    EventDate = DateTime.Parse(reader["eventdate"].ToString()),
                    KickOffTime = DateTime.Parse(reader["kickofftime"].ToString()),
                    MatchCode = reader["matchcode"].ToString(),
                    Seg = new List<int> { int.Parse(reader["seq"].ToString()) },
                    Ball = new List<int> { int.Parse(reader["number"].ToString()) }
                });
            }
        }

        /// <summary>
        /// Check the column existed in data reader.
        /// </summary>
        /// <param name="reader">The data reader.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>True or False.</returns>
        private static bool ColumnExisted(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Bind game detail value.
        /// </summary>
        /// <param name="reader">The data reader.</param>
        /// <returns>The game detail value.</returns>
        private static GameDetail BindGameDetail(IDataReader reader)
        {
            return new GameDetail
            {
                MatchId = int.Parse(reader["matchid"].ToString()),
                ResourceId = reader["ResourceId"].ToString(),
                HomeValue = reader["HomeValue"].ToString(),
                AwayValue = reader["AwayValue"].ToString(),
                ResultStatusID = int.Parse(reader["ResultStatusID"].ToString()),
                SettlementStatusID = int.Parse(reader["SettlementStatusID"].ToString()),
                OtherInfo=reader["OtherInfo"].ToString()
            };
        }

        private static int GetBetTypeFromToken(IDataReader reader)
        {
            const string BettypeColumn = "bettype";
            if (ColumnExisted(reader, BettypeColumn))
            {
                return int.Parse(reader[BettypeColumn].ToString());
            }

            return 0;
        }

        private static string GetBetIDFromToken(IDataReader reader)
        {
            const string BetcheckColumn = "betcheck";
            if (ColumnExisted(reader, BetcheckColumn))
            {
                return reader[BetcheckColumn].ToString();
            }

            return string.Empty;
        }

        #endregion Utilites
    }
}