using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using KPMG.WebKik.Models.ProjectCompanies;


namespace KPMG.WebKik.DocumentProcessing
{
    public class ExcelDocsCreator
    {
        public const string NamedRusSheet = "Стр.";
        public const string TemplateNPFilePath = @"\Templates\UU.xlsx";
        public const string TemplateKIKFilePath = @"\Templates\KIK.xlsx";
        public const string PersonalCompanyCodeRus = "ИП";
        public const string ForeginCompanyCodeRus = "ИО";
        public const string ForeginLightCompanyCodeRus = "ИС";

        public int SheetCount { get; set; }
        public int CompanyId { get; set; }


        public XLWorkbook GetFilledKikDoc(string path, ProjectCompany company)
        {
            var kikWorkDoc = new XLWorkbook(path);
            WriteStringInTripledCells(18, 1, company.Name, kikWorkDoc, 1);
            return kikWorkDoc;
        }

        public XLWorkbook GetFilledNotificationOfParticipation(string path, ProjectCompany company, List<ProjectCompanyShare> shares, List<ProjectCompanyFactShare> factShares)
        {
            return GetFilledNotificationOfParticipation(path, company);
            
        }

        public XLWorkbook GetFilledNotificationOfParticipation(string path, ProjectCompany company)
        {

            var notificationDoc = new XLWorkbook(path);
            //var dependedForeginCompaies = company.OwnerProjectCompanyShares.Where(x => x.DependentProjectCompany.State == State.Foreign);
            //var dependedFactForeginCompaies = company.OwnerProjectCompanyFactShares.Where(x => x.DependentProjectCompany.State == State.Foreign);

            //var dependedDomesticCompaies = company.OwnerProjectCompanyShares.Where(x => x.DependentProjectCompany.State == State.Domestic);
            //var dependedDomesticCompaiesCount = dependedDomesticCompaies.Count();
            //var dependedFactDomesticCompaies = company.OwnerProjectCompanyFactShares.Where(x => x.DependentProjectCompany.State == State.Domestic);

            //var dependedForeginLightCompaies = company.OwnerProjectCompanyShares.Where(x => x.DependentProjectCompany.State == State.ForeignLight);
            //var dependedForeginLightCompaiesCount = dependedForeginLightCompaies.Count();
            //var dependedFactForeginLightCompaies = company.OwnerProjectCompanyFactShares.Where(x => x.DependentProjectCompany.State == State.ForeignLight);

            CompanyId = company.Id;
            SheetCount = 1;

            ProcessPage1(SheetCount, notificationDoc, company);
            SheetCount++;
            if (company.State == State.Individual)
            {
                ProcessPage2(SheetCount, notificationDoc, company);
            }
            SheetCount ++;

            int fCompanyNumber = 1;
            
            //foreach (var share in dependedForeginCompaies)
            //{
            //    var factShare = new ProjectCompanyFactShare();
            //    //if (dependedFactForeginCompaies.Count()>0 )
            //    //{
            //    //    factShare = dependedFactForeginCompaies.Where(f => f.DependentProjectCompanyId == share.DependentProjectCompanyId).FirstOrDefault();
            //    //}

            //    ProcessListA(SheetCount, fCompanyNumber, notificationDoc, share, factShare);

            //    notificationDoc.Worksheet(SheetCount).CopyTo(NamedRusSheet + (SheetCount + 1), SheetCount + 1);
            //    SheetCount++;
            //    fCompanyNumber++;
            //}
            //SheetCount += dependedForeginCompaiesCount;

            //FillNpForeginLight(notificationDoc, dependedForeginLightCompaies, SheetCount);
            //SheetCount += dependedForeginLightCompaiesCount;

            //FillNpPersonalCompanyDetailShare(notificationDoc, dependedPersonalCompaies, dependedFactPersonalCompaies, SheetCount);
            //SheetCount += dependedPersonalCompaiesCount;

            //FillNpForeginCompanyDetailShare(notificationDoc, dependedForeginCompaies, dependedFactForeginCompaies, SheetCount);
            //SheetCount += dependedForeginCompaiesCount;

            //FillNpForeginLightDetailShare(notificationDoc, dependedForeginLightCompaies, dependedFactForeginLightCompaies, SheetCount);
            //SheetCount += dependedForeginLightCompaiesCount;
            ////Fill PageCountOnFirstPage
            //RemoveLastSheet(notificationDoc);
            return notificationDoc;
        }

        private void ProcessPage1(int currentSheetNum, XLWorkbook wb, ProjectCompany company)
        {

            var fullCompanyName = "";
            var taxpayerCode = 0;
            var reasonCode = 0;
            int correctionNumber = 0;
            
            switch (company.State)
            {
                case State.Domestic:
                    taxpayerCode = 1;
                    fullCompanyName = company.DomesticCompany.FullName;
                    break;

                case State.Individual:
                    taxpayerCode = 2;
                    fullCompanyName = company.IndividualCompany.Surname + " " + company.IndividualCompany.Name + " " +
                               company.IndividualCompany.MiddleName;
                    break;
                    
                case State.Foreign:
                    taxpayerCode = 3;
                    fullCompanyName = company.ForeignCompany.FullName;
                    break;
            }
            
            WriteStringInTripledCells(1, 37, company.DomesticCompany.INN.ToString(), wb, currentSheetNum);
            WriteStringInTripledCells(4, 37, company.DomesticCompany.KPP, wb, currentSheetNum);
            WriteStringInTripledCells(4, 70, currentSheetNum.ToString("D3"), wb, currentSheetNum);
            WriteStringInTripledCells(12, 24, correctionNumber.ToString("D3"), wb, currentSheetNum);
            WriteStringInTripledCells(14, 28, taxpayerCode.ToString("D1"), wb, currentSheetNum);
            WriteStringInTripledCells(14, 76, reasonCode.ToString("D1"), wb, currentSheetNum);
            WriteStringInTripledCells(18, 1, fullCompanyName, wb, currentSheetNum);
            WriteStringInTripledCells(28, 10, company.DomesticCompany.OGRN.ToString(), wb, currentSheetNum);

            WriteMultipleStringInTripledCells(37, 1, fullCompanyName, wb, currentSheetNum);
            WriteStringInTripledCells(47, 10, company.DomesticCompany.INN.ToString(), wb, currentSheetNum);



        }

        private void ProcessPage2(int currentSheetNum, XLWorkbook wb, ProjectCompany company)
        {

                WriteStringInTripledCells(9, 16, company.IndividualCompany.Surname, wb, currentSheetNum);
                WriteStringInTripledCells(11, 16, company.IndividualCompany.Name, wb, currentSheetNum);
                WriteStringInTripledCells(13, 16, company.IndividualCompany.MiddleName, wb, currentSheetNum);
                WriteStringInTripledCells(13, 16, company.IndividualCompany.MiddleName, wb, currentSheetNum);
                WriteStringInTripledCells(15, 16, company.IndividualCompany.GenderCodeId.ToString(), wb, currentSheetNum);
                WriteStringInTripledCells(15, 70, company.IndividualCompany.BirthDate.Date.ToString("dd"), wb, currentSheetNum);
                WriteStringInTripledCells(15, 79, company.IndividualCompany.BirthDate.Month.ToString("D2"), wb, currentSheetNum);
                WriteStringInTripledCells(15, 88, company.IndividualCompany.BirthDate.Year.ToString("D4"), wb, currentSheetNum);
                WriteStringInTripledCells(19, 1, company.IndividualCompany.BirthPlace, wb, currentSheetNum);
                WriteStringInTripledCells(24, 16, company.IndividualCompany.GenderCodeId.ToString(), wb, currentSheetNum);
                WriteStringInTripledCells(26, 115, company.IndividualCompany.ConfirmedPersonalityDocInfoId.ToString() ?? "1", wb, currentSheetNum);
                WriteStringInTripledCells(28, 13, company.IndividualCompany.ConfirmedPersonalityDocInfo.SeriesAndNumber, wb, currentSheetNum);
                WriteStringInTripledCells(28, 91, company.IndividualCompany.ConfirmedPersonalityDocInfo.IssueDate.Date.ToString("dd"), wb, currentSheetNum);
                WriteStringInTripledCells(28, 100, company.IndividualCompany.ConfirmedPersonalityDocInfo.IssueDate.Month.ToString("D2"), wb, currentSheetNum);
                WriteStringInTripledCells(28, 109, company.IndividualCompany.ConfirmedPersonalityDocInfo.IssueDate.Year.ToString("D4"), wb, currentSheetNum);
                WriteStringInTripledCells(30, 16, company.IndividualCompany.ConfirmedPersonalityDocInfo.IssuePlace, wb, currentSheetNum);
                WriteStringInTripledCells(35, 13, company.IndividualCompany.VerifedPersonalityDocInfo.SeriesAndNumber, wb, currentSheetNum);
                WriteStringInTripledCells(35, 91, company.IndividualCompany.VerifedPersonalityDocInfo.IssueDate.Date.ToString("dd"), wb, currentSheetNum);
                WriteStringInTripledCells(35, 100, company.IndividualCompany.VerifedPersonalityDocInfo.IssueDate.Month.ToString("D2"), wb, currentSheetNum);
                WriteStringInTripledCells(35, 109, company.IndividualCompany.VerifedPersonalityDocInfo.IssueDate.Year.ToString("D4"), wb, currentSheetNum);
                WriteStringInTripledCells(37, 16, company.IndividualCompany.VerifedPersonalityDocInfo.IssuePlace, wb, currentSheetNum);
                WriteStringInTripledCells(39, 91, company.IndividualCompany.ConfirmedPersonalityDocInfoId.ToString(), wb, currentSheetNum);
                WriteStringInTripledCells(41, 22, company.IndividualCompany.PostIndex, wb, currentSheetNum);
                WriteStringInTripledCells(43, 22, company.IndividualCompany.District, wb, currentSheetNum);
                WriteStringInTripledCells(45, 22, company.IndividualCompany.City, wb, currentSheetNum);
                WriteStringInTripledCells(47, 22, company.IndividualCompany.CityType, wb, currentSheetNum);
                WriteStringInTripledCells(49, 22, company.IndividualCompany.District, wb, currentSheetNum);
                WriteStringInTripledCells(51, 16, company.IndividualCompany.HouseNumber, wb, currentSheetNum);
                WriteStringInTripledCells(51, 59, company.IndividualCompany.BuildingNumber, wb, currentSheetNum);
                WriteStringInTripledCells(51, 97, company.IndividualCompany.AppartamentNumber, wb, currentSheetNum);
                WriteStringInTripledCells(54, 112, "1", wb, currentSheetNum);
                WriteStringInTripledCells(56, 1, company.IndividualCompany.ForeignAddress ?? "", wb, currentSheetNum);

        }

        private void ProcessListA(int currentSheetNum, int companyNumber, XLWorkbook wb, ProjectCompanyShare share, ProjectCompanyFactShare factShare)
        {
            int shareType = 1;
            double shareResult = share.SharePart;

            if (factShare.ShareFactPart > 0 && share.SharePart == 0)
            {
                shareType = 2;
            }
            if (factShare.ShareFactPart > 0 && share.SharePart >0)
            {
                shareType = 3;
                shareResult += factShare.ShareFactPart;
            }
                wb.Worksheet(currentSheetNum).Name = NamedRusSheet + currentSheetNum;
                WriteStringInTripledCells(13, 52, companyNumber.ToString("D5"), wb, currentSheetNum);
                WriteMultipleStringInTripledCells(17, 1, share.DependentProjectCompany.ForeignCompany.FullName, wb, currentSheetNum);
                WriteMultipleStringInTripledCells(25, 1, share.DependentProjectCompany.ForeignCompany.Name, wb, currentSheetNum);
                WriteStringInTripledCells(33, 44, share.DependentProjectCompany.ForeignCompany.CountryCodeId.ToString("D3") ?? "---", wb, currentSheetNum);
                WriteMultipleStringInTripledCells(37, 1, share.DependentProjectCompany.ForeignCompany.RegistrationNumber, wb, currentSheetNum);
                WriteMultipleStringInTripledCells(43, 1, share.DependentProjectCompany.ForeignCompany.TaxPayerCodeId.ToString() ?? "-", wb, currentSheetNum);
                WriteStringInTripledCells(37, 1, share.DependentProjectCompany.ForeignCompany.RegistrationNumber, wb, currentSheetNum);

                WriteStringInTripledCells(56, 43, (shareType).ToString(), wb, currentSheetNum);

                WriteStringInTripledCells(58, 43, share.ShareStartDate.Date.ToString("dd"), wb, currentSheetNum);
                WriteStringInTripledCells(58, 52, share.ShareStartDate.Month.ToString("D2"), wb, currentSheetNum);
                WriteStringInTripledCells(58, 61, share.ShareStartDate.Date.Year.ToString("D4"), wb, currentSheetNum);

                WriteStringInTripledCells(60, 43, Math.Truncate(shareResult).ToString("000"), wb, currentSheetNum);
                WriteStringInTripledCells(60, 55, ((shareResult - Math.Truncate(shareResult)) * 100000).ToString("00000"), wb, currentSheetNum);

                var isDateNull = share.ShareFinishDate == null;

                WriteStringInTripledCells(62, 43, isDateNull ? "--" : share.ShareFinishDate.Value.Date.ToString("dd"), wb, currentSheetNum);
                WriteStringInTripledCells(62, 52, isDateNull ? "--" : share.ShareFinishDate.Value.Month.ToString("D2"), wb, currentSheetNum);
                WriteStringInTripledCells(62, 61, isDateNull ? "----" : share.ShareFinishDate.Value.Year.ToString("D4"), wb, currentSheetNum);

            
        }

        private void FillNpForeginLight(XLWorkbook wb, IEnumerable<ProjectCompanyShare> depShares, int currentSheetNum)
        {

            var cmpNum = 1;
            foreach (var share in depShares)
            {
                wb.Worksheet(currentSheetNum).Name = NamedRusSheet + currentSheetNum;
                WriteStringInTripledCells(13, 52, cmpNum.ToString("D5"), wb, currentSheetNum);
                WriteStringInTripledCells(16, 37, share.DependentProjectCompany.ForeignLightCompany.ForeignOrganizationalFormCodeId.ToString(), wb, currentSheetNum);
                WriteMultipleStringInTripledCells(20, 1, share.DependentProjectCompany.ForeignLightCompany.RussianName, wb, currentSheetNum);
                WriteMultipleStringInTripledCells(28, 1, share.DependentProjectCompany.ForeignLightCompany.EnglishName, wb, currentSheetNum);
                WriteMultipleStringInTripledCells(38, 1, share.DependentProjectCompany.ForeignLightCompany.RequisitesRus, wb, currentSheetNum);
                WriteMultipleStringInTripledCells(42, 1, share.DependentProjectCompany.ForeignLightCompany.RequisitesEng, wb, currentSheetNum);
                WriteStringInTripledCells(47, 37, share.DependentProjectCompany.ForeignLightCompany.FoundDate.Date.ToString("dd"), wb, currentSheetNum);
                WriteStringInTripledCells(47, 46, share.DependentProjectCompany.ForeignLightCompany.FoundDate.Month.ToString("D2"), wb, currentSheetNum);
                WriteStringInTripledCells(47, 55, share.DependentProjectCompany.ForeignLightCompany.FoundDate.Year.ToString("D4"), wb, currentSheetNum);
                WriteStringInTripledCells(47, 112, share.DependentProjectCompany.ForeignLightCompany.CountryCodeId.ToString("D3"), wb, currentSheetNum);
                WriteStringInTripledCells(52, 1, share.DependentProjectCompany.ForeignLightCompany.RegNumber, wb, currentSheetNum);
                WriteMultipleStringInTripledCells(56, 1, share.DependentProjectCompany.ForeignLightCompany.OtherInfo, wb, currentSheetNum);
                int nextSheetNum = currentSheetNum + 1;
                wb.Worksheet(currentSheetNum).CopyTo(NamedRusSheet + nextSheetNum, nextSheetNum);
                cmpNum++;
                currentSheetNum++;
            }

            //if (cCount == 0)
            //{
            //    wb.Worksheet(sheetNum).Delete();
            //    SheetCount--;
            //    return;
            //}

            //wb.Worksheet(sheetNum).Name = NamedRusSheet + sheetNum;
            //WriteStringInTripledCells(4, 70, sheetNum.ToString("D3"), wb, sheetNum);

            //if (cCount > 1)
            //{
            //    for (var i = 1; i < cCount; i++)
            //    {
            //        var wsSource = wb.Worksheet(sheetNum);
            //        var sheetForNum = sheetNum + i;
            //        wsSource.CopyTo(NamedRusSheet + sheetForNum, sheetForNum);
            //        WriteStringInTripledCells(4, 70, sheetForNum.ToString("D3"), wb, sheetForNum);
            //    }

            //}
            //var cmpNum = 1;
            //foreach (var share in depShares)
            //{
            //    WriteStringInTripledCells(13, 52, cmpNum.ToString("D5"), wb, sheetNum);
            //    WriteStringInTripledCells(16, 37, share.DependentProjectCompany.ForeignLightCompany.ForeignOrganizationalFormCodeId.ToString(), wb, sheetNum);
            //    WriteMultipleStringInTripledCells(20, 1, share.DependentProjectCompany.ForeignLightCompany.RussianName, wb, sheetNum);
            //    WriteMultipleStringInTripledCells(28, 1, share.DependentProjectCompany.ForeignLightCompany.EnglishName, wb, sheetNum);
            //    WriteMultipleStringInTripledCells(38, 1, share.DependentProjectCompany.ForeignLightCompany.RequisitesRus, wb, sheetNum);
            //    WriteMultipleStringInTripledCells(42, 1, share.DependentProjectCompany.ForeignLightCompany.RequisitesEng, wb, sheetNum);
            //    WriteStringInTripledCells(47, 37, share.DependentProjectCompany.ForeignLightCompany.FoundDate.Date.ToString("dd"), wb, sheetNum);
            //    WriteStringInTripledCells(47, 46, share.DependentProjectCompany.ForeignLightCompany.FoundDate.Month.ToString("D2"), wb, sheetNum);
            //    WriteStringInTripledCells(47, 55, share.DependentProjectCompany.ForeignLightCompany.FoundDate.Year.ToString("D4"), wb, sheetNum);
            //    WriteStringInTripledCells(47, 112, share.DependentProjectCompany.ForeignLightCompany.CountryCodeId.ToString("D3"), wb, sheetNum);
            //    WriteStringInTripledCells(52, 1, share.DependentProjectCompany.ForeignLightCompany.RegNumber, wb, sheetNum);
            //    WriteMultipleStringInTripledCells(56, 1, share.DependentProjectCompany.ForeignLightCompany.OtherInfo, wb, sheetNum);
            //    cmpNum++;
            //    sheetNum++;
            //}
            if (depShares.Count() == 0)
            {
                SheetCount++;
            }
            

        }

        private void FillNpPersonalCompanyDetailShare(XLWorkbook wb, IEnumerable<ProjectCompanyShare> depShares, IEnumerable<ProjectCompanyFactShare> depFactShares, int currentSheetNum)
        {

            var cmpNum = 1;
            foreach (var share in depShares)
            {
                wb.Worksheet(currentSheetNum).Name = NamedRusSheet + currentSheetNum;
                var fullName = share.DependentProjectCompany.IndividualCompany.Surname + " " +
                share.DependentProjectCompany.IndividualCompany.Name + " " +
                share.DependentProjectCompany.IndividualCompany.MiddleName;

                WriteStringInTripledCells(4, 70, currentSheetNum.ToString("D3"), wb, currentSheetNum);
                WriteStringInTripledCells(15, 61, PersonalCompanyCodeRus, wb, currentSheetNum);
                WriteStringInTripledCells(15, 70, cmpNum.ToString("D5"), wb, currentSheetNum);

                WriteMultipleStringInTripledCells(19, 1, fullName, wb, currentSheetNum);

                int nextSheetNum = currentSheetNum + 1;
                wb.Worksheet(currentSheetNum).CopyTo(NamedRusSheet + nextSheetNum, nextSheetNum);
                cmpNum++;
                currentSheetNum++;
            }

            //foreach (var dp in depFactShares)
            //{
            //    var paths = Helper.GetSharedInfo(depFactShares.Where(s => s.ShareFactPart > 0).ToList(), CompanyId, dp.Id);
            //    foreach (TaggedEdge<int, double> path in paths)
            //    {

            //      WriteMultipleStringInTripledCells(39, 45, path.Tag.ToString("D5"), wb, currentSheetNum);
            //    }
            //}



        }

        private void FillNpForeginCompanyDetailShare(XLWorkbook wb, IEnumerable<ProjectCompanyShare> depShares, IEnumerable<ProjectCompanyFactShare> depFactShares, int currentSheetNum)
        {
            var cmpNum = 1;
            foreach (var share in depShares)
            {
                wb.Worksheet(currentSheetNum).Name = NamedRusSheet + currentSheetNum;
                WriteStringInTripledCells(15, 61, ForeginCompanyCodeRus, wb, currentSheetNum);
                WriteStringInTripledCells(15, 70, cmpNum.ToString("D5"), wb, currentSheetNum);
                WriteMultipleStringInTripledCells(19, 1, share.DependentProjectCompany.ForeignCompany.FullName, wb, currentSheetNum);
                int nextSheetNum = currentSheetNum + 1;
                wb.Worksheet(currentSheetNum).CopyTo(NamedRusSheet + nextSheetNum, nextSheetNum);
                cmpNum++;
                currentSheetNum++;
            }

        }

        private void FillNpForeginLightDetailShare(XLWorkbook wb, IEnumerable<ProjectCompanyShare> depShares, IEnumerable<ProjectCompanyFactShare> depFactShares, int currentSheetNum)
        {
            var cmpNum = 1;
            foreach (var share in depShares)
            {
                wb.Worksheet(currentSheetNum).Name = NamedRusSheet + currentSheetNum;

                WriteStringInTripledCells(4, 70, currentSheetNum.ToString("D3"), wb, currentSheetNum);
                WriteStringInTripledCells(15, 61, ForeginLightCompanyCodeRus, wb, currentSheetNum);
                WriteStringInTripledCells(15, 70, cmpNum.ToString("D5"), wb, currentSheetNum);
                WriteMultipleStringInTripledCells(19, 1, share.DependentProjectCompany.ForeignLightCompany.RussianName, wb, currentSheetNum);

                int nextSheetNum = currentSheetNum + 1;
                wb.Worksheet(currentSheetNum).CopyTo(NamedRusSheet + nextSheetNum, nextSheetNum);
                cmpNum++;
                currentSheetNum++;
            }

        }




        private void RemoveLastSheet(XLWorkbook wb)
        {
            wb.Worksheet(wb.Worksheets.Count).Delete();
        }

        private void WriteStringInTripledCells(int rowNum, int colNum, string nameValue, XLWorkbook wb, int worksheetNumber)
        {
            if (nameValue != null)
            {
                foreach (char ch in nameValue)
                {
                    wb.Worksheet(worksheetNumber).Cell(rowNum, colNum).Value = "'" + ch;
                    colNum = colNum + 3;
                }
            }
        }

        private void WriteStringInCell(int rowNum, int colNum, string nameValue, XLWorkbook wb, int worksheetNumber)
        {
            //for email field on first page
            if (nameValue != null)
            {
                    wb.Worksheet(worksheetNumber).Cell(rowNum, colNum).Value = nameValue;
            }
        }


        private void WriteMultipleStringInTripledCells(int rowNum, int colNum, string nameValue, XLWorkbook wb, int worksheetNumber)
        {
            if (nameValue != null)
            {
                int initialColNum = colNum;

                foreach (char ch in nameValue)
                {
                    if (colNum <= 118)
                    {
                        wb.Worksheet(worksheetNumber).Cell(rowNum, colNum).Value = "'" + ch;
                        colNum = colNum + 3;
                    }
                    else
                    {
                        colNum = initialColNum;
                        rowNum = rowNum + 2;

                        wb.Worksheet(worksheetNumber).Cell(rowNum, colNum).Value = "'" + ch;
                        colNum = colNum + 3;
                    }

                }
            }
        }




    }
}
