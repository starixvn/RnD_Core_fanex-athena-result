namespace Fanex.Result.Core.Base.Extension
{
    using NPOI.SS.UserModel;

    public static class HSSFWorkbookExtension
    {
        public static ISheet CloneSheet(this IWorkbook workbook, string sourceSheetName, string newSheetName)
        {
            var sheetIndex = workbook.GetSheetIndex(sourceSheetName);
            var newSheet = workbook.CloneSheet(sheetIndex);
            var newSheetIndex = workbook.GetSheetIndex(newSheet);
            workbook.SetSheetName(newSheetIndex, newSheetName);
            return newSheet;
        }

        public static void RemoveSheet(this IWorkbook workbook, string sheetName)
        {
            var sheet = workbook.GetSheet(sheetName);

            if (sheet != null)
            {
                var sheetIndex = workbook.GetSheetIndex(sheet);
                workbook.RemoveSheetAt(sheetIndex);
            }
        }
    }
}