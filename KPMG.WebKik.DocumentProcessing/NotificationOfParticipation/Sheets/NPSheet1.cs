using System.Collections.Generic;
using KPMG.WebKik.DocumentProcessing.Helpers;
using KPMG.WebKik.Models.ProjectCompanies;
using OfficeOpenXml;
using System.Linq;
using KPMG.WebKik.Models;

namespace KPMG.WebKik.DocumentProcessing.NotificationOfParticipation.Sheets
{
    internal class NPSheet1 : NPSheetBase
    {
        private readonly int totalPagesCount;
        private readonly Signatory signature;
        private int correctionNumber => 1;
        private int taxpayerCode => OwnerCompany.State == State.Domestic ? 1 : 2;
        private int reasonCode => 0;

        private Signatory signatory;
        private Signatory Signatory
        {
            get
            {
                return signatory;
            }
        }

        public NPSheet1(ExcelWorksheet sheet, ProjectCompany ownerCompany, int totalPagesCount, Signatory signature)
            : base(sheet, ownerCompany, 1)
        {

            this.totalPagesCount = totalPagesCount;
            this.signature = signature;
        }

        private string Name => OwnerCompany.State == State.Domestic
            ? OwnerCompany.Name
            : string.Join(" ", OwnerCompany?.IndividualCompany?.Surname, OwnerCompany?.IndividualCompany?.Name, OwnerCompany?.IndividualCompany?.MiddleName);

        internal override void InitRanges()
        {
            base.InitRanges();

            Ranges.AddRange(new List<SheetRange>()
            {
                new SheetRange(Sheet.Cells[12, 24, 12, 30]) { Value = correctionNumber.ToString("D3")}, //Номер корректировки
                new SheetRange(Sheet.OneCell(14, 28)) { Value = taxpayerCode.ToString("D1")}, //код Налогоплательщика
                new SheetRange(Sheet.OneCell(14, 76)) { Value = reasonCode.ToString("D1")}, //основание для подачи
                new SheetRange(Sheet.Cells[18, 1, 24, 118]) { Value = Name }, //Наименование организации / ФИО
                new SheetRange(Sheet.Cells[28, 10, 28, 46]) { Value = OwnerCompany?.DomesticCompany?.OGRN.ToString() }, //ОГРН
                new SheetRange(Sheet.Cells[30, 37, 30, 43]) { Value = totalPagesCount.ToString("D3") }, //Количество страниц

                new SheetRange(Sheet.OneCell(35,1)) { Value = Signatory?.SignatoryCode?.Code }, //Тип подписанта
                new SheetRange(Sheet.Cells[37, 1, 42, 58]) { Value = Signatory?.GetFio() }, //ФИО подписанта
                new SheetRange(Sheet.CellsInRow(47, 10, 12)) { Value = Signatory?.Inn }, //ИНН подписанта
                new SheetRange(Sheet.CellsInRow(52, 1, 20)) { Value = Signatory?.PhoneNumber }, //Телефон подписанта
                new SheetRange(Sheet.OneCell(54, 10)) { Value = Signatory?.Email, DontSplit = true }, //Email подписанта
            });
        }
    }
}
