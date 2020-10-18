import { ProjectCompanyShareViewModel } from '../../company/models/company-share.model';
import { ProjectCompanyViewModel } from '../../company/models/project-company.model';
import { RegisterType } from '../models/register-type.model';

export
    class Register1ViewModel {

    public Id: number;
    public Year: number;
    public Type: RegisterType;
    public Currency: string;
    public OwnerProjectCompany: ProjectCompanyViewModel;
    public OwnerProjectCompanyId: number;  

    public ProfitAmountBeforeTax: number;
    public IncomeKIKNotIncluded: number;
    public IncomeFromDerivatives: number;
    public IncomeFromRegisteredCapitals: number;
    public IncomeFromShares: number;
    public IncomeFromSubsidiary: number;
    public IncomeFromSecurities: number;
    public IncomeFromReserveRestore: number;

    public CostsKIKNotIncluded: number;
    public CostsFromRegisteredCapitals: number;
    public CostsFromShares: number;
    public CostsFromSecurities: number;
    public CostsFromDerivatives: number;
    public CostsFromSubsidiary: number;
    public CostsFromReserveRestore: number;

    public IncomeAndCostsTotalAmount:number;
    public ProfitTotalAmountCorrection: number;
    public ReassessmentTotalAmount: number;
    public ReassessmentFromRegisteredCapitals: number;
    public ReassessmentFromShares: number;
    public ReassessmentFromSecurities: number;
    public ReassessmentFromDerivatives: number;

    public IncomeSymmetricCorrection: number;
    public IncomeNotIncludedInProfit: number;
    public CostsIncludedInProfit: number;
    public AdjustedProfitAmount: number;
    public ProfitExclusion: number;
    public DividendsCurrentYear: number;
    public DistributedProfitAmount: number;
    public IncomeProperty: number;
    public LossProperty: number;
    public ProfitAmount: number;
    public AverageForeignCurrency: number;
    public ProfitAmountConvertedCurrency: number;

    public StandartKKIKProfit : number;
        public ReceivedDividends: number;
        public ProfitAmountCurrentYear: number;
        public ProfitAmountForTax: number;
        public LossKIKFromPastYears: number;
        public CountableProfitAmountForTax: number;
        public ShareInKIKProfit: number;
        public PartKIKProfit: number;
        public ControlledProfitAmount: number;
        public KIKTaxBase: number;

    constructor() {
        this.OwnerProjectCompany = new ProjectCompanyViewModel();
    }
}