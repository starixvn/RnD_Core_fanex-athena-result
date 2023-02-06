namespace MvcExample.Models.ResultServices
{
    using DTO;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using System.Collections.Generic;

    public class SportsResultService : IResultService<Sport>
    {
        public IEnumerable<Sport> GetResult(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<Sport>(criteria.GetSpParams());
            }
        }

        public IEnumerable<Sport> GetResultFromExcel(string filePath)
        {
            return new List<Sport>
            {
                new Sport { Id = 6, Name = "Volleyball" },
                new Sport { Id = 175, Name = "Bitcoin" },
                new Sport { Id = 45, Name = "Beach Volleyball" },
                new Sport { Id = 1, Name = "Soccer" },
                new Sport { Id = 50, Name = "Cricket" },
                new Sport { Id = 2, Name = "Basketball" },
                new Sport { Id = 5, Name = "Tennis" },
                new Sport { Id = 9, Name = "Badminton" },
                new Sport { Id = 10, Name = "Golf" },
                new Sport { Id = 161, Name = "NumberGame" },
                new Sport { Id = 164, Name = "Happy5" },
                new Sport { Id = 181, Name = "Virtual Horse Racing" },
                new Sport { Id = 182, Name = "Virtual Greyhounds" },
                new Sport { Id = 183, Name = "Virtual Speedway" },
                new Sport { Id = 184, Name = "Virtual Motorsports" },
                new Sport { Id = 185, Name = "Virtual Cycling" },
                new Sport { Id = 186, Name = "Virtual Tennis" },
                new Sport { Id = 192, Name = "VirtualSoccerWorldCup" },
                new Sport { Id = 193, Name = "VirtualBasketball" },
                new Sport { Id = 194, Name = "VirtualSoccerAsianCup" },
                new Sport { Id = 47, Name = "Kabaddi" },
                new Sport { Id = 48, Name = "Sepak Takraw" },
                new Sport { Id = 49, Name = "Futsal" },
                new Sport { Id = 51, Name = "Beach Soccer" },
                new Sport { Id = 52, Name = "Upcoming Sport 52" },
                new Sport { Id = 53, Name = "Upcoming Sport 53" },
                new Sport { Id = 54, Name = "Upcoming Sport 54" },
                new Sport { Id = 55, Name = "Upcoming Sport 55" },
                new Sport { Id = 56, Name = "Upcoming Sport 56" },
                new Sport { Id = 57, Name = "Upcoming Sport 57" },
                new Sport { Id = 43, Name = "ESport" },
                new Sport { Id = 231, Name = "LG Virtual Sports" },
                new Sport { Id = 233, Name = "WM" },
                new Sport { Id = 211, Name = "Allbet" },
                new Sport { Id = 244, Name = "SA Gaming" },
                new Sport { Id = 243, Name = "SexyGaming" },
                new Sport { Id = 242, Name = "GTCasino" }
            };
        }
    }
}