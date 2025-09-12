namespace Fanex.Result.Sites.NET8.Models;

public class ResultRequest
{
    public int CustId { get; set; }
    public string TranId3rd { get; set; } = string.Empty;
    public string SiteId { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string GameId3rd { get; set; } = string.Empty;
    public int CurrencyId { get; set; }
    public int BetTypeId { get; set; }
    public string EndTime { get; set; } = string.Empty;
    public long OwTransId { get; set; }
    public string Platform { get; set; } = string.Empty;
    public string IsDarkMode { get; set; } = string.Empty;
    public string GMT { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Site { get; set; } = string.Empty;
    public string TransDesc { get; set; } = string.Empty;
}
