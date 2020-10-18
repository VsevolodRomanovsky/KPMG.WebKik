using System.Collections.Generic;
using System.IO;
using System.Linq;
using KPMG.WebKik.DocumentProcessing.Helpers;
using KPMG.WebKik.DocumentProcessing.Kik.Models;
using KPMG.WebKik.DocumentProcessing.Kik.Sheets;
using OfficeOpenXml;
using System;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.DocumentProcessing.Kik
{
    public class KikWorkbook
    {
        private const string SheetNameDelimeter = "_";

        private ICollection<SheetBase> sheets;
        private IEnumerable<ProjectCompany> companies;
        private IEnumerable<ProjectCompanyShare> shares;
        private CompanyNumberContainer companyNumberContainer;
        private ProjectCompany OwnerCompany => ownerCompanyId.GetCompany(companies);
        private int ownerCompanyId;
        private int pageNumber = 1;
        private IEnumerable<KikReportCompany> reportCompanies;

        public KikWorkbook()
        {
        }

        public byte[] GetDocumentData(IEnumerable<ProjectCompany> companies, IEnumerable<ProjectCompanyShare> shares, int ownerCompanyId, int year, string templatePath)
        {
            Init(companies, shares, ownerCompanyId);

            byte[] result;
            using (var package = new ExcelPackage(new FileInfo(templatePath)))
            {
                CreateSheets(package.Workbook, year);
                foreach (var sheet in sheets)
                {
                    sheet.InitRanges();
                    sheet.WriteValues();
                }
                result = package.GetAsByteArray();
            }
            return result;
        }

        private void Init(IEnumerable<ProjectCompany> companies, IEnumerable<ProjectCompanyShare> shares, int ownerCompanyId)
        {
            pageNumber = 1;
            this.companies = companies;
            this.shares = shares;
            this.ownerCompanyId = ownerCompanyId;
            sheets = new List<SheetBase>();
            companyNumberContainer = new CompanyNumberContainer();
        }

        private void CreateSheets(ExcelWorkbook workbook, int year)
        {
            CreateReportCompanies();

            CreateOrDeleteSheet2(workbook);
            CreateASheets(workbook);
            CreateBSheets(workbook);
            CreateGSheets(workbook);
            CreateSheet1(workbook, year);
        }

        private void CreateReportCompanies()
        {
            reportCompanies = OwnerCompany.OwnerProjectCompanyFactShares
                    .Select(x => new KikReportCompany(OwnerCompany, x.DependentProjectCompanyId.GetCompany(companies), companyNumberContainer));
        }

        private void CreateSheet1(ExcelWorkbook workbook, int year)
        {
            var sheet1 = workbook.Worksheets["стр 1"];
            sheets.Add(new KikSheet1(sheet1, OwnerCompany, year, pageNumber));
        }

        private void CreateOrDeleteSheet2(ExcelWorkbook workbook)
        {
            var sheet2 = workbook.Worksheets["стр 2"];
            if (OwnerCompany.State == State.Individual)
            {
                sheets.Add(new KikSheet2(sheet2, OwnerCompany.IndividualCompany, ++pageNumber));
            }
            else
            {
                workbook.Worksheets.Delete(sheet2);
            }
        }

        private void CreateASheets(ExcelWorkbook workbook)
        {
            var originalSheetA = workbook.Worksheets["А"];
            var originalSheetA1 = workbook.Worksheets["А1"];

            var companiesForSheetA = reportCompanies
                .Where(x => x.ControlGrounds.Any())
                .Where(x => x.ProjectCompany.State == State.Foreign);

            foreach (var company in companiesForSheetA)
            {
                var sheetA = workbook.Worksheets.Add(
                    originalSheetA.Name + SheetNameDelimeter + company.ShortNumber, originalSheetA);
                sheets.Add(new KikSheetA(sheetA, company, ++pageNumber));

                var sheetA1 = workbook.Worksheets.Add(
                    originalSheetA1.Name + SheetNameDelimeter + company.ShortNumber, originalSheetA1);
                sheets.Add(new KikSheetA1(sheetA1, company, ++pageNumber));
            }
            workbook.Worksheets.Delete(originalSheetA);
            workbook.Worksheets.Delete(originalSheetA1);
        }

        private void CreateBSheets(ExcelWorkbook workbook)
        {
            var originalSheetB = workbook.Worksheets["Б"];
            var originalSheetB1 = workbook.Worksheets["Б1"];

            foreach (var company in reportCompanies.Where(x => x.ProjectCompany.IsKIKCompany && x.ProjectCompany.State == State.ForeignLight))
            {
                var sheetB = workbook.Worksheets.Add(originalSheetB.Name + SheetNameDelimeter + company.ShortNumber, originalSheetB);
                sheets.Add(new KikSheetB(sheetB, company, ++pageNumber));

                var sheetB1 = workbook.Worksheets.Add(
                    originalSheetB1.Name + SheetNameDelimeter + company.ShortNumber, originalSheetB1);
                sheets.Add(new KikSheetB1(sheetB1, company, ++pageNumber));
            }
            workbook.Worksheets.Delete(originalSheetB);
            workbook.Worksheets.Delete(originalSheetB1);
        }

        private void CreateGSheets(ExcelWorkbook workbook)
        {
            var originalSheetG = workbook.Worksheets["Г"];
            var kikCompanies = reportCompanies.Where(x => x.ProjectCompany.IsKIKCompany && x.ProjectCompany.State != State.Domestic);

            foreach (var company in kikCompanies)
            {
                foreach (var chain in GetCompanyChains(company))
                {
                    var sheetName = originalSheetG.Name + SheetNameDelimeter + company.ShortNumber + SheetNameDelimeter + chain.Number;
                    var sheet = workbook.Worksheets.Add(sheetName, originalSheetG);
                    sheets.Add(new KikSheetG(sheet, company, chain, ++pageNumber));
                }
            }
            workbook.Worksheets.Delete(originalSheetG);
        }


        private IEnumerable<CompanyChain> GetCompanyChains(KikReportCompany company)
        {
            throw new NotImplementedException();
            //const int PathMaxItemsCount = 15;
            //var shareGraph = new BidirectionalGraph<int, Edge<int>>();
            //foreach (var share in shares)
            //{
            //    shareGraph.AddVerticesAndEdge(new TaggedEdge<int, double>(share.OwnerProjectCompanyId, share.DependentProjectCompanyId, share.SharePart));
            //}
            //return shareGraph
            //    .RankedShortestPathHoffmanPavley(e => 0, sourceProjectCompanyId, targetProjectCompanyId, PathMaxItemsCount)
            //    .Cast<Edge<int>>().ToList();
        }

        private IEnumerable<ProjectCompanyShare> GetCompanyOwnShares(ProjectCompany company)
        {
            var shares = new List<ProjectCompanyShare>();
            var ownShares = company.OwnerProjectCompanyShares ?? new List<ProjectCompanyShare>();
            shares.AddRange(ownShares);
            foreach (var share in ownShares)
            {
                shares.AddRange(GetCompanyOwnShares(share.DependentProjectCompany));
            }
            return shares;
        }

        private void TryAddShare(ICollection<ProjectCompanyShare> shares, ProjectCompanyShare share)
        {
            var isExist = shares.Any(
                x => x.OwnerProjectCompanyId == share.OwnerProjectCompanyId && x.DependentProjectCompanyId == share.DependentProjectCompanyId);
            if (!isExist)
            {
                shares.Add(share);
            }
        }


    }
}
