import { ProjectCompanyShareViewModel } from '../../company/models/company-share.model';
import { ProjectCompanyViewModel } from '../../company/models/project-company.model';
import { RegisterType } from '../models/register-type.model';

export
    class Register3ViewModel {

    public Id: number;
    public Year: number;
    public Type: RegisterType;
    public Currency: string;
    public OwnerProjectCompany: ProjectCompanyViewModel;
    public OwnerProjectCompanyId: number;  

    public KIKProfitCurrency: number;
    public KIKConversionRate: number;
    public KIKProfitRUR: number;
    public KIKSharePart: number;
    public KIKProfitSharePart: number;
    public KIKProfitPartForTax: number;
    public KIKTaxBasePart: number;
    public TaxPercentValue: number;
    public TaxSumCurrency: number;
    public TaxSum: number;

    public ForeginContryProfitCurrency: number;
    public ForeginContryProfitRUR: number;
    public ForeginContryEarningsCurrency: number;
    public ForeginContryEarningsRUR: number;

    public DomesticProfitCurrency: number;
    public DomesticContryProfitRUR: number;
    public DomesticContryEarningsCurrency: number;
    public DomesticContryEarningsRUR: number;

    public RURResultSum: number;
    public RURResultForPart: number;
    public RURResultTax: number;

    constructor() {
        this.OwnerProjectCompany = new ProjectCompanyViewModel();
    }
}