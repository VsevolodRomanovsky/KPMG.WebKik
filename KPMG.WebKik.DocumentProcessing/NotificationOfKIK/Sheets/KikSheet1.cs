using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using KPMG.WebKik.Models.ProjectCompanies;
using KPMG.WebKik.Models;

namespace KPMG.WebKik.DocumentProcessing.NotificationOfKIK.Sheets
{
    internal class KikSheet1 : KikSheetBase
    {
        private readonly int year;
        private readonly int totalPagesCount;
        private readonly int correction;
        private readonly int taxAuthorityCode;

        public KikSheet1(ExcelWorksheet sheet, ProjectCompany company, Signatory signatore, int pageNumber, int year, int totalPagesCount, int correction,
            int taxAuthorityCode) : base(sheet, company, 1)
        {
            this.year = year;
            this.totalPagesCount = totalPagesCount;
            this.correction = correction;
            this.taxAuthorityCode = taxAuthorityCode; ;
        }

        internal override void InitValues()
        {
            FillCell("[inn]", Inn);
            FillCell("[kpp]", Kpp);
            FillCell("[period]", year.ToString("D4"));
            FillCell("[pagecount]", totalPagesCount.ToString("D3"));


            FillCell("[signatorycode]", Signatore?.SignatoryCode?.Code);
            FillLongCell("[signatoryname]", $"{Signatore.LastName} {Signatore.FirstName} {Signatore.MiddleName}", 20);
            FillCell("[signatoryinn]", Signatore.Inn);
            FillCell("[signatoryphone]", Signatore.PhoneNumber);
            FillCellValue("B55:BH55", Signatore.Email);
            FillLongCell("[signatorydocument]", Signatore?.ConfirmationDocument?.Name, 20);

            FillCell("[correctionnumber]", correction.ToString("D3"));
            FillCell("[taxauthoritycode]", taxAuthorityCode);
        }
    }
}
