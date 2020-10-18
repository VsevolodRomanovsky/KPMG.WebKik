using System.Linq;

namespace KPMG.WebKik.DocumentProcessing
{
    internal abstract class SheetBase
    {
        protected readonly SheetRangeList Ranges = new SheetRangeList();

        internal abstract void InitRanges();

        internal void WriteValues()
        {
            foreach (var range in Ranges.Where(x => !string.IsNullOrWhiteSpace(x.Value) ))
            {
                range.WriteValue();
            }
        }
    }
}