using System.Collections.Generic;
using KPMG.WebKik.DocumentProcessing.Helpers;
using KPMG.WebKik.DocumentProcessing.NotificationOfParticipation.Models;
using OfficeOpenXml;
using System.Linq;

namespace KPMG.WebKik.DocumentProcessing.NotificationOfParticipation.Sheets
{
    internal class NPSheetG : NPCompanySheetBase
    {
        private readonly CompanyChain chain;
        public NPSheetG(ExcelWorksheet sheet, CompanyChain chain, int pageNumber)
            : base(sheet, chain.TargetCompany, pageNumber)
        {
            this.chain = chain;
        }

        protected override ExcelRange CompanyNumberRange => Sheet.CellsInRow(15, 61, 8);

        internal override void InitRanges()
        {
            base.InitRanges();

            Ranges.AddRange(new List<SheetRange>()
                {
                    new SheetRange(Sheet.CellsInRows(19, 4)) {Value = Company.FullName},//1.2. Полное наименование (в русской транскрипции)
                    new SheetRange(Sheet.CellsInRow(27, 61, 3))  { Value = TotalIndirectSharePart.ToInt().ToString("D3") }, //1.3. Доля косвенного участия - итого, %
                    new SheetRange(Sheet.CellsInRow(27, 73, 5)) { Value = TotalIndirectSharePart.GetNumbersAfterDot(5) }, //1.3. Доля косвенного участия - итого, %
                    new SheetRange(Sheet.CellsInRow(31, 61, 5)) { Value = chain.Number.ToString("D5") }, //2.1. Номер последовательности участия
                    new SheetRange(Sheet.CellsInRow(33, 61, 3))  { Value = chain.IndirectSharePart.ToInt().ToString("D3") }, //2.2. Доля косвенного участия в последовательности - итого, %
                    new SheetRange(Sheet.CellsInRow(33, 73, 5)) { Value = chain.IndirectSharePart.GetNumbersAfterDot(5) }, //2.2. Доля косвенного участия в последовательности - итого, %
                });

            var index = 0;
            foreach (var participant in chain.Participants)
            {
                Ranges.AddRange(GetParticipantRanges(index, participant));
                index++;
            }
        }

        private double TotalIndirectSharePart => Company.FactShare.ShareFactPart - Company.FactShare.ShareDirectPart;

        private IEnumerable<SheetRange> GetParticipantRanges(int index, ChainParticipant participant)
        {
            const int firstRowNumber = 39;
            const int rowIncrement = 2;
            var row = firstRowNumber + index * rowIncrement;
            return new List<SheetRange>() {
                new SheetRange(Sheet.CellsInRow(row, 7, 8)) {Value = participant.CompanyNumber },// 2.3.1. Номер участника
                new SheetRange(Sheet.CellsInRow(row, 45, 3)) {Value = participant.DirectSharePart.ToInt().ToString("D3") },// 2.3.2. Доля прямого участия, % 
                new SheetRange(Sheet.CellsInRow(row, 57, 5)) {Value = participant.DirectSharePart.GetNumbersAfterDot(5) },// 2.3.2. Доля прямого участия, % 
                new SheetRange(Sheet.CellsInRow(row, 86, 3)) {Value = participant.IndirectSharePart.ToInt().ToString("D3") },// 2.3.2. Доля прямого участия, % 
                new SheetRange(Sheet.CellsInRow(row, 98, 5)) {Value = participant.IndirectSharePart.GetNumbersAfterDot(5) },// 2.3.2. Доля прямого участия, % 
            };
        }

    }
}
