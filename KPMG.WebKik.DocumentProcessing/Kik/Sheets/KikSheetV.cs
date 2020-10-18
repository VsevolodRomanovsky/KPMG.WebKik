using System.Collections.Generic;
using KPMG.WebKik.DocumentProcessing.Helpers;
using KPMG.WebKik.Models.Companies;
using OfficeOpenXml;

namespace KPMG.WebKik.DocumentProcessing.Kik.Sheets
{
    internal class KikSheetV : KikCompanySheetBase
    {
        private ForeignCompany foreignCompany => Company.ProjectCompany.ForeignCompany;
        public KikSheetV(ExcelWorksheet sheet, KikReportCompany company, int pageNumber)
            : base(sheet, company, pageNumber)
        {
        }

        protected override ExcelRange CompanyNumberRange => Sheet.CellsInRow(14, 45, 8);
        internal override void InitRanges()
        {
            base.InitRanges();

            Ranges.AddRange(new List<SheetRange>()
            {
                new SheetRange(Sheet.CellsInRow(32, 62, 3)) { Value = foreignCompany?.CountryCode.Code.FormatCode("D3") }, //8.1. Код страны регистрации (инкорпорации)
                new SheetRange(Sheet.Cells[44, 1, 46, 118]) { Value = foreignCompany?.TaxPayerCode.Name }, //10. Код налогоплательщика в стране регистрации (инкорпорации) или аналог (если имеется)
            });
        }
    }
}
