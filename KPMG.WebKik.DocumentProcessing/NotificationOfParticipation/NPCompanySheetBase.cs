using OfficeOpenXml;

namespace KPMG.WebKik.DocumentProcessing.NotificationOfParticipation
{
    internal abstract class NPCompanySheetBase : NPSheetBase
    {
        protected readonly NPReportCompany Company;
        protected abstract ExcelRange CompanyNumberRange { get; }

        protected NPCompanySheetBase(ExcelWorksheet sheet, NPReportCompany company, int pageNumber)
            : base(sheet, company.OwnerCompany, pageNumber)
        {
            Company = company;
        }

        internal override void InitRanges()
        {
            base.InitRanges();
            Ranges.Add(new SheetRange(CompanyNumberRange) { Value = Company.FullNumber });
        }
    }
}
