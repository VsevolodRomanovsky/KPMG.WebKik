import { ProjectCompanyShareViewModel } from '../../company/models/company-share.model';
import { ProjectCompanyViewModel } from '../../company/models/project-company.model';
import { RegisterType } from '../models/register-type.model';

export
    class Register2ViewModel {

    public Id: number;
    public Year: number;
    public Type: RegisterType;
    public Currency: string;
    public OwnerProjectCompany: ProjectCompanyViewModel;
    public OwnerProjectCompanyId: number;  

    public BalanceLoss: number;
    public BalanceLoss2012: number;
    public BalanceLoss2013: number;
    public BalanceLoss2014: number;
    public BalanceLoss00: number;
    public BalanceLoss01: number;

    public TaxBaseForTaxPeriod: number;
    public LossSumTaxBase: number;
    public SumLoss2012: number;
    public SumLoss2013: number;
    public SumLoss2014: number;
    public SumLoss00: number;
    public SumLoss01: number;

    public FullBalanceLoss: number;
    public FullBalanceLoss2012: number;
    public FullBalanceLoss2013: number;
    public FullBalanceLoss2014: number;
    public FullBalanceLoss00: number;
    public FullBalanceLoss01: number;

    constructor() {
        this.OwnerProjectCompany = new ProjectCompanyViewModel();
    }
}