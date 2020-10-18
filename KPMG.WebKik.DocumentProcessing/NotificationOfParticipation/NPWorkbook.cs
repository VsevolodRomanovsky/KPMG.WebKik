using System.Collections.Generic;
using System.IO;
using System.Linq;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.DocumentProcessing.Helpers;
using KPMG.WebKik.Models.ProjectCompanies;
using OfficeOpenXml;
using KPMG.WebKik.DocumentProcessing.NotificationOfParticipation.Models;
using KPMG.WebKik.DocumentProcessing.NotificationOfParticipation.Sheets;
using QuickGraph;
using QuickGraph.Algorithms;
using KPMG.Webkik.Utils;
using KPMG.WebKik.Models;
using System;
using KPMG.WebKik.Contracts.Algorithms;
using System.Threading.Tasks;
using KPMG.WebKik.Models.Companies;

namespace KPMG.WebKik.DocumentProcessing.NotificationOfParticipation
{
    public class NPWorkbook : INotificationOfParticipationDocument
    {
        private const string SheetNameDelimeter = "_";

        private ICollection<SheetBase> sheets;
        private IEnumerable<ProjectCompanyFactShare> factShares;
        private IEnumerable<ProjectCompanyShare> shares;
        private CompanyNumberContainer companyNumberContainer;
        private ProjectCompany ownerCompany;
        private ProjectCompany currentCompany;
        private INotificationOfParticipationCalculation notificationCalculation;
        private IDictionary<int, ProjectCompany> allCompanies;
        private IProjectCompanyService projectCompanyService;
        private int pageNumber = 1;

        public NPWorkbook(INotificationOfParticipationCalculation notificationCalculation, IProjectCompanyService projectCompanyService)
        {
            this.notificationCalculation = notificationCalculation;
            this.projectCompanyService = projectCompanyService;
        }

        public async Task<byte[]> GetDocumentData(ProjectCompany ownerCompany, IList<ProjectCompanyFactShare> factShares, Signatory signature, string templatePath, IList<ProjectCompanyShare> shares)
        {           
            pageNumber = 1;
            this.ownerCompany = ownerCompany;
            this.factShares = factShares;
            this.shares = shares;
            sheets = new List<SheetBase>();
            companyNumberContainer = new CompanyNumberContainer();
            allCompanies = (await projectCompanyService.GetAllByProjectId(ownerCompany.ProjectId)).ToDictionary(x => x.Id);

            
            //currentCompany = await projectCompanyService.GetById(ownerCompany.Id);

            byte[] result;
            using (var package = new ExcelPackage(new FileInfo(templatePath)))
            {
                CreateSheets(package.Workbook, signature);
                foreach (var sheet in sheets)
                {
                    sheet.InitRanges();
                    sheet.WriteValues();
                }
                result = package.GetAsByteArray();
            }
            return await Task.Run(()=>result);
        }
        
        private void CreateSheets(ExcelWorkbook workbook, Signatory signature)
        {
            var reportCompanies = GetReportCompanies().ToList();
            var chains = GetChains(reportCompanies);

            CreateOrDeleteSheet2(workbook);
            CreateASheets(workbook, reportCompanies);
            CreateBSheets(workbook, reportCompanies);
            CreateVSheets(workbook, reportCompanies);
            CreateGSheets(workbook, reportCompanies, chains);
            CreateSheet1(workbook, signature);
        }

        private IEnumerable<NPReportCompany> GetReportCompanies()
        {
            return factShares
                .Where(share => notificationCalculation.CalculateIsNotificationOfParticipationRequired(share))
                .Select(share => new NPReportCompany(share, companyNumberContainer))
                .ToArray();
        }        

        private void CreateSheet1(ExcelWorkbook workbook, Signatory signature)
        {
            var sheet1 = workbook.Worksheets["стр 1"];
            sheets.Add(new NPSheet1(sheet1, ownerCompany, pageNumber, signature));
        }

        private async void CreateOrDeleteSheet2(ExcelWorkbook workbook)
        {
            var sheet2 = workbook.Worksheets["стр 2"];
            if (ownerCompany.State == State.Individual)
            {
                var c = (projectCompanyService.GetIndividualCompId(ownerCompany.Id)).First();
                sheets.Add(new NPSheet2(sheet2, c, ++pageNumber));
            }
            else
            {
                workbook.Worksheets.Delete(sheet2);
            }
        }

        private void CreateASheets(ExcelWorkbook workbook, IEnumerable<NPReportCompany> reportCompanies)
        {
            var originalSheetA = workbook.Worksheets["А"];

            var companiesForSheetA = reportCompanies.Where(x => x.ProjectCompany.State == State.Foreign);

            foreach (var company in companiesForSheetA)
            {
                var sheetA = workbook.Worksheets.Add(
                    originalSheetA.Name + SheetNameDelimeter + company.ShortNumber, originalSheetA);
                sheets.Add(new NPSheetA(sheetA, company, ++pageNumber));

            }
            workbook.Worksheets.Delete(originalSheetA);
        }

        private void CreateBSheets(ExcelWorkbook workbook, IEnumerable<NPReportCompany> reportCompanies)
        {
            var originalSheetB = workbook.Worksheets["Б"];

            var companiesForSheetB = reportCompanies.Where(x => x.ProjectCompany.State == State.Domestic);

            foreach (var company in companiesForSheetB)
            {
                var sheetB = workbook.Worksheets.Add(
                    originalSheetB.Name + SheetNameDelimeter + company.ShortNumber, originalSheetB);
                sheets.Add(new NPSheetB(sheetB, company, ++pageNumber));

            }
            workbook.Worksheets.Delete(originalSheetB);
        }

        private void CreateVSheets(ExcelWorkbook workbook, IEnumerable<NPReportCompany> reportCompanies)
        {
            var originalSheetV = workbook.Worksheets["В"];
            var originalSheetV1 = workbook.Worksheets["В1"];

            var companiesForSheetV = reportCompanies.Where(x => x.ProjectCompany.State == State.ForeignLight);

            foreach (var company in companiesForSheetV)
            {
                var sheetV = workbook.Worksheets.Add(originalSheetV.Name + SheetNameDelimeter + company.ShortNumber, originalSheetV);
                sheets.Add(new NPSheetV(sheetV, company, ++pageNumber));

                var sheetV1 = workbook.Worksheets.Add(
                    originalSheetV1.Name + SheetNameDelimeter + company.ShortNumber, originalSheetV1);
                sheets.Add(new NPSheetV1(sheetV1, company, ++pageNumber));
            }
            workbook.Worksheets.Delete(originalSheetV);
            workbook.Worksheets.Delete(originalSheetV1);
        }

        private void CreateGSheets(ExcelWorkbook workbook, IEnumerable<NPReportCompany> reportCompanies, IEnumerable<CompanyChain> chains)
        {
            var originalSheetG = workbook.Worksheets["Г"];

            foreach (var chain in chains)
            {
                var sheetName = originalSheetG.Name + SheetNameDelimeter + chain.TargetCompany.ShortNumber + SheetNameDelimeter + chain.Number;
                var sheet = workbook.Worksheets.Add(sheetName, originalSheetG);
                sheets.Add(new NPSheetG(sheet, chain, ++pageNumber));
            }

            workbook.Worksheets.Delete(originalSheetG);
        }

        private IEnumerable<CompanyChain> GetChains(ICollection<NPReportCompany> reportCompanies)
        {
            var companies = reportCompanies.ToList();
            var chains = new List<CompanyChain>();
            foreach (var company in companies)
            {
                chains.AddRange(GetCompanyChainsWithUpdateReportCompanies(company.ProjectCompany.Id, reportCompanies));
            }
            return chains;
        }

        private IEnumerable<CompanyChain> GetCompanyChainsWithUpdateReportCompanies(int targetCompanyId, ICollection<NPReportCompany> reportCompanies)
        {
            var chains = new List<CompanyChain>();
            var idirectPaths = GetPaths(targetCompanyId).Where(x => x.Count() > 1);
            foreach (var path in idirectPaths)
            {
                var chainCompanies = new List<NPReportCompany>();
                foreach (var edge in path)
                {
                    var reportCompany = reportCompanies.SingleOrDefault(x => x.ProjectCompany.Id == edge.Target);
                    if (reportCompany == null)
                    {
                        var factShare = factShares.First(x => x.DependentProjectCompanyId == edge.Target);//
                        reportCompany = new NPReportCompany(factShare, companyNumberContainer);
                        reportCompanies.Add(reportCompany);
                    }
                    chainCompanies.Add(reportCompany);
                }
                chains.Add(new CompanyChain(chains.Count() + 1, chainCompanies));
            }
            return chains;
        }

        private IEnumerable<IEnumerable<Edge<int>>> GetPaths(int targetCompanyId)
        {
            const int PathMaxItemsCount = 15;
            var graph = new BidirectionalGraph<int, Edge<int>>();

            foreach (var share in shares)
            {
                graph.AddVerticesAndEdge(new TaggedEdge<int, double>(share.OwnerProjectCompanyId, share.DependentProjectCompanyId, share.SharePart));
            }

            return graph.RankedShortestPathHoffmanPavley(e => 0, ownerCompany.Id, targetCompanyId, PathMaxItemsCount);
        }
    }
}
