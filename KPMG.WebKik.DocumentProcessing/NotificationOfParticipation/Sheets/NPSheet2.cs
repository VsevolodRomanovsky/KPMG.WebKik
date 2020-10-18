using System.Collections.Generic;
using KPMG.WebKik.DocumentProcessing.Helpers;
using KPMG.WebKik.Models.Companies;
using OfficeOpenXml;

namespace KPMG.WebKik.DocumentProcessing.NotificationOfParticipation.Sheets
{
    internal class NPSheet2 : NPSheetBase
    {
        private readonly IndividualCompany person;

        public NPSheet2(ExcelWorksheet sheet, IndividualCompany person, int pageNumber) : base(sheet, null, pageNumber)
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

                new SheetRange (Sheet.Cells[9, 16, 9, 118])     { Value = person.Surname }, //Фамилия
                new SheetRange (Sheet.Cells[11, 16, 11, 118])   { Value = person.Name }, //Имя
                new SheetRange (Sheet.Cells[13, 16, 13, 118])   { Value = person.MiddleName }, //Отчество
                new SheetRange (Sheet.OneCell(15, 16))          { Value = person.GenderCode?.Code }, //Пол
                new SheetRange (Sheet.Cells[15, 70, 15, 73])    { Value = person.BirthDate.Day.ToString("D2") }, //Число рождения
                new SheetRange (Sheet.Cells[15, 79, 15, 82])    { Value = person.BirthDate.Month.ToString("D2") }, //Месяц рождения
                new SheetRange (Sheet.Cells[15, 88, 15, 97])    { Value = person.BirthDate.Year.ToString("D2") }, //Год рождения
                new SheetRange (Sheet.Cells[19, 1, 21, 118])    { Value = person.BirthPlace }, //Место рождения
                new SheetRange (Sheet.OneCell(24, 16))          { Value = person.CitizenshipCode?.Code }, //Гражданство
                new SheetRange (Sheet.Cells[24, 100, 24, 106])  { Value = person.ForeignCountryCode?.Code.FormatCode("D3") }, //Код страны для иностранного гражданина

                new SheetRange (Sheet.Cells[26, 115, 26, 118])  { Value = person.VerifedPersonalityDocInfo?.DocumentCode?.Code.FormatCode("D2")}, //Документ удостоверяющего личность, код вида
                new SheetRange (Sheet.Cells[28, 13, 28, 70])    { Value = person.VerifedPersonalityDocInfo?.SeriesAndNumber }, //Документ удостоверяющего личность, серия и номер
                new SheetRange (Sheet.Cells[28, 91, 28, 94])    { Value = person.VerifedPersonalityDocInfo?.IssueDate.Day.ToString("D2") }, //Документ удостоверяющего личность, дата выдачи - день
                new SheetRange (Sheet.Cells[28, 100, 28, 103])  { Value = person.VerifedPersonalityDocInfo?.IssueDate.Month.ToString("D2") }, //Документ удостоверяющего личность, дата выдачи - месяц
                new SheetRange (Sheet.Cells[28, 109, 28, 118])  { Value = person.VerifedPersonalityDocInfo?.IssueDate.Year.ToString("D4") }, //Документ удостоверяющего личность, дата выдачи - год
                new SheetRange (Sheet.Cells[30, 16, 30, 118])   { Value = person.VerifedPersonalityDocInfo?.IssuePlace }, //Документ удостоверяющего личность, кем выдан

                new SheetRange (Sheet.Cells[32, 115, 32, 118])  { Value = person.ConfirmedPersonalityDocInfo?.DocumentCode?.Code.FormatCode("D2")}, //Документ подтверждающий регистрацию физического лица, код вида
                new SheetRange (Sheet.Cells[35, 13, 35, 70])    { Value = person.ConfirmedPersonalityDocInfo?.SeriesAndNumber }, //Документ подтверждающий регистрацию физического лица, серия и номер
                new SheetRange (Sheet.Cells[35, 91, 35, 94])    { Value = person.ConfirmedPersonalityDocInfo?.IssueDate.Day.ToString("D2") }, //Документ подтверждающий регистрацию физического лица, дата выдачи - день
                new SheetRange (Sheet.Cells[35, 100, 35, 103])  { Value = person.ConfirmedPersonalityDocInfo?.IssueDate.Month.ToString("D2") }, //Документ подтверждающий регистрацию физического лица, дата выдачи - месяц
                new SheetRange (Sheet.Cells[35, 109, 35, 118])  { Value = person.ConfirmedPersonalityDocInfo?.IssueDate.Year.ToString("D4") }, //Документ подтверждающий регистрацию физического лица, дата выдачи - год
                new SheetRange (Sheet.Cells[37, 16, 37, 118])   { Value = person.ConfirmedPersonalityDocInfo?.IssuePlace }, //Документ подтверждающий регистрацию физического лица, кем выдан

                new SheetRange (Sheet.Cells[39, 91, 39, 91])    { Value = person.RussianLocationCode?.Code}, //Место жительства/пребывания, код
                new SheetRange (Sheet.Cells[41, 22, 41, 37])    { Value = person.PostIndex}, //Место жительства/пребывания, почтовый индекс
                new SheetRange (Sheet.Cells[41, 61, 41, 64])    { Value = person.RegionCode?.Code.FormatCode("D2")}, //Место жительства/пребывания, код региона 
                new SheetRange (Sheet.Cells[43, 22, 43, 118])   { Value = person.District}, //Место жительства/пребывания, район 
                new SheetRange (Sheet.Cells[45, 22, 45, 118])   { Value = person.City}, //Место жительства/пребывания, город 
                new SheetRange (Sheet.Cells[47, 22, 47, 118])   { Value = person.CityType}, //Место жительства/пребывания, населенный пункт 
                new SheetRange (Sheet.Cells[49, 22, 49, 118])   { Value = person.Street}, //Место жительства/пребывания, улица
                new SheetRange (Sheet.Cells[51, 16, 51, 37])    { Value = person.HouseNumber}, //Место жительства/пребывания, номер дома
                new SheetRange (Sheet.Cells[51, 59, 51, 80])    { Value = person.BuildingNumber}, //Место жительства/пребывания, корпус
                new SheetRange (Sheet.Cells[51, 97, 51, 118])   { Value = person.AppartamentNumber}, //Место жительства/пребывания, квартира

                new SheetRange (Sheet.Cells[54, 112, 54, 118])  { Value = person.ForeignCountryCode?.Code.FormatCode("D3")}, //Место жительства иностранного гражданина, Код страны
                new SheetRange (Sheet.Cells[56, 1, 60, 118])    { Value = person.ForeignAddress}, //Место жительства иностранного гражданина, адрес
            });

            if (person.VerifedPersonalityDocInfo?.DocumentCode?.Code != "21") // 21 - Паспорт гражданина РФ
            {
                Ranges.AddRange(new List<SheetRange>() {
                    new SheetRange (Sheet.Cells[26, 115, 26, 118])  { Value = person.ConfirmedPersonalityDocInfo?.DocumentCode?.Code?.FormatCode("D2")}, //Документ подтверждающий регистрацию, код вида
                    new SheetRange (Sheet.Cells[28, 13, 28, 70])    { Value = person.ConfirmedPersonalityDocInfo?.SeriesAndNumber }, //Документ подтверждающий регистрацию, серия и номер
                    new SheetRange (Sheet.Cells[28, 91, 28, 94])    { Value = person.ConfirmedPersonalityDocInfo?.IssueDate.Day.ToString("D2") }, //Документ подтверждающий регистрацию, дата выдачи - день
                    new SheetRange (Sheet.Cells[28, 100, 28, 103])  { Value = person.ConfirmedPersonalityDocInfo?.IssueDate.Month.ToString("D2") }, //Документ подтверждающий регистрацию, дата выдачи - месяц
                    new SheetRange (Sheet.Cells[28, 109, 28, 118])  { Value = person.ConfirmedPersonalityDocInfo?.IssueDate.Year.ToString("D4") }, //Документ подтверждающий регистрацию, дата выдачи - год
                    new SheetRange (Sheet.Cells[30, 16, 30, 118])   { Value = person.ConfirmedPersonalityDocInfo?.IssuePlace }, //Документ удостоверяющего личность, кем выдан
                });
            }

        }
    }
}
