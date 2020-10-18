using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using KPMG.WebKik.Algorithms;
using System.Threading.Tasks;
using KPMG.WebKik.Data;
using KPMG.WebKik.Models.ProjectCompanies;
using KPMG.WebKik.Services;

namespace KPMG.WebKik.Tests.DocumentProcessing
{
    [TestClass]
    public class ExcelKikTests
    {
        private static readonly Random random = new Random();

        private const string templateRelativePath = @"\Templates\KIK.xlsx";

        [TestMethod]
        public async Task ProjectCompanyService_GetKIKDocument_Test()
        {
            const int ownerCompanyId = 9;
            var projectCompanyRepository = new ProjectCompanyRepository(new WebKikDataContext());
            var factShareCalculation = new FactShareCalculation();
            var shareRepository = new EntityRepository<ProjectCompanyShare, int>(new WebKikDataContext());
            var service = new ProjectCompanyService(projectCompanyRepository, factShareCalculation, null, null, null, shareRepository);
            var path = Environment.CurrentDirectory + templateRelativePath;
            
            var data = await service.GetKIKDocument(ownerCompanyId, path, 2016);
            File.WriteAllBytes(@"TestExcelKik.xlsx", data);
        }
    }
}
 