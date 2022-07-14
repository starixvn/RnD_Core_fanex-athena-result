namespace Fanex.Result.Core.Volleyball.DTO
{
    using System.Collections.Generic;

    public class MapVolleyballResultMetadata
    {
        private static readonly IDictionary<int, MapVolleyballResultMetadata> ResultTypeOrder =
            new Dictionary<int, MapVolleyballResultMetadata>
        {
            { 3901, new MapVolleyballResultMetadata(100, true) },
            { 3914, new MapVolleyballResultMetadata(200, true, isYesNo:true) },
            { 3903, new MapVolleyballResultMetadata(300, false, localResourceKey: "Volleyball_3903XXYY") },
            { 3909, new MapVolleyballResultMetadata(400, true, localResourceKey: "Volleyball_3909XXYY") },
            { 3904, new MapVolleyballResultMetadata(300, false, localResourceKey: "Volleyball_3903XXYY") },
            { 3905, new MapVolleyballResultMetadata(300, false, localResourceKey: "Volleyball_3903XXYY") },
            { 3912, new MapVolleyballResultMetadata(300, false, localResourceKey: "Volleyball_3903XXYY") },            
            { 3915, new MapVolleyballResultMetadata(500, false, localResourceKey: "Volleyball_3915XXYY") },
            { 3916, new MapVolleyballResultMetadata(600, false, localResourceKey: "Volleyball_3915XXYY") }
        };

        public MapVolleyballResultMetadata(
            int order,
            bool isSingleResult,
            bool isYesNo = false,
            string localResourceKey = "",
            bool isSingleScore = false)
        {
            Order = order;
            IsSingleResult = isSingleResult;
            IsSingleScore = isSingleScore;
            IsYesNo = isYesNo;
            LocalResourceKey = localResourceKey;
        }

        public int Order { get; }

        public bool IsSingleResult { get; }

        public bool IsSingleScore { get; }

        public bool IsYesNo { get; }

        public string LocalResourceKey { get; }

        public static MapVolleyballResultMetadata GetMapByBettypeId(int bettypeId)
            => SupportBettype(bettypeId) ? ResultTypeOrder[bettypeId] : null;

        public static bool SupportBettype(int bettypeId)
            => ResultTypeOrder.ContainsKey(bettypeId);
    }
}