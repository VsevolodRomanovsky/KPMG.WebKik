using KPMG.WebKik.DocumentProcessing.Helpers;
using OfficeOpenXml;

namespace KPMG.WebKik.DocumentProcessing
{
    public class SheetRange
    {
        private readonly int nextCellIncrement;
        private readonly int nextRowIncrement;
        private string value;

        public int Id;

        private readonly ExcelRange range;
        internal bool DontSplit;

        public string Value
        {
            get { return value; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.value = value;
                }
            }
        }

        public SheetRange(ExcelRange range, int nextCellIncrement = ExcelRangeHelper.DefaultCellIncrement, int nextRowIncrement = ExcelRangeHelper.DefaultRowIncrement)
        {
            this.range = range;
            this.nextCellIncrement = nextCellIncrement;
            this.nextRowIncrement = nextRowIncrement;
        }

        public void WriteValue()
        {
            if (DontSplit)
            {
                GetCell(0, 0).Value = Value;
                return;
            }

            var x = 0;
            var y = 0;
            foreach (var ch in Value)
            {
                var cell = GetCell(x, y);
                if (!RangeContains(cell))
                {
                    y = y + nextRowIncrement;
                    x = 0;
                    cell = GetCell(x, y);

                    if (range.Rows == 1 || !RangeContains(cell))
                    {
                        return;
                    }
                }
                cell.Value = ch.ToString();
                x = x + nextCellIncrement;
            }
        }

        private ExcelRange GetCell(int x, int y)
        {
            return range.Worksheet.Cells[range.Start.Row + y, range.Start.Column + x];
        }

        private bool RangeContains(ExcelAddressBase cell)
        {
            return cell.Start.Row >= range.Start.Row &&
                    cell.Start.Column >= range.Start.Column &&
                    cell.End.Row <= range.End.Row &&
                    cell.End.Column <= range.End.Column;
        }
    }
}
