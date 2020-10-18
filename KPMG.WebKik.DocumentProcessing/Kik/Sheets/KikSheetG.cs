using System.Collections.Generic;
using KPMG.WebKik.DocumentProcessing.Helpers;
using KPMG.WebKik.DocumentProcessing.Kik.Models;
using OfficeOpenXml;

namespace KPMG.WebKik.DocumentProcessing.Kik.Sheets
{
    internal class KikSheetG : KikCompanySheetBase
    {
        private readonly CompanyChain chain;
        public KikSheetG(ExcelWorksheet sheet, KikReportCompany company, CompanyChain chain, int pageNumber)
            : base(sheet, company, pageNumber)
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
                    new SheetRange(Sheet.CellsInRow(27, 61, 3))  { Value = TotalIndirectSharePart.ToString("D3") }, //1.3. Доля косвенного участия - итого, %
                    new SheetRange(Sheet.CellsInRow(27, 73, 5)) { Value = TotalIndirectSharePart.GetNumbersAfterDot(5) }, //1.3. Доля косвенного участия - итого, %
                    new SheetRange(Sheet.CellsInRow(31, 61, 5)) { Value = chain.Number.ToString("D5") }, //2.1. Номер последовательности участия
                    new SheetRange(Sheet.CellsInRow(33, 61, 3))  { Value = chain.IndirectSharePart.ToString("D3") }, //2.2. Доля косвенного участия в последовательности - итого, %
                    new SheetRange(Sheet.CellsInRow(33, 73, 5)) { Value = chain.IndirectSharePart.GetNumbersAfterDot(5) }, //2.2. Доля косвенного участия в последовательности - итого, %
                });

            var index = 0;
            foreach (var participant in chain)
            {
                Ranges.AddRange(GetParticipantRanges(index, participant));// 2.3. Участники последовательности
                index++;
            }
        }

        private double TotalIndirectSharePart => Company.FactShare.ShareFactPart - Company.Share?.SharePart ?? 0;

        private IEnumerable<SheetRange> GetParticipantRanges(int index, ChainParticipant participant)
        {
            const int firstRowNumber = 40;
            const int rowIncrement = 4;
            const int indirectRowIncrement = 2;
            var rowIndex = firstRowNumber + index * rowIncrement;
            var indirectRowIndex = rowIndex + indirectRowIncrement;
            return new List<SheetRange>() {
                new SheetRange(Sheet.CellsInRow(rowIndex, 7, 8)) {Value = participant.CompanyNumber },// 2.3.1. Номер участника
                new SheetRange(Sheet.CellsInRow(rowIndex, 45, 3)) {Value = participant.DirectShare.ToString("D3") },// 2.3.2. Доля прямого участия, % 
                new SheetRange(Sheet.CellsInRow(rowIndex, 57, 15)) {Value = participant.DirectShare.GetNumbersAfterDot(15) },// 2.3.2. Доля прямого участия, % 
                new SheetRange(Sheet.CellsInRow(indirectRowIndex, 45, 3)) {Value = participant.IndirectShare.ToString("D3") },// 2.3.2. Доля прямого участия, % 
                new SheetRange(Sheet.CellsInRow(indirectRowIndex, 57, 15)) {Value = participant.IndirectShare.GetNumbersAfterDot(15) },// 2.3.2. Доля прямого участия, % 
            };
        }

    }
}
