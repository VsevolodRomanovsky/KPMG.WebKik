using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Contracts.Service.Registers;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.ProjectCompanies;
using OfficeOpenXml;
using System.IO;
using System.Linq;
using KPMG.WebKik.DocumentProcessing.Helpers;


namespace KPMG.WebKik.DocumentProcessing.NotificationOfKIK
{
    public class NotificationOfKIKWorkbook : INotificationOfKIKDocument
    {
        private IList<ProjectCompany> kiks;
        private ProjectCompany ownerCompany;

        //private CompanyNumberContainer companyNumberContainer;

        private IEnumerable<ProjectCompanyFactShare> factShares;
        private IProjectCompanyService projectCompanyService;
        private IReportCompanyService reportCompanyService;
        private IProjectCompanyShareService shareService;
        private IRegister1Service register1Service;
        private IRegister3Service register3Service;
        private IRegister9Service register9Service;

        public NotificationOfKIKWorkbook(IProjectCompanyService projectCompanyService, IProjectCompanyShareService shareService, IReportCompanyService reportCompanyService, IRegister1Service register1Service,
            IRegister3Service register3Service, IRegister9Service register9Service)
        {
            this.projectCompanyService = projectCompanyService;
            this.shareService = shareService;
            this.reportCompanyService = reportCompanyService;
            this.register1Service = register1Service;
            this.register3Service = register3Service;
            this.register9Service = register9Service;
        }


        public async Task<byte[]> GetDocumentData(ProjectCompany ownerCompany, IList<ProjectCompanyFactShare> factShares, Signatory signature,
            string templatePath, int year, int correction, int taxAuthorityCode)
        {
            //companyNumberContainer = new CompanyNumberContainer();
            this.factShares = factShares;

            byte[] result;
            using (var package = new ExcelPackage(new FileInfo(templatePath)))
            {

                this.ownerCompany = ownerCompany;

                ExcelWorkbook workbook = package.Workbook;
                var sheet1 = workbook.Worksheets["стр 1"];
                var sheet2 = workbook.Worksheets["стр 2"];
                var sheetA = workbook.Worksheets["А"];
                var sheetA1 = workbook.Worksheets["А1"];
                var sheetB = workbook.Worksheets["Б"];
                //var sheetB1 = workbook.Worksheets["Б1"];
                var sheetV = workbook.Worksheets["В"];
                var sheetG = workbook.Worksheets["Г"];
                var sheetG1 = workbook.Worksheets["Г1"];
                var sheetG2 = workbook.Worksheets["Г2"];


                FillCell(sheet1, "[correctionnumber]", correction.ToString("D3"));
                FillCell(sheet1, "[period]", year);
                FillCell(sheet1, "[taxauthoritycode]", taxAuthorityCode);



                //Подписант
                FillCell(sheet1, "[signatorycode]", signature?.SignatoryCode?.Code);
                FillLongCell(sheet1, "[signatoryname]",
                    $"{signature.LastName} {signature.FirstName} {signature.MiddleName}", 20);
                FillCell(sheet1, "[signatoryinn]", signature.Inn);
                FillCell(sheet1, "[signatoryphone]", signature.PhoneNumber);
                FillCellValue(sheet1, "B55:BH55", signature.Email);
                FillLongCell(sheet1, "[signatorydocument]", signature?.ConfirmationDocument?.Name, 20);

                var pageNumber = 3;

                switch (this.ownerCompany.State)
                {
                    case State.Domestic:

                        FillCell(sheet1, "[inn]", this.ownerCompany.DomesticCompany.INN);
                        FillCell(sheet1, "[kpp]", this.ownerCompany.DomesticCompany.KPP);


                        FillCell(sheet1, "[taxpayercode]", 1);
                        FillLongCell(sheet1, "[information]", this.ownerCompany.DomesticCompany.FullName, 40);

                        workbook.Worksheets.Delete(sheet2);
                        pageNumber--;

                        break;

                    case State.Individual:

                        if (string.IsNullOrEmpty(signature.Inn))
                        {
                            FillCellValue(sheet2, "P6:CO6", signature.LastName);
                            FillCellValue(sheet2, "CU6:CY6", signature.FirstName[0]);
                            FillCellValue(sheet2, "DE6:DI6", signature.MiddleName[0]);
                        }

                        FillCell(sheet1, "[inn]", this.ownerCompany.IndividualCompany.INN);
                        FillCell(sheet1, "[taxpayercode]", 2);
                        FillLongCell(sheet1, "[information]",
                            $"{this.ownerCompany.IndividualCompany.Surname} {this.ownerCompany.IndividualCompany.Name} {this.ownerCompany.IndividualCompany.MiddleName}", 40);

                        //стр 2
                        FillCell(sheet2, "[gender]", this.ownerCompany.IndividualCompany.GenderCode);
                        FillCell(sheet2, "[datebirth]", this.ownerCompany.IndividualCompany.BirthDate.Day > 9 ? this.ownerCompany.IndividualCompany.BirthDate.Day.ToString() : "0" + this.ownerCompany.IndividualCompany.BirthDate.Day);
                        FillCell(sheet2, "[monthbirth]", this.ownerCompany.IndividualCompany.BirthDate.Month > 9 ? this.ownerCompany.IndividualCompany.BirthDate.Month.ToString() : "0" + this.ownerCompany.IndividualCompany.BirthDate.Month);
                        FillCell(sheet2, "[yearbirth]", this.ownerCompany.IndividualCompany.BirthDate.Year);
                        FillLongCell(sheet2, "[placeofbirth]", this.ownerCompany.IndividualCompany.BirthPlace, 40);
                        FillCell(sheet2, "[citizenship]", this.ownerCompany.IndividualCompany.CitizenshipCode);
                        FillCell(sheet2, "[countrycode]", this.ownerCompany.IndividualCompany.RegionCode);

                        if (this.ownerCompany.IndividualCompany.VerifedPersonalityDocInfo != null)
                        {
                            FillCell(sheet2, "[passportcode]", this.ownerCompany.IndividualCompany.VerifedPersonalityDocInfo.DocumentCode?.Code);
                            FillCell(sheet2, "[passportnumber]", this.ownerCompany.IndividualCompany.VerifedPersonalityDocInfo.SeriesAndNumber);
                            FillCell(sheet2, "[datepassport]", this.ownerCompany.IndividualCompany.VerifedPersonalityDocInfo.IssueDate.Day > 9 ?
                                this.ownerCompany.IndividualCompany.VerifedPersonalityDocInfo.IssueDate.Day.ToString() : "0" + this.ownerCompany.IndividualCompany.VerifedPersonalityDocInfo.IssueDate.Day);
                            FillCell(sheet2, "[monthpassport]", this.ownerCompany.IndividualCompany.VerifedPersonalityDocInfo.IssueDate.Month > 9 ?
                                this.ownerCompany.IndividualCompany.VerifedPersonalityDocInfo.IssueDate.Month.ToString() : "0" + this.ownerCompany.IndividualCompany.VerifedPersonalityDocInfo.IssueDate.Month);
                            FillCell(sheet2, "[yearpassport]", this.ownerCompany.IndividualCompany.VerifedPersonalityDocInfo.IssueDate.Year);
                            FillCell(sheet2, "[issuedbypassport]", this.ownerCompany.IndividualCompany.VerifedPersonalityDocInfo.IssuePlace);
                        }

                        if (this.ownerCompany.IndividualCompany.ConfirmedPersonalityDocInfo != null)
                        {

                            FillCell(sheet2, "[documentcode]", this.ownerCompany.IndividualCompany.ConfirmedPersonalityDocInfo.DocumentCode?.Code);
                            FillCell(sheet2, "[documentnumber]", this.ownerCompany.IndividualCompany.ConfirmedPersonalityDocInfo.SeriesAndNumber);
                            FillCell(sheet2, "[datedocument]", this.ownerCompany.IndividualCompany.ConfirmedPersonalityDocInfo.IssueDate.Day > 9 ?
                                this.ownerCompany.IndividualCompany.ConfirmedPersonalityDocInfo.IssueDate.Day.ToString() : "0" + this.ownerCompany.IndividualCompany.ConfirmedPersonalityDocInfo.IssueDate.Day);
                            FillCell(sheet2, "[monthdocument]", this.ownerCompany.IndividualCompany.ConfirmedPersonalityDocInfo.IssueDate.Month > 9 ?
                                this.ownerCompany.IndividualCompany.ConfirmedPersonalityDocInfo.IssueDate.Month.ToString() : "0" + this.ownerCompany.IndividualCompany.ConfirmedPersonalityDocInfo.IssueDate.Month);
                            FillCell(sheet2, "[yeardocument]", this.ownerCompany.IndividualCompany.ConfirmedPersonalityDocInfo.IssueDate.Year);
                            FillCell(sheet2, "[issuedbydocument]", this.ownerCompany.IndividualCompany.ConfirmedPersonalityDocInfo.IssuePlace);
                        }

                        FillCell(sheet2, "[locationcode]", this.ownerCompany.IndividualCompany.RussianLocationCode);
                        FillCell(sheet2, "[district]", this.ownerCompany.IndividualCompany.District);
                        FillCell(sheet2, "[city]", this.ownerCompany.IndividualCompany.City);
                        FillCell(sheet2, "[locality]", this.ownerCompany.IndividualCompany.CityType);
                        FillCell(sheet2, "[street]", this.ownerCompany.IndividualCompany.Street);
                        FillCell(sheet2, "[housenumber]", this.ownerCompany.IndividualCompany.HouseNumber);
                        FillCell(sheet2, "[hullnumber]", this.ownerCompany.IndividualCompany.BuildingNumber);
                        FillCell(sheet2, "[appartmentnumber]", this.ownerCompany.IndividualCompany.AppartamentNumber);


                        FillCell(sheet2, "[addresscountrycode]", this.ownerCompany.IndividualCompany.ForeignCountryCode);
                        FillLongCell(sheet2, "[address]", this.ownerCompany.IndividualCompany.ForeignAddress, 40);

                        ClearCells(sheet2);
                        break;

                    default:
                        break;
                }


                var ACount = 1;
                var BCount = 1;
                var VCount = 1;

                kiks = await shareService.GetAllKIKsByProjectCompanyId(this.ownerCompany.Id);


                foreach (var kik in kiks)
                {
                    var company = await projectCompanyService.GetById(kik.Id);

                    var sheetVCopy = workbook.Worksheets.Add("В-" + VCount, sheetV);
                    FillCell(sheetVCopy, "[pagenumber]", pageNumber.ToString("D3"));
                    pageNumber++;

                    FillCellValue(sheetVCopy, "Q7:CP7", signature.LastName);
                    FillCellValue(sheetVCopy, "CV7:CZ7", signature.FirstName[0]);
                    FillCellValue(sheetVCopy, "DF7:DJ7", signature.MiddleName[0]);

                    FillCell(sheetVCopy, "[profitcode]", 1);

                    switch (this.ownerCompany.State)
                    {
                        case State.Domestic:

                            FillCell(sheetVCopy, "[inn]", this.ownerCompany.DomesticCompany.INN);
                            FillCell(sheetVCopy, "[kpp]", this.ownerCompany.DomesticCompany.KPP);
                            break;

                        case State.Individual:

                            FillCell(sheetVCopy, "[inn]", this.ownerCompany.IndividualCompany.INN);
                            break;
                    }

                    ProjectCompanyShare share = null;
                    var shares = await shareService.GetAllByProjectCompanyId(this.ownerCompany.Id);
                    shares = shares.Where(x => x.DependentProjectCompanyId == company.Id).ToList();
                    if (shares.Count > 0)
                        share = shares.Where(x => x.DependentProjectCompanyId == company.Id).First();
                    

                    switch (company.State)
                    {
                        case State.Foreign:

                            var sheetACopy = workbook.Worksheets.Add("А-" + ACount, sheetA);
                            FillCell(sheetACopy, "[pagenumber]", pageNumber.ToString("D3"));
                            pageNumber++;

                            var sheetA1Copy = workbook.Worksheets.Add("А1-" + ACount, sheetA1);
                            FillCell(sheetA1Copy, "[pagenumber]", pageNumber.ToString("D3"));
                            pageNumber++;

                            switch (this.ownerCompany.State)
                            {
                                case State.Domestic:

                                    FillCell(sheetACopy, "[inn]", this.ownerCompany.DomesticCompany.INN);
                                    FillCell(sheetACopy, "[kpp]", this.ownerCompany.DomesticCompany.KPP);

                                    FillCell(sheetA1Copy, "[inn]", this.ownerCompany.DomesticCompany.INN);
                                    FillCell(sheetA1Copy, "[kpp]", this.ownerCompany.DomesticCompany.KPP);

                                    FillCell(sheetA1Copy, "[103]", "2");
                                    FillCell(sheetA1Copy, "[104]", "2");
                                    FillCell(sheetA1Copy, "[105]", "2");


                                    break;

                                case State.Individual:

                                    FillCell(sheetACopy, "[inn]", this.ownerCompany.IndividualCompany.INN);
                                    FillCell(sheetA1Copy, "[inn]", this.ownerCompany.IndividualCompany.INN);

                                    if (share != null)
                                    {

                                        var factShare = factShares.Where(x => x.OwnerProjectCompanyId == this.ownerCompany.Id && x.DependentProjectCompanyId == company.Id).FirstOrDefault();

                                        if (factShare != null && factShare.ShareDirectPart == 0)
                                        {
                                            FillCell(sheetA1Copy, "[103]", "0");
                                            FillCell(sheetA1Copy, "[104]", "0");
                                            FillCell(sheetA1Copy, "[105]", "0");
                                        }
                                        else
                                        {
                                            if (share.IsOwnInterest == true)
                                                FillCell(sheetA1Copy, "[103]", "1");
                                            else if (!share.IsOwnInterest == false)
                                                FillCell(sheetA1Copy, "[103]", "0");
                                            else FillCell(sheetA1Copy, "[103]", "0");


                                            if (share.IsPartnerInterest == true)
                                                FillCell(sheetA1Copy, "[104]", "1");
                                            else if (!share.IsPartnerInterest == false)
                                                FillCell(sheetA1Copy, "[104]", "0");
                                            else FillCell(sheetA1Copy, "[104]", "0");


                                            if (share.IsChildInterest == true)
                                                FillCell(sheetA1Copy, "[105]", "1");
                                            else if (!share.IsChildInterest == false)
                                                FillCell(sheetA1Copy, "[105]", "0");
                                            else FillCell(sheetA1Copy, "[105]", "0");
                                        }
                                        FillLongCell(sheetA1Copy, "[foundation]", share.ControlGrounds, 40);
                                    }
                                    

                                    break;
                            }



                            FillCell(sheetACopy, "[companynumber]", ACount.ToString("D5"));

                            FillCellValue(sheetACopy, "P7:CO7", signature.LastName);
                            FillCellValue(sheetACopy, "CU7:CY7", signature.FirstName[0]);
                            FillCellValue(sheetACopy, "DE7:DI7", signature.MiddleName[0]);

                            FillLongCell(sheetACopy, "[companynamerus]", company.ForeignCompany.FullName, 40);
                            FillLongCell(sheetACopy, "[companynameeng]", company.ForeignCompany.Name, 40);
                            FillCell(sheetACopy, "[countrycode]", company.ForeignCompany.CountryCode?.Code);
                            FillLongCell(sheetACopy, "[regnumber]", company.ForeignCompany.RegistrationNumber, 40);

                            FillLongCell(sheetACopy, "[taxpayercode]", company.ForeignCompany.TaxPayerCode?.Code, 40);

                            FillLongCell(sheetACopy, "[address]", company.ForeignCompany.Address, 40);

                            FillCell(sheetVCopy, "[companytype]", "О");
                            FillCell(sheetVCopy, "[companynumber]", VCount.ToString("D5"));
                            var taxExemption = projectCompanyService.TaxExemptionFor(this.ownerCompany.Id, company.Id, year);


                            FillCell(sheetVCopy, "[1001]", CheckTaxExemptionStatus(taxExemption, RationalyType.NonProfitOrganization));
                            FillCell(sheetVCopy, "[1002]", CheckTaxExemptionStatus(taxExemption, RationalyType.EurAsECMember));
                            FillCell(sheetVCopy, "[1003]", CheckTaxExemptionStatus(taxExemption, RationalyType.ByESPN));

                            if (CheckTaxExemptionStatus(taxExemption, RationalyType.ActiveHoldingCompany) == "1" || CheckTaxExemptionStatus(taxExemption, RationalyType.ActiveSubholdingCompany) == "1")
                                FillCell(sheetVCopy, "[1004]", "1");
                            else
                                FillCell(sheetVCopy, "[1004]", "0");

                            if (CheckTaxExemptionStatus(taxExemption, RationalyType.BankWithLexPersonalis) == "1" || CheckTaxExemptionStatus(taxExemption, RationalyType.InsuranceAgencyWithLexPersonalis) == "1")
                                FillCell(sheetVCopy, "[1005]", "1");
                            else
                                FillCell(sheetVCopy, "[1005]", "0");

                            FillCell(sheetVCopy, "[1006]", CheckTaxExemptionStatus(taxExemption, RationalyType.TradedBondsIssuer));
                            FillCell(sheetVCopy, "[1007]", CheckTaxExemptionStatus(taxExemption, RationalyType.ProjectMemberMining));
                            FillCell(sheetVCopy, "[1008]", CheckTaxExemptionStatus(taxExemption, RationalyType.OffshoreFieldOperator));


                            FillCellValue(sheetA1Copy, "Q7:CP7", signature.LastName);
                            FillCellValue(sheetA1Copy, "CV7:CZ7", signature.FirstName[0]);
                            FillCellValue(sheetA1Copy, "DF7:DJ7", signature.MiddleName[0]);
                            FillCell(sheetA1Copy, "[companynumber]", ACount.ToString("D5"));

                            if (share != null)
                            {
                                var isIndependentRecognition = share.IsIndependentRecognition == true ? "1" : "0";
                                FillCell(sheetA1Copy, "[isindependentrecognition]", isIndependentRecognition);
                            }




                            ACount++;
                            ClearCells(sheetACopy);
                            ClearCells(sheetA1Copy);

                            break;

                        case State.ForeignLight:

                            var sheetBCopy = workbook.Worksheets.Add("Б-" + BCount, sheetB);

                            switch (this.ownerCompany.State)
                            {
                                case State.Domestic:

                                    FillCell(sheetBCopy, "[inn]", this.ownerCompany.DomesticCompany.INN);
                                    FillCell(sheetBCopy, "[kpp]", this.ownerCompany.DomesticCompany.KPP);
                                    break;

                                case State.Individual:

                                    FillCell(sheetBCopy, "[inn]", this.ownerCompany.IndividualCompany.INN);
                                    break;
                            }

                            FillCell(sheetBCopy, "[pagenumber]", pageNumber.ToString("D3"));
                            pageNumber++;

                            FillCell(sheetBCopy, "[companynumber]", BCount.ToString("D5"));

                            FillCellValue(sheetBCopy, "P7:CO7", signature.LastName);
                            FillCellValue(sheetBCopy, "CU7:CY7", signature.FirstName[0]);
                            FillCellValue(sheetBCopy, "DE7:DI7", signature.MiddleName[0]);


                            FillLongCell(sheetBCopy, "[companynamerus]", company.ForeignLightCompany.RussianName, 40);
                            FillLongCell(sheetBCopy, "[companynameeng]", company.ForeignLightCompany.EnglishName, 40);

                            FillLongCell(sheetBCopy, "[docnamerus]", company.ForeignLightCompany.RequisitesRus, 40);
                            FillLongCell(sheetBCopy, "[docnameeng]", company.ForeignLightCompany.RequisitesEng, 40);

                            FillCell(sheetBCopy, "[datereg]", company.ForeignLightCompany.FoundDate.Day > 9 ? company.ForeignLightCompany.FoundDate.Day.ToString() :
                                "0" + company.ForeignLightCompany.FoundDate.Day);
                            FillCell(sheetBCopy, "[monthreg]", company.ForeignLightCompany.FoundDate.Month > 9 ? company.ForeignLightCompany.FoundDate.Month.ToString() :
                                "0" + company.ForeignLightCompany.FoundDate.Month);
                            FillCell(sheetBCopy, "[yearreg]", company.ForeignLightCompany.FoundDate.Year);
                            FillCell(sheetBCopy, "[countrycode]", company.ForeignLightCompany.CountryCode?.Code);
                            FillCell(sheetBCopy, "[regnumber]", company.ForeignLightCompany.RegNumber);

                            FillCell(sheetBCopy, "[orgform]", company.ForeignLightCompany.ForeignOrganizationalFormCodeId);
                            FillLongCell(sheetBCopy, "[otherinformation]", company.ForeignLightCompany.OtherInfo, 40);

                            FillCell(sheetVCopy, "[companytype]", "C");
                            FillCell(sheetVCopy, "[companynumber]", VCount.ToString("D5"));

                            ClearCells(sheetBCopy);

                            BCount++;

                            break;

                        default:
                            break;
                    }
                    VCount++;

                    ClearCells(sheetVCopy);

                }


                var GCount = 1;
                var G1Count = 1;
                var G2Count = 1;
                var chainCount = 1;

                //var reportCompanies = GetReportCompanies().ToList();
                var chains = reportCompanyService.GetChains();
                foreach (var chain in chains)
                {
                    
                    var sheetGCopy = workbook.Worksheets.Add("Г-" + GCount, sheetG);
                    FillCell(sheetGCopy, "[pagenumber]", pageNumber.ToString("D3"));
                    pageNumber++;

                    FillCellValue(sheetGCopy, "P7:CO7", signature.LastName);
                    FillCellValue(sheetGCopy, "CU7:CY7", signature.FirstName[0]);
                    FillCellValue(sheetGCopy, "DE7:DI7", signature.MiddleName[0]);

                    switch (this.ownerCompany.State)
                    {
                        case State.Domestic:

                            FillCell(sheetGCopy, "[inn]", this.ownerCompany.DomesticCompany.INN);
                            FillCell(sheetGCopy, "[kpp]", this.ownerCompany.DomesticCompany.KPP);
                            break;

                        case State.Individual:

                            FillCell(sheetGCopy, "[inn]", this.ownerCompany.IndividualCompany.INN);
                            break;
                    }

                    switch (chain.TargetCompany.ProjectCompany.State)
                    {
                        case State.Foreign:
                            FillCell(sheetGCopy, "[companytype]", "O");
                            FillLongCell(sheetGCopy, "[companynamerus]", chain.TargetCompany.ProjectCompany?.ForeignCompany?.FullName, 40);

                            break;
                        case State.ForeignLight:
                            FillCell(sheetGCopy, "[companytype]", "C");
                            FillLongCell(sheetGCopy, "[companynamerus]", chain.TargetCompany.ProjectCompany.ForeignLightCompany.RussianName, 40);

                            break;
                        default:
                            break;
                    }


                    FillCell(sheetGCopy, "[numberinsequence]", chainCount.ToString("D5"));
                    chainCount++;

                    FillCell(sheetGCopy, "[companynumber]", GCount.ToString("D5"));
                    GCount++;


                    var count = 1;
                    double sum = 0;
                    foreach (var participant in chain.Participants)
                    {
                        var currentCompany = participant.Company.ProjectCompany;

                        sum += participant.IndirectSharePart;

                        var directSharePart = participant.DirectSharePart.ToString().Split(',', '.');
                        var directMain = participant.DirectSharePart > 100 ? "100" : Convert.ToInt32(directSharePart[0]).ToString("D3");
                        var directFraction = directSharePart.Length > 1 ? Convert.ToDouble(directSharePart[1]).ToInt().ToString("D15") : 0.ToString("D15");


                        var indirectSharePart = participant.IndirectSharePart.ToString().Split(',', '.');
                        var indirectMain = participant.IndirectSharePart > 100 ? "100" : Convert.ToInt32(indirectSharePart[0]).ToString("D3");
                        var indirectFraction = indirectSharePart.Length > 1 ? Convert.ToDouble(indirectSharePart[1]).ToInt().ToString("D15") : 0.ToString("D15");


                        
                        FillCell(sheetGCopy, $"[directshareofparticipantmain-{count}]", directMain);
                        FillCell(sheetGCopy, $"[directshareofparticipantfraction-{count}]", directFraction);

                        FillCell(sheetGCopy, $"[indirectshareofparticipantmain-{count}]", indirectMain);
                        FillCell(sheetGCopy, $"[indirectshareofparticipantfraction-{count}]", indirectFraction);



                        switch (currentCompany.State)
                        {
                            case State.ForeignLight:

                                FillCell(sheetGCopy, $"[membernumbertype-{count}]", "ИC");
                                FillCell(sheetGCopy, $"[membernumber-{count}]", G1Count.ToString("D5"));
                                G1Count++;
                                count++;
                                break;

                            case State.Foreign:

                                var sheetG2Copy = workbook.Worksheets.Add("Г2-" + G2Count, sheetG2);
                                FillCell(sheetG2Copy, "[pagenumber]", pageNumber.ToString("D3"));
                                pageNumber++;


                                FillCell(sheetG2Copy, "[companynumber]", G2Count.ToString("D5"));
                                FillCell(sheetGCopy, $"[membernumbertype-{count}]", "ИO");
                                FillCell(sheetGCopy, $"[membernumber-{count}]", G2Count.ToString("D5"));
                                count++;
                                G2Count++;

                                FillCellValue(sheetG2Copy, "P7:CO7", signature.LastName);
                                FillCellValue(sheetG2Copy, "CU7:CY7", signature.FirstName[0]);
                                FillCellValue(sheetG2Copy, "DE7:DI7", signature.MiddleName[0]);

                                FillLongCell(sheetG2Copy, "[companynamerus]", currentCompany?.ForeignCompany?.FullName, 40);
                                FillLongCell(sheetG2Copy, "[companynameeng]", currentCompany?.ForeignCompany?.Name, 40);

                                FillCell(sheetG2Copy, "[countrycode]", currentCompany?.ForeignCompany?.CountryCode?.Code);
                                FillLongCell(sheetG2Copy, "[regnumber]", currentCompany?.ForeignCompany?.RegistrationNumber, 40);

                                FillLongCell(sheetG2Copy, "[taxpayercode]", currentCompany?.ForeignCompany?.TaxPayerCode?.Code, 40);
                                FillLongCell(sheetG2Copy, "[address]", currentCompany?.ForeignCompany?.Address, 40);

                                switch (this.ownerCompany.State)
                                {
                                    case State.Domestic:

                                        FillCell(sheetG2Copy, "[inn]", this.ownerCompany.DomesticCompany.INN);
                                        FillCell(sheetG2Copy, "[kpp]", this.ownerCompany.DomesticCompany.KPP);
                                        break;

                                    case State.Individual:

                                        FillCell(sheetG2Copy, "[inn]", this.ownerCompany.IndividualCompany.INN);
                                        break;
                                }

                                ClearCells(sheetG2Copy);

                                break;
                            case State.Domestic:

                                FillCell(sheetGCopy, $"[membernumbertype-{count}]", "РО");
                                FillCell(sheetGCopy, $"[membernumber-{count}]", G1Count.ToString("D5"));
                                G1Count++;
                                count++;

                                var sheetG1Copy = workbook.Worksheets.Add("Г1-" + G1Count, sheetG1);
                                FillCell(sheetG1Copy, "[pagenumber]", pageNumber.ToString("D3"));
                                pageNumber++;
                                FillCell(sheetG1Copy, "[companynumber]", G1Count.ToString("D5"));


                                G1Count++;

                                FillCellValue(sheetG1Copy, "P7:CO7", signature.LastName);
                                FillCellValue(sheetG1Copy, "CU7:CY7", signature.FirstName[0]);
                                FillCellValue(sheetG1Copy, "DE7:DI7", signature.MiddleName[0]);


                                FillCell(sheetG1Copy, "[ogrn]", currentCompany.DomesticCompany.OGRN);
                                FillCell(sheetG1Copy, "[companyinn]", currentCompany.DomesticCompany.INN);
                                FillCell(sheetG1Copy, "[companykpp]", currentCompany.DomesticCompany.KPP);
                                FillLongCell(sheetG1Copy, "[companyname]", currentCompany.DomesticCompany.FullName, 40);



                                switch (this.ownerCompany.State)
                                {
                                    case State.Domestic:

                                        FillCell(sheetG1Copy, "[inn]", this.ownerCompany.DomesticCompany.INN);
                                        FillCell(sheetG1Copy, "[kpp]", this.ownerCompany.DomesticCompany.KPP);
                                        break;

                                    case State.Individual:

                                        FillCell(sheetG1Copy, "[inn]", this.ownerCompany.IndividualCompany.INN);
                                        break;
                                }

                                ClearCells(sheetG1Copy);


                                break;
                            default:
                                break;
                        }
                    }

                    var fsum = sum.ToString().Split(',', '.');
                    

                    var fSumfraction = fsum.Length > 1 ? Convert.ToDouble(fsum[1]).ToInt().ToString("D15") : 0.ToString("D15");
                    var sumIndirectshareinsequence = sum > 100 ? "100" : Convert.ToInt32(fsum[0]).ToString("D3");

                    FillCell(sheetGCopy, "[indirectshareinsequence-main]", sumIndirectshareinsequence);
                    FillCell(sheetGCopy, "[indirectshareinsequence-fraction]", fSumfraction);

                    var fact = factShares.Where(f => f.DependentProjectCompanyId == chain.TargetCompany.ProjectCompany.Id).First();


                    fsum = fact.ShareFactPart.ToString().Split(',', '.');


                    fSumfraction = fsum.Length > 1 ? Convert.ToDouble(fsum[1]).ToInt().ToString("D15") : 0.ToString("D15");
                    sumIndirectshareinsequence = sum > 100 ? "100" : Convert.ToInt32(fsum[0]).ToString("D3");

                    FillCell(sheetGCopy, "[indirectshare-main]", sumIndirectshareinsequence);
                    FillCell(sheetGCopy, "[indirectshare-fraction]", fSumfraction);

                    ClearCells(sheetGCopy);
                }

                /*

                var gCompany = await projectCompanyService.GetById(factShares.Last().DependentProjectCompanyId);

                var sheetGCopy = workbook.Worksheets.Add("Г-" + GCount, sheetG);
                FillCell(sheetGCopy, "[pagenumber]", pageNumber.ToString("D3"));
                pageNumber++;

                FillCellValue(sheetGCopy, "P7:CO7", signature.LastName);
                FillCellValue(sheetGCopy, "CU7:CY7", signature.FirstName[0]);
                FillCellValue(sheetGCopy, "DE7:DI7", signature.MiddleName[0]);

                switch (this.ownerCompany.State)
                {
                    case State.Domestic:

                        FillCell(sheetGCopy, "[inn]", this.ownerCompany.DomesticCompany.INN);
                        FillCell(sheetGCopy, "[kpp]", this.ownerCompany.DomesticCompany.KPP);
                        break;

                    case State.Individual:

                        FillCell(sheetGCopy, "[inn]", this.ownerCompany.IndividualCompany.INN);
                        break;
                }

                switch (gCompany.State)
                {
                    case State.Foreign:
                        FillCell(sheetGCopy, "[companytype]", "O");
                        FillLongCell(sheetGCopy, "[companynamerus]", gCompany.ForeignCompany.FullName, 40);

                        break;
                    case State.ForeignLight:
                        FillCell(sheetGCopy, "[companytype]", "C");
                        FillLongCell(sheetGCopy, "[companynamerus]", gCompany.ForeignLightCompany.RussianName, 40);

                        break;
                    default:
                        break;
                }


                FillCell(sheetGCopy, "[numberinsequence]", 1.ToString("D5"));

                FillCell(sheetGCopy, "[companynumber]", GCount.ToString("D5"));
                GCount++;

                */
                /*
                var tCompany = factShares.Where(x => x.OwnerProjectCompanyId == gCompany.Id).First();

                var factPart = tCompany.ShareFactPart.ToString().Split(',');
                FillCell(sheetGCopy, "[indirectshare-main]", factPart[0]);
                FillCell(sheetGCopy, "[indirectshare-fraction]", factPart[1]);

                var directPart = tCompany.ShareDirectPart.ToString().Split(',');
                FillCell(sheetGCopy, "[indirectshareinsequence-main]", directPart[0]);
                FillCell(sheetGCopy, "[indirectshareinsequence-fraction]", directPart[1]);
                */
                /*
                var count = 1;
                foreach (var factShare in factShares)
                {


                    var currentCompany = await projectCompanyService.GetById(factShare.DependentProjectCompanyId);

                    var fPart = factShare.ShareFactPart.ToString().Split(',', '.');
                    var dPart = factShare.ShareDirectPart.ToString().Split(',', '.');

                    var fPartfraction = fPart.Length > 1 ? fPart[1] : 0.ToString("D15");
                    var dPartfraction = dPart.Length > 1 ? dPart[1] : 0.ToString("D15");

                    var indirectshareinsequence = factShare.ShareFactPart > 100 ? "100" : fPart[0];
                    var indirectsharemain = factShare.ShareDirectPart > 100 ? "100" : dPart[0];


                    FillCell(sheetGCopy, "[indirectshare-main]", indirectsharemain);
                    FillCell(sheetGCopy, "[indirectshare-fraction]", dPartfraction);

                    FillCell(sheetGCopy, "[indirectshareinsequence-main]", indirectshareinsequence);
                    FillCell(sheetGCopy, "[indirectshareinsequence-fraction]", fPartfraction);

                    if (currentCompany.State != State.Domestic)
                    {
                        FillCell(sheetGCopy, $"[directshareofparticipantmain-{count}]", indirectsharemain);
                        FillCell(sheetGCopy, $"[directshareofparticipantfractionfraction-{count}]", dPartfraction);

                        FillCell(sheetGCopy, $"[indirectshareofparticipantmain-{count}]", indirectshareinsequence);
                        FillCell(sheetGCopy, $"[indirectshareofparticipantfraction-{count}]", fPartfraction);
                    }

                    switch (currentCompany.State)
                    {
                        case State.ForeignLight:

                            FillCell(sheetGCopy, $"[membernumbertype-{count}]", "ИC");
                            FillCell(sheetGCopy, $"[membernumber-{count}]", G1Count.ToString("D5"));
                            count++;
                            break;

                        case State.Foreign:

                            var sheetG2Copy = workbook.Worksheets.Add("Г2-" + G2Count, sheetG2);
                            FillCell(sheetG2Copy, "[pagenumber]", pageNumber.ToString("D3"));
                            pageNumber++;


                            FillCell(sheetG2Copy, "[companynumber]", G2Count.ToString("D5"));
                            FillCell(sheetGCopy, $"[membernumbertype-{count}]", "ИO");
                            FillCell(sheetGCopy, $"[membernumber-{count}]", G2Count.ToString("D5"));
                            count++;
                            G2Count++;

                            FillCellValue(sheetG2Copy, "P7:CO7", signature.LastName);
                            FillCellValue(sheetG2Copy, "CU7:CY7", signature.FirstName[0]);
                            FillCellValue(sheetG2Copy, "DE7:DI7", signature.MiddleName[0]);

                            FillLongCell(sheetG2Copy, "[companynamerus]", currentCompany?.ForeignCompany?.FullName, 40);
                            FillLongCell(sheetG2Copy, "[companynameeng]", currentCompany?.ForeignCompany?.Name, 40);

                            FillCell(sheetG2Copy, "[countrycode]", currentCompany?.ForeignCompany?.CountryCode?.Code);
                            FillLongCell(sheetG2Copy, "[regnumber]", currentCompany?.ForeignCompany?.RegistrationNumber, 40);

                            FillLongCell(sheetG2Copy, "[taxpayercode]", currentCompany?.ForeignCompany?.TaxPayerCode?.Code, 40);
                            FillLongCell(sheetG2Copy, "[address]", currentCompany?.ForeignCompany?.Address, 40);

                            switch (this.ownerCompany.State)
                            {
                                case State.Domestic:

                                    FillCell(sheetG2Copy, "[inn]", this.ownerCompany.DomesticCompany.INN);
                                    FillCell(sheetG2Copy, "[kpp]", this.ownerCompany.DomesticCompany.KPP);
                                    break;

                                case State.Individual:

                                    FillCell(sheetG2Copy, "[inn]", this.ownerCompany.IndividualCompany.INN);
                                    break;
                            }

                            ClearCells(sheetG2Copy);

                            break;
                        case State.Domestic:

                            var sheetG1Copy = workbook.Worksheets.Add("Г1-" + G1Count, sheetG1);
                            FillCell(sheetG1Copy, "[pagenumber]", pageNumber.ToString("D3"));
                            pageNumber++;
                            FillCell(sheetG1Copy, "[companynumber]", G1Count.ToString("D5"));


                            G1Count++;

                            FillCellValue(sheetG1Copy, "P7:CO7", signature.LastName);
                            FillCellValue(sheetG1Copy, "CU7:CY7", signature.FirstName[0]);
                            FillCellValue(sheetG1Copy, "DE7:DI7", signature.MiddleName[0]);


                            FillCell(sheetG1Copy, "[ogrn]", currentCompany.DomesticCompany.OGRN);
                            FillCell(sheetG1Copy, "[companyinn]", currentCompany.DomesticCompany.INN);
                            FillCell(sheetG1Copy, "[companykpp]", currentCompany.DomesticCompany.KPP);
                            FillLongCell(sheetG1Copy, "[companyname]", currentCompany.DomesticCompany.FullName, 40);



                            switch (this.ownerCompany.State)
                            {
                                case State.Domestic:

                                    FillCell(sheetG1Copy, "[inn]", this.ownerCompany.DomesticCompany.INN);
                                    FillCell(sheetG1Copy, "[kpp]", this.ownerCompany.DomesticCompany.KPP);
                                    break;

                                case State.Individual:

                                    FillCell(sheetG1Copy, "[inn]", this.ownerCompany.IndividualCompany.INN);
                                    break;
                            }

                            ClearCells(sheetG1Copy);


                            break;
                        default:
                            break;
                    }

                    

            }
            */
                workbook.Worksheets.Delete(sheetA);
                workbook.Worksheets.Delete(sheetA1);
                workbook.Worksheets.Delete(sheetB);
                workbook.Worksheets.Delete(sheetV);
                workbook.Worksheets.Delete(sheetG);
                workbook.Worksheets.Delete(sheetG1);
                workbook.Worksheets.Delete(sheetG2);

                FillCell(sheet1, "[pagecount]", (pageNumber - 1).ToString("D3"));
                ClearCells(sheet1);
                


                result = package.GetAsByteArray();
            }

            return result;
        }

        private string CheckTaxExemptionStatus(TaxExemption taxExemption, RationalyType rationalyType)
        {
            if (taxExemption.Rationaly.Contains(rationalyType))
                return "1";

            return "0";
        }

        private void FillCellValue(ExcelWorksheet sheet, string key, object value)
        {
            if (value == null) return;

            sheet.Cells[key].Value = value.ToString();
        }
        private void FillCell(ExcelWorksheet sheet, string key, object value, bool isNumber = false)
        {
            if (value == null) return;

            var newVal = value.ToString();

            if (isNumber)
            {
                newVal = newVal.Replace(",", ".");

                if (newVal.IndexOf(".") != -1)
                {
                    var main = newVal.Split('.')[0];
                    var fraction = newVal.Split('.')[1];
                    fraction = fraction.Length > 5 ? fraction.Substring(0, 5) : fraction;

                    newVal = $"{main}.{fraction}";
                }
            }

            for (int i = sheet.Cells.Start.Row; i < 70; i++)
                for (int j = sheet.Cells.Start.Column; j < 150; j++)
                {
                    var val = sheet.Cells[i, j].Value;

                    if (val != null && val.Equals(key))
                    {
                        for (int k = 0; k < newVal.Length; k++)
                        {
                            sheet.Cells[i, j, i, j + 2].Value = "'" + newVal[k];
                            j += 3;
                        }
                            
                        return;
                    }
                }
        }
        private void FillLongCell(ExcelWorksheet sheet, string key, string value, int length)
        {
            if (string.IsNullOrEmpty(value)) return;

            var splitedVal = SplitByLength(value, length);

            for (int i = 1; i <= splitedVal.Length; i++)
            {
                FillCell(sheet, key.Replace("]", $"-{i}]"), splitedVal[i - 1]);
            }
        }
        private string[] SplitByLength(string value, int length)
        {
            int strLength = value.Length;
            int strCount = (strLength + length - 1) / length;
            string[] result = new string[strCount];
            for (int i = 0; i < strCount; ++i)
            {
                result[i] = value.Substring(i * length, Math.Min(length, strLength));
                strLength -= length;
            }
            return result;
        }
        private void ClearCells(ExcelWorksheet sheet)
        {
            for (int i = sheet.Cells.Start.Row; i < 80; i++)
                for (int j = sheet.Cells.Start.Column; j < 150; j++)
                {
                    var val = sheet.Cells[i, j].Value;

                    if (val != null)
                    {
                        if (val.ToString().StartsWith("["))
                        {
                            sheet.Cells[i, j].LoadFromText("");
                        }
                        else if (val.ToString().StartsWith("'"))
                        {
                            sheet.Cells[i, j].Value = sheet.Cells[i, j].Value.ToString().Replace("'", "");
                        }
                    }
                }
        }

        //private IEnumerable<ReportCompany> GetReportCompanies()
        //{
        //    return factShares
        //        .Select(share => new ReportCompany(share, companyNumberContainer))
        //        .ToArray();
        //}
        
    }
}
