import { ProjectCompanyShareViewModel } from '../../company/models/company-share.model';
import { ProjectCompanyViewModel } from '../../company/models/project-company.model';
import { RegisterType } from '../models/register-type.model';

export
    class Register5ViewModel {

    public Id: number;
    public Year: number;
    public Type: RegisterType;
    public Currency: string;
    public OwnerProjectCompany: ProjectCompanyViewModel;
    public OwnerProjectCompanyId: number;  

    public SumProfit: number;
    public SumPercentForBondsProfit: number;
    public PartPercentForBondsProfit: number;

    constructor() {
        this.OwnerProjectCompany = new ProjectCompanyViewModel();
    }
}