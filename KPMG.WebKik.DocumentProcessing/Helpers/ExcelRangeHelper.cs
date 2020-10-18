using System.Collections.Generic;
using System.Linq;
using KPMG.WebKik.Models.ProjectCompanies;
using OfficeOpenXml;

namespace KPMG.WebKik.DocumentProcessing.Helpers
{
    internal static class ExcelRangeHelper
    {
        public const int DefaultCellIncrement = 3;
        public const int DefaultRowIncrement = 2;
        private const int LastColumn = 118;

        public static ExcelRange CellsInRows(this ExcelWorksheet sheet, int fromRow, int rowsCount)
        {
            return sheet.Cells[fromRow, 1, fromRow + (rowsCount - 1) * DefaultRowIncrement, LastColumn];
        }

        public static ExcelRange CellsInRow(this ExcelWorksheet sheet, int fromRow, int fromCol, int count)
        {
            return sheet.Cells[fromRow, fromCol, fromRow, fromCol + (count - 1) * DefaultCellIncrement];
        }

        public static ExcelRange OneCell(this ExcelWorksheet sheet, int fromRow, int fromCol)
        {
            return sheet.CellsInRow(fromRow, fromCol, 1);
        }

        public static ExcelRange TwoCells(this ExcelWorksheet sheet, int fromRow, int fromCol)
        {
            return sheet.CellsInRow(fromRow, fromCol, 2);
        }
    }
}
