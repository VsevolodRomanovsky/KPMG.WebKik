using KPMG.WebKik.DocumentProcessing.Helpers;
using KPMG.WebKik.Models.Companies;
using OfficeOpenXml;

namespace KPMG.WebKik.DocumentProcessing.Kik.Sheets
{
    internal class KikSheetB1 : KikCompanySheetBase
    {
        private ForeignLightCompany foreignLightCompany => Company.ProjectCompany.ForeignLightCompany;

        public KikSheetB1(ExcelWorksheet sheet, KikReportCompany company, int pageNumber)
            : base(sheet, company, pageNumber)
        {
        }

        protected override ExcelRange CompanyNumberRange => Sheet.CellsInRow(15, 62, 8);

        internal override void InitRanges()
        {
            base.InitRanges();
        }
    }
}
