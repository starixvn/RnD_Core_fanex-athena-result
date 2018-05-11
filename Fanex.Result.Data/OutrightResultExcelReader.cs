namespace Fanex.Result.Data
{
    using System;
    using Core.Sportsbook.DTO;
    using NPOI.SS.UserModel;

    public class OutrightResultExcelReader : ResultExcelReader<Outright>
    {
        public OutrightResultExcelReader(string filePath) : base(filePath)
        {
        }

        public static OutrightResultExcelReader CreateInstance(string filePath)
        {
            return new OutrightResultExcelReader(filePath);
        }

        protected override Outright MapResultData(IRow row)
        {
            var resultData = new Outright
            {
                SportType = Convert.ToInt32(row.GetCell(0)?.ToString()),
                LeagueId = Convert.ToInt32(row.GetCell(1)?.ToString()),
                TeamId = Convert.ToInt32(row.GetCell(2)?.ToString()),
                EventDate = Convert.ToDateTime(row.GetCell(3)?.ToString()),
                Status = row.GetCell(4)?.ToString(),
            };

            return resultData;
        }
    }
}