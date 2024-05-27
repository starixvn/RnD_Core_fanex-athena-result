namespace Fanex.Result.Core.SportsLottery
{
    public sealed class SportsLotteryConfigs
    {
        public string UrlPage { get; set; }

        public int Debug { get; set; }

        public int SiteId { get; set; }

        public string ProxyIP { get; set; } = string.Empty;

        public int ProxyPort { get; set; }
    }
}