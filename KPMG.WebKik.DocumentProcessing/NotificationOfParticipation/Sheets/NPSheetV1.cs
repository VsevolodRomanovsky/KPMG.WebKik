using System;
using System.Collections.Generic;
using System.Linq;
using KPMG.WebKik.DocumentProcessing.Helpers;
using KPMG.WebKik.Models.Companies;
using OfficeOpenXml;

namespace KPMG.WebKik.DocumentProcessing.NotificationOfParticipation.Sheets
{
    internal class NPSheetV1 : NPCompanySheetBase
    {
        private ForeignLightCompany foreignLightCompany => Company.ProjectCompany.ForeignLightCompany;
        public NPSheetV1(ExcelWorksheet sheet, NPReportCompany company, int pageNumber)
            : base(sheet, company, pageNumber)
        {
        }

        private int isFounder => Company.FactShare.IsFounder ? 1 : 0;
        private int isControlledBy => Company.FactShare.IsControlledBy ? 1 : 0;
        private int hasRightsForProfit => 0;

        protected override ExcelRange CompanyNumberRange => Sheet.CellsInRow(13, 55, 8);
        internal override void InitRanges()
        {
            base.InitRanges();

            var controlGrounds = Company.FactShare.DirectShares.Length == 1 ? Company.FactShare.DirectShares[0].ControlGrounds : "";

            DateTime? startDate = null;
            DateTime? finishDate = null;


            if (Company.FactShare.DependentProjectCompany.DependentProjectCompanyShares.Count == 1)
            {
                var projectCompanySgare = Company.FactShare.DependentProjectCompany.DependentProjectCompanyShares.First();
                startDate = projectCompanySgare.ShareStartDate;
                finishDate = projectCompanySgare.ShareFinishDate;
            }

            Ranges.AddRange(new List<SheetRange>
            {
                new SheetRange(Sheet.OneCell(18, 19)) { Value = isFounder.ToString()}, //2.1. Учредитель
                new SheetRange(Sheet.OneCell(18, 64)) { Value = isControlledBy.ToString()}, //2.2. Лицо, осуществляющее контроль
                new SheetRange(Sheet.OneCell(18, 109)) { Value = hasRightsForProfit.ToString()}, //2.3. Лицо, имеющее фактическое право на доход
                new SheetRange(Sheet.CellsInRows(19, 4)) {Value = string.Empty},// Not implemented 3. Статус участника в соответствии с личным законом и (или) документом об учреждении иностранной структуры
                new SheetRange(Sheet.CellsInRows(30, 4)) {Value = string.Empty},// Not implemented 3. Статус участника в соответствии с личным законом и (или) документом об учреждении иностранной структуры
                new SheetRange(Sheet.CellsInRows(40, 7)) {Value = controlGrounds},// 4. Наименование и реквизиты документа, являющегося основанием для осуществления контроля над иностранной структурой
                new SheetRange(Sheet.CellsInRows(56, 3)) {Value = string.Empty},// Not implemented 5. Описание оснований для осуществления контроля над иностранной структурой и (или) фактического права на доход
                new SheetRange(Sheet.OneCell(63, 37)) {Value = ((int)Company.ShareType + 1).ToString() },// 6. Участие в иностранной структуре

                new SheetRange(Sheet.TwoCells(63, 91)) {Value = startDate?.Day.ToString("D2") },// Not implemented 7. Дата возникновения участия
                new SheetRange(Sheet.TwoCells(63, 100)) {Value = startDate?.Month.ToString("D2") },// Not implemented 7. Дата возникновения участия
                new SheetRange(Sheet.CellsInRow(63, 109, 4)) {Value = startDate?.Year.ToString("D4") },// Not implemented 7. Дата возникновения участия

                new SheetRange(Sheet.CellsInRow(65, 31, 3)) {Value = Company.FactShare.ShareFactPart.ToInt().ToString("D3") },// 8. Доля участия, %
                new SheetRange(Sheet.CellsInRow(65, 43, 5)) {Value = Company.FactShare.ShareFactPart.GetNumbersAfterDot(5) },// 8. Доля участия, %

                new SheetRange(Sheet.TwoCells(65, 91)) {Value = finishDate?.Day.ToString("D2") },// Not implemented 9. Дата окончания участия
                new SheetRange(Sheet.TwoCells(65, 100)) {Value = finishDate?.Month.ToString("D2") },// Not implemented 9. Дата окончания участия
                new SheetRange(Sheet.CellsInRow(65, 109, 4)) {Value = finishDate?.Year.ToString("D4") },// Not implemented 9. Дата окончания участия
            });
        }
    }
}
