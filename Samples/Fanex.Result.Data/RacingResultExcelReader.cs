namespace Fanex.Result.Data
{
    using System;    
    using Fanex.Result.Core.VirtualRacing.DTO;
    using NPOI.SS.UserModel;

    public class RacingResultExcelReader : ResultExcelReader<VirtualRacing>
    {
        public RacingResultExcelReader(string filePath) : base(filePath)
        {
        }

        public static RacingResultExcelReader CreateInstance(string filePath)
        {
            return new RacingResultExcelReader(filePath);
        }

        protected override VirtualRacing MapResultData(IRow row)
        {
            var resultData = new VirtualRacing();

            resultData.EventId = Convert.ToInt32(row.GetCell(0)?.ToString());
            resultData.LeagueId = Convert.ToInt32(row.GetCell(1)?.ToString());
            resultData.VendorMatchId = Convert.ToInt32(row.GetCell(2)?.ToString());
            resultData.Distance = row.GetCell(3).ToString();
            resultData.EventTime = Convert.ToDateTime(row.GetCell(4)?.ToString());
            resultData.EventStatus = row.GetCell(5)?.ToString();
            resultData.BetStatus = row.GetCell(6)?.ToString();

            return resultData;
        }
    }
}