import { ProjectCompanyShareViewModel } from '../../company/models/company-share.model';
import { ProjectCompanyViewModel } from '../../company/models/project-company.model';
import { RegisterType } from '../models/register-type.model';

export
    class Register6ViewModel {

    public Id: number;
    public Year: number;
    public Type: RegisterType;
    public Currency: string;
    public OwnerProjectCompany: ProjectCompanyViewModel;
    public OwnerProjectCompanyId: number;  

    public IncomeTaxRated: number;
    public IncomeTaxDeducted: number;
    public IncomeTaxCorrection: number;
    public KIKProfit: number;
    public IncomeTaxEffected: number;
    public KIKDividends: number;
    public KIKProfitMinusDividends: number;
    public ProfitTaxRateArticle1: number;
    public ProfitTaxRateArticel3: number;
    public AverageTaxRate: number;
    public AverageTaxRatePart: number;

    constructor() {
        this.OwnerProjectCompany = new ProjectCompanyViewModel();
    }
}