namespace Fanex.Result.Core.Base.Extension
{
    #region Using

    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using NPOI.SS.UserModel;
    using NPOI.SS.Util;

    #endregion Using

    /// <summary>
    /// Row index, column index, cell index are based by zero
    /// </summary>
    public static class HSSFSheetExtension
    {
        public static IRow CopyRow(this ISheet sheet, ISheet sourceSheet, int sourceRowIndex, int destRowIndex)
        {
            if (sheet.LastRowNum < destRowIndex)
            {
                for (var i = sheet.LastRowNum + 1; i <= destRowIndex; i++)
                {
                    sheet.CreateRow(i);
                }
            }

            var sourceRow = sourceSheet.GetRow(sourceRowIndex);
            if (sourceRow == null)
            {
                return null;
            }

            var newRow = sheet.GetRow(destRowIndex);
            if (newRow == null)
            {
                newRow = sheet.CreateRow(destRowIndex);
            }

            for (int i = 0; i < sourceRow.LastCellNum; i++)
            {
                var oldCell = sourceRow.GetCell(i);
                var newCell = newRow.CreateCell(i);

                sheet.CopyCell(oldCell, newCell);
            }

            return newRow;
        }

        public static IRow CopyRow(this ISheet sheet, ISheet sourceSheet, int sourceRowIndex, int destRowIndex, IList<int> cellIndexList)
        {
            if (sheet.LastRowNum < destRowIndex)
            {
                for (var i = sheet.LastRowNum + 1; i <= destRowIndex; i++)
                {
                    sheet.CreateRow(i);
                }
            }

            var sourceRow = sourceSheet.GetRow(sourceRowIndex);

            if (sourceRow == null)
            {
                return null;
            }

            var newRow = sheet.GetRow(destRowIndex);

            if (newRow == null)
            {
                newRow = sheet.CreateRow(destRowIndex);
            }

            var startIndex = 0;

            foreach (var cellIndex in cellIndexList)
            {
                var oldCell = sourceRow.GetCell(cellIndex);
                var newCell = newRow.CreateCell(startIndex);
                sheet.CopyCell(oldCell, newCell);
                startIndex++;
            }

            return newRow;
        }

        public static IRow CopyRow(this ISheet sheet, ISheet sourceSheet, int sourceRowIndex, int destRowIndex, IList<int> cellIndexList, ICellStyle defaultStyle)
        {
            if (sheet.LastRowNum < destRowIndex)
            {
                for (var i = sheet.LastRowNum + 1; i <= destRowIndex; i++)
                {
                    sheet.CreateRow(i);
                }
            }

            var sourceRow = sourceSheet.GetRow(sourceRowIndex);

            if (sourceRow == null)
            {
                return null;
            }

            var newRow = sheet.GetRow(destRowIndex);

            if (newRow == null)
            {
                newRow = sheet.CreateRow(destRowIndex);
            }

            var startIndex = 0;

            foreach (var cellIndex in cellIndexList)
            {
                var oldCell = sourceRow.GetCell(cellIndex);
                var newCell = newRow.CreateCell(startIndex);
                sheet.CopyCell(oldCell, newCell);
                startIndex++;
            }

            for (int i = cellIndexList.Count; i < newRow.Cells.Count; i++)
            {
                var cell = newRow.Cells[i];
                cell.CellStyle = defaultStyle;
                cell.SetCellValue(string.Empty);
            }

            return newRow;
        }

        public static IRow InsertCopy(this ISheet sheet, int rowNum, int sourceRowNum)
        {
            sheet.ShiftRows(rowNum, sheet.LastRowNum, 1);

            var newRow = sheet.CreateRow(rowNum);
            var sourceRow = sheet.GetRow(checked(sourceRowNum + 1));

            for (int i = 0; i < sourceRow.LastCellNum; i++)
            {
                var oldCell = sourceRow.GetCell(i);
                var newCell = newRow.CreateCell(i);

                sheet.CopyCell(oldCell, newCell);
            }

            return newRow;
        }

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Reviewed.")]
        public static ICell CopyCell(this ISheet sheet, ICell sourceCell, ICell destCell)
        {
            if (sourceCell == null)
            {
                destCell = null;
                return null;
            }

            destCell.CellStyle = sourceCell.CellStyle;

            if (destCell.CellComment != null)
            {
                destCell.CellComment = sourceCell.CellComment;
            }

            if (sourceCell.Hyperlink != null)
            {
                destCell.Hyperlink = sourceCell.Hyperlink;
            }

            destCell.SetCellType(sourceCell.CellType);

            switch (sourceCell.CellType)
            {
                case CellType.Blank:
                case CellType.Unknown:
                    destCell.SetCellValue(sourceCell.StringCellValue);
                    break;

                case CellType.Boolean:
                    destCell.SetCellValue(sourceCell.BooleanCellValue);
                    break;

                case CellType.Error:
                    destCell.SetCellErrorValue(sourceCell.ErrorCellValue);
                    break;

                case CellType.Numeric:
                    destCell.SetCellValue(sourceCell.NumericCellValue);
                    break;

                case CellType.String:
                    destCell.SetCellValue(sourceCell.RichStringCellValue);
                    break;

                default:
                    return destCell;
            }

            return destCell;
        }

        public static ICell GetCell(this ISheet sheet, int rowIndex, int colIndex)
        {
            return sheet.GetRow(rowIndex).GetCell(colIndex);
        }

        public static int MergeCells(this ISheet sheet, int firstRow, int firstCol, int lastRow, int lastCol)
        {
            var rangeAddress = new CellRangeAddress(firstRow, lastRow, firstCol, lastCol);
            return sheet.AddMergedRegion(rangeAddress);
        }

        public static void DeleteRow(this ISheet sheet, int rowIndex)
        {
            sheet.RemoveRow(sheet.GetRow(rowIndex));
        }
    }
}