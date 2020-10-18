using System.Collections.Generic;
using KPMG.WebKik.DocumentProcessing.Helpers;
using KPMG.WebKik.Models.Companies;
using OfficeOpenXml;

namespace KPMG.WebKik.DocumentProcessing.Kik.Sheets
{
    internal class KikSheetA : KikCompanySheetBase
    {
        private ForeignCompany foreignCompany => Company.ProjectCompany.ForeignCompany;
        public KikSheetA(ExcelWorksheet sheet, KikReportCompany company, int pageNumber)
            : base(sheet, company, pageNumber)
        {
        }

        protected override ExcelRange CompanyNumberRange => Sheet.CellsInRow(14, 45, 8);

        internal override void InitRanges()
        {
            base.InitRanges();

            Ranges.AddRange(new List<SheetRange>()
            {
                new SheetRange(Sheet.Cells[18, 1, 24, 118]) { Value = foreignCompany.FullName }, //2. Полное наименование русское
                new SheetRange(Sheet.Cells[26, 1, 32, 118]) { Value = foreignCompany.Name }, //2. Полное наименование латинское
                new SheetRange(Sheet.Cells[34, 44, 34, 50]) { Value = foreignCompany.CountryCode.Code.FormatCode("D3") }, //3. Код страны регистрации (инкорпорации)
                new SheetRange(Sheet.Cells[38, 1, 40, 118]) { Value = foreignCompany.RegistrationNumber }, //4. Регистрационный номер в стране регистрации (инкорпорации)
                new SheetRange(Sheet.Cells[44, 1, 46, 118]) { Value = foreignCompany.TaxPayerCode?.Name }, //5. Код налогоплательщика в стране регистрации (инкорпорации) или аналог (если имеется)
                new SheetRange(Sheet.Cells[50, 1, 54, 118]) { Value = foreignCompany.Address }, //6. Адрес в стране регистрации (инкорпорации)

                new SheetRange(Sheet.Cells[58, 43, 58, 46]) { Value = foreignCompany.FoundDate.Day.ToString("D2") }, //10. Дата регистрации (инкорпорации) день
                new SheetRange(Sheet.Cells[58, 52, 58, 55]) { Value = foreignCompany.FoundDate.Month.ToString("D2") }, //10. Дата регистрации (инкорпорации) день
                new SheetRange(Sheet.Cells[58, 61, 58, 70]) { Value = foreignCompany.FoundDate.Year.ToString("D4") }, //10. Дата регистрации (инкорпорации) год
            });
        }
    }
}
