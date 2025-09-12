using System;
using System.Collections.Generic;

namespace Fanex.Result.Core.Sportsbook.ViewModels.BasketBallDetail
{
    public class BasketballPlayerInfos
    {
        public BasketballPlayerInfos(string json)
        {
            Players = new List<BasketballPlayerInfo>();
            var array = json.ToLower().Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in array)
            {
                Players.Add(new BasketballPlayerInfo(item));
            }
        }

        public List<BasketballPlayerInfo> Players { get; set; }
    }

    public class BasketballPlayerInfo
    {
        public BasketballPlayerInfo(string json)
        {
            var array = json.ToLower().Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (array.Length == 2)
            {
                PlayerId = array[0];
                TeamId = array[1];
            }
        }

        public string PlayerId { get; set; }

        public string TeamId { get; set; }
    }
}
