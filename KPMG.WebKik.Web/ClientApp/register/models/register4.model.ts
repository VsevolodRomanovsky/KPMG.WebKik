import { ProjectCompanyShareViewModel } from '../../company/models/company-share.model';
import { ProjectCompanyViewModel } from '../../company/models/project-company.model';
import { RegisterType } from '../models/register-type.model';

export
    class Register4ViewModel {

    public Id: number;
    public Year: number;
    public Type: RegisterType;
    public Currency: string;
    public OwnerProjectCompany: ProjectCompanyViewModel;
    public OwnerProjectCompanyId: number;  

    public IncomeKIKTotalAmount: number;
    public IncomeNotIncluded: number;
    public IncomeFromRateDifference: number;
    public IncomeFromRegisteredCapitals: number;
    public IncomeFromShares: number;
    public IncomeFromSecurities: number;
    public IncomeFromDerivatives: number;
    public IncomeFromSubsidiary: number;
    public IncomeFromReserveRestore: number;

    public IncomeKIKSummary: number;
    public PassiveIncomeKIKSummary: number;
    public DividendsSum: number;
    public DividendsFromActiveCompanies: number;
    public DividendsFromHoldingCompanies: number;
    public IncomeFromAppropriationProfit: number;
    public IncomeFromDebentures: number;
    public IntellectialPropRightsIncome: number;
    public SharedPartsIncome: number;
    public FISSIncome: number;
    public ImmovablePropertyIncome: number;
    public LeasePropertyIncome: number;
    public InvestmentUnitsIncome: number;

    public ServicesIncome: number;
    public ServicesConsultingIncome: number;
    public ServicesLegalIncome: number;
    public ServicesAccountingIncome: number;
    public ServicesAuditIncome: number;
    public ServicesEngineeringIncome: number;
    public ServicesAdvertisingIncome: number;
    public ServicesMarketingIncome: number;
    public ServicesInformationProcessingIncome: number;
    public ServicesReseachAndDevelopmentIncome: number;
    public ServicesStaffProvidingIncome: number;
    public OtherIncome: number;
    public IncomeSumExceptDividendsFromActiveCompanies: number;
    public IncomeSumExceptDividendsFromHoldingCompanies: number;
    public PassivePartIncomeValue: number;
    public PassivePartWithoutDividendsIncomeValue: number;
    public PassivePartWithoutDividendsAndHoldingsIncomeValue: number;

    constructor() {
        this.OwnerProjectCompany = new ProjectCompanyViewModel();
    }
}