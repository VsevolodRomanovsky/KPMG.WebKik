using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Contracts.Service.Registers;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.ProjectCompanies;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KPMG.WebKik.DocumentProcessing.TaxReturn
{
    public class TaxReturnWorkbook : ITaxReturnDocument
    {
        private IList<ProjectCompany> kiks;
        private ProjectCompany ownerCompany;

        private IEnumerable<ProjectCompanyFactShare> factShares;
        private IProjectCompanyService projectCompanyService;
        private IProjectCompanyShareService shareService;
        private IRegister1Service register1Service;
        private IRegister3Service register3Service;
        private IRegister9Service register9Service;

        public TaxReturnWorkbook(IProjectCompanyService projectCompanyService, IProjectCompanyShareService shareService, IRegister1Service register1Service,
            IRegister3Service register3Service, IRegister9Service register9Service)
        {
            this.projectCompanyService = projectCompanyService;
            this.shareService = shareService;
            this.register1Service = register1Service;
            this.register3Service = register3Service;
            this.register9Service = register9Service;
        }

        public async Task<byte[]> GetDocumentData(ProjectCompany ownerCompany, IList<ProjectCompanyFactShare> factShares, string templatePath, int year)
        {


            byte[] result;
            using (var package = new ExcelPackage(new FileInfo(templatePath)))
            {

                this.ownerCompany = ownerCompany;

                ExcelWorkbook workbook = package.Workbook;
                var sheet01 = workbook.Worksheets["Лист 01"];
                var sheet09a = workbook.Worksheets["Лист 09 раздел А"];
                var sheet09b1 = workbook.Worksheets["Лист 09 раздел Б1"];

                kiks = await shareService.GetAllKIKsByProjectCompanyId(this.ownerCompany.Id);

                var kikCount = 1;
                var rusName = "";
                var engName = "";
                var code = "";
                var regNumber = "";

                foreach (var kik in kiks)
                {
                    var company = await projectCompanyService.GetById(kik.Id);

                    var sheet09aCopy = workbook.Worksheets.Add("Лист 09 раздел А-" + kikCount, sheet09a);
                    var sheet09b1Copy = workbook.Worksheets.Add("Лист 09 раздел Б1-" + kikCount, sheet09b1);


                    switch (this.ownerCompany.State)
                    {
                        case State.Domestic:

                            FillCell(sheet09aCopy, "[inn]", this.ownerCompany.DomesticCompany.INN);
                            FillCell(sheet09aCopy, "[kpp]", this.ownerCompany.DomesticCompany.KPP);
                            FillCell(sheet09b1Copy, "[inn]", this.ownerCompany.DomesticCompany.INN);
                            FillCell(sheet09b1Copy, "[kpp]", this.ownerCompany.DomesticCompany.KPP);
                            break;

                        case State.Individual:
                            FillCell(sheet09aCopy, "[inn]", this.ownerCompany.IndividualCompany.INN);
                            FillCell(sheet09b1Copy, "[inn]", this.ownerCompany.IndividualCompany.INN);
                            break;
                        default:
                            break;
                    }

                    switch (company.State)
                    {
                        case State.Foreign:
                            rusName = company.ForeignCompany.FullName;
                            engName = company.ForeignCompany.Name;
                            code = company.ForeignCompany.CountryCode.Code;
                            regNumber = company.ForeignCompany.RegistrationNumber;

                            var taxReturnCode = company.ForeignCompany.TaxPayerCode != null ? company.ForeignCompany.TaxPayerCode.Code : "";
                            var address = company.ForeignCompany.Address;
                            

                            FillLongCell(sheet09aCopy, "[address]", SplitByLength(address, 40));
                            FillLongCell(sheet09aCopy, "[taxpayercode]", SplitByLength(taxReturnCode, 40));
                            FillCell(sheet09aCopy, "[companynumbertype]", "О");
                            FillCell(sheet09b1Copy, "[companynumbertype]", "О");
                            break;

                        case State.ForeignLight:
                            
                            rusName = company.ForeignLightCompany.RussianName;
                            engName = company.ForeignLightCompany.EnglishName;
                            code = company.ForeignLightCompany.CountryCode.Code;

                            var docNameRus = company.ForeignLightCompany.RequisitesRus;
                            var docNameEng = company.ForeignLightCompany.RequisitesEng;
                            var orgFormCode = company.ForeignLightCompany.ForeignOrganizationalFormCodeId;
                            //factShare = factShares.First(x => x.DependentProjectCompanyId == company.ForeignLightCompany.Id).ShareFactPart;
                            regNumber = company.ForeignLightCompany.RegNumber;

                            FillLongCell(sheet09aCopy, "[requisitesrus]", SplitByLength(docNameRus, 40));
                            FillLongCell(sheet09aCopy, "[requisiteseng]", SplitByLength(docNameEng, 40));
                            FillCell(sheet09aCopy, "[orgform]", orgFormCode);
                            FillCell(sheet09aCopy, "[companynumbertype]", "С");
                            FillCell(sheet09b1Copy, "[companynumbertype]", "С");
                            
                            break;

                        case State.Individual:
                            rusName = company.IndividualCompany.Surname + " " + company.IndividualCompany.Name + " " + company.IndividualCompany.MiddleName;
                            break;
                        default:
                            break;
                    }


                    FillLongCell(sheet09aCopy, "[rusname]", SplitByLength(rusName, 40));
                    FillLongCell(sheet09aCopy, "[engname]", SplitByLength(engName, 40));
                    FillCell(sheet09aCopy, "[countrycode]", code);
                    FillCell(sheet09aCopy, "[foreignorganizationalformcode]", code);

                    FillCell(sheet09aCopy, "[companynumber]", "0000" + kikCount);
                    FillCell(sheet09b1Copy, "[companynumber]", "0000" + kikCount);

                    FillLongCell(sheet09aCopy, "[regnumber]", SplitByLength(regNumber, 40));

                    var factShare = factShares.First(x => x.DependentProjectCompanyId == kik.Id).ShareFactPart;
                    var factShareVal = factShare.ToString().Replace(",", ".");
                    var factShareValSplit = factShareVal.Split('.');
                    if (factShareValSplit.Length == 2)
                    {
                        FillCell(sheet09aCopy, "[share]", factShareValSplit[0]);
                        FillCell(sheet09aCopy, "[share-1]", factShareValSplit[1]);
                    }
                    else
                    {
                        FillCell(sheet09aCopy, "[share]", factShareValSplit[0]);
                    }

                    var reg1 = register1Service.GetRegister1ByCompanyId(company.Id, year);
                    var reg3 = register3Service.GetRegister3ByCompanyId(company.Id, year);
                    var reg9 = register9Service.GetRegister9ByCompanyId(company.Id, year);


                    if (reg1 != null)
                    {
                        FillCell(sheet09b1Copy, "[currency]", reg1.Currency);

                        FillCell(sheet09b1Copy, "[010]", reg1.ProfitAmountBeforeTax); 
                        FillCell(sheet09b1Copy, "[020]", reg1.IncomeAndCostsTotalAmount);

                        FillCell(sheet09b1Copy, "[030]", reg1.ProfitTotalAmountCorrection);
                        FillCell(sheet09b1Copy, "[040]", reg1.AdjustedProfitAmount);
                        FillCell(sheet09b1Copy, "[070]", reg1.DistributedProfitAmount);
                        FillCell(sheet09b1Copy, "[080]", reg1.ProfitAmount);

                        var averageForeignCurrency = reg1.AverageForeignCurrency.ToString().Replace(",", ".");
                        var averageForeignCurrencySplit = averageForeignCurrency.Split('.');
                        if (averageForeignCurrencySplit.Length == 2)
                        {
                            FillCell(sheet09b1Copy, "[090]", averageForeignCurrencySplit[0]);
                            FillCell(sheet09b1Copy, "[090-1]", averageForeignCurrencySplit[1]);
                        }
                        else
                        {
                            FillCell(sheet09b1Copy, "[090]", averageForeignCurrencySplit[0]);
                        }

                        FillCell(sheet09b1Copy, "[100]", reg1.ProfitAmountConvertedCurrency);
                        FillCell(sheet09b1Copy, "[110]", reg1.ReceivedDividends);
                        FillCell(sheet09b1Copy, "[120]", reg1.ProfitAmountCurrentYear);
                        FillCell(sheet09b1Copy, "[130]", reg1.ProfitAmountForTax);
                        FillCell(sheet09b1Copy, "[140]", reg1.LossKIKFromPastYears);
                        FillCell(sheet09b1Copy, "[150]", reg1.CountableProfitAmountForTax);
                        FillCell(sheet09b1Copy, "[160]", reg1.PartKIKProfit);
                        FillCell(sheet09b1Copy, "[170]", reg1.ControlledProfitAmount);
                        FillCell(sheet09b1Copy, "[180]", reg1.KIKTaxBase);
                    }

                    if (reg3 != null)
                    {
                        FillCell(sheet09b1Copy, "[190]", reg3.TaxSum);
                        FillCell(sheet09b1Copy, "[200]", reg3.ForeginContryEarningsRUR + reg3.DomesticProfitCurrency);
                        FillCell(sheet09b1Copy, "[210]", reg3.TaxSum - (reg3.ForeginContryEarningsRUR + reg3.DomesticProfitCurrency));
                    }

                    if (reg9 != null)
                    {
                        FillCell(sheet09b1Copy, "[050]", reg9.Register9Data.Sum(x => x.CurrentYearTransitionalDividendSum));
                        FillCell(sheet09b1Copy, "[060]", reg9.Register9Data.Sum(x => x.CurrentYearDividendSum));
                    }

                    ClearCells(sheet09aCopy);
                    ClearCells(sheet09b1Copy);

                    kikCount = kikCount + 1;
                }

                workbook.Worksheets.Delete(sheet09a);
                workbook.Worksheets.Delete(sheet09b1);

                result = package.GetAsByteArray();
            }
            return result;
        }

        private void FillCell(ExcelWorksheet sheet, string key, object value)
        {
            var newVal = value.ToString().Replace(",", ".");

            if(newVal.IndexOf(".") != -1)
            {
                var main = newVal.Split('.')[0];
                var fraction = newVal.Split('.')[1];
                fraction = fraction.Length > 5 ? fraction.Substring(0, 5) : fraction;

                newVal = $"{main}.{fraction}";
            }

            for (int i = sheet.Cells.Start.Row; i < 70; i++)
                for (int j = sheet.Cells.Start.Column; j < 45; j++)
                {
                    var val = sheet.Cells[i, j].Value;

                    if (val != null && val.Equals(key))
                    {
                        newVal = "'" + String.Join<char>(",'", newVal);
                        sheet.Cells[i, j].LoadFromText(newVal);
                        return;
                    }
                }
        }

        private void FillLongCell(ExcelWorksheet sheet, string key, string[] value)
        {
            for (int i = 1; i <= value.Length; i++)
            {
                FillCell(sheet, key.Replace("]", $"-{i}]"), value[i-1]);
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
            for (int i = sheet.Cells.Start.Row; i < 70; i++)
                for (int j = sheet.Cells.Start.Column; j < 45; j++)
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
    }
}
