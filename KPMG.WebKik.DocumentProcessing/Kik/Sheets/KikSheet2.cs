using System.Collections.Generic;
using KPMG.WebKik.DocumentProcessing.Helpers;
using KPMG.WebKik.Models.Companies;
using OfficeOpenXml;

namespace KPMG.WebKik.DocumentProcessing.Kik.Sheets
{
    internal class KikSheet2 : KikSheetBase
    {
        private readonly IndividualCompany person;

        public KikSheet2(ExcelWorksheet sheet, IndividualCompany person, int pageNumber) : base(sheet, null, pageNumber)
        {
            this.person = person;
        }

        protected override string Inn => string.Empty;
        protected override string Kpp => string.Empty;

        internal override void InitRanges()
        {
            base.InitRanges();

            Ranges.AddRange(new List<SheetRange>()
            {
                new SheetRange (Sheet.OneCell(10, 16))          { Value = person.GenderCode.Code }, //Пол
                new SheetRange (Sheet.Cells[10, 70, 10, 73])    { Value = person.BirthDate.Day.ToString("D2") }, //Число рождения
                new SheetRange (Sheet.Cells[10, 79, 10, 82])    { Value = person.BirthDate.Month.ToString("D2") }, //Месяц рождения
                new SheetRange (Sheet.Cells[10, 88, 10, 97])    { Value = person.BirthDate.Year.ToString("D2") }, //Год рождения
                new SheetRange (Sheet.Cells[10, 88, 10, 97])    { Value = person.BirthDate.Year.ToString("D2") }, //Место рождения
                new SheetRange (Sheet.Cells[14, 1, 16, 118])    { Value = person.BirthPlace }, //Место рождения
                new SheetRange (Sheet.Cells[19, 16, 19, 16])    { Value = person.CitizenshipCode.Code }, //Гражданство
                new SheetRange (Sheet.Cells[19, 100, 19, 106])  { Value = person.ForeignCountryCode?.Code.FormatCode("D3") }, //Код страны для иностранного гражданина

                new SheetRange (Sheet.Cells[21, 115, 21, 118])  { Value = person.VerifedPersonalityDocInfo.DocumentCode.Code.FormatCode("D2")}, //Документ удостоверяющего личность, код вида
                new SheetRange (Sheet.Cells[23, 13, 23, 70])    { Value = person.VerifedPersonalityDocInfo.SeriesAndNumber }, //Документ удостоверяющего личность, серия и номер
                new SheetRange (Sheet.Cells[23, 91, 23, 94])    { Value = person.VerifedPersonalityDocInfo.IssueDate.Day.ToString("D2") }, //Документ удостоверяющего личность, дата выдачи - день
                new SheetRange (Sheet.Cells[23, 100, 23, 103])  { Value = person.VerifedPersonalityDocInfo.IssueDate.Month.ToString("D2") }, //Документ удостоверяющего личность, дата выдачи - месяц
                new SheetRange (Sheet.Cells[23, 109, 23, 118])  { Value = person.VerifedPersonalityDocInfo.IssueDate.Year.ToString("D4") }, //Документ удостоверяющего личность, дата выдачи - год
                new SheetRange (Sheet.Cells[25, 16, 25, 118])   { Value = person.VerifedPersonalityDocInfo.IssuePlace }, //Документ удостоверяющего личность, кем выдан

                new SheetRange (Sheet.Cells[34, 91, 34, 91])    { Value = person.RussianLocationCode.Code}, //Место жительства/пребывания, код
                new SheetRange (Sheet.Cells[36, 22, 36, 37])    { Value = person.PostIndex}, //Место жительства/пребывания, почтовый индекс
                new SheetRange (Sheet.Cells[36, 61, 36, 64])    { Value = person.RegionCode.Code.FormatCode("D2")}, //Место жительства/пребывания, код региона 
                new SheetRange (Sheet.Cells[38, 22, 38, 118])   { Value = person.District}, //Место жительства/пребывания, район 
                new SheetRange (Sheet.Cells[40, 22, 40, 118])   { Value = person.City}, //Место жительства/пребывания, город 
                new SheetRange (Sheet.Cells[40, 22, 40, 118])   { Value = person.CityType}, //Место жительства/пребывания, населенный пункт 
                new SheetRange (Sheet.Cells[44, 22, 44, 118])   { Value = person.Street}, //Место жительства/пребывания, улица
                new SheetRange (Sheet.Cells[46, 16, 46, 37])    { Value = person.HouseNumber}, //Место жительства/пребывания, номер дома
                new SheetRange (Sheet.Cells[46, 59, 46, 80])    { Value = person.BuildingNumber}, //Место жительства/пребывания, корпус
                new SheetRange (Sheet.Cells[46, 97, 46, 118])   { Value = person.AppartamentNumber}, //Место жительства/пребывания, квартира

                new SheetRange (Sheet.Cells[49, 112, 49, 118])  { Value = person.ForeignCountryCode?.Code.FormatCode("D3")}, //Место жительства иностранного гражданина, Код страны
                new SheetRange (Sheet.Cells[51, 1, 55, 118])    { Value = person.ForeignAddress}, //Место жительства иностранного гражданина, адрес
            });

            if (person.VerifedPersonalityDocInfo.DocumentCode.Code != "21") // 21 - Паспорт гражданина РФ
            {
                Ranges.AddRange(new List<SheetRange>() {
                    new SheetRange (Sheet.Cells[27, 115, 27, 118])  { Value = person.ConfirmedPersonalityDocInfo.DocumentCode.Code.FormatCode("D2")}, //Документ подтверждающий регистрацию, код вида
                    new SheetRange (Sheet.Cells[30, 13, 30, 70])    { Value = person.ConfirmedPersonalityDocInfo.SeriesAndNumber }, //Документ подтверждающий регистрацию, серия и номер
                    new SheetRange (Sheet.Cells[30, 91, 30, 94])    { Value = person.ConfirmedPersonalityDocInfo.IssueDate.Day.ToString("D2") }, //Документ подтверждающий регистрацию, дата выдачи - день
                    new SheetRange (Sheet.Cells[30, 100, 30, 103])  { Value = person.ConfirmedPersonalityDocInfo.IssueDate.Month.ToString("D2") }, //Документ подтверждающий регистрацию, дата выдачи - месяц
                    new SheetRange (Sheet.Cells[30, 109, 30, 118])  { Value = person.ConfirmedPersonalityDocInfo.IssueDate.Year.ToString("D4") }, //Документ подтверждающий регистрацию, дата выдачи - год
                    new SheetRange (Sheet.Cells[32, 16, 32, 118])   { Value = person.ConfirmedPersonalityDocInfo.IssuePlace }, //Документ удостоверяющего личность, кем выдан
                });
            }

        }
    }
}
