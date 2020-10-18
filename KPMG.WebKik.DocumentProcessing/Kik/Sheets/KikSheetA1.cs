using System.Collections.Generic;
using System.Linq;
using KPMG.WebKik.DocumentProcessing.Helpers;
using KPMG.WebKik.DocumentProcessing.Kik.Models;
using KPMG.WebKik.Models.Companies;
using OfficeOpenXml;

namespace KPMG.WebKik.DocumentProcessing.Kik.Sheets
{
    internal class KikSheetA1 : KikCompanySheetBase
    {
        private ForeignCompany foreignCompany => Company.ProjectCompany.ForeignCompany;

        private bool is201or202or203;
        private bool is204;

        public KikSheetA1(ExcelWorksheet sheet, KikReportCompany company, int pageNumber)
            : base(sheet, company, pageNumber)
        {
        }

        protected override ExcelRange CompanyNumberRange => Sheet.CellsInRow(15, 62, 8);

        internal override void InitRanges()
        {
            base.InitRanges();

            InitRangesFor1_2();
            InitRangesFor2_1();
            InitRangesFor2_2();
            InitRangesFor2_3();
            InitRangesFor2_4();
            InitRangesFor2_5();
        }

        private void InitRangesFor1_2()//1.2. Основания для признания налогоплательщика контролирующим лицом контролируемой иностранной компании
        {
            if (Company.ControlGrounds.Contains(ControlGround._101))
            {
                Ranges.Add(new SheetRange(Sheet.OneCell(19, 10)) { Value = "1" }); //101
            }

            if (Company.ControlGrounds.Contains(ControlGround._102))
            {
                Ranges.Add(new SheetRange(Sheet.OneCell(19, 21)) { Value = "1" }); //102
            }

            if (Company.ControlGrounds.Contains(ControlGround._103))
            {
                Ranges.Add(new SheetRange(Sheet.OneCell(19, 31)) { Value = "1" }); //103
            }

            if (Company.ControlGrounds.Contains(ControlGround._104))
            {
                Ranges.Add(new SheetRange(Sheet.OneCell(19, 42)) { Value = "1" }); //104
            }

            if (Company.ControlGrounds.Contains(ControlGround._105))
            {
                Ranges.Add(new SheetRange(Sheet.OneCell(19, 53)) { Value = "1" }); //105
            }
        }

        private void InitRangesFor2_1()//2.1. Вид участия
        {
            if (Company.FactShare.ShareFactPart == Company.Share?.SharePart)
            {
                Ranges.Add(new SheetRange(Sheet.OneCell(27, 10)) { Value = "1" }); //201
                is201or202or203 = true;
            }
            else if (Company.Share == null)
            {
                Ranges.Add(new SheetRange(Sheet.OneCell(27, 21)) { Value = "1" }); //202
                is201or202or203 = true;
            }
            else if (Company.Share != null && Company.FactShare.ShareFactPart != Company.Share.SharePart)
            {
                Ranges.Add(new SheetRange(Sheet.OneCell(27, 31)) { Value = "1" }); //203
                is201or202or203 = true;
            }

            if (Company.Share?.IsPartnerInterest == true || Company.Share?.IsChildInterest == true)
            {
                Ranges.Add(new SheetRange(Sheet.OneCell(27, 42)) { Value = "1" }); //204
                is204 = true;
            }

            if (Company.ControlGrounds.Contains(ControlGround._102))
            {
                Ranges.Add(new SheetRange(Sheet.OneCell(27, 53)) { Value = "1" }); //205
            }
        }

        private void InitRangesFor2_2()//2.2  Доля участия
        {
            if (is201or202or203)
            {
                Ranges.AddRange(new List<SheetRange>()
                {
                    new SheetRange(Sheet.CellsInRow(31, 46, 3))  { Value = Company.FactShare.ShareFactPart.ToString("D3") }, //2.2. 
                    new SheetRange(Sheet.CellsInRow(31, 58, 15)) { Value = Company.FactShare.ShareFactPart.GetNumbersAfterDot(15) }, //2.2.
                });
            }
        }

        private void InitRangesFor2_3()//2.3. Доля совместного участия с супругами и несовершеннолетними детьми
        {
            if (is204)
            {
                Ranges.AddRange(new List<SheetRange>()
                {
                    new SheetRange(Sheet.CellsInRow(33, 46, 3))  { Value = Company.Share.SharePart.ToString("D3") }, //2.3. 
                    new SheetRange(Sheet.CellsInRow(33, 58, 15)) { Value = Company.Share.SharePart.GetNumbersAfterDot(15) }, //2.3. 
                });
            }
        }

        private void InitRangesFor2_4()// 2.4 Доля совместного участия с налоговыми резидентами РФ (не являются супругой или несовершеннолетними детьми).
        {
            if (Company.ControlGrounds.Contains(ControlGround._102))
            {
                Ranges.AddRange(new List<SheetRange>()
                {
                    new SheetRange(Sheet.CellsInRow(37, 46, 3))  { Value = Company.Share.SharePart.ToString("D3") }, //2.4 
                    new SheetRange(Sheet.CellsInRow(37, 58, 15)) { Value = Company.Share.SharePart.GetNumbersAfterDot(15) }, //2.4 
                });
            }
        }

        private void InitRangesFor2_5()// 2.5. Дата возникновения основания
        {
            if (Company.Share?.ControlEmergenceDate.HasValue == true)
            {
                Ranges.AddRange(new List<SheetRange>()
                {
                    new SheetRange(Sheet.TwoCells(40, 46)) { Value = Company.Share.ControlEmergenceDate.Value.Day.ToString("D2") }, //2.5. 
                    new SheetRange(Sheet.TwoCells(40, 55)) { Value = Company.Share.ControlEmergenceDate.Value.Month.ToString("D2") }, //2.5.
                    new SheetRange(Sheet.CellsInRow(40, 64, 4)) { Value = Company.Share.ControlEmergenceDate.Value.Year.ToString("D4") }, //2.5. 
                 });
            }
        }
    }
}
