import { ProjectCompanyShareViewModel } from '../../company/models/company-share.model';
import { ProjectCompanyViewModel } from '../../company/models/project-company.model';
import { RegisterType } from '../models/register-type.model';
import { Register8ReservesViewModel } from '../models/register8reserves.model';

export
    class Register8ViewModel {

    public Id: number;
    public Type: RegisterType;
    public Year: number;
    public Currency: string;
    public OwnerProjectCompany: ProjectCompanyViewModel;
    public OwnerProjectCompanyId: number;  
	public Register8Data: Register8ReservesViewModel[];

    constructor() {
		this.OwnerProjectCompany = new ProjectCompanyViewModel();
		this.Register8Data = new Array<Register8ReservesViewModel>();
    }
}