namespace Fanex.Result.Core.Base.Extension
{
    using System;
    using NPOI.SS.UserModel;

    public static class HSSFCellExtension
    {
        public static void SetCellValue(this ICell cell, decimal value)
        {
            cell.SetCellValue(Convert.ToDouble(value));
        }

        public static void SetCellValue(this ICell cell, object value)
        {
            var valueType = value.GetType();

            if (valueType == typeof(bool))
            {
                cell.SetCellValue(Converter.To<bool>(value));
                return;
            }

            if (valueType == typeof(DateTime))
            {
                cell.SetCellValue(Converter.To<DateTime>(value));
                return;
            }

            if (valueType == typeof(decimal))
            {
                cell.SetCellValue(Converter.To<decimal>(value));
                return;
            }

            if (valueType == typeof(double))
            {
                cell.SetCellValue(Converter.To<double>(value));
                return;
            }

            if (valueType == typeof(int))
            {
                cell.SetCellValue(Converter.To<int>(value));
                return;
            }

            if (valueType == typeof(long))
            {
                cell.SetCellValue(Converter.To<long>(value));
                return;
            }

            if (valueType == typeof(string))
            {
                cell.SetCellValue(Converter.To<string>(value));
                return;
            }
        }

        public static void SetCellValue(this ICell cell, long value)
        {
            cell.SetCellValue(Convert.ToDouble(value));
        }

        public static void SetEmptyCell(this ICell cell, object value, ICellStyle defaultStyle)
        {
            switch (value.GetType().Name)
            {
                case "String":
                    cell.SetCellValue((string)value);
                    break;

                case "Decimal":
                    cell.SetCellValue((decimal)value);
                    break;

                default:
                    break;
            }

            cell.CellStyle = defaultStyle;
        }
    }
}