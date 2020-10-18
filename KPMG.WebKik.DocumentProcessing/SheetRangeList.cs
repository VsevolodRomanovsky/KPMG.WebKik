using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KPMG.WebKik.DocumentProcessing
{
    public class SheetRangeList : IEnumerable<SheetRange>
    {
        private readonly List<SheetRange> ranges = new List<SheetRange>();

        public SheetRange this[int index]
        {
            get { return ranges.Single(x => x.Id == index); }
        }

        public void Add(SheetRange range)
        {
            ranges.Add(range);
        }

        public void AddRange(IEnumerable<SheetRange> ranges)
        {
            this.ranges.AddRange(ranges);
        }

        public IEnumerator<SheetRange> GetEnumerator()
        {
            return ranges.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
