import { ProjectCompanyShareViewModel } from '../../company/models/company-share.model';
import { ProjectCompanyViewModel } from '../../company/models/project-company.model';
import { RegisterType } from '../models/register-type.model';

export
    class Register7ViewModel {

    public Id: number;
    public Type: RegisterType;
    public Year: number;
    public Currency: string;
    public OwnerProjectCompany: ProjectCompanyViewModel;
    public OwnerProjectCompanyId: number;  

    public KikTotalSumIncome: number;
    public SumIncomeSRP: number;
    public PartPercentSRPIncome: number;

    constructor() {
        this.OwnerProjectCompany = new ProjectCompanyViewModel();
    }
}