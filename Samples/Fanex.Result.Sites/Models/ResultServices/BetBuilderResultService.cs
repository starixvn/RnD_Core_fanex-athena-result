namespace MvcExample.Models.ResultServices
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Criteria;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Fanex.Result.Data;

    public class BetBuilderResultService : IResultService<Sportsbook>
    {
        public IEnumerable<Sportsbook> GetResult(ICriteria criteria)
        {
            throw new NotSupportedException();
        }

        public IEnumerable<Sportsbook> GetPopupResult(ICriteria criteria)
        {
            throw new NotSupportedException();
        }

        public Sportsbook GetGameDetail(GetBetBuilderPopupResultCriteria criteria)
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
                            result.BetBuilderDetails.Add(BindBetBuilderDetail(reader));
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
                .GetResultData().ToList();

            sportBooks.First().BetBuilderDetails = BetBuilderDetailResultExcelReader
                .CreateInstance(filePath.Replace("BetBuilder", "BetBuilderDetail"))
                .GetResultData().ToList();

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

        protected BetBuilderDetail BindBetBuilderDetail(IDataReader reader)
        {
            return new BetBuilderDetail
            {
                SelectionType = int.Parse(reader["SelectionType"].ToString()),
                Result = reader["Result"].ToString()
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