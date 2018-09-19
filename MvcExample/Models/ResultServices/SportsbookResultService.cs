namespace MvcExample.Models.ResultServices
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Criteria;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Fanex.Result.Data;

    public class SportsbookResultService : IResultService<Sportsbook>
    {
        public IEnumerable<Sportsbook> GetResult(ICriteria criteria)
        {
            var results = new List<Sportsbook>();
            var gameDetails = new List<GameDetail>();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        results.Add(BindData(reader));
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

        public IEnumerable<Sportsbook> GetPopupResult(ICriteria criteria)
        {
            var sportsBookCriteria = criteria as GetSportbookPopupResultCriteria;
            var results = new List<Sportsbook>();
            var gameDetails = new List<GameDetail>();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        var item = BindData(reader);
                        item.LiveHomeScore = reader["livehomescore"] != DBNull.Value ? int.Parse(reader["livehomescore"].ToString()) : new int?();
                        item.LiveAwayScore = reader["liveawayscore"] != DBNull.Value ? int.Parse(reader["liveawayscore"].ToString()) : new int?();

                        item.Bettype = sportsBookCriteria.Bettype;
                        item.BetID = sportsBookCriteria.BetId.ToString();
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

        public Sportsbook GetGameDetail(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    var result = new Sportsbook();
                    while (reader.Read())
                    {
                        result = BindData(reader);
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

        public IEnumerable<Sportsbook> GetResultFromExcel(string filePath)
        {
            var sportBooks = SportsbookResultExcelReader
                .CreateInstance(filePath)
                .GetResultData();

            if (filePath.Contains($"\\{SportType.Soccer.ToString()}.xls"))
            {
                sportBooks.First().GameDetails = SportsbookDetailResultExcelReader
                    .CreateInstance(filePath.Replace("Soccer", "SoccerDetail"))
                    .GetResultData().ToList();
            }

            if (filePath.Contains(SportType.VirtualBasketBall.ToString()))
            {
                sportBooks.First().GameDetails = SportsbookDetailResultExcelReader
                    .CreateInstance(filePath.Replace("VirtualBasketBall", "VirtualBasketBallDetail"))
                    .GetResultData().ToList();
            }

            if (filePath.Contains(SportType.ESport.ToString()))
            {
                sportBooks.First().GameDetails = SportsbookDetailResultExcelReader
                    .CreateInstance(filePath.Replace("ESport", "ESportDetail"))
                    .GetResultData().ToList();
            }

            return sportBooks;
        }

        protected Sportsbook BindData(IDataReader reader)
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
                //LiveHomeScore =
                //    reader["livehomescore"] != DBNull.Value
                //        ? int.Parse(reader["livehomescore"].ToString())
                //        : new int?(),
                //LiveAwayScore =
                //    reader["liveawayscore"] != DBNull.Value
                //        ? int.Parse(reader["liveawayscore"].ToString())
                //        : new int?(),
                EventDate = DateTime.Parse(reader["eventdate"].ToString()),
                IsNeutral = bool.Parse(reader["IsNeutral"].ToString()),
                KickOffTime = reader["kickofftime"].ToString() == "" ? DateTime.Now : DateTime.Parse(reader["kickofftime"].ToString()),
                MatchCode = reader["matchcode"].ToString(),
                GoalRecord = reader["goalrecord"].ToString(),
                IsHTCal = Convert.ToBoolean(reader["IsHTCal"]),
                Calculate = Convert.ToBoolean(reader["Calculate"]),
                SettlementStatus = Convert.ToByte(reader["settlementstatus"]),
                RefNo = string.Empty,
                GameDetails = new List<GameDetail>(),
                Bettype = GetBetTypeFromToken(reader),
                BetID = GetBetIDFromToken(reader)
            };

            return item;
        }

        protected GameDetail BindGameDetail(IDataReader reader)
        {
            return new GameDetail
            {
                MatchId = int.Parse(reader["matchid"].ToString()),
                ResourceId = reader["ResourceId"].ToString(),
                HomeValue = reader["HomeValue"].ToString(),
                AwayValue = reader["AwayValue"].ToString(),
                ResultStatusID = Convert.IsDBNull(reader["ResultStatusID"]) ? 0 : int.Parse(reader["ResultStatusID"].ToString()),
                SettlementStatusID = Convert.IsDBNull(reader["SettlementStatusID"]) ? 0 : int.Parse(reader["SettlementStatusID"].ToString()),
                OtherInfo = reader["OtherInfo"].ToString()
            };
        }

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
    }
}