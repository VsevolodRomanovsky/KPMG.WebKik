using System;
using System.Collections.Generic;
using System.Linq;
using KPMG.WebKik.Algorithms;
using KPMG.WebKik.DocumentProcessing;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.Companies;
using KPMG.WebKik.Models.Directories;
using KPMG.WebKik.Models.ProjectCompanies;
using Xunit;

namespace KPMG.WebKik.Tests.Test.Services
{
    public class ServiceTests
    {
        [Fact]
        public void Test()
        {
        }
    }


    public class ExcelTester
    {
        public void TestNPC()
        {
            ExcelDocsCreator ec = new ExcelDocsCreator();
            ProjectCompany pc0 = new ProjectCompany() { Name = "ООО ЛЮКСОФТ ПРОФЕШОНАЛ", State = State.Domestic };

            DocumentInformation di1 = new DocumentInformation() { IssueDate = DateTime.Parse("05.05.1991"), IssuePlace = "г.Москва", SeriesAndNumber = "123 989 89 89 " };
            DocumentInformation di2 = new DocumentInformation() { IssueDate = DateTime.Parse("05.05.1992"), IssuePlace = "г.Москва", SeriesAndNumber = "456 989 89 89 " };
            DocumentInformation di3 = new DocumentInformation() { IssueDate = DateTime.Parse("05.05.1993"), IssuePlace = "г.Москва", SeriesAndNumber = "789 989 89 89 " };
            DocumentInformation di4 = new DocumentInformation() { IssueDate = DateTime.Parse("05.05.1994"), IssuePlace = "г.Москва", SeriesAndNumber = "0987 989 89 89 " };
            IndividualCompany ic1 = new IndividualCompany() { AppartamentNumber = "100", BirthDate = DateTime.Parse("05.05.1990"), BirthPlace = "город москва", BuildingNumber = "строение 1", CitizenshipCodeId = 1, City = "Москва", CityType = "город", ConfirmedPersonalityDocInfo = di1, INN = 09876543210, Name = "Василий", MiddleName = "Иванович", Surname = "Пупкин", GenderCodeId = 1, PostIndex = "495980", RegionCode = new RegionCode() { Id = 77 }, District = "Московская", Street = "Ленина", HouseNumber = "10", VerifedPersonalityDocInfo = di4 };
            IndividualCompany ic2 = new IndividualCompany() { AppartamentNumber = "200", BirthDate = DateTime.Parse("05.05.1980"), BirthPlace = "город москва", BuildingNumber = "строение 2", CitizenshipCodeId = 1, City = "Питер", CityType = "город", ConfirmedPersonalityDocInfo = di2, INN = 1234567890, Name = "Иван", MiddleName = "Петрович", Surname = "Сидоров", GenderCodeId = 1, PostIndex = "495980", RegionCode = new RegionCode() { Id = 47 }, District = "Омская", Street = "Ленина", HouseNumber = "1000", VerifedPersonalityDocInfo = di3 };
            IndividualCompany ic3 = new IndividualCompany() { AppartamentNumber = "300", BirthDate = DateTime.Parse("05.05.1970"), BirthPlace = "город москва", BuildingNumber = "строение 43", CitizenshipCodeId = 1, City = "Омск", CityType = "город", ConfirmedPersonalityDocInfo = di3, INN = 1334567890, Name = "Пётр", MiddleName = "Сидорович", Surname = "Иванов", GenderCodeId = 1, PostIndex = "495980", RegionCode = new RegionCode() { Id = 05 }, District = "Омская", Street = "Ленина", HouseNumber = "10/4", VerifedPersonalityDocInfo = di2 };
            IndividualCompany ic4 = new IndividualCompany() { AppartamentNumber = "400", BirthDate = DateTime.Parse("05.05.1960"), BirthPlace = "город москва", BuildingNumber = "строение 143", CitizenshipCodeId = 1, City = "Ленинград", CityType = "город", ConfirmedPersonalityDocInfo = di4, INN = 1134567890, Name = "Алексей", MiddleName = "Иванович", Surname = "Петров", GenderCodeId = 1, PostIndex = "495980", RegionCode = new RegionCode() { Id = 98 }, District = "Ленинградская", Street = "Ленина", HouseNumber = "108", VerifedPersonalityDocInfo = di1 };

            ProjectCompany pc1 = new ProjectCompany() { Name = "ООО ЛЮКСОФТ ПРОФЕШОНАЛ40", State = State.Individual, IndividualCompany = ic1 };
            ProjectCompany pc2 = new ProjectCompany() { Name = "ООО ЛЮКСОФТ ПРОФЕШОНАЛ41", State = State.Individual, IndividualCompany = ic2 };
            ProjectCompany pc3 = new ProjectCompany() { Name = "ООО ЛЮКСОФТ ПРОФЕШОНАЛ42", State = State.Individual, IndividualCompany = ic3 };
            ProjectCompany pc4 = new ProjectCompany() { Name = "ООО ЛЮКСОФТ ПРОФЕШОНАЛ43", State = State.Individual, IndividualCompany = ic4 };

            ForeignCompany fc1 = new ForeignCompany() { FullName = "ООО Иностранная компания 1", Name = "ИК1", Address = "г. Москва 1ый Волоколамский проезд дом 3", CountryCodeId = 1, TaxPayerCodeId = 1, RegistrationNumber = "123" };
            ForeignCompany fc2 = new ForeignCompany() { FullName = "ООО Иностранная компания 2", Name = "ИК2", Address = "г. Москва 2ый Волоколамский проезд дом 4", CountryCodeId = 1, TaxPayerCodeId = 1, RegistrationNumber = "21222223" };
            ForeignCompany fc3 = new ForeignCompany() { FullName = "ООО Иностранная компания 3", Name = "ИК3", Address = "г. Москва 3ий Волоколамский проезд дом 5", CountryCodeId = 5, TaxPayerCodeId = 1, RegistrationNumber = "31222243522223" };
            ForeignCompany fc4 = new ForeignCompany() { FullName = "ООО Иностранная компания 4", Name = "ИК4", Address = "г. Москва 4ый Волоколамский проезд дом 6", CountryCodeId = 5, TaxPayerCodeId = 1, RegistrationNumber = "421222243522223" };


            ProjectCompany pc5 = new ProjectCompany() { Name = "ООО ЛЮКСОФТ ПРОФЕШОНАЛ30", State = State.Foreign, ForeignCompany = fc1 };
            ProjectCompany pc6 = new ProjectCompany() { Name = "ООО ЛЮКСОФТ ПРОФЕШОНАЛ31", State = State.Foreign, ForeignCompany = fc2 };
            ProjectCompany pc7 = new ProjectCompany() { Name = "ООО ЛЮКСОФТ ПРОФЕШОНАЛ32", State = State.Foreign, ForeignCompany = fc3 };
            ProjectCompany pc8 = new ProjectCompany() { Name = "ООО ЛЮКСОФТ ПРОФЕШОНАЛ33", State = State.Foreign, ForeignCompany = fc4 };

            ForeignLightCompany fcl1 = new ForeignLightCompany() { EnglishName = "FOREGIN LIGHT COMPANY 1 FOREGIN LIGHT COMPANY 1 FOREGIN LIGHT COMPANY 1 FOREGIN LIGHT COMPANY 1", RussianName = "ООО Иностранная компания БОЮЛ 1", CountryCodeId = 5, FoundDate = DateTime.Parse("05.12.1997"), Number = "1423", ForeignOrganizationalFormCodeId = 4, RequisitesEng = "DESC NUM 3 DESC NUM 2 DESC NUM 1", RegNumber = "123123123", RequisitesRus = "Реквизит1, 2 ,3 ,5 10 реквизиты", OtherInfo = "Дополнительная информация 1" };
            ForeignLightCompany fcl2 = new ForeignLightCompany() { EnglishName = "FOREGIN LIGHT COMPANY 2", RussianName = "ООО Иностранная компания БОЮЛ 2", CountryCodeId = 5, FoundDate = DateTime.Parse("05.12.1998"), Number = "1234", ForeignOrganizationalFormCodeId = 3, RequisitesEng = "DESC NUM 3 DESC NUM 2 DESC NUM 1", RegNumber = "123123123", RequisitesRus = "Реквизит1, 2 ,3 ,5 10 реквизиты", OtherInfo = "Дополнительная информация 2" };
            ForeignLightCompany fcl3 = new ForeignLightCompany() { EnglishName = "FOREGIN LIGHT COMPANY 3", RussianName = "ООО Иностранная компания БОЮЛ 3", CountryCodeId = 6, FoundDate = DateTime.Parse("05.12.1999"), Number = "1235", ForeignOrganizationalFormCodeId = 2, RequisitesEng = "DESC NUM 3 DESC NUM 2 DESC NUM 1", RegNumber = "123123123", RequisitesRus = "Реквизит1, 2 ,3 ,5 10 реквизиты", OtherInfo = "Дополнительная информация 3 " };
            ForeignLightCompany fcl4 = new ForeignLightCompany() { EnglishName = "FOREGIN LIGHT COMPANY 4", RussianName = "ООО Иностранная компания БОЮЛ 4", CountryCodeId = 8, FoundDate = DateTime.Parse("05.12.2005"), Number = "1236", ForeignOrganizationalFormCodeId = 1, RequisitesEng = "DESC NUM 3 DESC NUM 2 DESC NUM 1", RegNumber = "123123123", RequisitesRus = "Реквизит1, 2 ,3 ,5 10 реквизиты", OtherInfo = "Дополнительная информация 4" };

            ProjectCompany pc9 = new ProjectCompany() { Name = "ООО ЛЮКСОФТ ПРОФЕШОНАЛ20", State = State.ForeignLight, ForeignLightCompany = fcl1 };
            ProjectCompany pc10 = new ProjectCompany() { Name = "ООО ЛЮКСОФТ ПРОФЕШОНАЛ21", State = State.ForeignLight, ForeignLightCompany = fcl2 };
            ProjectCompany pc11 = new ProjectCompany() { Name = "ООО ЛЮКСОФТ ПРОФЕШОНАЛ22", State = State.ForeignLight, ForeignLightCompany = fcl3 };
            ProjectCompany pc12 = new ProjectCompany() { Name = "ООО ЛЮКСОФТ ПРОФЕШОНАЛ23", State = State.ForeignLight, ForeignLightCompany = fcl4 };

            ProjectCompanyShare share1 = new ProjectCompanyShare() { OwnerProjectCompanyId = pc0.Id, DependentProjectCompanyId = pc1.Id, DependentProjectCompany = pc1, SharePart = 50.00, ShareType = ShareType.Direct, ShareStartDate = DateTime.Parse("05.12.2005") };
            ProjectCompanyShare share2 = new ProjectCompanyShare() { OwnerProjectCompanyId = pc0.Id, DependentProjectCompanyId = pc2.Id, DependentProjectCompany = pc2, SharePart = 20.00, ShareType = ShareType.Direct, ShareStartDate = DateTime.Parse("05.12.2005") };
            ProjectCompanyShare share3 = new ProjectCompanyShare() { OwnerProjectCompanyId = pc0.Id, DependentProjectCompanyId = pc3.Id, DependentProjectCompany = pc3, SharePart = 10.00, ShareType = ShareType.Direct, ShareStartDate = DateTime.Parse("05.12.2005") };
            ProjectCompanyShare share4 = new ProjectCompanyShare() { OwnerProjectCompanyId = pc0.Id, DependentProjectCompanyId = pc4.Id, DependentProjectCompany = pc4, SharePart = 5.00, ShareType = ShareType.Direct, ShareStartDate = DateTime.Parse("05.12.2005") };

            ProjectCompanyShare share5 = new ProjectCompanyShare() { OwnerProjectCompanyId = pc0.Id, DependentProjectCompanyId = pc5.Id, DependentProjectCompany = pc5, SharePart = 10.10, ShareType = ShareType.Direct, ShareStartDate = DateTime.Parse("05.12.2005") };
            ProjectCompanyShare share6 = new ProjectCompanyShare() { OwnerProjectCompanyId = pc0.Id, DependentProjectCompanyId = pc6.Id, DependentProjectCompany = pc6, SharePart = 10.03, ShareType = ShareType.Direct, ShareStartDate = DateTime.Parse("05.12.2005") };
            ProjectCompanyShare share7 = new ProjectCompanyShare() { OwnerProjectCompanyId = pc0.Id, DependentProjectCompanyId = pc7.Id, DependentProjectCompany = pc7, SharePart = 10.00, ShareType = ShareType.Direct, ShareStartDate = DateTime.Parse("05.12.2005") };
            ProjectCompanyShare share8 = new ProjectCompanyShare() { OwnerProjectCompanyId = pc0.Id, DependentProjectCompanyId = pc8.Id, DependentProjectCompany = pc8, SharePart = 10.00, ShareType = ShareType.Direct, ShareStartDate = DateTime.Parse("05.12.2005") };

            ProjectCompanyShare share9 = new ProjectCompanyShare() { OwnerProjectCompanyId = pc0.Id, DependentProjectCompanyId = pc9.Id, DependentProjectCompany = pc9, SharePart = 10.00, ShareType = ShareType.Direct, ShareStartDate = DateTime.Parse("05.12.2005") };
            ProjectCompanyShare share10 = new ProjectCompanyShare() { OwnerProjectCompanyId = pc0.Id, DependentProjectCompanyId = pc10.Id, DependentProjectCompany = pc10, SharePart = 10.00, ShareType = ShareType.Direct, ShareStartDate = DateTime.Parse("05.12.2005") };
            ProjectCompanyShare share11 = new ProjectCompanyShare() { OwnerProjectCompanyId = pc0.Id, DependentProjectCompanyId = pc11.Id, DependentProjectCompany = pc11, SharePart = 10.20, ShareType = ShareType.Direct, ShareStartDate = DateTime.Parse("05.12.2005") };
            ProjectCompanyShare share12 = new ProjectCompanyShare() { OwnerProjectCompanyId = pc0.Id, DependentProjectCompanyId = pc12.Id, DependentProjectCompany = pc12, SharePart = 10.13, ShareType = ShareType.Direct, ShareStartDate = DateTime.Parse("05.12.2005") };


            ProjectCompanyShare share01 = new ProjectCompanyShare() { OwnerProjectCompanyId = pc0.Id, DependentProjectCompanyId = pc1.Id, DependentProjectCompany = pc1, SharePart = 50.00, ShareType = ShareType.Direct, ShareStartDate = DateTime.Parse("05.12.2005") };
            ProjectCompanyShare share02 = new ProjectCompanyShare() { OwnerProjectCompanyId = pc0.Id, DependentProjectCompanyId = pc2.Id, DependentProjectCompany = pc2, SharePart = 20.00, ShareType = ShareType.Direct, ShareStartDate = DateTime.Parse("05.12.2005") };
            ProjectCompanyShare share03 = new ProjectCompanyShare() { OwnerProjectCompanyId = pc0.Id, DependentProjectCompanyId = pc3.Id, DependentProjectCompany = pc3, SharePart = 10.00, ShareType = ShareType.Direct, ShareStartDate = DateTime.Parse("05.12.2005") };
            ProjectCompanyShare share04 = new ProjectCompanyShare() { OwnerProjectCompanyId = pc0.Id, DependentProjectCompanyId = pc4.Id, DependentProjectCompany = pc4, SharePart = 5.00, ShareType = ShareType.Direct, ShareStartDate = DateTime.Parse("05.12.2005") };




            //List<ProjectCompanyShare> fd = new List<ProjectCompanyShare> { share1, share2, share3, share4, share5, share6, share7, share8, share9, share10, share11, share12 };
            List<ProjectCompanyShare> fd = new List<ProjectCompanyShare> { share7, share11 };



            // List<ProjectCompanyShare> fdd = new List<ProjectCompanyShare> { share01, share02, share03, share04};

            //pc1.DependentProjectCompanyShares = new List<ProjectCompanyShare>{share1,share2,share3,share4,share5, share6, share7, share8, share9, share10,share11, share12};
            //pc1.OwnerProjectCompanyShares = new List<ProjectCompanyShare> { share1, share2, share3, share4, share5, share6, share7, share8, share9, share10, share11, share12 };




            FactShareCalculation fc = new FactShareCalculation();
            var smplList = fc.GetFactShares(GetShares()).Where(s => s.ShareFactPart > 0);

            ////Граф 
            //var graphModel = new AdjacencyGraph<int, TaggedEdge<int, double>>();
            ////Вершины
            //IList<int> vecities = GetOrderedIds(smplList);
            //foreach (var v in vecities)
            //{
            //    graphModel.AddVertex(v);
            //}
            ////Ребра и Веса
            //foreach (var share in smplList)
            //{
            //    graphModel.AddEdge(new TaggedEdge<int, double>(share.DependentProjectCompanyId, share.OwnerProjectCompanyId, share.ShareFactPart));
            //}



            ////Граф 
            //var shareGraph = new BidirectionalGraph<int, Edge<int>>();
            ////Вершины(Vertices) Ребра(Edge) и Веса (a Tag)
            //foreach (var share in smplList)
            //{
            //    shareGraph.AddVerticesAndEdge(new TaggedEdge<int, double>(share.OwnerProjectCompanyId, share.DependentProjectCompanyId, share.ShareFactPart));
            //};
            ////откуда
            //int Source = 1;
            ////Куда
            //int Target = 2;
            ////Ограничение на кол-во путей
            //int pathCount = 10;
            //// e- edgesWeights
            //foreach (IEnumerable<Edge<int>> paths in shareGraph.RankedShortestPathHoffmanPavley(e => 0, Source, Target, pathCount))
            //{
            //    Console.WriteLine("Path Exmple:");
            //    foreach (TaggedEdge<int, double> path in paths)
            //    {
            //        Console.WriteLine(path.Source + " >  " + path.Target + "Costs: " + path.Tag);
            //    }
            //}

            //var graphModel1 = new AdjacencyGraph<int, TaggedEdge<int, double>>();
            //foreach (var share in smplList)
            //{
            //    graphModel1.AddVerticesAndEdge(new TaggedEdge<int, double>(share.DependentProjectCompanyId, share.OwnerProjectCompanyId, share.ShareFactPart));
            //}

            //var edges = new SEdge<int>[] { new SEdge<int>(1, 2), new SEdge<int>(0, 1) };
            //var graph = edges.ToAdjacencyGraph<int, SEdge<int>>(true);

            //var factShareLsit = fc.GetFactShares(fd);


            //string path = @"C:\Users\FyodorSt\Source\Repos\WebKIK\KPMG.WebKik.DocumentProcessing\Templates\UU.xlsx";

            //var nn = ec.GetFilledNotificationOfParticipation(path, pc0, fd, factShareLsit.ToList());
            //nn.SaveAs("NN"+ pc0.Name+".xlsx");

        }

        private static IEnumerable<ProjectCompanyShare> GetShares()
        {
            var part0 = new ProjectCompanyShare() { OwnerProjectCompanyId = 1, DependentProjectCompanyId = 2, SharePart = 55 };
            var part1 = new ProjectCompanyShare() { OwnerProjectCompanyId = 2, DependentProjectCompanyId = 3, SharePart = 40 };
            var part2 = new ProjectCompanyShare() { OwnerProjectCompanyId = 3, DependentProjectCompanyId = 1, SharePart = 30 };
            var part3 = new ProjectCompanyShare() { OwnerProjectCompanyId = 4, DependentProjectCompanyId = 2, SharePart = 45 };
            var part4 = new ProjectCompanyShare() { OwnerProjectCompanyId = 5, DependentProjectCompanyId = 1, SharePart = 70 };
            var part5 = new ProjectCompanyShare() { OwnerProjectCompanyId = 6, DependentProjectCompanyId = 3, SharePart = 60 };
            var part6 = new ProjectCompanyShare() { OwnerProjectCompanyId = 5, DependentProjectCompanyId = 1, SharePart = 70 };
            var part7 = new ProjectCompanyShare() { OwnerProjectCompanyId = 6, DependentProjectCompanyId = 5, SharePart = 30 };
            return new List<ProjectCompanyShare> { part0, part1, part2, part3, part4, part5, part6, part7 };
        }
    }

}