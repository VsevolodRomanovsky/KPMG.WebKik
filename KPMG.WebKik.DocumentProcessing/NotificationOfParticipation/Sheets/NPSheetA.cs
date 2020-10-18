using System.Collections.Generic;
using KPMG.WebKik.DocumentProcessing.Helpers;
using KPMG.WebKik.Models.Companies;
using OfficeOpenXml;
using System;
using System.Linq;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace KPMG.WebKik.DocumentProcessing.NotificationOfParticipation.Sheets
{
    internal class NPSheetA : NPCompanySheetBase
    {
        private ForeignCompany foreignCompany => Company.ProjectCompany.ForeignCompany;
        public NPSheetA(ExcelWorksheet sheet, NPReportCompany company, int pageNumber)
            : base(sheet, company, pageNumber)
        {

        }

        protected override ExcelRange CompanyNumberRange => Sheet.CellsInRow(13, 43, 8);

        internal override void InitRanges()
        {
            base.InitRanges();
            
            DateTime? startDate = null;
            DateTime? finishDate = null;

           // Company.FactShare.DependentProjectCompany.DependentProjectCompanyShares.Count == 1;
           /* if (Company.FactShare.ShareFactPart == Company.FactShare.ShareDirectPart && Company.FactShare.DirectShares.Length == 1)
            {
                startDate = Company.FactShare.DirectShares[0].ShareStartDate;
                finishDate = Company.FactShare.DirectShares[0].ShareFinishDate;
            }*/

            if (Company.FactShare.DependentProjectCompany.DependentProjectCompanyShares.Count == 1)
            {
                var projectCompanySgare = Company.FactShare.DependentProjectCompany.DependentProjectCompanyShares.First();
                startDate = projectCompanySgare.ShareStartDate == DateTime.MinValue
                    ? (DateTime?) null
                    : projectCompanySgare.ShareStartDate;

                finishDate = projectCompanySgare.ShareFinishDate;
            }

            if (foreignCompany != null)
            { 
            Ranges.AddRange(new List<SheetRange>
            {
                new SheetRange(Sheet.CellsInRows(17, 4)) { Value = foreignCompany.FullName }, //2. Полное наименование русское
                new SheetRange(Sheet.CellsInRows(25, 4)) { Value = foreignCompany.Name }, //2. Полное наименование латинское
                new SheetRange(Sheet.Cells[33, 44, 33, 50]) { Value = foreignCompany.CountryCode?.Code?.FormatCode("D3") }, //3. Код страны регистрации (инкорпорации)
                new SheetRange(Sheet.Cells[37, 1, 39, 118]) { Value = foreignCompany.RegistrationNumber }, //4. Регистрационный номер в стране регистрации (инкорпорации)
                new SheetRange(Sheet.Cells[43, 1, 45, 118]) { Value = foreignCompany.TaxPayerCode?.Name }, //5. Код налогоплательщика в стране регистрации (инкорпорации) или аналог (если имеется)
                new SheetRange(Sheet.Cells[49, 1, 53, 118]) { Value = foreignCompany.Address }, //6. Адрес в стране регистрации (инкорпорации)

                new SheetRange(Sheet.OneCell(56, 43)) { Value = ((int)Company.ShareType + 1).ToString()}, // 7. Участие

                new SheetRange(Sheet.Cells[58, 43, 58, 46]) { Value = startDate?.Day.ToString("D2") }, // 8. Дата возникновения участия
                new SheetRange(Sheet.Cells[58, 52, 58, 55]) { Value = startDate?.Month.ToString("D2") },// 8. Дата возникновения участия
                new SheetRange(Sheet.Cells[58, 61, 58, 70]) { Value = startDate?.Year.ToString("D4") },// 8. Дата возникновения участия

                new SheetRange(Sheet.CellsInRow(60, 43, 3))  { Value = Company.FactShare.ShareFactPart.ToInt().ToString("D3") }, // 9. Доля участия
                new SheetRange(Sheet.CellsInRow(60, 55, 5)) { Value = Company.FactShare.ShareFactPart.GetNumbersAfterDot(5) }, // 9. Доля участия

                new SheetRange(Sheet.TwoCells(62, 43)) { Value = finishDate?.Day.ToString("D2") }, // 10. Дата окончания участия
                new SheetRange(Sheet.TwoCells(62, 52)) { Value = finishDate?.Month.ToString("D2") }, // 10. Дата окончания участия
                new SheetRange(Sheet.CellsInRow(62, 61, 4)) { Value = finishDate?.Year.ToString("D4") }, // 10. Дата окончания участия
            });
            }

        }
    }
}
