namespace Fanex.Result.Core.ESport.DTO
{
    using System.Collections.Generic;

    public class MapResult
    {
        public string ResourceId { get; set; }

        public string ResourceName { get; set; }

        public string HomeValue { get; set; }

        public string AwayValue { get; set; }

        public bool IsDisplayHome { get; set; }

        public bool IsDisplayAway { get; set; }

        public bool IsDisplayBothTeam { get; set; }


    }
}