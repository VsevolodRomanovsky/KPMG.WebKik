import { ProjectCompanyShareViewModel } from '../../company/models/company-share.model';
import { ProjectCompanyViewModel } from '../../company/models/project-company.model';
import { RegisterType } from '../models/register-type.model';
import { Register11DataViewModel } from '../models/register11data.model ';

export
    class Register11ViewModel {

    public Id: number;
    public Type: RegisterType;
    public Year: number;
    public Currency: string;
    public OwnerProjectCompany: ProjectCompanyViewModel;
    public OwnerProjectCompanyId: number;  
	public Register11Data: Register11DataViewModel[];
	public DecisionOfLiquidationData: Date;
	public CompletionOfLiquidationData: Date;
	public Summary1: Register11DataViewModel;
	public Summary2: Register11DataViewModel;
	public Summary3: Register11DataViewModel;
	public Summary4: Register11DataViewModel;
	public Summary: Register11DataViewModel;

	constructor() {
		this.OwnerProjectCompany = new ProjectCompanyViewModel();
		this.Register11Data = new Array<Register11DataViewModel>();
    }
}