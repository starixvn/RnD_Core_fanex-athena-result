namespace Fanex.Result.Core.UnitTest.Stub
{
    using System.Collections.Generic;
    using System.IO;
    using Fanex.Result.Core.Models;

    public class ExcelParserStub : IExcelParser
    {
        public MemoryStream Parser<T>(
           IList<T> dataList,
           SportType sportType,
           string lang,
           string templatePath,
           string excelTitle)
        {
            return new MemoryStream();
        }

        public MemoryStream ParserOutRight<T>(
            IList<T> dataList,
            string lang,
            string templatePath,
            string excelTitle)
        {
            return new MemoryStream();
        }
    }
}