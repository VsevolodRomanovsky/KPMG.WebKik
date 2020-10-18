using System;
using System.Collections.Generic;
using KPMG.WebKik.Models.ProjectCompanies;
using OfficeOpenXml;

namespace KPMG.WebKik.DocumentProcessing.NotificationOfParticipation
{
    internal abstract class NPSheetBase : SheetBase
    {
        protected readonly ExcelWorksheet Sheet;

        protected readonly ProjectCompany OwnerCompany;

        protected int PageNumber { get; }
        protected virtual string Inn
        {
            get
            {
                switch (OwnerCompany.State)
                {
                    case State.Domestic:
                        return OwnerCompany?.DomesticCompany?.INN.ToString();
                    case State.Individual:
                        return OwnerCompany?.IndividualCompany?.INN.ToString();
                }
                throw new ArgumentException($"Wrong company State. Expected Domestic or Individual. Got {OwnerCompany.State}");
            }
        }
        protected virtual string Kpp => OwnerCompany?.DomesticCompany?.KPP;

        protected NPSheetBase(ExcelWorksheet sheet, ProjectCompany ownerCompany, int pageNumber)
        {
            Sheet = sheet;
            OwnerCompany = ownerCompany;
            PageNumber = pageNumber;
        }

        internal override void InitRanges()
        {
            Ranges.AddRange(new List<SheetRange>()
            {
                new SheetRange(Sheet.Cells[1, 37, 1, 70])   { Value = Inn??  string.Empty }, //ИНН
                new SheetRange(Sheet.Cells[4, 37, 4, 61])   { Value = Kpp?? string.Empty }, //КПП
                new SheetRange(Sheet.Cells[4, 70, 4, 76])   { Value = PageNumber.ToString("D3") }, //Номер страницы
            });
        }
    }
}
