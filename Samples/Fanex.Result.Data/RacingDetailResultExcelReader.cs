namespace Fanex.Result.Data
{
    using System;    
    using Fanex.Result.Core.VirtualRacing.DTO;
    using NPOI.SS.UserModel;

    public class RacingDetailResultExcelReader : ResultExcelReader<VirtualRacingDetail>
    {
        public RacingDetailResultExcelReader(string filePath) : base(filePath)
        {
        }

        public static RacingDetailResultExcelReader CreateInstance(string filePath)
        {
            return new RacingDetailResultExcelReader(filePath);
        }

        protected override VirtualRacingDetail MapResultData(IRow row)
        {
            var resultData = new VirtualRacingDetail();

            resultData.Orid = Convert.ToInt32(row.GetCell(0)?.ToString());
            resultData.EventId = Convert.ToInt32(row.GetCell(1)?.ToString());
            resultData.Placing = Convert.ToInt32(row.GetCell(2)?.ToString());
            resultData.Lane = row.GetCell(3)?.ToString();
            resultData.RacerId = Convert.ToInt32(row.GetCell(4)?.ToString());

            return resultData;
        }
    }
}