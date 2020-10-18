using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using KPMG.WebKik.Algorithms;
using System.Threading.Tasks;
using KPMG.WebKik.Data;
using KPMG.WebKik.DocumentProcessing;
using KPMG.WebKik.Models.ProjectCompanies;
using KPMG.WebKik.Services;
using KPMG.WebKik.Models;

namespace KPMG.WebKik.Tests.DocumentProcessing
{
    [TestClass]
    public class ExcelNPTests
    {
        //private static readonly Random random = new Random();

        //private const string templateRelativePath = @"\Templates\UU.xlsx";

        //[TestMethod]
        //public async Task NotificationOfParticipationService_GetDocument_Test()
        //{
        //    const int ownerCompanyId = 9;
        //    const int signatoryId = 2;
        //    var notificationRepository = new EntityRepository<NotificationOfParticipation, int>(new WebKikDataContext());
        //    var projectCompanyRepository = new ProjectCompanyRepository(new WebKikDataContext());
        //    var factShareCalculation = new FactShareCalculation();
        //    var notificationCalculation = new NotificationOfParticipationCalculation();
        //    var shareRepository = new EntityRepository<ProjectCompanyShare, int>(new WebKikDataContext());

        //    var service = new NotificationOfParticipationService(
        //        notificationRepository,
        //        projectCompanyRepository,
        //        shareRepository,
        //        factShareCalculation,
        //        notificationCalculation);

        //    var path = Environment.CurrentDirectory + templateRelativePath;

        //    var data = await service.GetDocument(ownerCompanyId, signatoryId, path);
        //    File.WriteAllBytes(@"TestExcelNP.xlsx", data);
        //}

        [TestMethod]
        public void TestExcel()
        {

        }
    }
}
