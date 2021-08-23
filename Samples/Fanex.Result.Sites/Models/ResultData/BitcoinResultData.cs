namespace MvcExample.Models.ResultData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Hosting;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Bitcoin.DTO;
    using MetaModel;
    using ResultServices;


    public class BitcoinResultData
    {
        private readonly BitcoinResultService resultService = new BitcoinResultService();

        public IEnumerable<Bitcoin> GetResult(int sportType)
        => GetResultFromExcel(sportType);

        public IEnumerable<Bitcoin> GetPopupResult(int sportType, ResultPopupMetaModel metaModel)
        {
            var data = GetResultFromExcel(sportType);
            return data.Where(item => item.Match3rdId == metaModel.MatchId);
        }

        public IEnumerable<Bitcoin> GetResultFromExcel(int sportType)
            => resultService.GetResultFromExcel(HostingEnvironment.MapPath(string.Format(Constant.ExcelFilePath,(SportType) sportType)));
    }
}