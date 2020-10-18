using System.Collections.Generic;
using KPMG.WebKik.DocumentProcessing.Helpers;
using KPMG.WebKik.Models.Companies;
using OfficeOpenXml;

namespace KPMG.WebKik.DocumentProcessing.Kik.Sheets
{
    internal class KikSheetB : KikCompanySheetBase
    {
        private ForeignLightCompany foreignLightCompany => Company.ProjectCompany.ForeignLightCompany;
        public KikSheetB(ExcelWorksheet sheet, KikReportCompany company, int pageNumber) 
            : base(sheet, company, pageNumber)
        {
        }

        protected override ExcelRange CompanyNumberRange => Sheet.CellsInRow(14, 44, 8);

        internal override void InitRanges()
        {
            base.InitRanges();

            Ranges.AddRange(new List<SheetRange>()
            {
                new SheetRange(Sheet.Cells[17, 37, 17, 37]) { Value = foreignLightCompany.ForeignOrganizationalFormCode.Code }, //2.1. Организационная форма (код)
                new SheetRange(Sheet.Cells[21, 1, 27, 118]) { Value = foreignLightCompany.RussianName }, //2.2. Наименование иностранной структуры русское
                new SheetRange(Sheet.Cells[29, 1, 35, 118]) { Value = foreignLightCompany.EnglishName }, //2.2 Наименование иностранной структуры латинское
                new SheetRange(Sheet.Cells[39, 1, 41, 118]) { Value = foreignLightCompany.RequisitesRus }, //2.3. Наименование и реквизиты документа об учреждении иностранной структуры русское
                new SheetRange(Sheet.Cells[43, 1, 45, 118]) { Value = foreignLightCompany.RequisitesEng }, //2.3. Наименование и реквизиты документа об учреждении иностранной структуры латинское

                new SheetRange(Sheet.Cells[48, 37, 48, 40]) { Value = foreignLightCompany.FoundDate.Day.ToString("D2") }, //2.4. Дата учреждения (регистрации) день
                new SheetRange(Sheet.Cells[48, 46, 48, 49]) { Value = foreignLightCompany.FoundDate.Month.ToString("D2") }, //2.4. Дата учреждения (регистрации) месяц
                new SheetRange(Sheet.Cells[48, 55, 48, 64]) { Value = foreignLightCompany.FoundDate.Year.ToString("D4") }, //2.4. Дата учреждения (регистрации) год

                new SheetRange(Sheet.Cells[48, 112, 48, 118]) { Value = foreignLightCompany.CountryCode.Code.FormatCode("D3") }, //2.5. Код страны, в которой учреждена иностранная структура
                new SheetRange(Sheet.Cells[53, 1, 53, 118]) { Value = foreignLightCompany.RegNumber }, //2.6. Регистрационный номер (иной идентификатор) иностранной структуры в стране учреждения (регистрации) (если применимо)
                new SheetRange(Sheet.Cells[57, 1, 67, 118]) { Value = foreignLightCompany.OtherInfo }, //2.7. Иные сведения, идентифицирующие иностранную структуру
            });
        }
    }
}
