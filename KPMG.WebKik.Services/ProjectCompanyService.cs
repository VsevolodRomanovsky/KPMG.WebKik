using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Threading.Tasks;
using KPMG.WebKik.Models.ProjectCompany;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Contracts.Repository;
using KPMG.WebKik.Contracts.Algorithms;
using System.Linq;
using KPMG.WebKik.DocumentProcessing;
using System.IO;
using System.Reflection;
using KPMG.WebKik.Models;
using KPMG.Webkik.Utils;
using KPMG.WebKik.DocumentProcessing.NotificationOfParticipation;
using KPMG.WebKik.Models.ProjectCompanies;
using KPMG.WebKik.Models.Registers;
using KPMG.WebKik.Data;
using KPMG.WebKik.Models.Companies;
using KPMG.WebKik.Models.Directories;
using KPMG.WebKik.Contracts.Service.Registers;
using KPMG.WebKik.Services.Helpers;

namespace KPMG.WebKik.Services
{
    public class ProjectCompanyService : EntityService<ProjectCompany, int>, IProjectCompanyService
    {
        private readonly WebKikDataContext dbContext;
        private readonly IFactShareCalculation factShareCalculation;
        private readonly IControlCompanyCalculation controlCalculation;
        private readonly IKIKCompanyCalculation kikCompanyCalculation;
        private readonly INotificationOfParticipationCalculation notificationCalculation;
		//  private readonly IEntityRepository<ProjectCompanyShare, int> shareRepository;
		private readonly IRegister4Service register4Service;

        //IProjectCompanyShareRepository repository;
        IEntityRepository<DoubleTaxationAgreementCountryCode, int> sidnRepository;

        IEntityRepository<EAECCountryCode, int> eaecRepository;
        private readonly ISupportingDocumentsService supportingDocsService;

		public ProjectCompanyService(WebKikDataContext dbContext, IProjectCompanyRepository repository,
            ISupportingDocumentsService supportingDocsService,
            IFactShareCalculation factShareCalculation, IControlCompanyCalculation controlCalculation,
            IKIKCompanyCalculation kikCompanyCalculation,
            INotificationOfParticipationCalculation notificationCalculation,
            IRegister4Service register4Service,
			//IEntityRepository<ProjectCompanyShare, int> shareRepository, IProjectCompanyShareRepository repository,
			IEntityRepository<DoubleTaxationAgreementCountryCode, int> sidnRepository,
            IEntityRepository<EAECCountryCode, int> eaecRepository) : base(repository)
        {
            this.dbContext = dbContext;
			this.factShareCalculation = factShareCalculation;
            this.controlCalculation = controlCalculation;
            this.kikCompanyCalculation = kikCompanyCalculation;
            this.notificationCalculation = notificationCalculation;
            this.sidnRepository = sidnRepository;
            this.eaecRepository = eaecRepository;
            this.supportingDocsService = supportingDocsService;
            this.register4Service = register4Service;
		}

        public override async Task<ProjectCompany> GetById(int id)
        {
            repository.ClearQuery();

            var result = await repository
                .Where(x => x.Id == id)
                .Include(
                    c => c.DomesticCompany,
                    c => c.ForeignCompany,
                    c => c.ForeignCompany.CountryCode,
                    c => c.ForeignLightCompany,
                    c => c.ForeignLightCompany.CountryCode,
                    c => c.IndividualCompany,
                    c => c.OwnerProjectCompanyShares,
                    c => c.IndividualCompany.VerifedPersonalityDocInfo,
                    c => c.IndividualCompany.VerifedPersonalityDocInfo.DocumentCode,
                    c => c.IndividualCompany.ConfirmedPersonalityDocInfo,
                    c => c.IndividualCompany.ConfirmedPersonalityDocInfo.DocumentCode,
                    c => c.IndividualCompany.GenderCode,
                    c => c.IndividualCompany.RegionCode,
                    c => c.IndividualCompany.ForeignCountryCode,
                    c => c.IndividualCompany.CitizenshipCode,
                    c => c.IndividualCompany.RussianLocationCode)
                .FirstOrDefaultAsync();
            return result;
        }

        public override async Task<ProjectCompany> Create(ProjectCompany entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;
            repository.Add(entity);
            await repository.SaveChangesAsync();
            return entity;
        }

        public override async Task Update(ProjectCompany entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;
            repository.Update(entity);
            await repository.SaveChangesAsync();
        }


        public IList<IndividualCompany> GetIndividualCompId(int id)
        {
            using (var context = new WebKikDataContext())
            {
                var result =  context.IndividualCompany.Where(c => c.Id == id)
                    .Include(c => c.VerifedPersonalityDocInfo)
                    .Include(c => c.VerifedPersonalityDocInfo.DocumentCode)
                    .Include(c => c.ConfirmedPersonalityDocInfo)
                    .Include(c => c.ConfirmedPersonalityDocInfo.DocumentCode)
                    .Include(c => c.GenderCode)
                    .Include(c => c.RegionCode)
                    .Include(c => c.ForeignCountryCode)
                    .Include(c => c.CitizenshipCode)
                    .Include(c => c.RussianLocationCode)
                    .ToList();
                return result;
            }
        }

        public async Task<IList<ProjectCompany>> GetAllByProjectId(int projectId)
        {
            var companies = await repository
                .Where(comp => comp.ProjectId == projectId)
                .Include(
                    c => c.DomesticCompany,
                    c => c.ForeignCompany,
                    c => c.ForeignLightCompany,
                    c => c.IndividualCompany
                    //c => c.IndividualCompany.VerifedPersonalityDocInfo,
                    //c => c.IndividualCompany.VerifedPersonalityDocInfo.DocumentCode,
                    //c => c.IndividualCompany.ConfirmedPersonalityDocInfo,
                    //c => c.IndividualCompany.ConfirmedPersonalityDocInfo.DocumentCode,
                   // c => c.IndividualCompany.GenderCode,
                    //c => c.IndividualCompany.RegionCode,
                    //c => c.IndividualCompany.ForeignCountryCode,
                    //c => c.IndividualCompany.CitizenshipCode,
                    //c => c.IndividualCompany.RussianLocationCode
                    )
                .ToListAsync();

            return companies;
        }

        public async Task<IEnumerable<ProjectCompany>> GetCompaniesForNotification(int projectId)
        {
            return await repository
                .Where(x => x.ProjectId == projectId)
                .Where(x => x.State == State.Domestic || x.State == State.Individual)
                .ToListAsync();
        }


        public async Task CalculateProjectInfo(int projectId)
        {
            var companies = await repository.Where(c => c.ProjectId == projectId)
                .Include(y => y.OwnerProjectCompanyShares,
                    x => x.DependentProjectCompanyShares)
                .ToListAsync();

            if (companies != null && companies.Count > 0)
            {
                //await CalculateFactShares(companies);
                //await DetermineIfNeedNotificationOfParticipation(projectId);
                await DetermineControlledPerson(companies);
                //await RecognitionKiKForeignCompany(companies);
            }
        }

        #region Освобождение от налогов

        public TaxExemption TaxExemptionFor(int ownerCompanyId, int companyId, int year)
        {
            var item = dbContext.TaxExemptions.FirstOrDefault(
                x => x.DependentProjectCompanyId == companyId && x.OwnerProjectCompanyId == ownerCompanyId &&
                     x.Year == year);

            if (item == null)
            {
                item = new TaxExemption()
                {
                    OwnerProjectCompanyId = ownerCompanyId,
                    DependentProjectCompanyId = companyId,
                    Year = year,
                };
            }

            return item;
        }

        public async Task<TaxExemptionResult> DefineTaxStatus(TaxExemption entity)
        {
            bool isExempted = false;
            bool isNotEnoughData = false;

            var owner = await GetById(entity.OwnerProjectCompanyId);
            var kik = await GetById(entity.DependentProjectCompanyId);

            var countryCodeId = kik.ForeignCompany != null
                ? kik.ForeignCompany.CountryCode.Code
                : kik.ForeignLightCompany.CountryCode.Code;
            var allEurMembers = await eaecRepository.ToListAsync();
            var allSidnMembers = await sidnRepository.ToListAsync();
			TaxStatusHelper helper = new TaxStatusHelper(dbContext, register4Service, factShareCalculation, kikCompanyCalculation);
			foreach (var item in entity.Rationaly)
            {
                switch (item)
                {
                    case RationalyType.NonProfitOrganization:
                        isExempted = true;
                        break;
                    case RationalyType.OffshoreFieldOperator:
                        isExempted = true;
                        break;
                    case RationalyType.OffshoreFieldAuctioneer:
                        isExempted = true;
                        break;
                    case RationalyType.EurAsECMember:
                        isExempted = IsEurAsECMember(allEurMembers, countryCodeId);
                        break;
                    case RationalyType.InsuranceAgencyWithLexPersonalis:
                    case RationalyType.BankWithLexPersonalis:
                        isExempted = IsSidnMember(allSidnMembers, countryCodeId);
                        break;
                    case RationalyType.TradedBondsIssuer:
                    case RationalyType.CededRightsOrganization:
                        var register = dbContext.Registers5.FirstOrDefault(
                            rg5 => (rg5.Year == (Year)entity.Year &&
                                    rg5.OwnerProjectCompanyId == entity.DependentProjectCompanyId));
                        if (register != null)
                        {
                            isExempted = IsSidnMember(allSidnMembers, countryCodeId)
                                         && IsPartPercentForBondsProfitMoreThen90Percent(register);
                            break;
                        }
                        isNotEnoughData = true;
                        break;
                    case RationalyType.ProjectMemberMining:
                        var register7 = dbContext.Registers7.FirstOrDefault(
                            rg7 => (rg7.Year == (Year)entity.Year &&
                                    rg7.OwnerProjectCompanyId == entity.DependentProjectCompanyId));
                        if (register7 != null)
                        {
                            isExempted = register7.PartPercentSRPIncome >= 91;
                            break;
                        }
                        isNotEnoughData = true;
                        break;
                    case RationalyType.ByESPN:
                        var register6 = dbContext.Registers6.FirstOrDefault(
                            rg6 => (rg6.Year == (Year)entity.Year &&
                                    rg6.OwnerProjectCompanyId == entity.DependentProjectCompanyId));
                        if (register6 != null)
                        {
                            isExempted = IsSidnMember(allSidnMembers, countryCodeId)
                                         && IsAverageTaxRatePartMoreThen75(register6);
                            break;
                        }
                        isNotEnoughData = true;
                        break;
                    case RationalyType.ActiveForeignCompany:
                        var register4 = dbContext.Registers4.FirstOrDefault(
                            rg4 => (rg4.Year == (Year)entity.Year &&
                                    rg4.OwnerProjectCompanyId == entity.DependentProjectCompanyId));
                        if (register4 != null)
                        {
                            isExempted = register4.PassivePartIncomeValue <= 20;
                            break;
                        }
                        isNotEnoughData = true;
                        break;
                    case RationalyType.ActiveHoldingCompany:
						bool isIHK = helper.IsIHK(entity.Year, entity.DependentProjectCompanyId, entity.OwnerProjectCompanyId);
						if (isIHK)
						{
							bool checkIncomeKIKTotalAmount = helper.CheckIncomeKIKTotalAmount(entity.Year, entity.DependentProjectCompanyId);
							bool passivePartWithoutDividends = helper.CheckPassivePartWithoutDividends(entity.Year, entity.DependentProjectCompanyId);
							if (checkIncomeKIKTotalAmount || passivePartWithoutDividends)
							{
								// Прямое участие в АСК 
								bool askShareMoreThan75 = helper.IsAskShareMoreThan75(entity);
								// Прямое участие в АК
								bool akShareMoreThan50 = helper.IsAkShareMoreThan50(entity.Year, entity.DependentProjectCompanyId);
								if (askShareMoreThan75 && akShareMoreThan50)
								{
									isExempted = true;
								}
							}
						}
						break;
                    case RationalyType.ActiveSubholdingCompany:
						
						bool isISK = helper.IsISK(entity); 
						if (isISK)
						{
							bool checkIncomeKIKTotalAmount = helper.CheckIncomeKIKTotalAmount(entity.Year, entity.DependentProjectCompanyId);
							bool passivePartWithoutDividendsIncomeValue = helper.CheckPassivePartWithoutDividendsIncomeValue(entity.Year, entity.DependentProjectCompanyId);
							// проверяю регистр 
							if (checkIncomeKIKTotalAmount || passivePartWithoutDividendsIncomeValue)
							{
								// Проверяю прямое участие в АК 
								bool isAK = false;
								isAK = helper.IsAkShareMoreThan50(entity.Year, entity.DependentProjectCompanyId);
								if (isAK)
								{
									isExempted = true;
								}
							}
						}
						break;
                    default:
                        break;
                }

                //if (!isExempted || isNotEnoughData)
                //    return new TaxExemptionResult { IsExempted = isExempted, IsNotEnoughData = isNotEnoughData };
            }

            //entity.IsExemptFromTaxes = isExempted;
            //(repository as IProjectCompanyShareRepository).Update(entity);
            //await repository.SaveChangesAsync();

            var row = TaxExemptionFor(entity.OwnerProjectCompanyId, entity.DependentProjectCompanyId, entity.Year);
            row.Result = isExempted;
            row.Rationaly = entity.Rationaly;

            if (row.Id == 0)
            {
                dbContext.TaxExemptions.Add(row);
            }
            await dbContext.SaveChangesAsync();

            return new TaxExemptionResult { IsExempted = isExempted, IsNotEnoughData = isNotEnoughData };
        }

        private bool CheckExemption(RationalyType exemption)
        {
            return (
                exemption == RationalyType.NonProfitOrganization ||
                exemption == RationalyType.OffshoreFieldOperator ||
                exemption == RationalyType.OffshoreFieldAuctioneer);
        }



        private bool IsEurAsECMember(IList<EAECCountryCode> allEurMembers, string countryCodeId)
        {
            return allEurMembers.Any(x => x.Code == countryCodeId);
        }

        private bool IsSidnMember(IList<DoubleTaxationAgreementCountryCode> allSidnMembers, string countryCodeId)
        {
            return allSidnMembers.Any(x => x.Code == countryCodeId);
        }

        private bool IsPartPercentForBondsProfitMoreThen90Percent(Register5 register)
        {
            return register.PartPercentForBondsProfit >= 90;
        }

        private bool IsAverageTaxRatePartMoreThen75(Register6 register)
        {
            return register.AverageTaxRatePart > 75;
        }
        #endregion

        #region private

        private List<ProjectCompanyFactShare> CalculateFactShares(IList<ProjectCompany> companies)
        {
            var shares = new List<ProjectCompanyShare>();
            var factShares = new List<ProjectCompanyFactShare>();

            foreach (var company in companies)
            {
                if (company.OwnerProjectCompanyShares != null && company.OwnerProjectCompanyShares.Count > 0)
                {
                    shares.AddRange(company.OwnerProjectCompanyShares);
                }
            }
            if (shares.Count > 0)
            {
                factShares = factShareCalculation.GetFactShares(shares).ToList();
            }

            return factShares;

            //if (factShares.Count > 0)
            //{
            //    await RemoveOldFactShares(companies.Select(c => c.Id).ToList());
            //    await AddNewFactShareParts(factShares);
            //}
        }

        //private async Task RemoveOldFactShares(List<int> companyIds)
        //{
        //    var oldShares = await factShareRepository.Where(s => companyIds.Contains(s.OwnerProjectCompanyId)).ToListAsync();

        //    if (oldShares != null && oldShares.Count > 0)
        //    {
        //        foreach (var share in oldShares)
        //        {
        //            factShareRepository.Delete(share);
        //        }

        //        await factShareRepository.SaveChangesAsync();
        //    }
        //}

        //private async Task AddNewFactShareParts(List<ProjectCompanyFactShare> factShares)
        //{
        //    foreach (var factShare in factShares)
        //    {
        //        factShareRepository.Add(factShare);
        //    }

        //    await factShareRepository.SaveChangesAsync();
        //}

        private async Task DetermineControlledPerson(IList<ProjectCompany> companies)
        {

            var factShares = CalculateFactShares(companies);

            foreach (var company in companies)
            {
                company.IsControlCompany = false;

                foreach (var share in company.OwnerProjectCompanyShares)
                {
                    var isControl = controlCalculation.IsControlCompany(share, companies, factShares);

                    if (isControl) company.IsControlCompany = true;
                    //share.IsControlledBy = isControl;
                }
            }

            await repository.SaveChangesAsync();
        }

        // будем считать каждый раз!
        //private async Task RecognitionKiKForeignCompany(IList<ProjectCompany> companies)
        //{
        //    foreach (var company in companies)
        //    {
        //        foreach (var share in company.OwnerProjectCompanyShares)
        //        {
        //            share.IsKIKCompany = kikCompanyCalculation.IsKIKCompany(share);
        //        }
        //    }

        //    await repository.SaveChangesAsync();
        //}

        private async Task DetermineIfNeedNotificationOfParticipation(int projectId)
        {
            //TODO: think if this is really needed if yes, then implement

            //var shares = await shareRepository
            //    .Where(share => !share.ShareFinishDate.HasValue)
            //    .Where(share => share.OwnerProjectCompany.Project.Id == projectId ||
            //                    share.DependentProjectCompany.Project.Id == projectId)
            //    .ToListAsync();

            //var factShares = factShareCalculation.GetFactShares(shares);


            //foreach (var factShare in factShares)
            //{
            //    var share = factShare

            //    share.IsNotificationOfParticipationRequired = notificationCalculation.CalculateIsNotificationOfParticipationRequired
            //}

            //await repository.SaveChangesAsync();
        }

        public SupportingDocument UploadFile(int year, int companyType, int companyId, bool isUU, bool isUKIK,
            bool isND, string uKIKDocType, byte[] fileData, string fileName)
        {

            return supportingDocsService.CreateDocument(year, companyType, companyId, isUU, isUKIK, isND, uKIKDocType,
                fileData, fileName);
        }

        /// <summary>
        /// Устанавливает значения по умолчанию в 0 для double полей у регистров
        /// </summary>
        /// <param name="_register"></param>
        /// <returns></returns>
        private object SetAllFieldsAsDefault(object _register)
        {
            Type register = _register.GetType();
            var fields = register.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                if (field.FieldType != typeof(double)) continue;
                field.SetValue(_register, 0);
            }
            return _register;
        }


        public void UploadKlDataFromExcel(byte[] file, int sharedId, int yaer)
        {
            var dict = ExcelDocsImporter.UploadRegistersData(file);

            var year = (Year)yaer;

            var register01 = new Register1 { Year = year, Type = RegisterType.Register1, Currency = "USD" };
            var register02 = new Register2 { Year = year, Type = RegisterType.Register2, Currency = "USD" };
            var register04 = new Register4 { Year = year, Type = RegisterType.Register4, Currency = "USD" };

            using (var context = new WebKikDataContext())
            {
                var _ProjectCompany = context.ProjectCompanies
                    .Include(c => c.Registers1)
                    .Include(c => c.Registers2)
                    .Include(c => c.Registers4)
                    .First(c => c.Id == sharedId);

                if (_ProjectCompany.Registers1.SingleOrDefault(c => c.OwnerProjectCompanyId == sharedId && c.Year == year) == null)
                    _ProjectCompany.Registers1.Add(SetAllFieldsAsDefault(register01) as Register1);
                else
                    register01 = _ProjectCompany.Registers1.First(c => c.OwnerProjectCompanyId == sharedId && c.Year == year);


                if (_ProjectCompany.Registers2.SingleOrDefault(c => c.OwnerProjectCompanyId == sharedId && c.Year == year) == null)
                    _ProjectCompany.Registers2.Add(SetAllFieldsAsDefault(register02) as Register2);
                else
                    register02 = _ProjectCompany.Registers2.First(c => c.OwnerProjectCompanyId == sharedId && c.Year == year);


                if (_ProjectCompany.Registers4.SingleOrDefault(c => c.OwnerProjectCompanyId == sharedId && c.Year == year) == null)
                    _ProjectCompany.Registers4.Add(SetAllFieldsAsDefault(register04) as Register4);
                else
                    register04 = _ProjectCompany.Registers4.First(c => c.OwnerProjectCompanyId == sharedId && c.Year == year);

                foreach (var item in dict)
                {
                    double value;
                    double.TryParse(item.Value, out value);
                    #region switch
                    switch (item.Key)
                    {
                        case "ID001":
                            register01.IncomeSymmetricCorrection = value;
                            break;
                        case "ID002":
                            register01.ControlledProfitAmount = value;
                            break;
                        case "ID003":
                            register04.DividendsFromActiveCompanies = value;
                            break;
                        case "ID004":
                            register04.DividendsFromHoldingCompanies = value;
                            break;
                        case "ID005":
                            register02.BalanceLoss = value;
                            break;
                        case "ID006":
                            register02.BalanceLoss2012 = value;
                            break;
                        case "ID007":
                            register02.BalanceLoss2013 = value;
                            break;
                        case "ID008":
                            register02.BalanceLoss2014 = value;
                            break;
                        case "ID009":
                            register02.BalanceLoss00 = value;
                            break;
                        case "ID010":
                            register02.BalanceLoss01 = value;
                            break;
                        case "ID011":
                            register02.LossSumTaxBase = value;
                            break;
                        case "ID012":
                            register02.SumLoss2012 = value;
                            break;
                        case "ID013":
                            register02.SumLoss2013 = value;
                            break;
                        case "ID014":
                            register02.SumLoss2014 = value;
                            break;
                        case "ID015":
                            register02.SumLoss00 = value;
                            break;
                        case "ID016":
                            register02.SumLoss01 = value;
                            break;
                    }
                    #endregion
                }
                context.SaveChanges();
            }

        }

        public void UploadDataFromExcel(byte[] file, int sharedId, int yaer)
        {
            var dict = ExcelDocsImporter.UploadRegistersData(file);

            var year = (Year)yaer;

            var register01 = new Register1 { Year = year, Type = RegisterType.Register1, Currency = "USD" };
            var register02 = new Register2 { Year = year, Type = RegisterType.Register2, Currency = "USD" };
            var register03 = new Register3 { Year = year, Type = RegisterType.Register3, Currency = "USD" };
            var register04 = new Register4 { Year = year, Type = RegisterType.Register4, Currency = "USD" };
            var register05 = new Register5 { Year = year, Type = RegisterType.Register5, Currency = "USD" };
            var register06 = new Register6 { Year = year, Type = RegisterType.Register6, Currency = "USD" };
            var register07 = new Register7 { Year = year, Type = RegisterType.Register7, Currency = "USD" };


            #region set default
            /*
            register01.IncomeKIKNotIncluded = 0;
            register01.IncomeFromRegisteredCapitals = 0;
            register01.IncomeFromShares = 0;
            register01.IncomeFromSecurities = 0;
            register01.IncomeFromDerivatives = 0;
            register01.IncomeFromSubsidiary = 0;
            register01.IncomeFromReserveRestore = 0;
            register01.CostsKIKNotIncluded = 0;
            register01.CostsFromRegisteredCapitals = 0;
            register01.CostsFromShares = 0;
            register01.CostsFromSecurities = 0;
            register01.CostsFromDerivatives = 0;
            register01.CostsFromSubsidiary = 0;
            register01.CostsFromReserveRestore = 0;
            register01.IncomeAndCostsTotalAmount = 0;
            register01.ProfitTotalAmountCorrection = 0;
            register01.ReassessmentTotalAmount = 0;
            register01.ReassessmentFromRegisteredCapitals = 0;
            register01.ReassessmentFromShares = 0;
            register01.ReassessmentFromSecurities = 0;
            register01.ReassessmentFromDerivatives = 0;
            register01.IncomeSymmetricCorrection = 0;
            register01.IncomeNotIncludedInProfit = 0;
            register01.CostsIncludedInProfit = 0;
            register01.AdjustedProfitAmount = 0;
            register01.ProfitExclusion = 0;
            register01.DividendsCurrentYear = 0;
            register01.DistributedProfitAmount = 0;
            register01.IncomeProperty = 0;
            register01.LossProperty = 0;
            register01.ProfitAmount = 0;
            register01.AverageForeignCurrency = 0;
            register01.ProfitAmountConvertedCurrency = 0;
            register01.StandartKKIKProfit = 0;
            register01.ReceivedDividends = 0;
            register01.ProfitAmountCurrentYear = 0;
            register01.ProfitAmountForTax = 0;
            register01.LossKIKFromPastYears = 0;
            register01.CountableProfitAmountForTax = 0;
            register01.ShareInKIKProfit = 0;
            register01.PartKIKProfit = 0;
            register01.ControlledProfitAmount = 0;
            register01.KIKTaxBase = 0;
            register02.BalanceLoss = 0;
            register02.BalanceLoss2012 = 0;
            register02.BalanceLoss2013 = 0;
            register02.BalanceLoss2014 = 0;
            register02.BalanceLoss00 = 0;
            register02.BalanceLoss01 = 0;
            register02.TaxBaseForTaxPeriod = 0;
            register02.LossSumTaxBase = 0;
            register02.SumLoss2012 = 0;
            register02.SumLoss2013 = 0;
            register02.SumLoss2014 = 0;
            register02.SumLoss00 = 0;
            register02.SumLoss01 = 0;
            register02.FullBalanceLoss = 0;
            register02.FullBalanceLoss2012 = 0;
            register02.FullBalanceLoss2013 = 0;
            register02.FullBalanceLoss2014 = 0;
            register02.FullBalanceLoss00 = 0;
            register02.FullBalanceLoss01 = 0;
            register03.KIKProfitCurrency = 0;
            register03.KIKConversionRate = 0;
            register03.KIKProfitRUR = 0;
            register03.KIKSharePart = 0;
            register03.KIKProfitSharePart = 0;
            register03.KIKProfitPartForTax = 0;
            register03.KIKTaxBasePart = 0;
            register03.TaxPercentValue = 0;
            register03.TaxSumCurrency = 0;
            register03.TaxSum = 0;
            register03.ForeginContryProfitCurrency = 0;
            register03.ForeginContryProfitRUR = 0;
            register03.ForeginContryEarningsCurrency = 0;
            register03.ForeginContryEarningsRUR = 0;
            register03.DomesticProfitCurrency = 0;
            register03.DomesticContryProfitRUR = 0;
            register03.DomesticContryEarningsCurrency = 0;
            register03.DomesticContryEarningsRUR = 0;
            register03.RURResultSum = 0;
            register03.RURResultForPart = 0;
            register03.RURResultTax = 0;
            register04.IncomeKIKTotalAmount = 0;
            register04.IncomeNotIncluded = 0;
            register04.IncomeFromRateDifference = 0;
            register04.IncomeFromRegisteredCapitals = 0;
            register04.IncomeFromShares = 0;
            register04.IncomeFromSecurities = 0;
            register04.IncomeFromDerivatives = 0;
            register04.IncomeFromSubsidiary = 0;
            register04.IncomeFromReserveRestore = 0;
            register04.IncomeKIKSummary = 0;
            register04.PassiveIncomeKIKSummary = 0;
            register04.DividendsSum = 0;
            register04.DividendsFromActiveCompanies = 0;
            register04.DividendsFromHoldingCompanies = 0;
            register04.IncomeFromAppropriationProfit = 0;
            register04.IncomeFromDebentures = 0;
            register04.IntellectialPropRightsIncome = 0;
            register04.SharedPartsIncome = 0;
            register04.FISSIncome = 0;
            register04.ImmovablePropertyIncome = 0;
            register04.LeasePropertyIncome = 0;
            register04.InvestmentUnitsIncome = 0;
            register04.ServicesIncome = 0;
            register04.ServicesConsultingIncome = 0;
            register04.ServicesLegalIncome = 0;
            register04.ServicesAccountingIncome = 0;
            register04.ServicesAuditIncome = 0;
            register04.ServicesEngineeringIncome = 0;
            register04.ServicesAdvertisingIncome = 0;
            register04.ServicesMarketingIncome = 0;
            register04.ServicesInformationProcessingIncome = 0;
            register04.ServicesReseachAndDevelopmentIncome = 0;
            register04.ServicesStaffProvidingIncome = 0;
            register04.OtherIncome = 0;
            register04.IncomeSumExceptDividendsFromActiveCompanies = 0;
            register04.IncomeSumExceptDividendsFromHoldingCompanies = 0;
            register04.PassivePartIncomeValue = 0;
            register04.PassivePartWithoutDividendsIncomeValue = 0;
            register04.PassivePartWithoutDividendsAndHoldingsIncomeValue = 0;
            register05.SumProfit = 0;
            register05.SumPercentForBondsProfit = 0;
            register05.PartPercentForBondsProfit = 0;
            register06.IncomeTaxRated = 0;
            register06.IncomeTaxDeducted = 0;
            register06.IncomeTaxCorrection = 0;
            register06.KIKProfit = 0;
            register06.IncomeTaxEffected = 0;
            register06.KIKDividends = 0;
            register06.KIKProfitMinusDividends = 0;
            register06.AverageTaxRate = 0;
            register06.AverageTaxRatePart = 0;
            register07.KikTotalSumIncome = 0;
            register07.SumIncomeSRP = 0;
            register07.PartPercentSRPIncome = 0;
            */
            #endregion

            using (var context = new WebKikDataContext())
            {
                var _ProjectCompany = context.ProjectCompanies
                    .Include(c => c.Registers1)
                    .Include(c => c.Registers2)
                    .Include(c => c.Registers3)
                    .Include(c => c.Registers4)
                    .Include(c => c.Registers5)
                    .Include(c => c.Registers6)
                    .Include(c => c.Registers7)
                    .First(c => c.Id == sharedId);


                if (_ProjectCompany.Registers1.SingleOrDefault(c => c.OwnerProjectCompanyId == sharedId && c.Year == year) == null)
                    _ProjectCompany.Registers1.Add(SetAllFieldsAsDefault(register01) as Register1);
                else
                    register01 = _ProjectCompany.Registers1.First(c => c.OwnerProjectCompanyId == sharedId && c.Year == year);


                /* if (!_ProjectCompany.Registers2.SingleOrDefault(c => c.Id == sharedId && c.Year == year) && _ProjectCompany.Registers2.Count == 0)
                     _ProjectCompany.Registers2.Add(SetAllFieldsAsDefault(register02) as Register2);
                 else
                     register02 = _ProjectCompany.Registers2.Single(c => c.Id == sharedId && c.Year == year);*/

                if (_ProjectCompany.Registers3.SingleOrDefault(c => c.OwnerProjectCompanyId == sharedId && c.Year == year) == null)
                    _ProjectCompany.Registers3.Add(SetAllFieldsAsDefault(register03) as Register3);
                else
                    register03 = _ProjectCompany.Registers3.First(c => c.OwnerProjectCompanyId == sharedId && c.Year == year);

                if (_ProjectCompany.Registers4.SingleOrDefault(c => c.OwnerProjectCompanyId == sharedId && c.Year == year) == null)
                    _ProjectCompany.Registers4.Add(SetAllFieldsAsDefault(register04) as Register4);
                else
                    register04 = _ProjectCompany.Registers4.First(c => c.OwnerProjectCompanyId == sharedId && c.Year == year);

                if (_ProjectCompany.Registers5.SingleOrDefault(c => c.OwnerProjectCompanyId == sharedId && c.Year == year) == null)
                    _ProjectCompany.Registers5.Add(SetAllFieldsAsDefault(register05) as Register5);
                else
                    register05 = _ProjectCompany.Registers5.First(c => c.OwnerProjectCompanyId == sharedId && c.Year == year);

                if (_ProjectCompany.Registers6.SingleOrDefault(c => c.OwnerProjectCompanyId == sharedId && c.Year == year) == null)
                    _ProjectCompany.Registers6.Add(SetAllFieldsAsDefault(register06) as Register6);
                else
                    register06 = _ProjectCompany.Registers6.First(c => c.OwnerProjectCompanyId == sharedId && c.Year == year);

                if (_ProjectCompany.Registers7.SingleOrDefault(c => c.OwnerProjectCompanyId == sharedId && c.Year == year) == null)
                    _ProjectCompany.Registers7.Add(SetAllFieldsAsDefault(register07) as Register7);
                else
                    register07 = _ProjectCompany.Registers7.First(c => c.OwnerProjectCompanyId == sharedId && c.Year == year);


                foreach (var item in dict)
                {
                    double value;
                    double.TryParse(item.Value, out value);

                    #region switch

                    switch (item.Key)
                    {
                        case "ID001":
                            register01.ProfitAmountBeforeTax = value;
                            break;
                        case "ID003":
                            register01.IncomeFromRegisteredCapitals = value;
                            break;
                        case "ID004":
                            register01.IncomeFromShares = value;
                            break;
                        case "ID005":
                            register01.IncomeFromSecurities = value;
                            break;
                        case "ID006":
                            register01.IncomeFromDerivatives = value;
                            break;
                        case "ID007":
                            register01.IncomeFromSubsidiary = value;
                            break;
                        case "ID008":
                            register01.IncomeFromReserveRestore = value;
                            break;
                        case "ID009":
                            register01.IncomeFromReserveRestore = value;
                            break;
                        case "ID010":
                            register01.CostsFromRegisteredCapitals = value;
                            break;
                        case "ID011":
                            register01.CostsFromShares = value;
                            break;
                        case "ID012":
                            register01.CostsFromSecurities = value;
                            break;
                        case "ID013":
                            register01.CostsFromDerivatives = value;
                            break;
                        case "ID014":
                            register01.CostsFromSubsidiary = value;
                            break;
                        case "ID015":
                            register01.CostsFromReserveRestore = value;
                            break;
                        case "ID016":
                            register01.ReassessmentFromRegisteredCapitals = value;
                            break;
                        case "ID017":
                            register01.ReassessmentFromRegisteredCapitals = value;
                            break;
                        case "ID018":
                            register01.ReassessmentFromShares = value;
                            break;
                        case "ID019":
                            register01.ReassessmentFromSecurities = value;
                            break;
                        case "ID020":
                            register01.ReassessmentFromDerivatives = value;
                            break;
                        case "ID021":
                            register01.IncomeNotIncludedInProfit = value;
                            break;
                        case "ID022":
                            register01.CostsIncludedInProfit = value;
                            break;
                        case "ID023":
                            register01.DividendsCurrentYear = value;
                            break;
                        case "ID024":
                            register01.DividendsCurrentYear = value;
                            break;
                        case "ID025":
                            register01.DistributedProfitAmount = value;
                            break;
                        case "ID026":
                            register01.IncomeProperty = value;
                            break;
                        case "ID027":
                            register01.LossProperty = value;
                            break;
                        case "ID028":
                            register01.ReceivedDividends = value;
                            break;
                        case "ID029":
                            register01.ProfitAmountCurrentYear = value;
                            break;
                        /* case "ID031":
                             register02.BalanceLoss2012 = value;
                             break;
                         case "ID032":
                             register02.BalanceLoss2013 = value;
                             break;
                         case "ID033":
                             register02.BalanceLoss2014 = value;
                             break;
                         case "ID034":
                             register02.BalanceLoss00 = value;
                             break;
                         case "ID035":
                             register02.BalanceLoss01 = value;
                             break;*/
                        case "ID038":
                            register03.ForeginContryProfitCurrency = value;
                            break;
                        case "ID042":
                            register03.ForeginContryEarningsCurrency = value;
                            break;
                        case "ID047":
                            register03.DomesticProfitCurrency = value;
                            break;
                        case "ID057":
                            register04.IncomeKIKTotalAmount = value;
                            break;
                        case "ID058":
                            register04.IncomeFromRateDifference = value;
                            break;
                        case "ID059":
                            register04.DividendsSum = value;
                            break;
                        case "ID060":
                            register04.IncomeFromAppropriationProfit = value;
                            break;
                        case "ID061":
                            register04.IncomeFromDebentures = value;
                            break;
                        case "ID062":
                            register04.IntellectialPropRightsIncome = value;
                            break;
                        case "ID063":
                            register04.SharedPartsIncome = value;
                            break;
                        case "ID064":
                            register04.FISSIncome = value;
                            break;
                        case "ID065":
                            register04.ImmovablePropertyIncome = value;
                            break;
                        case "ID066":
                            register04.LeasePropertyIncome = value;
                            break;
                        case "ID067":
                            register04.InvestmentUnitsIncome = value;
                            break;
                        case "ID068":
                            register04.ServicesIncome = value;
                            break;
                        case "ID069":
                            register04.ServicesConsultingIncome = value;
                            break;
                        case "ID070":
                            register04.ServicesLegalIncome = value;
                            break;
                        case "ID071":
                            register04.ServicesAccountingIncome = value;
                            break;
                        case "ID072":
                            register04.ServicesAuditIncome = value;
                            break;
                        case "ID073":
                            register04.ServicesEngineeringIncome = value;
                            break;
                        case "ID074":
                            register04.ServicesAdvertisingIncome = value;
                            break;
                        case "ID075":
                            register04.ServicesMarketingIncome = value;
                            break;
                        case "ID076":
                            register04.ServicesInformationProcessingIncome = value;
                            break;
                        case "ID077":
                            register04.ServicesReseachAndDevelopmentIncome = value;
                            break;
                        case "ID078":
                            register04.ServicesStaffProvidingIncome = value;
                            break;
                        case "ID079":
                            register04.OtherIncome = value;
                            break;
                        case "ID081":
                            register06.IncomeTaxRated = value;
                            break;
                        case "ID082":
                            register06.IncomeTaxDeducted = value;
                            break;
                        case "ID083":
                            register06.IncomeTaxCorrection = value;
                            break;
                        case "ID085":
                            register05.SumProfit = value;
                            break;
                        case "ID086":
                            register05.SumPercentForBondsProfit = value;
                            break;
                        case "ID088":
                            register07.SumIncomeSRP = value;
                            break;
                    }

                    #endregion
                }

                /*_ProjectCompany.Registers1.Add(register01);
                _ProjectCompany.Registers2.Add(register02);
                _ProjectCompany.Registers3.Add(register03);
                _ProjectCompany.Registers4.Add(register04);
                _ProjectCompany.Registers5.Add(register05);
                _ProjectCompany.Registers6.Add(register06);
                _ProjectCompany.Registers7.Add(register07);*/

                context.SaveChanges();
            }
        }

        public void IndividualUploadDataFromExcel(byte[] file, int projectId)
        {
            var dlList = ExcelDocsImporter.GetIndividualCompanyList(file);


            IndividualCompany iCompany;
            using (var context = new WebKikDataContext())
            {
                foreach (var item in dlList)
                {
                    try
                    {

                        var genderId = item["Пол"].ToLower().Contains("м")
                            ? context.GenderCodes.First(c => c.Code == "1").Id
                            : context.GenderCodes.First(c => c.Code == "2").Id;


                        var sitizenCode = item["Гражданство"];
                        var sitizenCodeId = context.CitizenshipCodes.Any(c => c.Code == sitizenCode)
                            ? context.CitizenshipCodes.First(c => c.Code == sitizenCode).Id
                            : context.CitizenshipCodes.First(c => c.Code == "3").Id;

                        var documentCode = item["Код вида документа УЛ"];
                        var documentCodeId = context.DocumentCodes.Any(c => c.Code == documentCode)
                            ? context.DocumentCodes.First(c => c.Code == documentCode).Id
                            : context.DocumentCodes.First(c => c.Code == "91").Id;

                        DateTime issueDate;
                        issueDate = !DateTime.TryParse(item["Дата рождения"], out issueDate) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(item["Дата рождения"]);


                        var personalityDoc = new DocumentInformation
                        {
                            DocumentCodeId = documentCodeId,
                            IssuePlace = item["Кем выдан документ УЛ"],
                            SeriesAndNumber = item["Серия и номер документа УЛ"],
                            IssueDate = issueDate
                        };

                        var regionCode = item["Код региона"];
                        var regionCodeId = context.RegionCodes.Any(c => c.Code == regionCode)
                            ? context.RegionCodes.First(c => c.Code == regionCode).Id
                            : context.RegionCodes.First(c => c.Code == "99").Id;

                        var addressCode = item["Код адреса"];
                        var rusLocCodeId = context.RussianLocationCodes.Any(c => c.Code == addressCode)
                            ? context.RussianLocationCodes.First(c => c.Code == addressCode).Id
                            : context.RussianLocationCodes.First(c => c.Code == "1").Id;


                        var cCode = item["Код страны"];
                        var countryCodeId = context.CountryCodes.Any(c => c.Code == cCode)
                            ? (int?)context.CountryCodes.First(c => c.Code == cCode).Id
                            : null;

                        var projectCompanyName = string.IsNullOrEmpty(item["Фамилия"] + item["Имя"]) ? item["ИНН"] : $"{item["Фамилия"]} {item["Имя"]} {item["Отчество"]}";


                        DateTime birthDate;
                        birthDate = DateTime.TryParse(item["Дата рождения"], out birthDate) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(item["Дата рождения"]);

                        iCompany = new IndividualCompany
                        {
                            INN = Convert.ToInt64(item["ИНН"]),
                            Surname = item["Фамилия"],
                            Name = item["Имя"],
                            MiddleName = item["Отчество"],
                            BirthDate = birthDate,
                            GenderCodeId = genderId,
                            BirthPlace = item["Место рождения"],
                            CitizenshipCodeId = sitizenCodeId,

                            VerifedPersonalityDocInfoId = documentCodeId,
                            VerifedPersonalityDocInfo = personalityDoc,

                            ConfirmedPersonalityDocInfoId = documentCodeId,
                            ConfirmedPersonalityDocInfo = personalityDoc,

                            RussianLocationCodeId = rusLocCodeId,
                            //RussianLocationCode = rusLocCodeId,

                            RegionCodeId = regionCodeId,

                            PostIndex = item["Почтовый индекс"],
                            District = item["Район"],
                            City = item["Город"],
                            CityType = item["Населенный пункт"],
                            Street = item["Улица"],
                            HouseNumber = item["Номер дома"],
                            BuildingNumber = item["Номер корпуса"],
                            AppartamentNumber = item["Номер квартиры"],
                            ForeignCountryCodeId = countryCodeId,
                            //ForeignCountryCode = cCode,
                            ForeignAddress = item["Адрес"]
                        };

                        var comp = new ProjectCompany
                        {
                            Name = projectCompanyName,
                            ProjectId = projectId,
                            State = State.Individual,
                            ModifiedDate = DateTime.Now,
                            IndividualCompany = iCompany
                        };

                        context.ProjectCompanies.Add(comp);

                        context.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = $"{validationErrors.Entry.Entity}:{validationError.ErrorMessage}";
                                raise = new InvalidOperationException(message, raise);
                            }
                        }
                        throw raise;
                    }
                }
            }
        }

        public void ForeignUploadDataFromExcel(byte[] file, int projectId)
        {
            var flList = ExcelDocsImporter.GetForeignCompanyList(file);
            ForeignCompany fCompany;
            using (var context = new WebKikDataContext())
            {
                foreach (var item in flList)
                {
                    try
                    {
                        var cc = item["Код страны регистрации"];
                        var countryCodeId = context.CountryCodes.Any(c => c.Code == cc)
                            ? context.CountryCodes.First(c => c.Code == cc).Id
                            : context.CountryCodes.First(c => c.Code == "643").Id;

                        var isResident = item["Резидент РФ"].Equals("1");

                        int? taxPayerId = null;
                        var taxPayer = item["Код налогоплательщика в стране регистрации или аналог"];
                        if (context.TaxPayerCodes.Any(c => c.Code == taxPayer))
                            taxPayerId = context.TaxPayerCodes.First(c => c.Code == taxPayer).Id;


                        DateTime foundDate;
                        foundDate = !DateTime.TryParse(item["Дата регистрации"], out foundDate) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(item["Дата регистрации"]);

                        fCompany = new ForeignCompany
                        {
                            CountryCodeId = countryCodeId,
                            Number = item["Номер"],
                            Name = item["Полное наименование в латинской транскрипции"] ?? "не указано",
                            FullName = item["Полное наименование в русской транскрипции"] ?? "не указано",
                            RegistrationNumber = item["Регистрационный номер в стране регистрации"] ?? "не указан",
                            Address = item["Адрес в стране регистрации"] ?? "не указан",
                            FoundDate = foundDate,
                            TaxPayerCodeId = taxPayerId

                        };

                        var comp = new ProjectCompany
                        {
                            Name = item["Название"],
                            ProjectId = projectId,
                            State = State.Foreign,
                            ModifiedDate = DateTime.Now,
                            ForeignCompany = fCompany,
                            IsResident = isResident
                        };

                        context.ProjectCompanies.Add(comp);
                        context.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = $"{validationErrors.Entry.Entity}:{validationError.ErrorMessage}";
                                raise = new InvalidOperationException(message, raise);
                            }
                        }
                        throw raise;
                    }
                }
            }
        }

        public void ForeignLightUploadDataFromExcel(byte[] file, int projectId)
        {
            var flList = ExcelDocsImporter.GetForeignLigtCompanyList(file);

            using (var context = new WebKikDataContext())
            {
                foreach (var item in flList)
                {
                    try
                    {

                        var cc = item["Код страны, в которой учреждена иностранная структура"];
                        var countryCodeId = context.CountryCodes.Any(c => c.Code == cc)
                            ? context.CountryCodes.First(c => c.Code == cc).Id
                            : context.CountryCodes.First(c => c.Code == "643").Id;


                        var orgForm = item["Организационная форма"];
                        var fOfc = context.ForeignOrganizationalFormCodes.Any(c => c.Code == orgForm)
                            ? context.ForeignOrganizationalFormCodes.First(c => c.Code == orgForm).Id
                            : context.ForeignOrganizationalFormCodes.First(c => c.Code == "5").Id;

                        DateTime foundDate;
                        foundDate = !DateTime.TryParse(item["Дата учреждения"], out foundDate) ? new DateTime(1900, 1, 1) : DateTime.Parse(item["Дата учреждения"]);

                        var isResident = item["Резидент РФ"].Equals("1");

                        var flCompany = new ForeignLightCompany
                        {
                            Number = item["Номер"],
                            ForeignOrganizationalFormCodeId = fOfc,
                            EnglishName = item["Полное наименование в латинской транскрипции"] ?? "unknow",
                            RussianName = item["Полное наименование в русской транскрипции"] ?? "не указано",
                            FoundDate = foundDate,
                            RequisitesEng = item["Наименование и реквизиты документы об учреждении в латинской транскрипции"] ?? "unknow",
                            RequisitesRus = item["Наименование и реквизиты документы об учреждении в русской транскрипции"] ?? "не указано",
                            CountryCodeId = countryCodeId,
                            RegNumber = item["Регистрационный номер иностранной структуры в стране учреждения"] ?? "не указано",
                            OtherInfo = item["Иные сведения, характеризующие иностранную структуру"] ?? "не указано"
                        };

                        var comp = new ProjectCompany
                        {
                            Name = item["Название"],
                            ProjectId = projectId,
                            State = State.ForeignLight,
                            ModifiedDate = DateTime.Now,
                            ForeignLightCompany = flCompany,
                            IsResident = isResident
                        };

                        context.ProjectCompanies.Add(comp);

                        context.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = $"{validationErrors.Entry.Entity}:{validationError.ErrorMessage}";
                                raise = new InvalidOperationException(message, raise);
                            }
                        }
                        throw raise;
                    }
                }
            }
        }

        public void DomesticUploadDataFromExcel(byte[] file, int projectId)
        {
            var dlList = ExcelDocsImporter.GetDomesticCompanyList(file);

            using (var context = new WebKikDataContext())
            {
                try
                {
                    foreach (var item in dlList)
                    {
                        var dCompany = new DomesticCompany
                        {
                            Number = item["Номер"],
                            FullName = item["Полное наименование"],
                            INN = Convert.ToInt64(item["ИНН"]),
                            OGRN = Convert.ToInt64(item["ОГРН"]),
                            KPP = item["КПП"],
                            IsPublic = Convert.ToBoolean(Convert.ToInt32(item["ПАО"]))
                        };


                        var comp = new ProjectCompany
                        {
                            Name = item["Название"],
                            ProjectId = projectId,
                            State = State.Domestic,
                            ModifiedDate = DateTime.Now,
                            DomesticCompany = dCompany,
                            IsResident = item["Резидент РФ"].Equals("1")
                        };

                        context.ProjectCompanies.Add(comp);

                        context.SaveChanges();
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = $"{validationErrors.Entry.Entity}:{validationError.ErrorMessage}";
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }


        #endregion
    }
}
