namespace Fanex.Result.Core.Base.DataDomain
{
    using System.Diagnostics.CodeAnalysis;

    public static class DTOType
    {
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Reviewed.")]
        public const string SPORTSBOOK = "Sportsbook";

        public const string LIVECASINO = "LiveCasino";
        public const string NUMBERGAME = "NumberGame";
        public const string RACING = "Racing";
        public const string OUTRIGHT = "Outright";
        public const string PARLAY = "Parlay";
        public const string GOLDDELUXE = "GoldDeluxe";
        public const string MICROGAMING = "MicroGaming";
        public const string METRICGAMING = "MetricGaming";
        public const string AGCASINO = "AGCasino";
        public const string ALLBETCASINO = "AllBetCasino";
    }
}