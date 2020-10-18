using System.Collections.Generic;
using System.Threading.Tasks;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Contracts.Repository;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.ProjectCompanies;
using System.Linq;
using KPMG.WebKik.Models.ProjectCompany;
using KPMG.WebKik.Contracts.Algorithms;
using KPMG.WebKik.DocumentProcessing.NotificationOfParticipation;
using System;
using System.Xml.Serialization;
using System.IO;

namespace KPMG.WebKik.Services
{
    public class NotificationOfParticipationService : EntityService<NotificationOfParticipation, int>, INotificationOfParticipationService
    {
        private readonly IEntityRepository<ProjectCompanyShare, int> shareRepository;
        private readonly IEntityRepository<ProjectCompany, int> companyRepository;
        private readonly IFactShareCalculation factShareCalculation;
        private readonly INotificationOfParticipationCalculation notificationCalculation;
        private readonly ISignatoryService signatureService;
        private readonly IProjectCompanyShareService shareService;
        private IProjectCompanyService projectCompanyService;
		private ISupportingDocumentsService supportingDocumentsService;

		public NotificationOfParticipationService(
            IEntityRepository<NotificationOfParticipation, int> repository,
            IEntityRepository<ProjectCompany, int> companyRepository,
            IEntityRepository<ProjectCompanyShare, int> shareRepository,
            IProjectCompanyShareService shareService,
            IFactShareCalculation factShareCalculation,
            INotificationOfParticipationCalculation notificationCalculation,
            ISignatoryService signatureService,
            IProjectCompanyService projectCompanyService, ISupportingDocumentsService supportingDocumentsService) : base(repository)
        {
            this.shareRepository = shareRepository;
            this.companyRepository = companyRepository;
            this.factShareCalculation = factShareCalculation;
            this.notificationCalculation = notificationCalculation;
            this.signatureService = signatureService;
            this.shareService = shareService;
            this.projectCompanyService = projectCompanyService;
			this.supportingDocumentsService = supportingDocumentsService;
        }

        public async Task<IEnumerable<NotificationOfParticipation>> GetByProjectId(int projectId)
        {
			var notification =  await repository
                .Where(x => x.ProjectCompany.Project.Id == projectId)
                .Include(
                    x => x.ProjectCompany,
                    x => x.SubmissionGround)
                .ToListAsync();
			foreach(var n in notification)
			{
				n.ProjectCompany.SupportingDocuments = supportingDocumentsService.GetSupportingDocumentsByCompanyId(n.ProjectCompany.Id);
			}
			return notification;
		}

        public async Task<byte[]> GetDocument(int companyId, int sigantoryId, string path)
        {
            var factShares = await shareService.GetFactByProjectCompanyId(companyId, DateTime.Now);         
            var company = await companyRepository.GetByIdAsync(companyId);
            var signature = await signatureService.GetById(sigantoryId);
            var shares = await shareService.GetAllByProjectId(company.ProjectId, DateTime.Now);
            return await new NPWorkbook(notificationCalculation, projectCompanyService)
                .GetDocumentData(company, factShares, signature, path, shares);
        }

        public async Task<MemoryStream> GetXMLDocument(int companyId, int sigantoryId, int correction)
        {
            var factShares = await shareService.GetFactByProjectCompanyId(companyId, DateTime.Now);
            var company = await projectCompanyService.GetById(companyId);
            var signature = await signatureService.GetById(sigantoryId);

            //Файл
            Файл file = new Файл();

            //Аттрибуты Файла
            file.ВерсПрог = "123";
            file.ИдФайл = "321";
            file.ВерсФорм = ФайлВерсФорм.Item501;

            //Документ
            ФайлДокумент fileDocument = new ФайлДокумент();
            
            //Аттрибуты документа
            fileDocument.ДатаДок = DateTime.Today.ToString();
            fileDocument.КодНО = "Код налогового органа";
            fileDocument.НомКорр = correction.ToString();
            fileDocument.КНД = ФайлДокументКНД.Item1120411;
            

            //Подписант
            ФайлДокументПодписант fileDocumentSigantory = new ФайлДокументПодписант();

            //Аттрибуты подписанта
            fileDocumentSigantory.Email = signature.Email;
            fileDocumentSigantory.ИННФЛ = signature.Inn;
            fileDocumentSigantory.Тлф = signature.PhoneNumber;
            //Признак лица, подписавшего документ - ??
            fileDocumentSigantory.ПрПодп = ФайлДокументПодписантПрПодп.Item1;


            //ФИО
            ФИОТип fioType = new ФИОТип();
            fioType.Имя = signature.FirstName;
            fioType.Отчество = signature.MiddleName;
            fioType.Фамилия = signature.LastName;

            //Сведения о представителе налогоплательщика
            ФайлДокументПодписантСвПред fileDocumentRepresentative = new ФайлДокументПодписантСвПред();
            fileDocumentRepresentative.НаимДок = signature.ConfirmationDocument?.Name;


            fileDocumentSigantory.ФИО = fioType;
            fileDocumentSigantory.СвПред = fileDocumentRepresentative;
            //Подписант


            //Сведения о заявителе
            ФайлДокументСвНП fileDocumentApplicant = new ФайлДокументСвНП();
            //Налогоплательщик (код) - ??
            fileDocumentApplicant.ПрНП = ФайлДокументСвНППрНП.Item1;

            //Заявитель - физическое лицо
            if (company.State == State.Individual)
            {

                ФайлДокументСвНПНПФЛ fl = new ФайлДокументСвНПНПФЛ();
                fl.ИННФЛ = company.IndividualCompany?.INN.ToString();
                
                //Фио физ лица
                ФИОТип fioFl = new ФИОТип();
                fioFl.Имя = company.IndividualCompany?.Name;
                fioFl.Отчество = company.IndividualCompany?.MiddleName;
                fioFl.Фамилия = company.IndividualCompany?.Surname;

                //Сведения о физическом лице
                ФайлДокументСвНПНПФЛСведФЛ svedFl = new ФайлДокументСвНПНПФЛСведФЛ();

                //Аттрибуты Сведения о физическом лице
                svedFl.ДатаРожд = company.IndividualCompany?.BirthDate.ToString("dd.MM.yyyy");
                svedFl.МестоРожд = company.IndividualCompany?.BirthPlace;
                if (company.IndividualCompany.GenderCode != null)
                    svedFl.Пол = (ФайлДокументСвНПНПФЛСведФЛПол)Enum.Parse(typeof(ФайлДокументСвНПНПФЛСведФЛПол), company.IndividualCompany?.GenderCode?.Code);
                if (company.IndividualCompany.CitizenshipCode != null)
                    svedFl.ПрГражд = (ФайлДокументСвНПНПФЛСведФЛПрГражд)Enum.Parse(typeof(ФайлДокументСвНПНПФЛСведФЛПрГражд), company.IndividualCompany?.CitizenshipCode?.Code);
                svedFl.ОКСМ = company.IndividualCompany?.ForeignCountryCode?.Code;

                
                //Сведения о документе, удостоверяющем личность
                УдЛичнФЛТип flDoc = new УдЛичнФЛТип();
                flDoc.ДатаДок = company.IndividualCompany?.ConfirmedPersonalityDocInfo?.IssueDate.ToString("dd.MM.yyyy");
                flDoc.ВыдДок = company.IndividualCompany?.ConfirmedPersonalityDocInfo?.IssuePlace;
                flDoc.КодВидДок = company.IndividualCompany?.ConfirmedPersonalityDocInfo?.DocumentCode?.Code;
                flDoc.СерНомДок = company.IndividualCompany?.ConfirmedPersonalityDocInfo?.SeriesAndNumber;


                svedFl.УдЛичн = flDoc;

                fl.СведФЛ = svedFl;
                fl.ФИО = fioFl;

                fileDocumentApplicant.Item = fl;
            }
            else
            {

            }




            fileDocument.СвНП = fileDocumentApplicant;
            fileDocument.Подписант = fileDocumentSigantory;
            file.Документ = fileDocument;

            var serializer = new XmlSerializer(typeof(Файл));

            

            MemoryStream memStream = new MemoryStream();
            serializer.Serialize(memStream, file);
            memStream.Position = 0;

            return memStream;
        }
    }
}
