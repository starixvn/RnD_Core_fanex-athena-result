// -----------------------------------------------------------------------
// <copyright file="ResultService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace MvcExample.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data;
    using Fanex.Result.Core.DTO;
    using MvcExample.Models.Criteria;
    using MvcExample.Models.DTO;

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

        public IEnumerable<Racing> GetRacingResult(GetRacingResultCriteria criteria)
        {
            var result = new List<Racing>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(BindRacing(reader));
                    }
                }
            }
            return result;
        }

        public IEnumerable<Fanex.Result.Core.DTO.NumberGame> GetNumberGameResult(GetNumberGameResultCriteria criteria)
        {
            var result = new List<NumberGame>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BindNumberGame(result, reader);
                    }
                }
            }
            return result;
        }

        public IEnumerable<Fanex.Result.Core.DTO.LiveCasino> GetLiveCasinoResult(GetLiveCasinoResultCriteria criteria)
        {
            var result = new List<LiveCasino>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(BindLiveCasino(reader));
                    }
                }
            }
            return result;
        }

        public IEnumerable<Fanex.Result.Core.DTO.Sportbook> GetSportbookResult(GetSportbookResultCriteria criteria)
        {
            var result = new List<Sportbook>();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        result.Add(BindSportbook(reader));
                    }
                }
            }
            return result;
        }

        #endregion Normal-Report

        #region PopUp

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

        public IEnumerable<Fanex.Result.Core.DTO.Sportbook> GetSportbookPopupResult(GetSportbookPopupResultCriteria criteria)
        {
            var result = new List<Sportbook>();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        result.Add(BindSportbook(reader));
                    }
                }
            }
            return result;
        }

        #endregion PopUp

        public IEnumerable<Fanex.Result.Core.DTO.Parlay> GetParlayResult(GetParlayResultCriteria criteria)
        {
            var result = new List<Parlay>();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    var leagueId = 0;
                    var sportId = 0;
                    IList<Sportbook> sportbooks = new List<Sportbook>();
                    while (reader.Read())
                    {
                        var newLeagueId = int.Parse(reader["LeagueID"].ToString());

                        if (leagueId != newLeagueId)
                        {
                            sportId = int.Parse(reader["sporttype"].ToString());
                            if (leagueId > 0)
                            {
                                result.Add(new Parlay
                                {
                                    SportId = sportId,
                                    LeagueId = leagueId,
                                    Sportbook = sportbooks
                                });

                                sportbooks = new List<Sportbook>();
                            }
                        }
                        else
                        {
                            sportbooks.Add(BindSportbook(reader));
                        }

                        leagueId = newLeagueId;
                    }
                }
            }
            return result;
        }

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

        private static Sportbook BindSportbook(System.Data.IDataReader reader)
        {
            return new Sportbook
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
                        : new Nullable<int>(),
                HtAwayScore =
                    reader["htawayscore"] != DBNull.Value ? int.Parse(reader["htawayscore"].ToString()) : new Nullable<int>(),
                Color = reader.GetOrdinal("lcolor") >= 0 && reader["lcolor"] != DBNull.Value ? reader["lcolor"].ToString() : string.Empty,
                FinalHomeScore =
                    reader["finalhomescore"] != DBNull.Value
                        ? int.Parse(reader["finalhomescore"].ToString())
                        : new Nullable<int>(),
                HtHomeScore =
                    reader["hthomescore"] != DBNull.Value
                        ? int.Parse(reader["hthomescore"].ToString())
                        : new Nullable<int>(),
                EventDate = DateTime.Parse(reader["eventdate"].ToString()),
                IsNeutral = bool.Parse(reader["IsNeutral"].ToString()),
                IsPending = bool.Parse(reader["IsPending"].ToString()),
                KickOffTime = DateTime.Parse(reader["kickofftime"].ToString()),
                MatchCode = reader["matchcode"].ToString(),
                GoalRecord = reader["goalrecord"].ToString(),
                ResultValue = string.Empty,
                IsHTCal = Convert.ToBoolean(reader["IsHTCal"]),
                Calculate = Convert.ToBoolean(reader["Calculate"]),
                RefNo = string.Empty
            };
        }

        private static LiveCasino BindLiveCasino(System.Data.IDataReader reader)
        {
            return new LiveCasino
            {
                ShowTime = reader["showtime"].ToString(),
                LeagueId = int.Parse(reader["leagueid"].ToString()),
                EventStatus = reader["eventstatus"].ToString(),
                FinalAwayScore =
                    reader["finalawayscore"] != DBNull.Value ? int.Parse(reader["finalawayscore"].ToString()) : new Nullable<int>(),
                FinalHomeScore =
                    reader["finalhomescore"] != DBNull.Value ? int.Parse(reader["finalhomescore"].ToString()) : new Nullable<int>(),
                EventDate = DateTime.Parse(reader["eventdate"].ToString()),
                IsPending = bool.Parse(reader["IsPending"].ToString()),
                KickOffTime = DateTime.Parse(reader["kickofftime"].ToString()),
                IsHTCal = Convert.ToBoolean(reader["IsHTCal"]),
                Calculate = Convert.ToBoolean(reader["Calculate"]),
                BetStatus = reader["betstatus"] != DBNull.Value ? reader["betstatus"].ToString() : string.Empty,
                Bettype = int.Parse(reader["homeid2"].ToString())
            };
        }

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
                    IsPending = reader.GetOrdinal("isrefund") >= 0 && reader["isrefund"].ToString() != "0",
                    KickOffTime = DateTime.Parse(reader["kickofftime"].ToString()),
                    MatchCode = reader["matchcode"].ToString(),
                    Seg = new List<int> { int.Parse(reader["seq"].ToString()) },
                    Ball = new List<int> { int.Parse(reader["number"].ToString()) }
                });
            }
        }

        #endregion Utilites
    }
}