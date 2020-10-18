using System.Collections.Generic;
using KPMG.WebKik.Models.ProjectCompanies;
using OfficeOpenXml;

namespace KPMG.WebKik.DocumentProcessing.Kik.Sheets
{
    internal class KikSheet1 : KikSheetBase
    {
        private readonly int year;
        private readonly int totalPagesCount;

        public KikSheet1(ExcelWorksheet sheet, ProjectCompany ownerCompany, int year, int totalPagesCount) : base(sheet, ownerCompany, 1)
        {
            this.year = year;
            this.totalPagesCount = totalPagesCount;
        }

        private string Name => OwnerCompany.State == State.Domestic
            ? OwnerCompany.Name
            : string.Join(" ", OwnerCompany.IndividualCompany.Surname, OwnerCompany.IndividualCompany.Name, OwnerCompany.IndividualCompany.MiddleName);

        internal override void InitRanges()
        {
            base.InitRanges();

            Ranges.AddRange(new List<SheetRange>()
            {
                new SheetRange(Sheet.Cells[18, 1, 24, 118]) { Value = Name }, //Наименование организации / ФИО
                new SheetRange(Sheet.Cells[12, 75, 12, 84]) { Value = year.ToString("D4") }, //Налоговый период
                new SheetRange(Sheet.Cells[30, 37, 30, 43]) { Value = totalPagesCount.ToString("D3") }, //Количество страниц
            });
        }
    }
}
