namespace MvcExample.Models.ResultServices
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Fanex.Result.Core.VirtualRacing.DTO;
    using Fanex.Result.Data;

    public class RacingResultService : IResultService<VirtualRacing>
    {
        public IEnumerable<VirtualRacing> GetResult(ICriteria criteria)
        {
            throw new NotImplementedException();
        }

        private static VirtualRacing BindData(IDataReader reader)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VirtualRacing> GetResultFromExcel(string filePath)
        {
            var racing = RacingResultExcelReader.CreateInstance(filePath).GetResultData();

            var details = RacingDetailResultExcelReader.CreateInstance(filePath.Replace("VirtualHorseRacing", "VirtualHorseRacingDetail")).GetResultData().ToList();

            foreach (var item in racing)
            {
                item.Details = details.Where(i => i.EventId == item.EventId).ToList();
            }

            return racing;
        }
    }
}