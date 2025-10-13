using Fanex.Result.Core.Base;
using Fanex.Result.Core.Sportsbook.DTO;
using Fanex.Result.Sites.NET8.Models.ResultServices;

namespace Fanex.Result.Sites.NET8.Models.ResultData;

public class SportsbookResultData : BaseResultData
{
    private readonly SportsbookResultService resultService = new SportsbookResultService();


    public override List<object> GetResult()
    {
        // Placeholder implementation
        return new List<object>();
    }

    public IEnumerable<Sportsbook> GetPopupResult(SportType sportType, int licenseeId, int userId, ResultPopupMetaModel metaModel)
    {
        IEnumerable<Sportsbook> data;

            data = GetResultFromExcel(sportType, metaModel.Bettype);
            data = data.Where(item => item.MatchId == metaModel.MatchId);
        return data;
    }

    public IEnumerable<Sportsbook> GetNormalResult(DateTime date, short sportId, int leagueId, bool getDataFromExcel)
    {
        return GetResultFromExcel((SportType)sportId);
    }

    public IEnumerable<Sportsbook> GetResultFromExcel(SportType sportType, int? betType = null)
    {
        // Convert ASP.NET Framework MapPath to ASP.NET Core equivalent
        // Remove ~/ prefix and build proper path
        var fileName = $"{sportType}.xls";
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "Result_Data", fileName);
        return resultService.GetResultFromExcel(fullPath);
    }
}
