namespace Fanex.Result.Core.ESport.DTO
{
    using System.Collections.Generic;

    public class MapResultMetadata
    {
#pragma warning disable S3887 // Mutable, non-private fields should not be "readonly"
#pragma warning disable S2386 // Mutable fields should not be "public static"
        public static readonly IDictionary<int, MapResultMetadata> ResultTypeOrder = 
            new Dictionary<int, MapResultMetadata>
        {
            { 9006, new MapResultMetadata(1, true) },
            { 9007, new MapResultMetadata(2, true) },
            { 9002, new MapResultMetadata(3, false) },
            { 9011, new MapResultMetadata(4, true) },
            { 9008, new MapResultMetadata(5, false) },
            { 9015, new MapResultMetadata(6, true) },
            { 9016, new MapResultMetadata(7, true) },
            { 9017, new MapResultMetadata(8, true) },
            { 9012, new MapResultMetadata(9, false) },
            { 9021, new MapResultMetadata(10, true) },
            { 9022, new MapResultMetadata(11, true) },
            { 9023, new MapResultMetadata(12, true) },
            { 9018, new MapResultMetadata(13, false) },
            { 9027, new MapResultMetadata(14, true) },
            { 9024, new MapResultMetadata(15, false) },
            { 9031, new MapResultMetadata(16, true) },
            { 9032, new MapResultMetadata(17, true) },
            { 9033, new MapResultMetadata(18, true) },
            { 9028, new MapResultMetadata(19, false) },
            { 9037, new MapResultMetadata(20, true) },
            { 9038, new MapResultMetadata(21, true) },
            { 9039, new MapResultMetadata(22, true) },
            { 9034, new MapResultMetadata(23, false) },
            { 9043, new MapResultMetadata(24, true) },
            { 9044, new MapResultMetadata(25, true) },
            { 9045, new MapResultMetadata(26, true) },
            { 9040, new MapResultMetadata(27, false) },
            { 9049, new MapResultMetadata(28, true) },
            { 9050, new MapResultMetadata(29, true) },
            { 9051, new MapResultMetadata(30, true) },
            { 9046, new MapResultMetadata(31, false) },
            { 9055, new MapResultMetadata(32, true) },
            { 9056, new MapResultMetadata(33, true) },
            { 9057, new MapResultMetadata(34, true) },
            { 9052, new MapResultMetadata(35, false) },
            { 9058, new MapResultMetadata(36, true, isDuration: true) },
            { 9062, new MapResultMetadata(37, true) },
            { 9063, new MapResultMetadata(38, true) },
            { 9067, new MapResultMetadata(39, true) },
            { 9064, new MapResultMetadata(40, true) },
            { 9059, new MapResultMetadata(41, false) },
            { 9074, new MapResultMetadata(42, false) },
            { 9065, new MapResultMetadata(43, false) },
            { 9066, new MapResultMetadata(44, false) },
            { 9075, new MapResultMetadata(45, true, isYesNo: true) },
            { 9068, new MapResultMetadata(46, true) },
            { 9072, new MapResultMetadata(47, true) },
            { 9069, new MapResultMetadata(48, false) },
            { 9073, new MapResultMetadata(49, true, isYesNo: true) }
        };
#pragma warning restore S2386 // Mutable fields should not be "public static"
#pragma warning restore S3887 // Mutable, non-private fields should not be "readonly"

        public MapResultMetadata(int order, bool isSingleResult, bool isYesNo = false, bool isDuration = false)
        {
            Order = order;
            IsSingleResult = isSingleResult;
            IsYesNo = isYesNo;
            IsDuration = isDuration;
        }

        public int Order { get; }

        public bool IsSingleResult { get; }

        public bool IsYesNo { get; }

        public bool IsDuration { get; }
    }
}