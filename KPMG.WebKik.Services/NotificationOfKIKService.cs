using System.Collections.Generic;
using System.Threading.Tasks;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Contracts.Repository;
using kikXML = KPMG.WebKik.Models.NotificationOdKikXML;
using KPMG.WebKik.Models.ProjectCompanies;
using KPMG.WebKik.Contracts.Algorithms;
using System;
using KPMG.WebKik.DocumentProcessing.NotificationOfKIK;
using KPMG.WebKik.Contracts.Service.Registers;
using KPMG.WebKik.Data;
using System.Linq;
using System.Data.Entity;
using System.IO;
using System.Xml.Serialization;
using KPMG.WebKik.Models;

namespace KPMG.WebKik.Services
{
    public class NotificationOfKIKService : EntityService<Models.NotificationOfKIK, int>, INotificationOfKIKService
    {
        private readonly IEntityRepository<ProjectCompanyShare, int> shareRepository;
        private readonly IEntityRepository<ProjectCompany, int> companyRepository;
        private readonly IFactShareCalculation factShareCalculation;
        private readonly ISignatoryService signatureService;
        private readonly IProjectCompanyShareService shareService;
        private IProjectCompanyService projectCompanyService;
        private IRegister1Service register1Service;
        private IRegister3Service register3Service;
        private IRegister9Service register9Service;

        public NotificationOfKIKService(
            IEntityRepository<NotificationOfKIK, int> repository,
            IEntityRepository<ProjectCompany, int> companyRepository,
            IEntityRepository<ProjectCompanyShare, int> shareRepository,
            IProjectCompanyShareService shareService,
            IFactShareCalculation factShareCalculation,
            ISignatoryService signatureService,
            IProjectCompanyService projectCompanyService,
            IRegister1Service register1Service,
            IRegister3Service register3Service,
            IRegister9Service register9Service) : base(repository)
        {
            this.shareRepository = shareRepository;
            this.companyRepository = companyRepository;
            this.factShareCalculation = factShareCalculation;
            this.signatureService = signatureService;
            this.shareService = shareService;
            this.projectCompanyService = projectCompanyService;
            this.register1Service = register1Service;
            this.register3Service = register3Service;
            this.register9Service = register9Service;
        }

        public async Task<IEnumerable<NotificationOfKIK>> GetByProjectId(int projectId)
        {
            using (var context = new WebKikDataContext())
            {
                return await context.NotificationOfKIK.Where(x => x.ProjectCompany.Project.Id == projectId)
                .Include(
                    x => x.ProjectCompany)
                .ToListAsync();
            }
        }

        public async Task<byte[]> GetDocument(int companyId, int sigantoryId, string path, int year, int correction, int taxAuthorityCode)
        {
            var company = await projectCompanyService.GetById(companyId);
            var signature = await signatureService.GetSignatureById(sigantoryId);
            var factShares = await shareService.GetFactByProjectCompanyId(companyId, DateTime.Now);
            var shares = await shareService.GetAllByProjectId(company.ProjectId);
            IReportCompanyService reportCompanyService = new ReportCompanyService(company, shares, factShares);

            return await new NotificationOfKIKWorkbook(projectCompanyService, shareService, reportCompanyService, register1Service, register3Service, register9Service)
                .GetDocumentData(company, factShares, signature, path, year, correction, taxAuthorityCode);
        }

        public async Task<MemoryStream> GetXMLDocument(int companyId, int sigantoryId, int year, int correction, int taxAuthorityCode)
        {
            var factShares = await shareService.GetFactByProjectCompanyId(companyId, DateTime.Now);
            var company = await projectCompanyService.GetById(companyId);
            var signature = await signatureService.GetSignatureById(sigantoryId);

            kikXML.Файл file = new kikXML.Файл();
            file.ВерсПрог = "123";
            file.ИдФайл = "321";

            kikXML.ФайлДокумент fileDocument = new kikXML.ФайлДокумент();
            fileDocument.ДатаДок = DateTime.Today.ToString();
            fileDocument.КодНО = taxAuthorityCode.ToString();
            fileDocument.НалПер = year.ToString();
            fileDocument.НомКорр = correction.ToString();
            fileDocument.КодНО = "1120416";

            // Сведения заявителя
            kikXML.ФайлДокументСвНП claimInfo = new kikXML.ФайлДокументСвНП();
            if (company.State == State.Individual)
            {
                claimInfo.ПрНП = 2;
                kikXML.ФайлДокументСвНПНПФЛ fl = new kikXML.ФайлДокументСвНПНПФЛ();
                kikXML.ФИОТип fiotype = new kikXML.ФИОТип();
                fiotype.Имя = company.IndividualCompany.Name;
                fiotype.Фамилия = company.IndividualCompany.Surname;
                fiotype.Отчество = company.IndividualCompany.MiddleName;
                fl.ФИО = new[] { fiotype };
                kikXML.ФайлДокументСвНПНПФЛСведФЛ svedFl = new kikXML.ФайлДокументСвНПНПФЛСведФЛ();

                svedFl.Пол = company.IndividualCompany.GenderCode.Id;
                svedFl.ДатаРожд = company.IndividualCompany.BirthDate.ToString("dd.MM.yyyy");
                svedFl.МестоРожд = company.IndividualCompany.BirthPlace;
                svedFl.ПрГражд = company.IndividualCompany.CitizenshipCode.Id;
                svedFl.СтрРег = company.IndividualCompany.RegionCodeId.ToString();

                if (company.IndividualCompany.VerifedPersonalityDocInfo != null)
                {
                    kikXML.УдЛичнФЛТип udLichnType = new kikXML.УдЛичнФЛТип();
                    udLichnType.КодВидДок = company.IndividualCompany.VerifedPersonalityDocInfo.DocumentCode?.Code;
                    udLichnType.СерНомДок = company.IndividualCompany.VerifedPersonalityDocInfo.SeriesAndNumber;
                    udLichnType.ДатаДок = company.IndividualCompany.VerifedPersonalityDocInfo.IssueDate.ToString("dd.MM.yyyy");
                    udLichnType.ВыдДок = company.IndividualCompany.VerifedPersonalityDocInfo.IssuePlace;
                    svedFl.УдЛичн = new[] { udLichnType };
                }
                if (company.IndividualCompany.ConfirmedPersonalityDocInfo != null)
                {
                    kikXML.УдЛичнФЛТип registration = new kikXML.УдЛичнФЛТип();
                    registration.КодВидДок = company.IndividualCompany.ConfirmedPersonalityDocInfo.DocumentCode?.Code;
                    registration.СерНомДок = company.IndividualCompany.ConfirmedPersonalityDocInfo.SeriesAndNumber;
                    registration.ДатаДок = company.IndividualCompany.ConfirmedPersonalityDocInfo.IssueDate.ToString("dd.MM.yyyy");
                    registration.ВыдДок = company.IndividualCompany.ConfirmedPersonalityDocInfo.IssuePlace;
                    svedFl.СведДокРег = new[] { registration };
                }
                fl.СведФЛ = new[] { svedFl };
                claimInfo.НПФЛ = new[] { fl };

            }
            else if (company.State == State.Domestic || company.State == State.ForeignLight)
            {
                claimInfo.ПрНП = company.State == State.Domestic ? 1 : 3;
                kikXML.ФайлДокументСвНПНПЮЛ ul = new kikXML.ФайлДокументСвНПНПЮЛ();
                ul.ИННЮЛ = company.State == State.Domestic ? company.DomesticCompany.INN.ToString() : company.DomesticCompany.INN.ToString();
                ul.КПП = company.State == State.Domestic ? company.DomesticCompany.KPP.ToString() : company.DomesticCompany.KPP.ToString();
                ul.НаимОрг = company.State == State.Domestic ? company.DomesticCompany.FullName : company.DomesticCompany.FullName;
                claimInfo.НПЮЛ = new[] { ul };
            }

            fileDocument.СвНП = new[] { claimInfo };
            kikXML.ФайлДокументПодписант fileDocumentSigantory = new kikXML.ФайлДокументПодписант();
           // fileDocumentSigantory.Email = signature.Email;
            fileDocumentSigantory.ИННФЛ = signature.Inn;
            fileDocumentSigantory.Тлф = signature.PhoneNumber;
            fileDocumentSigantory.ПрПодп = signature?.SignatoryCode?.Code;
            fileDocumentSigantory.ЭлАдрес = signature.Email;

            kikXML.ФИОТип fioType = new kikXML.ФИОТип();
            fioType.Имя = signature.FirstName;
            fioType.Отчество = signature.MiddleName;
            fioType.Фамилия = signature.LastName;
            fileDocumentSigantory.ФИО = new[] { fioType };

            if (signature != null && signature.ConfirmationDocument != null)
            {
                kikXML.ФайлДокументПодписантСвПред svedPredstav = new kikXML.ФайлДокументПодписантСвПред();
                svedPredstav.НаимДок = signature?.ConfirmationDocument?.Name;
                fileDocumentSigantory.СвПред = new[] { svedPredstav };
            }

            fileDocument.Подписант = new[] { fileDocumentSigantory };

            //file.ВерсФорм = kikXML.ФайлВерсФорм.Item501;
            file.Документ = new[] { fileDocument };

            var serializer = new XmlSerializer(typeof(kikXML.Файл));

            MemoryStream memStream = new MemoryStream();
            serializer.Serialize(memStream, file);
            memStream.Position = 0;

            return memStream;
        }

    }
}
