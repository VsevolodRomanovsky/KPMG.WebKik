using System.Collections.Generic;
using KPMG.WebKik.DocumentProcessing.Helpers;
using KPMG.WebKik.Models.Companies;
using OfficeOpenXml;

namespace KPMG.WebKik.DocumentProcessing.NotificationOfParticipation.Sheets
{
    internal class NPSheetB : NPCompanySheetBase
    {
        private DomesticCompany domesticCompany => Company.ProjectCompany.DomesticCompany;

        public NPSheetB(ExcelWorksheet sheet, NPReportCompany company, int pageNumber) 
            : base(sheet, company, pageNumber)
        {
        }

        protected override ExcelRange CompanyNumberRange => Sheet.CellsInRow(13, 37, 8);

        internal override void InitRanges()
        {
            base.InitRanges();

            Ranges.AddRange(new List<SheetRange>()
            {
                new SheetRange(Sheet.Cells[15, 37, 15, 73]) { Value = domesticCompany.OGRN.ToString() }, 
                new SheetRange(Sheet.Cells[17, 37, 17, 64]) { Value = domesticCompany.INN.ToString() }, 
                new SheetRange(Sheet.Cells[19, 37, 19, 61]) { Value = domesticCompany.KPP }, 
                new SheetRange(Sheet.Cells[23, 1, 29, 118]) { Value = domesticCompany.FullName }, 
           
            });
        }
    }
}
