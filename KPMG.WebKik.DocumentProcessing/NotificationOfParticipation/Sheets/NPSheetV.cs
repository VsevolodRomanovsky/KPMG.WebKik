using System.Collections.Generic;
using KPMG.WebKik.DocumentProcessing.Helpers;
using KPMG.WebKik.Models.Companies;
using KPMG.WebKik.Models.Directories;
using OfficeOpenXml;

namespace KPMG.WebKik.DocumentProcessing.NotificationOfParticipation.Sheets
{
    internal class NPSheetV : NPCompanySheetBase
    {
        private ForeignLightCompany foreignLightCompany => Company.ProjectCompany.ForeignLightCompany;
        public NPSheetV(ExcelWorksheet sheet, NPReportCompany company, int pageNumber)
            : base(sheet, company, pageNumber)
        {
        }

        protected override ExcelRange CompanyNumberRange => Sheet.CellsInRow(13, 43, 8);
        internal override void InitRanges()
        {
            base.InitRanges();

            //var c = Company.ProjectCompany.ForeignLightCompany.ForeignOrganizationalFormCode.Code;
            

            Ranges.AddRange(new List<SheetRange>()
            {
                new SheetRange(Sheet.Cells[16, 37, 16, 37]) { Value = foreignLightCompany?.ForeignOrganizationalFormCode?.Code }, //2.1. Организационная форма (код)
                new SheetRange(Sheet.Cells[20, 1, 26, 118]) { Value = foreignLightCompany?.RussianName }, //2.2. Наименование иностранной структуры русское
                new SheetRange(Sheet.Cells[28, 1, 34, 118]) { Value = foreignLightCompany?.EnglishName }, //2.2 Наименование иностранной структуры латинское
                new SheetRange(Sheet.Cells[38, 1, 40, 118]) { Value = foreignLightCompany?.RequisitesRus }, //2.3. Наименование и реквизиты документа об учреждении иностранной структуры русское
                new SheetRange(Sheet.Cells[42, 1, 44, 118]) { Value = foreignLightCompany?.RequisitesEng }, //2.3. Наименование и реквизиты документа об учреждении иностранной структуры латинское

                new SheetRange(Sheet.Cells[47, 37, 47, 40]) { Value = foreignLightCompany?.FoundDate.Day.ToString("D2") }, //2.4. Дата учреждения (регистрации) день
                new SheetRange(Sheet.Cells[47, 46, 47, 49]) { Value = foreignLightCompany?.FoundDate.Month.ToString("D2") }, //2.4. Дата учреждения (регистрации) месяц
                new SheetRange(Sheet.Cells[47, 55, 47, 64]) { Value = foreignLightCompany?.FoundDate.Year.ToString("D4") }, //2.4. Дата учреждения (регистрации) год

                new SheetRange(Sheet.Cells[47, 112, 47, 118]) { Value = foreignLightCompany?.CountryCode?.Code.FormatCode("D3") }, //2.5. Код страны, в которой учреждена иностранная структура
                new SheetRange(Sheet.Cells[52, 1, 52, 118]) { Value = foreignLightCompany?.RegNumber }, //2.6. Регистрационный номер (иной идентификатор) иностранной структуры в стране учреждения (регистрации) (если применимо)
                new SheetRange(Sheet.Cells[56, 1, 66, 118]) { Value = foreignLightCompany?.OtherInfo }, //2.7. Иные сведения, идентифицирующие иностранную структуру
            });
        }
    }
}
