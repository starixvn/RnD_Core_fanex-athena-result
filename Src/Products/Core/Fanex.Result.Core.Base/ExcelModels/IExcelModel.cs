namespace Fanex.Result.Core.Base.ExcelModels
{
    using System.IO;

    public interface IExcelModel
    {
        MemoryStream Export();
    }
}