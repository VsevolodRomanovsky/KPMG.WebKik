using OfficeOpenXml;

namespace KPMG.WebKik.DocumentProcessing.Kik
{
    internal abstract class KikCompanySheetBase : KikSheetBase
    {
        protected readonly KikReportCompany Company;
        protected abstract ExcelRange CompanyNumberRange { get; }

        protected KikCompanySheetBase(ExcelWorksheet sheet, KikReportCompany company, int pageNumber)
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
