namespace Fanex.Result.Core.Volleyball.DTO
{
    using System.Collections.Generic;

    public class MapBaseballResultMetadata
    {
        private static readonly IDictionary<int, MapBaseballResultMetadata> ResultTypeOrder =
            new Dictionary<int, MapBaseballResultMetadata>
        {
            { 8815, new MapBaseballResultMetadata(200, isSingleResult: true, isYesNo: true) },
            { 8820, new MapBaseballResultMetadata(400, isSingleResult: true) },
            { 8821, new MapBaseballResultMetadata(100, isSingleResult: true) },
            { 8823, new MapBaseballResultMetadata(500, isSingleResult: false, isYesNo: false, localResourceKey: "Baseball_8823XX") },
            { 8824, new MapBaseballResultMetadata(600, isSingleResult: false, isYesNo: false, localResourceKey: "Baseball_8823XX") },
            { 8827, new MapBaseballResultMetadata(300, isSingleResult: true) }
        };

        public MapBaseballResultMetadata(
            int order,
            bool isSingleResult,
            bool isYesNo = false,
            string localResourceKey = "")
        {
            Order = order;
            IsSingleResult = isSingleResult;
            IsYesNo = isYesNo;
            LocalResourceKey= localResourceKey;
        }

        public int Order { get; }

        public string LocalResourceKey { get; }

        public bool IsSingleResult { get; }

        public bool IsYesNo { get; }

        
        public static MapBaseballResultMetadata GetMapByBettypeId(int bettypeId)
            => SupportBettype(bettypeId) ? ResultTypeOrder[bettypeId] : null;

        public static bool SupportBettype(int bettypeId)
            => ResultTypeOrder.ContainsKey(bettypeId);
    }
}